#pragma warning disable CS1591

#if INTEROP
using System.Runtime.InteropServices;
#endif
using System;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Uni.Business.DFe.Utility;

namespace Uni.Business.DFe.Xml.NFCom
{
#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.Business.DFe.Xml.NFCom.NfcomProc")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlRoot("nfcomProc", Namespace = "http://www.portalfiscal.inf.br/nfcom", IsNullable = false)]
    public class NfcomProc : XMLBase
    {
        [XmlAttribute(AttributeName = "versao", DataType = "token")]
        public string Versao { get; set; }

        [XmlAttribute(AttributeName = "ipTransmissor")]
        public string IPTransmissor { get; set; }

        [XmlAttribute(AttributeName = "nPortaCom")]
        public string NPortaCom { get; set; }

        #region DhConexao
        [XmlIgnore]
#if INTEROP
        public DateTime DhConexao { get; set; }
#else
        public DateTimeOffset DhConexao { get; set; }
#endif

        [XmlAttribute(AttributeName = "dhConexao")]
        public string DhConexaoField
        {
            get => DhConexao.ToString("yyyy-MM-ddTHH:mm:sszzz");
#if INTEROP
            set => DhConexao = DateTime.Parse(value);
#else
            set => DhConexao = DateTimeOffset.Parse(value);
#endif
        }
        #endregion

        [XmlElement("NFCom")]
        public NFCom NFCom { get; set; }

        [XmlElement("protNFCom")]
        public ProtNFCom ProtNFCom { get; set; }

        /// <summary>
        /// Nome do arquivo de distribuição
        /// </summary>
        [XmlIgnore]
        public string NomeArquivoDistribuicao
        {
            get
            {
                switch (ProtNFCom.InfProt.CStat)
                {
                    case 110: //Uso Denegado
                    case 205: //NF-e está denegada na base de dados da SEFAZ [nRec:999999999999999]
                    case 301: //Uso Denegado: Irregularidade fiscal do emitente
                    case 302: //Uso Denegado: Irregularidade fiscal do destinatário
                    case 303: //Uso Denegado: Destinatário não habilitado a operar na UF
                        return ProtNFCom.InfProt.ChNFCom + "-den.xml";

                    case 100: //Autorizado o uso da NF-e
                    case 150: //Autorizado o uso da NF-e, autorização fora de prazo
                    default:
                        return ProtNFCom.InfProt.ChNFCom + "-procnfe.xml";
                }
            }
        }

        public override XmlDocument GerarXML()
        {
            XmlDocument xmlDocument = base.GerarXML();

            XmlRootAttribute attribute = GetType().GetCustomAttribute<XmlRootAttribute>();
            XmlElement xmlElementNFCom = (XmlElement)xmlDocument.GetElementsByTagName("NFCom")[0];
            xmlElementNFCom.SetAttribute("xmlns", attribute.Namespace);
            XmlElement xmlElementProtNFCom = (XmlElement)xmlDocument.GetElementsByTagName("protNFCom")[0];
            xmlElementProtNFCom.SetAttribute("xmlns", attribute.Namespace);

            return xmlDocument;
        }

        /// <summary>
        /// Desserializar o XML no objeto NfcomProc
        /// </summary>
        /// <param name="filename">Localização do arquivo XML de distribuição do NFCom</param>
        /// <returns>Objeto do XML de distribuição do NFCom</returns>
        public NfcomProc LoadFromFile(string filename)
        {
            var doc = new XmlDocument();
            doc.LoadXml(System.IO.File.ReadAllText(filename, Encoding.UTF8));
            return XMLUtility.Deserializar<NfcomProc>(doc);
        }

        /// <summary>
        /// Desserializar o XML NfcomProc no objeto NFCom
        /// </summary>
        /// <param name="xml">string do XML NfeProc</param>
        /// <returns>Objeto da NfeProc</returns>
        public NfcomProc LoadFromXML(string xml) => XMLUtility.Deserializar<NfcomProc>(xml);
    }
}
