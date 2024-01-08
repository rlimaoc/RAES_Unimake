#pragma warning disable CS1591

#if INTEROP
using System.Runtime.InteropServices;
#endif
using RUnimake.Business.DFe.Servicos.Enums;
using RUnimake.Business.DFe.Xml;
using System.Xml.Serialization;

namespace RUnimake.Business.DFe.Xml.MDFe
{
#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Unimake.Business.DFe.Xml.MDFe.ConsReciMDFe")]
    [ComVisible(true)]
#endif
    [XmlRoot("consReciMDFe", Namespace = "http://www.portalfiscal.inf.br/mdfe", IsNullable = false)]
    public class ConsReciMDFe : XMLBase
    {
        [XmlAttribute(AttributeName = "versao", DataType = "token")]
        public string Versao { get; set; }

        [XmlElement("tpAmb")]
        public TipoAmbiente TpAmb { get; set; }

        [XmlElement("nRec")]
        public string NRec { get; set; }
    }
}
