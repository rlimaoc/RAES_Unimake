using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Uni.Business.DFe.Servicos.Interop;
using Uni.Business.DFe.Utility;
using Uni.Business.DFe.Xml.NFCom;
using Uni.Exceptions;

namespace Uni.Business.DFe.Servicos.NFCom
{
    /// <summary>
    /// Enviar o XML de eventos da NFCom para o web-service
    /// </summary>
#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.Business.DFe.Servicos.NFCom.RecepcaoEvento")]
    [ComVisible(true)]
#endif
    public class RecepcaoEvento : ServicoBase, IInteropService<EventoNFCom>
    {
        #region Private Properties

        private EventoNFCom EventoNFCom => new EventoNFCom().LerXML<EventoNFCom>(ConteudoXML);

        #endregion Private Properties

        #region Private Methods

        private void ValidarXMLEvento(XmlDocument xml, string schemaArquivo, string targetNS)
        {
            var validar = new ValidarSchema();
            validar.Validar(xml, TipoDFe.NFCom.ToString() + "." + schemaArquivo, targetNS);

            if (!validar.Success)
            {
                throw new ValidarXMLException(validar.ErrorMessage);
            }
        }

        #endregion Private Methods

        #region Protected Methods

        /// <summary>
        /// Definir o valor de algumas das propriedades do objeto "Configuracoes"
        /// </summary>
        protected override void DefinirConfiguracao()
        {
            var xml = new EventoNFCom();
            xml = xml.LerXML<EventoNFCom>(ConteudoXML);

            if (!Configuracoes.Definida)
            {
                Configuracoes.CodigoUF = (int)xml.InfEvento.COrgao;
                Configuracoes.TipoAmbiente = xml.InfEvento.TpAmb;
                Configuracoes.SchemaVersao = xml.Versao;

                base.DefinirConfiguracao();
            }
        }

        /// <summary>
        /// Validar o XML
        /// </summary>
        protected override void XmlValidar()
        {
            XmlValidarConteudo(); // Efetuar a validação antes de validar schema para evitar alguns erros que não ficam claros para o desenvolvedor.

            var xml = EventoNFCom;

            var schemaArquivo = string.Empty;
            var schemaArquivoEspecifico = string.Empty;

            if (Configuracoes.SchemasEspecificos.Count > 0)
            {
                var tpEvento = ((int)xml.InfEvento.TpEvento);

                schemaArquivo = Configuracoes.SchemasEspecificos[tpEvento.ToString()].SchemaArquivo;
                schemaArquivoEspecifico = Configuracoes.SchemasEspecificos[tpEvento.ToString()].SchemaArquivoEspecifico;
            }

            #region Validar o XML geral

            ValidarXMLEvento(ConteudoXML, schemaArquivo, Configuracoes.TargetNS);

            #endregion Validar o XML geral

            #region Validar a parte específica de cada evento

            var listEvento = ConteudoXML.GetElementsByTagName("evento");
            for (var i = 0; i < listEvento.Count; i++)
            {
                var elementEvento = (XmlElement)listEvento[i];

                if (elementEvento.GetElementsByTagName("infEvento")[0] != null)
                {
                    var elementInfEvento = (XmlElement)elementEvento.GetElementsByTagName("infEvento")[0];
                    if (elementInfEvento.GetElementsByTagName("tpEvento")[0] != null)
                    {
                        var xmlEspecifico = new XmlDocument();
                        xmlEspecifico.LoadXml(elementInfEvento.GetElementsByTagName("detEvento")[0].OuterXml);

                        ValidarXMLEvento(xmlEspecifico, schemaArquivoEspecifico, Configuracoes.TargetNS);
                    }
                }
            }

            #endregion Validar a parte específica de cada evento
        }

        /// <summary>
        /// Validar o conteúdo das tags do XML, alguns validações manuais que o schema não faz. Vamos implementando novas regras na medida da necessidade de cada serviço.
        /// </summary>
        protected override void XmlValidarConteudo()
        {
            base.XmlValidarConteudo();

            var xml = EventoNFCom;

            var tpEvento = xml.InfEvento.TpEvento;

            var msgException = "Conteúdo da tag <descEvento> deve ser igual a \"$\", pois foi este o conteudo informado na tag <tpEvento>.";
            string descEvento = string.Empty;

            switch (tpEvento)
            {
                case TipoEventoNFCom.AutorizadaSubstituicao:
                    descEvento = "Autorizada NFCom de Substituição";
                    break;

                case TipoEventoNFCom.AutorizadaAjuste:
                    descEvento = "Autorizada NFCom de Ajuste";
                    break;

                case TipoEventoNFCom.CanceladaAjuste:
                    descEvento = "Cancelada NFCom de Ajuste";
                    break;

                case TipoEventoNFCom.AutorizadaCofaturamento:
                    descEvento = "Autorizada NFCom de Cofaturamento";
                    break;

                case TipoEventoNFCom.CanceladaCofaturamento:
                    descEvento = "Cancelada NFCom de Cofaturamento";
                    break;

                case TipoEventoNFCom.SubstituidaCofaturamento:
                    descEvento = "Substituída NFCom de Cofaturamento";
                    break;

                case TipoEventoNFCom.LiberacaoPrazoCancelamento:
                    descEvento = "Liberação Prazo Cancelamento";
                    break;
            }

            if (!xml.InfEvento.DetEvento.DescEvento.Equals(descEvento))
            {
                throw new Exception(msgException.Replace("$", descEvento));
            }
        }

