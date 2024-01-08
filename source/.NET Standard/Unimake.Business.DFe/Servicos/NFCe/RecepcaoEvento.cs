#if INTEROP
using System.Runtime.InteropServices;
#endif
using RUnimake.Business.DFe.Servicos;
using RUnimake.Business.DFe.Xml.NFe;

namespace RUnimake.Business.DFe.Servicos.NFCe
{
    /// <summary>
    /// Enviar o XML de eventos da NFCe para o web-service
    /// </summary>
#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Unimake.Business.DFe.Servicos.NFCe.RecepcaoEvento")]
    [ComVisible(true)]
#endif
    public class RecepcaoEvento : NFe.RecepcaoEvento
    {
        #region Public Constructors

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="envEvento">Objeto contendo o XML a ser enviado</param>
        /// <param name="configuracao">Configurações para conexão e envio do XML para o web-service</param>
        public RecepcaoEvento(EnvEvento envEvento, Configuracao configuracao) : base(envEvento, configuracao) { }

        /// <summary>
        /// Construtor
        /// </summary>
        public RecepcaoEvento() : base() { }

        #endregion Public Constructors
    }
}