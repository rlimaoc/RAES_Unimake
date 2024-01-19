#if INTEROP
using System.Runtime.InteropServices;
#endif
using System;
using System.Collections.Generic;
using System.Xml;
using Uni.Business.DFe.Servicos.Interop;
using Uni.Business.DFe.Utility;
using Uni.Business.DFe.Xml.NFCom;
using Uni.Exceptions;

namespace Uni.Business.DFe.Servicos.NFCom
{
    /// <summary>
    /// Enviar o XML de NFCom para o web-service
    /// </summary>
#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.Business.DFe.Servicos.NFCom.Autorizacao")]
    [ComVisible(true)]
#endif
    public class Autorizacao : ServicoBase, IInteropService<Xml.NFCom.NFCom>
    {
        #region Private Fields

        private Xml.NFCom.NFCom _NFCom;
        private Dictionary<string, NfcomProc> NfcomProcs = new Dictionary<string, NfcomProc>();

        #endregion Private Fields

        #region Private Methods

        /// <summary>
        /// Definir as propriedades do QRCode e Link da consulta manual da NFCe
        /// </summary>
        private void MontarQrCode()
        {
            NFCom = new Xml.NFCom.NFCom().LerXML<Xml.NFCom.NFCom>(ConteudoXML);

            if (NFCom.InfNFComSupl == null)
            {
                if (string.IsNullOrWhiteSpace(Configuracoes.CSC))
                {
                    throw new Exception("Para montagem do QRCode é necessário informar o conteúdo da propriedade \"Configuracao.CSC\"");
                }

                if (Configuracoes.CSCIDToken <= 0)
                {
                    throw new Exception("Para montagem do QRCode é necessário informar o conteúdo da propriedade \"Configuracao.CSCIDToken\"");
                }

                NFCom.InfNFComSupl = new InfNFComSupl();

                var urlQrCode = (Configuracoes.TipoAmbiente == TipoAmbiente.Homologacao ? Configuracoes.UrlQrCodeHomologacao : Configuracoes.UrlQrCodeProducao);
                string paramLinkQRCode = 
                    "?chNFCom=" + NFCom.InfNFCom[0].Chave +
                    "&tpAmb=" + ((int)NFCom.InfNFCom[0].Ide.TpAmb).ToString();

                if (NFCom.InfNFCom[0].Ide.TpEmis == TipoEmissao.ContingenciaOffLine)
                {
                    //paramLinkQRCode += Converter.ToHexadecimal(EnviNFe.NFe[i].Signature.SignedInfo.Reference.DigestValue.ToString());
                    paramLinkQRCode += Converter.ToSHA1HashData(paramLinkQRCode.Trim() + Configuracoes.CSC, true);
                }

                NFCom.InfNFComSupl.QrCodNFCom = urlQrCode + "?p=" + paramLinkQRCode.Trim();
            }

            //Atualizar a propriedade do XML da NFCom novamente com o conteúdo atual já a tag de QRCode e link de consulta
            ConteudoXML = NFCom.GerarXML();
        }

        /// <summary>
        /// Mudar o conteúdo da tag xMotivo caso a nota tenha sido rejeitada por problemas nos itens/produtos da nota. Assim vamos retornar na xMotivo algumas informações a mais para facilitar o entendimento para o usuário.
        /// </summary>
        private void MudarConteudoTagRetornoXMotivo()
        {
            try
            {
                if (RetornoWSXML.GetElementsByTagName("xMotivo") != null)
                {
                    var xMotivo = RetornoWSXML.GetElementsByTagName("xMotivo")[0].InnerText;
                    if (xMotivo.Contains("[nItem:"))
                    {
                        var nItem = Convert.ToInt32((xMotivo.Substring(xMotivo.IndexOf("[nItem:") + 7)).Substring(0, (xMotivo.Substring(xMotivo.IndexOf("[nItem:") + 7)).Length - 1));
                        RetornoWSString = RetornoWSString.Replace(xMotivo, xMotivo + " [cProd:" + NFCom.InfNFCom[0].Det[nItem - 1].Prod.CProd + "] [xProd:" + NFCom.InfNFCom[0].Det[nItem - 1].Prod.XProd + "]");

                        RetornoWSXML = new XmlDocument
                        {
                            PreserveWhitespace = false
                        };
                        RetornoWSXML.LoadXml(RetornoWSString);
                    }
                }
            }
            catch { }
        }

        #endregion Private Methods

        #region Protected Properties

        /// <summary>
        /// Objeto do XML da NFCom
        /// </summary>
        public Xml.NFCom.NFCom NFCom
        {
            get => _NFCom ?? (_NFCom = new Xml.NFCom.NFCom().LerXML<Xml.NFCom.NFCom>(ConteudoXML));
            protected set => _NFCom = value;
        }