        #endregion Protected Methods

        #region Public Properties

        /// <summary>
        /// Propriedade contendo o XML do evento com o protocolo de autorização anexado
        /// </summary>
        public List<ProcEventoNFCom> ProcEventoNFComResult
        {
            get
            {
                var retorno = new List<ProcEventoNFCom>();

                retorno.Add(new ProcEventoNFCom
                {
                    Versao = EventoNFCom.Versao,
                    EventoNFCom = EventoNFCom,
                    RetEventoNFCom = Result
                });

                return retorno;
            }
        }

        /// <summary>
        /// Conteúdo retornado pelo web-service depois do envio do XML
        /// </summary>
        public RetEventoNFCom Result
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(RetornoWSString))
                {
                    return XMLUtility.Deserializar<RetEventoNFCom>(RetornoWSXML);
                }

                return new RetEventoNFCom
                {
                    InfEvento = new InfEventoRetEvento()
                };
            }
        }

        #endregion Public Properties

        #region Public Constructors

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="eventoNFCom">Objeto contendo o XML a ser enviado</param>
        /// <param name="configuracao">Configurações para conexão e envio do XML para o web-service</param>
        public RecepcaoEvento(EventoNFCom eventoNFCom, Configuracao configuracao) : this()
        {
            if (configuracao is null)
            {
                throw new ArgumentNullException(nameof(configuracao));
            }

            Inicializar(eventoNFCom?.GerarXML() ?? throw new ArgumentNullException(nameof(eventoNFCom)), configuracao);
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public RecepcaoEvento() : base() { }

        #endregion Public Constructors

        #region Public Methods

#if INTEROP

        /// <summary>
        /// Executa o serviço: Assina o XML, valida e envia para o web-service
        /// </summary>
        /// <param name="eventoNFCom">Objeto contendo o XML a ser enviado</param>
        /// <param name="configuracao">Configurações a serem utilizadas na conexão e envio do XML para o web-service</param>
        [ComVisible(true)]
        public void Executar(EventoNFCom eventoNFCom, Configuracao configuracao)
        {
            try
            {
                if (configuracao is null)
                {
                    throw new ArgumentNullException(nameof(configuracao));
                }

                Inicializar(eventoNFCom?.GerarXML() ?? throw new ArgumentNullException(nameof(eventoNFCom)), configuracao);
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
        /// <param name="eventoNFCom">Objeto contendo o XML a ser enviado</param>
        /// <param name="configuracao">Configurações para conexão e envio do XML para o web-service</param>
        public void SetXMLConfiguracao(EventoNFCom eventoNFCom, Configuracao configuracao)
        {
            try
            {
                if (configuracao is null)
                {
                    throw new ArgumentNullException(nameof(configuracao));
                }

                Inicializar(eventoNFCom?.GerarXML() ?? throw new ArgumentNullException(nameof(eventoNFCom)), configuracao);
            }
            catch (Exception ex)
            {
                ThrowHelper.Instance.Throw(ex);
            }
        }

        /// <summary>
        /// Retorna o <see cref="ProcEventoNFCom"/> pelo índice ou nulo, se não existir
        /// </summary>
        /// <param name="index">Índice em que deve ser recuperado o evento e convertido para XML</param>
        /// <returns></returns>
        public string GetProcEventoNFComResultXMLByIndex(int index)
        {
            var list = ProcEventoNFComResult;

            if (list.Count == 0 ||
                index >= list.Count)
            {
                return "";
            }

            return list[index].GerarXML().InnerXml;
        }


        /// <summary>
        /// Retorna o <see cref="ProcEventoNFCom"/> pelo índice ou nulo, se não existir
        /// </summary>
        /// <param name="index">Índice em que deve ser recuperado o evento e convertido para XML</param>
        /// <returns></returns>
        public ProcEventoNFe GetProcEventoNFComResult(int index)
        {
            var list = ProcEventoNFComResult;

            if (list.Count == 0 ||
                index >= list.Count)
            {
                return null;
            }

            return list[index];
        }

#endif

        /// <summary>
        /// Gravar o XML de distribuição em uma pasta no HD
        /// </summary>
        /// <param name="pasta">Pasta onde deve ser gravado o XML</param>
        public void GravarXmlDistribuicao(string pasta)
        {
            try
            {
                GravarXmlDistribuicao(pasta, ProcEventoNFComResult[0].NomeArquivoDistribuicao, ProcEventoNFComResult[0].GerarXML().OuterXml);
            }
            catch (Exception ex)
            {
                ThrowHelper.Instance.Throw(ex);
            }
        }

        /// <summary>
        /// Grava o XML de distribuição no stream
        /// </summary>
        /// <param name="stream">Stream que vai receber o XML de distribuição</param>
        public void GravarXmlDistribuicao(Stream stream)
        {
            try
            {
                GravarXmlDistribuicao(stream, ProcEventoNFComResult[0].GerarXML().OuterXml);
            }
            catch (Exception ex)
            {
                ThrowHelper.Instance.Throw(ex);
            }
        }

        #endregion Public Methods
    }
}
