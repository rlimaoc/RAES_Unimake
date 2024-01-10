using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using Uni.NFCom.Servicos.Enums;
using Uni.NFCom.Utility;
using Uni.NFCom.Exceptions;

namespace Uni.NFCom.ConsumirServico
{
    /// <summary>{
    /// Classe para consumir API
    /// </summary>
    public class ConsumirAPI : ConsumirBase
    {

        /// <summary>
        /// Estabelece conexão com o Webservice e faz o envio do XML e recupera o retorno. Conteúdo retornado pelo webservice pode ser recuperado através das propriedades RetornoServicoXML ou RetornoServicoString.
        /// </summary>
        /// <param name="xml">XML a ser enviado para o webservice</param>
        /// <param name="apiConfig">Parâmetros para execução do serviço (parâmetros da API)</param>
        /// <param name="certificado">Certificado digital a ser utilizado na conexão com os serviços</param>
        public void ExecutarServico(XmlDocument xml, APIConfig apiConfig, X509Certificate2 certificado)
        {
            if (certificado == null && apiConfig.UsaCertificadoDigital)
            {
                throw new CertificadoDigitalException();
            }

            var Content = EnveloparXML(apiConfig, xml);

            var httpClientHandler = new HttpClientHandler();

            if (!apiConfig.UsaCertificadoDigital)
            {
                httpClientHandler.ClientCertificateOptions = ClientCertificateOption.Automatic;
                httpClientHandler.Credentials = CredentialCache.DefaultCredentials;
            }
            else
            {
                httpClientHandler.ClientCertificateOptions = ClientCertificateOption.Manual;
                httpClientHandler.ClientCertificates.Add(certificado);
            }

            var httpWebRequest = new HttpClient(httpClientHandler)
            {
                BaseAddress = new Uri(apiConfig.RequestURI),
            };

            if (!string.IsNullOrWhiteSpace(apiConfig.Token))
            {
                httpWebRequest.DefaultRequestHeaders.Add("Authorization", apiConfig.Token);
            }

            ServicePointManager.Expect100Continue = false;
            //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(RetornoValidacao);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            var postData = new HttpResponseMessage();
            if (apiConfig.MetodoAPI.ToLower() == "get")
            {
                postData = httpWebRequest.GetAsync("").GetAwaiter().GetResult();
            }
            else
            {
                postData = httpWebRequest.PostAsync(apiConfig.RequestURI, Content).GetAwaiter().GetResult();
            }

            WebException webException = null;
            var responsePost = string.Empty;
            try
            {
                responsePost = postData.Content.ReadAsStringAsync().Result;
            }
            catch (WebException ex)
            {
                if (ex.Response == null)
                {
                    throw ex;
                }
                else
                {
                    webException = ex;
                }
            }

            var retornoXml = new XmlDocument();
            try
            {
                var stream = default(Stream);
                retornoXml = TratarRetornoAPI.ReceberRetorno(ref apiConfig, postData, ref stream);
                RetornoStream = stream != null ? stream : null;
            }
            catch (XmlException)
            {
                if (webException != null)
                {
                    throw webException;
                }

                throw new Exception(responsePost);
            }
            catch
            {
                throw new Exception(responsePost);
            }

            if (apiConfig.TagRetorno.ToLower() != "prop:innertext" && postData.IsSuccessStatusCode == true)
            {
                if (retornoXml.GetElementsByTagName(apiConfig.TagRetorno)[0] == null)
                {
                    throw new Exception("Não foi possível localizar a tag <" + apiConfig.TagRetorno + "> no XML retornado pelo webservice.\r\n\r\n" +
                        "Conteúdo retornado pelo servidor:\r\n\r\n" +
                        retornoXml.InnerXml);
                }
                RetornoServicoString = retornoXml.GetElementsByTagName(apiConfig.TagRetorno)[0].OuterXml;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(retornoXml.InnerText))
                {
                    throw new Exception("A propriedade InnerText do XML retornado pelo webservice está vazia.");
                }

                RetornoServicoString = retornoXml.OuterXml;

                //Remover do XML retornado o conteúdo ﻿<?xml version="1.0" encoding="utf-8"?> ou gera falha na hora de transformar em XmlDocument
                if (RetornoServicoString.IndexOf("?>") >= 0)
                {
                    RetornoServicoString = RetornoServicoString.Substring(RetornoServicoString.IndexOf("?>") + 2);
                }

                //Remover quebras de linhas
                RetornoServicoString = RetornoServicoString.Replace("\r\n", "");
            }

            RetornoServicoXML = new XmlDocument
            {
                PreserveWhitespace = false
            };
            RetornoServicoXML.LoadXml(retornoXml.InnerXml);
        }

        /// <summary>
        /// Método para envolopar o XML, formando o JSON para comunicação com a API
        /// </summary>
        /// <param name="apiConfig"></param>    Configurações básicas para consumo da API
        /// <param name="xml"></param>          Arquivo XML que será enviado
        /// <returns></returns>
        private HttpContent EnveloparXML(APIConfig apiConfig, XmlDocument xml)
        {
            var xmlBody = xml.OuterXml;
            if (apiConfig.GZipCompress)
            {
                xmlBody = Convert.ToBase64String(Encoding.UTF8.GetBytes(xmlBody));
                xmlBody = Compress.GZIPCompress(xml);
            }

            //No momento, somente IPM 2.04 está utilizando WebSoapString em comunicação API, ele precisa o login acima
            if (!string.IsNullOrWhiteSpace(apiConfig.WebSoapString))
            {
                apiConfig.WebSoapString = apiConfig.WebSoapString.Replace("{xml}", xmlBody);
                HttpContent temp = new StringContent(apiConfig.WebSoapString, Encoding.UTF8, apiConfig.ContentType);
                return temp;
            }

            if (apiConfig.ContentType == "application/json")
            {
                var Json = "";
                var dicionario = new Dictionary<string, string>();

                dicionario.Add(string.IsNullOrWhiteSpace(apiConfig.WebAction) ? "xml" : apiConfig.WebAction, xmlBody);

                Json = JsonConvert.SerializeObject(dicionario);

                HttpContent temp = new StringContent(Json, Encoding.UTF8, apiConfig.ContentType);

                return temp;
            }
            else if (apiConfig.ContentType == "multipart/form-data")
            {
                var path = string.Empty;

                if (string.IsNullOrWhiteSpace(xml.BaseURI))
                {
                    path = "arquivo.xml";
                }
                else
                {
                    path = xml.BaseURI.Substring(8, xml.BaseURI.Length - 8);
                }

                var boundary = "----------------------------" + DateTime.Now.Ticks.ToString("x");

                #region ENVIO EM BYTES
                var xmlBytes = Encoding.UTF8.GetBytes(xmlBody);
                var xmlContent = new ByteArrayContent(xmlBytes);
                xmlContent.Headers.ContentType = MediaTypeHeaderValue.Parse("text/xml");
                xmlContent.Headers.ContentEncoding.Add("ISO-8859-1");
                xmlContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    Name = "f1",
                    FileName = path,

                };
                #endregion ENVIO EM BYTES

                HttpContent MultiPartContent = new MultipartContent("form-data", boundary)
                {
                    xmlContent,

                };

                return MultiPartContent;
            }

            return new StringContent(xmlBody, Encoding.UTF8, apiConfig.ContentType);
        }
    }
}