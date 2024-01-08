﻿#pragma warning disable CS1591

#if INTEROP
using System.Runtime.InteropServices;
#endif
using RUnimake.Business.DFe.Servicos.Enums;
using RUnimake.Business.DFe.Xml;
using System;
using System.Dynamic;
using System.Xml;
using System.Xml.Serialization;

namespace RUnimake.Business.DFe.Xml.GNRE
{
#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Unimake.Business.DFe.Xml.GNRE.TConsultaConfigUf")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlRoot("TConsultaConfigUf", Namespace = "http://www.gnre.pe.gov.br", IsNullable = false)]
    public class TConsultaConfigUf : XMLBase
    {
        [XmlElement("ambiente")]
        public TipoAmbiente Ambiente { get; set; }

        [XmlElement("uf")]
        public UFBrasil UF { get; set; }

        [XmlElement("receita")]
        public Receita Receita { get; set; }

        [XmlElement("tiposGnre")]
        public SimNaoLetra TiposGnre { get; set; }
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Unimake.Business.DFe.Xml.GNRE.Receita")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://www.gnre.pe.gov.br")]
    public class Receita
    {
        [XmlAttribute("courier")]
        public SimNaoLetra Courier { get; set; } = SimNaoLetra.Nao;

        [XmlText()]
        public int Value { get; set; }
    }
}