﻿#if INTEROP
using System.Runtime.InteropServices;
#endif

using System;
using Uni.Business.DFe.Servicos.Interop;
using Uni.Business.DFe.Utility;
using Uni.Business.DFe.Xml.NFCom;
using Uni.Exceptions;

namespace Uni.Business.DFe.Servicos.NFCom
{
    /// <summary>
    /// Enviar o XML de consulta protocolo da NFCom para o web-service
    /// </summary>
#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.Business.DFe.Servicos.NFCom.ConsultaProtocolo")]
    [ComVisible(true)]
#endif
    public class ConsultaProtocolo : ServicoBase, IInteropService<ConsSitNFCom>
    {
        #region Protected Constructors

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
            var xml = new ConsSitNFCom();
            xml = xml.LerXML<ConsSitNFCom>(ConteudoXML);

            if (!Configuracoes.Definida)
            {
                Configuracoes.Servico = Servico.NFeConsultaProtocolo;
                Configuracoes.CodigoUF = Convert.ToInt32(xml.ChNFCom.Substring(0, 2));
                Configuracoes.TipoAmbiente = xml.TpAmb;
                Configuracoes.Modelo = (ModeloDFe)int.Parse(xml.ChNFCom.Substring(20, 2));
                Configuracoes.SchemaVersao = xml.Versao;

                base.DefinirConfiguracao();
            }
        }

        #endregion Protected Constructors

        #region Public Properties

        /// <summary>
        /// Conteúdo retornado pelo web-service depois do envio do XML
        /// </summary>
        public RetConsSitNFCom Result
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(RetornoWSString))
                {
                    return XMLUtility.Deserializar<RetConsSitNFCom>(RetornoWSXML);
                }

                return new RetConsSitNFCom
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
        /// <param name="consSitNFCom">Objeto contendo o XML a ser enviado</param>
        /// <param name="configuracao">Configurações para conexão e envio do XML para o web-service</param>
        public ConsultaProtocolo(ConsSitNFCom consSitNFCom, Configuracao configuracao) : this()
        {
            if (configuracao is null)
            {
                throw new ArgumentNullException(nameof(configuracao));
            }

            Inicializar(consSitNFCom?.GerarXML() ?? throw new ArgumentNullException(nameof(consSitNFCom)), configuracao);
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public ConsultaProtocolo() : base() { }

        #endregion Public Constructors

        #region Public Methods

#if INTEROP

        /// <summary>
        /// Executa o serviço: Assina o XML, valida e envia para o web-service
        /// </summary>
        /// <param name="consSitNFCom">Objeto contendo o XML a ser enviado</param>
        /// <param name="configuracao">Configurações a serem utilizadas na conexão e envio do XML para o web-service</param>
        [ComVisible(true)]
        public void Executar(ConsSitNFCom consSitNFCom, Configuracao configuracao)
        {
            try
            {
                if (configuracao is null)
                {
                    throw new ArgumentNullException(nameof(configuracao));
                }

                Inicializar(consSitNFCom?.GerarXML() ?? throw new ArgumentNullException(nameof(consSitNFCom)), configuracao);
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

#endif

        /// <summary>
        /// Grava o XML de Distribuição em uma pasta definida - (Para este serviço não tem XML de distribuição).
        /// </summary>
        /// <param name="pasta">Pasta onde é para ser gravado do XML</param>
        /// <param name="nomeArquivo">Nome para o arquivo XML</param>
        /// <param name="conteudoXML">Conteúdo do XML</param>
        public override void GravarXmlDistribuicao(string pasta, string nomeArquivo, string conteudoXML)
        {
            try
            {
                throw new Exception("Não existe XML de distribuição para consulta de protocolo.");
            }
            catch (Exception ex)
            {
                Exceptions.ThrowHelper.Instance.Throw(ex);
            }
        }

        #endregion Public Methods
    }
}
