#pragma warning disable CS1591

#if INTEROP
using System.Runtime.InteropServices;
#endif
using System;
using System.Xml.Serialization;
using Uni.Business.DFe.Servicos;

namespace Uni.Business.DFe.Xml.NFCom
{
#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.Business.DFe.Xml.NFCom.RetEventoNFCom")]
    [ComVisible(true)]
#endif
    [XmlRoot("retEventoNFCom", Namespace = "http://www.portalfiscal.inf.br/nfcom", IsNullable = false)]
    public class RetEventoNFCom : XMLBase
    {
        [XmlElement("infEvento", Order = 0)]
        public InfEventoRetEvento InfEvento { get; set; }

        [XmlElement("Signature", Namespace = "http://www.w3.org/2000/09/xmldsig#", Order = 1)]
        public Signature Signature { get; set; }

        [XmlAttribute(AttributeName = "versao", DataType = "token")]
        public string Versao { get; set; }
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.Business.DFe.Xml.NFCom.InfEventoRetEvento")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfe")]
    public class InfEventoRetEvento
    {
        #region Public Properties

        #region COrgao
        [XmlIgnore]
        public UFBrasil COrgao { get; set; }

        [XmlElement("cOrgao")]
        public int COrgaoField
        {
            get => (int)COrgao;
            set => COrgao = (UFBrasil)Enum.Parse(typeof(UFBrasil), value.ToString());
        }
        #endregion

        [XmlElement("tpAmb")]
        public TipoAmbiente TpAmb { get; set; }

        [XmlElement("verAplic")]
        public string VerAplic { get; set; }

        [XmlElement("cStat")]
        public int CStat { get; set; }

        [XmlElement("xMotivo")]
        public string XMotivo { get; set; }

        [XmlElement("chNFCom")]
        public string ChNFCom { get; set; }

        #region DhRegEvento
        [XmlIgnore]
#if INTEROP
        public DateTime DhRegEvento { get; set; }
#else
        public DateTimeOffset DhRegEvento { get; set; }
#endif

        [XmlElement("dhRegEvento")]
        public string DhRegEventoField
        {
            get => DhRegEvento.ToString("yyyy-MM-ddTHH:mm:sszzz");
#if INTEROP
            set => DhRegEvento = DateTime.Parse(value);
#else
            set => DhRegEvento = DateTimeOffset.Parse(value);
#endif
        }
        #endregion

        [XmlElement("tpEvento")]
        public TipoEventoNFCom TpEvento { get; set; }

        [XmlElement("xEvento")]
        public string XEvento { get; set; }

        [XmlElement("nSeqEvento")]
        public int NSeqEvento { get; set; }

        [XmlElement("nProt")]
        public string NProt { get; set; }

        [XmlAttribute(DataType = "ID")]
        public string Id
        {
            get => "ID" + ((int)TpEvento).ToString() + NProt;
            set => _ = value;
        }

        #endregion Public Properties
    }
}
