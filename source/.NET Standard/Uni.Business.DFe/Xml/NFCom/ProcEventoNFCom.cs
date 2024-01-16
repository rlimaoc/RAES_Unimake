#pragma warning disable CS1591

#if INTEROP
using System.Runtime.InteropServices;
#endif
using System.Xml.Serialization;

namespace Uni.Business.DFe.Xml.NFCom
{
#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.Business.DFe.Xml.NFCom.ProcEventoNFCom")]
    [ComVisible(true)]
#endif
    [XmlRoot("procEventoNFCom", Namespace = "http://www.portalfiscal.inf.br/nfcom", IsNullable = false)]
    public class ProcEventoNFCom
    {
        [XmlAttribute(AttributeName = "versao", DataType = "token")]
        public string Versao { get; set; }

        [XmlAttribute(AttributeName = "ipTransmissor", DataType = "token")]
        public string IpTransmissor { get; set; }

        [XmlAttribute(AttributeName = "nPortaCon", DataType = "token")]
        public string NPortaCon { get; set; }

        [XmlAttribute(AttributeName = "dhConexao", DataType = "token")]
        public string DHConexao { get; set; }

        [XmlElement("eventoNFCom")]
        public EventoNFCom EventoNFCom { get; set; }

        [XmlElement("retEventoNFCom")]
        public RetEventoNFCom RetEventoNFCom { get; set; }
    }
}
