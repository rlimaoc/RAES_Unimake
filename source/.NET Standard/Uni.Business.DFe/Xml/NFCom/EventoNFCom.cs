#pragma warning disable CS1591

#if INTEROP
using System.Runtime.InteropServices;
#endif
using System;
using System.Xml.Serialization;
using Uni.Business.DFe.Servicos;
using Uni.Business.DFe.Xml.CTe;

namespace Uni.Business.DFe.Xml.NFCom
{
#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.Business.DFe.Xml.NFCom.EventoNFCom")]
    [ComVisible(true)]
#endif
    [XmlRoot("eventoNFCom", Namespace = "http://www.portalfiscal.inf.br/nfcom", IsNullable = false)]
    public class EventoNFCom : XMLBase
    {
        [XmlAttribute(AttributeName = "versao", DataType = "token")]
        public string Versao { get; set; }

        [XmlElement("infEvento")]
        public InfEvento InfEvento { get; set; }
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.Business.DFe.Xml.NFCom.InfEvento")]
    [ComVisible(true)]
#endif
    [XmlType(Namespace = "http://www.portalfiscal.inf.br/nfecom")]
    [XmlRoot("infEvento", Namespace = "http://www.portalfiscal.inf.br/nfcom", IsNullable = false)]
    public class InfEvento
    {
        #region Private Fields

        private EventoDetalhe _detEvento;

        #endregion Private Fields

        #region Public Properties
        
        [XmlIgnore]
        public UFBrasil COrgao { get; set; }

        [XmlElement("cOrgao")]
        public int COrgaoField
        {
            get => (int)COrgao;
            set => COrgao = (UFBrasil)Enum.Parse(typeof(UFBrasil), value.ToString());
        }

        [XmlElement("tpAmb")]
        public TipoAmbiente TpAmb { get; set; }

        [XmlElement("CNPJ")]
        public string CNPJ { get; set; }

        [XmlElement("chNFCom")]
        public string ChNFCom { get; set; }

        [XmlIgnore]
#if INTEROP
        public DateTime DhEvento { get; set; }
#else
        public DateTimeOffset DhEvento { get; set; }
#endif

        [XmlElement("dhEvento")]
        public string DhEventoField
        {
            get => DhEvento.ToString("yyyy-MM-ddTHH:mm:sszzz");
#if INTEROP
            set => DhEvento = DateTime.Parse(value);
#else
            set => DhEvento = DateTimeOffset.Parse(value);
#endif

        }

        [XmlElement("tpEvento")]
        public TipoEventoNFCom TpEvento { get; set; }

        [XmlElement("nSeqEvento")]
        public int NSeqEvento { get; set; }

        [XmlElement("detEvento")]
        public EventoDetalhe DetEvento
        {
            get => _detEvento;
            set
            {
                switch (TpEvento)
                {
                    case 0: 
                        _detEvento = value;
                        break;
                    case TipoEventoNFCom.Cancelamento:
                        _detEvento = new DetEventoCanc();
                        break;
                }

                _detEvento.XmlReader = value.XmlReader;
                _detEvento.ProcessReader();
            }
        }

        [XmlAttribute(DataType = "ID")]
        public string Id
        {
            get => "ID" + ((int)TpEvento).ToString() + ChNFCom + NSeqEvento.ToString("00");
            set => _ = value;
        }

        #endregion Public Properties

        #region Public Constructors

        public InfEvento() {}

        public InfEvento(EventoDetalhe detEvento)
        {
            if (detEvento is null)
            {
                throw new ArgumentNullException(nameof(detEvento));
            }

            DetEvento = detEvento;
        }

        #endregion Public Constructors

    }
}