        #endregion Protected Properties

        #region Protected Methods

        /// <summary>
        /// Efetuar um ajuste no XML da NFCom logo depois de assinado
        /// </summary>
        protected override void AjustarXMLAposAssinado()
        {
            MontarQrCode();
            base.AjustarXMLAposAssinado();
        }

        /// <summary>
        /// Validar o XML
        /// </summary>
        protected override void XmlValidar()
        {
            var validar = new ValidarSchema();
            validar.Validar(ConteudoXML, TipoDFe.NFCom.ToString() + "." + Configuracoes.SchemaArquivo, Configuracoes.TargetNS);

            if (!validar.Success)
            {
                throw new ValidarXMLException(validar.ErrorMessage);
            }
        }

        /// <summary>
        /// Definir o valor de algumas das propriedades do objeto "Configuracoes"
        /// </summary>
        protected override void DefinirConfiguracao()
        {
            if (NFCom == null)
            {
                Configuracoes.Definida = false;
                return;
            }

            var xml = NFCom;

            if (!Configuracoes.Definida)
            {
                Configuracoes.Servico = Servico.NFComRecepcao;
                Configuracoes.CodigoUF = (int)xml.InfNFCom[0].Ide.CUF;
                Configuracoes.TipoAmbiente = xml.InfNFCom[0].Ide.TpAmb;
                Configuracoes.Modelo = xml.InfNFCom[0].Ide.Mod;
                Configuracoes.TipoEmissao = xml.InfNFCom[0].Ide.TpEmis;
                Configuracoes.SchemaVersao = xml.InfNFCom[0].Versao;

                base.DefinirConfiguracao();
            }
        }

        #endregion Protected Methods

        #region Public Properties

        /// <summary>
        /// Lista com o conteúdo retornado das consultas situação do NFes enviadas
        /// </summary>
        public List<RetConsSitNFCom> RetConsSitNFComs = new List<RetConsSitNFCom>();

#if INTEROP

        /// <summary>
        /// Adicionar o retorno da consulta situação da NFCom na lista dos retornos para elaboração do XML de distribuição
        /// </summary>
        /// <param name="item">Resultado da consulta situação do NFCom</param>
        public void AddRetConsSitNFComs(RetConsSitNFCom item) => (RetConsSitNFComs ?? (RetConsSitNFComs = new List<RetConsSitNFCom>())).Add(item);

#endif

        /// <summary>
        /// Propriedade contendo o XML da NFCom com o protocolo de autorização anexado - Funciona para envio Assíncrono ou Síncrono
        /// </summary>
        public Dictionary<string, NfcomProc> NfcomProcResults
        {
            get
            {
                if (Result.ProtNFCom != null) //Envio síncrono
                {
                    if (NfcomProcs.ContainsKey(NFCom.InfNFCom[0].Chave))
                    {
                        NfcomProcs[NFCom.InfNFCom[0].Chave].ProtNFCom = Result.ProtNFCom;
                    }
                    else
                    {
                        NfcomProcs.Add(NFCom.InfNFCom[0].Chave,
                            new NfcomProc
                            {
                                Versao = NFCom.InfNFCom[0].Versao,
                                NFCom = NFCom,
                                ProtNFCom = Result.ProtNFCom
                            });
                    }
                }

                return NfcomProcs;
            }
        }

#if INTEROP
        /// <summary>
        /// Recupera o XML de distribuição da NFCom no formato string
        /// </summary>
        /// <param name="chaveDFe">Chave da NFCom que é para retornar o XML de distribuição</param>
        /// <returns>XML de distribuição da NFCom</returns>
        public string GetNFComProcResults(string chaveDFe)
        {
            var retornar = "";
            if (NfcomProcResults.Count > 0)
            {
                retornar = NfcomProcResults[chaveDFe].GerarXML().OuterXml;
            }

            return retornar;
        }


        /// <summary>
        /// Recupera o conteúdo da NFCom, assinada
        /// </summary>
        /// <returns>Retorna o conteúdo da NFCom, assinada</returns>
        public string GetConteudoNFComAssinada() => (ConteudoXMLAssinado != null ? ConteudoXMLAssinado.GetElementsByTagName("NFCom").OuterXml : "");

#endif

        /// <summary>
        /// Propriedade contendo o XML da NFCom com o protocolo de autorização anexado - Funciona somente para envio síncrono
        /// </summary>
        public NfcomProc NfeProcResult
        {
            get
            {
                return new NfcomProc
                {
                    Versao = NFCom.InfNFCom[0].Versao,
                    NFCom = NFCom,
                    ProtNFCom = Result.ProtNFCom
                };
            }
        }

        /// <summary>
        /// Conteúdo retornado pelo web-service depois do envio do XML
        /// </summary>
        public RetNFCom Result
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(RetornoWSString))
                {
                    return XMLUtility.Deserializar<RetNFCom>(RetornoWSXML);
                }

