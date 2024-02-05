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
    public class RetEventoNFCom
    {
        [XmlElement("infEvento")]
        public InfEventoRetEvento InfEvento { get; set; }

        [XmlAttribute(AttributeName = "versao", DataType = "token")]
        public string Versao { get; set; }
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.Business.DFe.Xml.NFCom.InfEventoRetEvento")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class InfEventoRetEvento
    {
        [XmlElement("tpAmb")]
        public TipoAmbiente TpAmb { get; set; }

        [XmlElement("verAplic")]
        public string VerAplic { get; set; }

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

        [XmlElement("cStat")]
        public int CStat { get; set; }

        [XmlElement("xMotivo")]
        public string XMotivo { get; set; }

        [XmlElement("chNFCom")]
        public string ChNFCom { get; set; }

        [XmlElement("tpEvento")]
        public TipoEventoNFCom TpEvento { get; set; }

        [XmlElement("xEvento")]
        public string XEvento { get; set; }

        [XmlElement("nSeqEvento")]
        public int NSeqEvento { get; set; }

        #region DhRrgEvento
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

        [XmlElement("nProt")]
        public string NProt { get; set; }

        [XmlAttribute(AttributeName = "Id", DataType = "token")]
        public string Id { get; set; }

        #region ShouldSerialize

        public bool ShouldSerializeChNFCom() => !string.IsNullOrWhiteSpace(ChNFCom);

        public bool ShouldSerializeTpEvento() => (TipoEventoNFCom)TpEvento > 0;

        public bool ShouldSerializeXEvento() => !string.IsNullOrWhiteSpace(XEvento);
        
        public bool ShouldSerializeNSeqEvento() => NSeqEvento > 0;

        public bool ShouldSerializeNProt() => !string.IsNullOrWhiteSpace(NProt);

        #endregion
    }
}
