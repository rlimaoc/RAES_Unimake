#pragma warning disable CS1591

#if INTEROP
using System.Runtime.InteropServices;
#endif
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using Uni.Business.DFe.Utility;

namespace Uni.Business.DFe.Xml.NFCom
{
#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.Business.DFe.Xml.NFCom.ProcEventoNFCom")]
    [ComVisible(true)]
#endif
    [XmlRoot("procEventoNFCom", Namespace = "http://www.portalfiscal.inf.br/nfcom", IsNullable = false)]
    public class ProcEventoNFCom : XMLBase
    {
        #region Public Properties

        [XmlAttribute(AttributeName = "versao")]
        public string Versao { get; set; }

        [XmlAttribute(AttributeName = "ipTransmissor")]
        public string IpTransmissor { get; set; }

        [XmlAttribute(AttributeName = "nPortaCon")]
        public string NPortaCon { get; set; }

        [XmlAttribute(AttributeName = "dhConexao")]
        public string DHConexao { get; set; }

        [XmlElement("eventoNFCom")]
        public EventoNFCom EventoNFCom { get; set; }

        /// <summary>
        /// Nome do arquivo de distribuição
        /// </summary>
        [XmlIgnore]
        public string NomeArquivoDistribuicao => EventoNFCom.InfEvento.ChNFCom + "_" + ((int)EventoNFCom.InfEvento.TpEvento).ToString("000000") + "_" + EventoNFCom.InfEvento.NSeqEvento.ToString("00") + "-proceventonfcom.xml";

        [XmlElement("retEventoNFCom")]
        public RetEventoNFCom RetEventoNFCom { get; set; }

        #endregion Public Properties

        #region Public Methods

        public override XmlDocument GerarXML()
        {
            var xmlDocument = base.GerarXML();

            var attribute = GetType().GetCustomAttribute<XmlRootAttribute>();

            var xmlElementEvento = (XmlElement)xmlDocument.GetElementsByTagName("eventoNFCom")[0];
            xmlElementEvento.SetAttribute("xmlns", attribute.Namespace);

            var xmlElementRetEvento = (XmlElement)xmlElementEvento.GetElementsByTagName("retEventoNFCom")[0];
            xmlElementRetEvento.SetAttribute("xmlns", attribute.Namespace);

            //var xmlElementRetEventoInfEvento = (XmlElement)xmlElementRetEvento.GetElementsByTagName("infEventoNFCom")[0];
            var xmlElementRetEventoInfEvento = (XmlElement)xmlElementRetEvento.GetElementsByTagName("infEvento")[0];
            xmlElementRetEventoInfEvento.SetAttribute("xmlns", attribute.Namespace);

            return xmlDocument;
        }

        public override void ReadXml(XmlDocument document)
        {
            var nodeListEvento = document.GetElementsByTagName("eventoNFCom");

            if (nodeListEvento != null)
            {
                EventoNFCom = XMLUtility.Deserializar<EventoNFCom>(((XmlElement)nodeListEvento[0]).OuterXml);
                var nodeListEventoSignature = ((XmlElement)nodeListEvento[0]).GetElementsByTagName("Signature");
                if (nodeListEventoSignature != null)
                {
                    EventoNFCom.Signature = XMLUtility.Deserializar<Signature>(((XmlElement)nodeListEventoSignature[0]).OuterXml);
                }
            }

            var nodeListRetEvento = document.GetElementsByTagName("retEventoNFCom");
            if (nodeListRetEvento.Count > 0)
            {
                RetEventoNFCom = XMLUtility.Deserializar<RetEventoNFCom>(((XmlElement)nodeListRetEvento[0]).OuterXml);
            }
        }

        #endregion Public Methods
    }
}
