#pragma warning disable CS1591

#if INTEROP
using System.Runtime.InteropServices;
#endif
using RUnimake.Business.DFe.Servicos.Enums;
using RUnimake.Business.DFe.Xml;
using System;
using System.Xml;
using System.Xml.Serialization;

namespace RUnimake.Business.DFe.Xml.GNRE
{
#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Unimake.Business.DFe.Xml.GNRE.TConsLoteConsGNRE")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlRoot("TConsLote_GNRE", Namespace = "http://www.gnre.pe.gov.br", IsNullable = false)]
    public class TConsLoteConsGNRE : XMLBase
    {
        private string NumeroReciboField;

        [XmlElement("ambiente")]
        public TipoAmbiente Ambiente { get; set; }

        [XmlElement("numeroRecibo")]
        public string NumeroRecibo
        {

            get => NumeroReciboField;
            set
            {
                if (value.Length != 10)
                {
                    throw new Exception("Conteúdo da tag <numeroRecibo> deve ter exatamente 10 caracteres.");
                }
                NumeroReciboField = value;
            }

        }
    }
}
