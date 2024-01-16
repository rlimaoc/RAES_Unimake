#if INTEROP
using System.Runtime.InteropServices;
#endif
using Uni.Business.DFe.Xml.CTe;
using Uni.Exceptions;

namespace Uni.Business.DFe.Servicos.CTeOS
{
    /// <summary>
    /// Envio do XML de Consulta Protocolo do CTeOS para o webservice
    /// </summary>
#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.Business.DFe.Servicos.CTeOS.ConsultaProtocolo")]
    [ComVisible(true)]
#endif
    public class ConsultaProtocolo : CTe.ConsultaProtocolo
    {
        #region Public Constructors

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="consSitNFe">Objeto contendo o XML a ser enviado</param>
        /// <param name="configuracao">Configurações para conexão e envio do XML para o web-service</param>
        public ConsultaProtocolo(ConsSitCTe consSitNFe, Configuracao configuracao) : base(consSitNFe, configuracao) { }

        /// <summary>
        /// Construtor
        /// </summary>
        public ConsultaProtocolo() : base() { }

        #endregion Public Constructors

        /// <summary>
        /// Validar o XML
        /// </summary>
        protected override void XmlValidar()
        {
            var validar = new ValidarSchema();
            validar.Validar(ConteudoXML, TipoDFe.CTe.ToString() + "." + Configuracoes.SchemaArquivo, Configuracoes.TargetNS);

            if (!validar.Success)
            {
                throw new ValidarXMLException(validar.ErrorMessage);
            }
        }
    }
}