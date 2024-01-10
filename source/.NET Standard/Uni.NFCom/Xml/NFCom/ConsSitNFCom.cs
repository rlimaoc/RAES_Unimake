#pragma warning disable CS1591

#if INTEROP
using System.Runtime.InteropServices;
#endif
using System.Xml;
using System.Xml.Serialization;
using Uni.NFCom.Servicos.Enums;

namespace Uni.NFCom.Xml.NFCom
{
#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.NFCom.Xml.NFCom.ConsSitNFCom")]
    [ComVisible(true)]
#endif
    [XmlRoot("consSitNFCom", Namespace = "http://www.portalfiscal.inf.br/nfcom", IsNullable = false)]
    public class ConsSitNFCom : XMLBase
    {
        [XmlAttribute(AttributeName = "versao", DataType = "token")]
        public string Versao { get; set; }

        [XmlElement("tpAmb")]
        public TipoAmbiente TpAmb { get; set; }

        [XmlElement("xServ")]
        public string XServ { get; set; } = "CONSULTAR";

        [XmlElement("chNFCom")]
        public string ChNFCom { get; set; }
    }
}