                return new RetNFCom
                {
                    CStat = 0,
                    XMotivo = "Ocorreu uma falha ao tentar criar o objeto a partir do XML retornado da SEFAZ."
                };
            }
        }

        #endregion Public Properties

        #region Public Constructors

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="nfCom">Objeto contendo o XML a ser enviado</param>
        /// <param name="configuracao">Configurações para conexão e envio do XML para o web-service</param>
        public Autorizacao(Xml.NFCom.NFCom nfCom, Configuracao configuracao) : this()
        {
            if (configuracao is null)
            {
                throw new ArgumentNullException(nameof(configuracao));
            }

            Inicializar(nfCom?.GerarXML() ?? throw new ArgumentNullException(nameof(nfCom)), configuracao);
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public Autorizacao() : base() { }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Executar o serviço
        /// </summary>
#if INTEROP
        [ComVisible(false)]
#endif
        public override void Executar()
        {
            base.Executar();

            MudarConteudoTagRetornoXMotivo();
        }

#if INTEROP

        /// <summary>
        /// Executa o serviço: Assina o XML, valida e envia para o web-service
        /// </summary>
        /// <param name="enviNFe">Objeto contendo o XML a ser enviado</param>
        /// <param name="configuracao">Configurações a serem utilizadas na conexão e envio do XML para o web-service</param>
        public void Executar(NFCom nfCom, Configuracao configuracao)
        {
            try
            {
                Inicializar(nfCom?.GerarXML() ?? throw new ArgumentNullException(nameof(nfCom)), configuracao);

                Executar();
            }
            catch (ValidarXMLException ex)
            {
                ThrowHelper.Instance.Throw(ex);
            }
            catch (CertificadoDigitalException ex)
            {
                ThrowHelper.Instance.Throw(ex);
            }
            catch (Exception ex)
            {
                ThrowHelper.Instance.Throw(ex);
            }
        }

        /// <summary>
        /// Definir o objeto contendo o XML a ser enviado e configuração de conexão e envio do XML para web-service
        /// </summary>
        /// <param name="nfCom">Objeto contendo o XML a ser enviado</param>
        /// <param name="configuracao">Configurações para conexão e envio do XML para o web-service</param>
        public void SetXMLConfiguracao(NFCom nfCom, Configuracao configuracao)
        {
            try
            {
                if (configuracao is null)
                {
                    throw new ArgumentNullException(nameof(configuracao));
                }

                Inicializar(enviNFe?.GerarXML() ?? throw new ArgumentNullException(nameof(enviNFe)), configuracao);
            }
            catch (Exception ex)
            {
                ThrowHelper.Instance.Throw(ex);
            }
        }

#endif

        /// <summary>
        /// Gravar o XML de distribuição em uma pasta no HD
        /// </summary>
        /// <param name="pasta">Pasta onde deve ser gravado o XML</param>
#if INTEROP
        [ComVisible(true)]
#endif
        public void GravarXmlDistribuicao(string pasta)
        {
            try
            {
                foreach (var item in NfcomProcResults)
                {
                    if (item.Value.ProtNFCom != null)
                    {
                        GravarXmlDistribuicao(pasta, item.Value.NomeArquivoDistribuicao, item.Value.GerarXML().OuterXml);
                    }
                    else
                    {
                        throw new Exception("Não foi localizado no retorno da consulta o protocolo da chave, abaixo, para a elaboração do arquivo de distribuição. Verifique se a chave ou recibo consultado estão de acordo com a informada na sequencia:\r\n\r\n" + Format.ChaveDFe(item.Key));
                    }
                }
            }
            catch (Exception ex)
            {
                Exceptions.ThrowHelper.Instance.Throw(ex);
            }
        }

        /// <summary>
        /// Grava o XML de distribuição no stream
        /// </summary>
        /// <param name="stream">Stream que vai receber o XML de distribuição</param>
#if INTEROP
        [ComVisible(false)]
#endif
        public void GravarXmlDistribuicao(System.IO.Stream stream)
        {
            try
            {
                foreach (var item in NfcomProcResults)
                {
                    if (item.Value.ProtNFCom != null)
                    {
                        GravarXmlDistribuicao(stream, item.Value.GerarXML().OuterXml);
                    }
                    else
                    {
                        throw new Exception("Não foi localizado no retorno da consulta o protocolo da chave, abaixo, para a elaboração do arquivo de distribuição. Verifique se a chave ou recibo consultado estão de acordo com a informada na sequencia:\r\n\r\n" + Format.ChaveDFe(item.Key));
                    }
                }
            }
            catch (Exception ex)
            {
                Exceptions.ThrowHelper.Instance.Throw(ex);
            }
        }

        #endregion Public Methods
    }
}
