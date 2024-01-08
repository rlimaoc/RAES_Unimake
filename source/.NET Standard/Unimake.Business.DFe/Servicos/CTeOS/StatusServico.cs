#if INTEROP
using System.Runtime.InteropServices;
#endif
using RUnimake.Business.DFe.Servicos;
using RUnimake.Business.DFe.Servicos.Enums;
using RUnimake.Business.DFe.Xml.CTe;
using Unimake.Exceptions;

namespace RUnimake.Business.DFe.Servicos.CTeOS
{
    /// <summary>
    /// Envio do XML de consulta status do serviço do CTeOS para o WebService
    /// </summary>
#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Unimake.Business.DFe.Servicos.CTeOS.StatusServico")]
    [ComVisible(true)]
#endif
    public class StatusServico : CTe.StatusServico
    {
        #region Public Constructors

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="consStatServ">Objeto contendo o XML a ser enviado</param>
        /// <param name="configuracao">Configurações para conexão e envio do XML para o web-service</param>
        public StatusServico(ConsStatServCte consStatServ, Configuracao configuracao) : base(consStatServ, configuracao) { }

        /// <summary>
        /// Construtor
        /// </summary>
        public StatusServico() : base() { }

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