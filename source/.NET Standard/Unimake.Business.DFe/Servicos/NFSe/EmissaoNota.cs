﻿#if INTEROP
using System.Runtime.InteropServices;
#endif
using System.Xml;

namespace Unimake.Business.DFe.Servicos.NFSe
{
    /// <summary>
    /// Enviar o XML de Emissão NFSe para o webservice
    /// </summary>
#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Unimake.Business.DFe.Servicos.NFSe.EmissaoNota")]
    [ComVisible(true)]
#endif
    public class EmissaoNota : GerarNfse
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public EmissaoNota() : base()
        { }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="conteudoXML">Conteúdo do XML que será enviado para o WebService</param>
        /// <param name="configuracao">Objeto "Configuracoes" com as propriedade necessária para a execução do serviço</param>
        public EmissaoNota(XmlDocument conteudoXML, Configuracao configuracao) : base(conteudoXML, configuracao)
        { }       
    }
}