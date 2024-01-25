#pragma warning disable CS1591

#if INTEROP
// The result of the expression is always the same since a value of this type is never equal to 'null'
#pragma warning disable CS0472 
#endif

#if INTEROP
using System.Runtime.InteropServices;
#endif
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Uni.Business.DFe.Servicos;
using Uni.Business.DFe.Utility;
using Uni.Business.DFe.Xml.EFDReinf;

namespace Uni.Business.DFe.Xml.NFCom
{
#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.NFCom.Xml.NFCom.NFCom")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlRoot("NFCom", Namespace = "http://www.portalfiscal.inf.br/nfcom", IsNullable = false)]
    public class NFCom : XMLBase
    {
        [XmlElement("infNFCom")]
        public List<InfNFCom> InfNFCom { get; set; }

        [XmlElement("infNFComSupl")]
        public InfNFComSupl InfNFComSupl { get; set; }

        [XmlElement(ElementName = "Signature", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public Signature Signature { get; set; }

        public override XmlDocument GerarXML()
        {
            var xmlDoc = base.GerarXML();

            foreach (var nodeNFCom in xmlDoc.GetElementsByTagName("NFCom"))
            {
                var elemInfNFCom = (XmlElement)nodeNFCom;

                foreach (var nodeInfNFCom in elemInfNFCom.GetElementsByTagName("InfNFCom"))
                {
                    var elemNFCom = (XmlElement)nodeInfNFCom;

                    var attribute = GetType().GetCustomAttribute<XmlRootAttribute>();
                    elemNFCom.SetAttribute("xmlns", attribute.Namespace);
                }
            }

            return xmlDoc;
        }

        /// <summary>
        /// Deserializar o XML NFCom no objeto NFCom
        /// </summary>
        /// <param name="filename">Localização do arquivo XML NFCom</param>
        /// <returns>Objeto da NFCom</returns>
        public NFCom LoadFromFile(string filename)
        {
            var doc = new XmlDocument();
            doc.LoadXml(System.IO.File.ReadAllText(filename, Encoding.UTF8));
            return XMLUtility.Deserializar<NFCom>(doc);
        }

        /// <summary>
        /// Deserializar o XML NFCom no objeto NFCom
        /// </summary>
        /// <param name="xml">string do XML NFCom</param>
        /// <returns>Objeto da NFCom</returns>
        public NFCom LoadFromXML(string xml) => XMLUtility.Deserializar<NFCom>(xml);

#if INTEROP

        /// <summary>
        /// Adicionar novo elemento a lista
        /// </summary>
        /// <param name="infNFCom">Elemento</param>
        public void AddInfNFCom(InfNFCom infNFCom)
        {
            if (InfNFCom == null)
            {
                InfNFCom = new List<InfNFCom>();
            }

            InfNFCom.Add(infNFCom);
        }

        /// <summary>
        /// Retorna o elemento da lista InfNFCom (Utilizado para linguagens diferentes do CSharp que não conseguem pegar o conteúdo da lista)
        /// </summary>
        /// <param name="index">Índice da lista a ser retornado (Começa com 0 (zero))</param>
        /// <returns>Conteúdo do index passado por parâmetro da InfNFCom</returns>
        public InfNFCom GetInfNFCom(int index)
        {
            if ((InfNFCom?.Count ?? 0) == 0)
            {
                return default;
            };

            return InfNFCom[index];
        }

        /// <summary>
        /// Retorna a quantidade de elementos existentes na lista InfNFCom
        /// </summary>
        public int GetInfNFComCount => (InfNFCom != null ? InfNFCom.Count : 0);

#endif
    }

    #region InfNFCom

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.Business.DFe.Xml.NFCom.InfNFCom")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class InfNFCom
    {
        private string IdField;

        [XmlAttribute(AttributeName = "versao", DataType = "token")]
        public string Versao { get; set; }

        [XmlElement("ide")]
        public Ide Ide { get; set; }

        [XmlElement("emit")]
        public Emit Emit { get; set; }

        [XmlElement("dest")]
        public Dest Dest { get; set; }

        [XmlElement("assinante")]
        public Assinante Assinante { get; set; }

        [XmlElement("gSub")]
        public GSub GSub { get; set; }

        [XmlElement("gCofat")]
        public GCofat GCofat { get; set; }

        [XmlElement("det")]
        public List<Det> Det { get; set; }

        [XmlElement("total")]
        public Total Total { get; set; }

        [XmlElement("gFidelidade")]
        public GFidelidade GFidelidade { get; set; }

        [XmlElement("gFat")]
        public GFat GFat { get; set; }

        [XmlElement("gFatCentral")]
        public GFatCentral GFatCentral { get; set; }

        [XmlElement("autXML")]
        public List<AutXML> AutXML { get; set; }

        [XmlElement("infAdic")]
        public InfAdic InfAdic { get; set; }

        [XmlElement("gRespTec")]
        public GRespTec GRespTec { get; set; }

        [XmlAttribute(AttributeName = "Id", DataType = "ID")]
        public string Id
        {
            get
            {
                IdField = "NFCom" + Chave;
                return IdField;
            }
            set => IdField = value;
        }

        private string ChaveField;

        [XmlIgnore]
        public string Chave
        {
            get
            {
                ChaveField = ((int)Ide.CUF).ToString() +
                    Ide.DhEmi.ToString("yyMM") +
                    Emit.CNPJ.PadLeft(14, '0') +
                    ((int)Ide.Mod).ToString().PadLeft(2, '0') +
                    Ide.Serie.ToString().PadLeft(3, '0') +
                    Ide.NNF.ToString().PadLeft(9, '0') +
                    ((int)Ide.TpEmis).ToString() +
                    Ide.NSiteAutoriz.ToString() +
                    Ide.CNF.PadLeft(7, '0');

                Ide.CDV = XMLUtility.CalcularDVChave(ChaveField);

                ChaveField += Ide.CDV.ToString();

                return ChaveField;
            }
            set => throw new Exception("Não é permitido atribuir valor para a propriedade Chave. Ela é calculada automaticamente.");
        }

        #region ShouldSerialize

        public bool ShouldSerializeGSub() => GSub != null;
        public bool ShouldSerializeGCofat() => GCofat != null;
        public bool ShouldSerializeGFidelidade() => GFidelidade != null;
        public bool ShouldSerializeGFat() => GFat != null;
        public bool ShouldSerializeGFatCentral() => GFatCentral != null;
        public bool ShouldSerializeAutXML() => AutXML != null;
        public bool ShouldSerializeInfAdic() => InfAdic != null;
        public bool ShouldSerializeGRespTec() => GRespTec != null;

        #endregion

#if INTEROP

        /// <summary>
        /// Adicionar novo elemento a lista
        /// </summary>
        /// <param name="autXML">Elemento</param>
        public void AddAutXml(AutXML autXML)
        {
            if (AutXML == null)
            {
                AutXML = new List<AutXML>();
            }

            AutXML.Add(autXML);
        }

        /// <summary>
        /// Adicionar novo elemento a lista
        /// </summary>
        /// <param name="det">Elemento</param>
        public void AddDet(Det det)
        {
            if (Det == null)
            {
                Det = new List<Det>();
            }

            Det.Add(det);
        }

        /// <summary>
        /// Retorna o elemento da lista AutXML (Utilizado para linguagens diferentes do CSharp que não conseguem pegar o conteúdo da lista)
        /// </summary>
        /// <param name="index">Índice da lista a ser retornado (Começa com 0 (zero))</param>
        /// <returns>Conteúdo do index passado por parâmetro da AutXML</returns>
        public AutXML GetAutXML(int index)
        {
            if ((AutXML?.Count ?? 0) == 0)
            {
                return default;
            };

            return AutXML[index];
        }

        /// <summary>
        /// Retorna a quantidade de elementos existentes na lista AutXML
        /// </summary>
        public int GetAutXMLCount => (AutXML != null ? AutXML.Count : 0);

        /// <summary>
        /// Retorna o elemento da lista Det (Utilizado para linguagens diferentes do CSharp que não conseguem pegar o conteúdo da lista)
        /// </summary>
        /// <param name="index">Índice da lista a ser retornado (Começa com 0 (zero))</param>
        /// <returns>Conteúdo do index passado por parâmetro da Det</returns>
        public Det GetDet(int index)
        {
            if ((Det?.Count ?? 0) == 0)
            {
                return default;
            };

            return Det[index];
        }

        /// <summary>
        /// Retorna a quantidade de elementos existentes na lista Det
        /// </summary>
        public int GetDetCount => (Det != null ? Det.Count : 0);

#endif
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.Business.DFe.Xml.NFCom.Ide")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class Ide
    {
        private string CNFField;
        private string XJustField;

        #region CUF
        [XmlIgnore]
        public UFBrasil CUF { get; set; }

        [XmlElement("cUF")]
        public int CUFField
        {
            get => (int)CUF;
            set => CUF = (UFBrasil)Enum.Parse(typeof(UFBrasil), value.ToString());
        }
        #endregion

        [XmlElement("tpAmb")]
        public TipoAmbiente TpAmb { get; set; }

        [XmlElement("mod")]
        public ModeloDFe Mod { get; set; } = ModeloDFe.NFCom;

        [XmlElement("serie")]
        public int Serie { get; set; }

        [XmlElement("nNF")]
        public int NNF { get; set; }

        #region CNF
        [XmlElement("cNF")]
        public string CNF
        {
            get
            {
                string retorno;
                if (string.IsNullOrWhiteSpace(CNFField))
                {
                    if (NNF < 0)
                    {
                        throw new Exception("Defina o conteúdo da TAG <nNF>, pois a mesma é utilizada como base para calcular o código numérico.");
                    }

                    retorno = XMLUtility.GerarCodigoNumerico(NNF, true).ToString("0000000");
                }
                else
                {
                    retorno = CNFField;
                }

                return retorno;
            }
            set => CNFField = value;
        }
        #endregion

        [XmlElement("cDV")]
        public int CDV { get; set; }

        #region DhEmi
        [XmlIgnore]
#if INTEROP
        public DateTime DhEmi { get; set; }
#else
        public DateTimeOffset DhEmi { get; set; }
#endif

        [XmlElement("dhEmi")]
        public string DhEmiField
        {
            get => DhEmi.ToString("yyyy-MM-ddTHH:mm:sszzz");
#if INTEROP
            set => DhEmi = DateTime.Parse(value);
#else
            set => DhEmi = DateTimeOffset.Parse(value);
#endif
        }
        #endregion

        [XmlElement("tpEmis")]
        public TipoEmissao TpEmis { get; set; }

        [XmlElement("nSiteAutoriz")]
        public int NSiteAutoriz { get; set; } = 0;

        [XmlElement("cMunFG")]
        public int CMunFG { get; set; }

        [XmlElement("finNFCom")]
        public FinalidadeNFCom FinNFCom { get; set; }

        [XmlElement("tpFat")]
        public TipoFaturamento TpFat { get; set; }

        [XmlElement("verProc")]
        public string VerProc { get; set; } = "1.00";

        [XmlElement("indPrePago")]
        public SimNao IndPrePago { get; set; } = 0;

        [XmlElement("indCessaoMeiosRede")]
        public SimNao IndCessaoMeiosRede { get; set; } = 0;

        [XmlElement("indNotaEntrada")]
        public SimNao IndNotaEntrada { get; set; } = 0;

        #region DhCont
        [XmlIgnore]
#if INTEROP
        public DateTime DhCont { get; set; }
#else
        public DateTimeOffset DhCont { get; set; }
#endif

        [XmlElement("dhCont")]
        public string DhContField
        {
            get => DhCont.ToString("yyyy-MM-ddTHH:mm:sszzz");
#if INTEROP
            set => DhCont = DateTime.Parse(value);
#else
            set => DhCont = DateTimeOffset.Parse(value);
#endif
        }
        #endregion

        [XmlElement("xJust")]
        public string XJust
        {
            get => XJustField;
            set => XJustField = value == null ? value : XMLUtility.UnescapeReservedCharacters(value).Truncate(256).Trim();
        }

        #region ShouldSerialize

        public bool ShouldSerializeIndPrePago() => (int)IndPrePago == 1;
        public bool ShouldSerializeIndCessaoMeiosRede() => (int)IndCessaoMeiosRede == 1;
        public bool ShouldSerializeIndNotaEntrada() => (int)IndNotaEntrada == 1;
        public bool ShouldSerializeDhContField() => !string.IsNullOrWhiteSpace(DhContField) && !string.IsNullOrWhiteSpace(XJust);
        public bool ShouldSerializeXJust() => !string.IsNullOrWhiteSpace(DhContField) && !string.IsNullOrWhiteSpace(XJust);

        #endregion
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.Business.DFe.Xml.NFCom.Emit")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class Emit
    {
        private string XNomeField;
        private string XFantField;

        [XmlElement("CNPJ")]
        public string CNPJ { get; set; }

        [XmlElement("IE")]
        public string IE { get; set; }

        [XmlElement("IEUFDest")]
        public string IEUFDest { get; set; }

        [XmlElement("CRT")]
        public CRT CRT { get; set; }

        [XmlElement("xNome")]
        public string XNome
        {
            get => XNomeField;
            set => XNomeField = value == null ? value : XMLUtility.UnescapeReservedCharacters(value).Truncate(60).Trim();
        }

        [XmlElement("xFant")]
        public string XFant
        {
            get => XFantField;
            set => XFantField = value == null ? value : XMLUtility.UnescapeReservedCharacters(value).Truncate(60).Trim();
        }

        [XmlElement("enderEmit")]
        public EnderEmit EnderEmit { get; set; }

        #region ShouldSerialize

        public bool ShouldSerializeIEUFDest() => !string.IsNullOrWhiteSpace(IEUFDest);
        public bool ShouldSerializeXFant() => !string.IsNullOrWhiteSpace(XFant);

        #endregion
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.Business.DFe.Xml.NFCom.EnderEmit")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class EnderEmit : TEndereco { }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.Business.DFe.Xml.NFCom.Dest")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class Dest
    {
        private string XNomeField;

        [XmlElement("xNome")]
        public string XNome
        {
            get => XNomeField;
            set => XNomeField = value == null ? value : XMLUtility.UnescapeReservedCharacters(value).Truncate(60).Trim();
        }

        [XmlElement("CNPJ")]
        public string CNPJ { get; set; }

        [XmlElement("CPF")]
        public string CPF { get; set; }

        [XmlIgnore]
        public string CpfCnpj
        {
            set
            {
                if (value.Length <= 11)
                {
                    CPF = value;
                }
                else
                {
                    CNPJ = value;
                }
            }
        }

        [XmlElement("idOutros")]
        public string IdOutros { get; set; }

        [XmlElement("indIEDest")]
        public IndicadorIEDestinatario IndIEDest { get; set; }

        [XmlElement("IE")]
        public string IE { get; set; }

        [XmlElement("IM")]
        public string IM { get; set; }

        [XmlElement("enderDest")]
        public EnderDest EnderDest { get; set; }

        #region ShouldSerialize

        public bool ShouldSerializeCNPJ() => !string.IsNullOrWhiteSpace(CNPJ);
        public bool ShouldSerializeCPF() => !string.IsNullOrWhiteSpace(CPF);
        public bool ShouldSerializeIdOutros() => !string.IsNullOrWhiteSpace(IdOutros) || (string.IsNullOrWhiteSpace(CNPJ) && string.IsNullOrWhiteSpace(CPF) && !string.IsNullOrWhiteSpace(XNome));
        public bool ShouldSerializeIE() => !string.IsNullOrWhiteSpace(IE);
        public bool ShouldSerializeIM() => !string.IsNullOrWhiteSpace(IM);

        #endregion
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.Business.DFe.Xml.NFCom.EnderDest")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class EnderDest
    {
        private string XLgrField;
        private string XCplField;
        private string NroField;
        private string XBairroField;
        private string XMunField;
        private string XPaisField = "BRASIL";

        [XmlElement("xLgr")]
        public string XLgr
        {
            get => XLgrField;
            set => XLgrField = value == null ? value : XMLUtility.UnescapeReservedCharacters(value).Truncate(60).Trim();
        }

        [XmlElement("nro")]
        public string Nro
        {
            get => NroField;
            set => NroField = value == null ? value : XMLUtility.UnescapeReservedCharacters(value).Truncate(60).Trim();
        }

        [XmlElement("xCpl")]
        public string XCpl
        {
            get => XCplField;
            set => XCplField = value == null ? value : XMLUtility.UnescapeReservedCharacters(value).Truncate(60).Trim();
        }

        [XmlElement("xBairro")]
        public string XBairro
        {
            get => XBairroField;
            set => XBairroField = value == null ? value : XMLUtility.UnescapeReservedCharacters(value).Truncate(60).Trim();
        }

        [XmlElement("cMun")]
        public int CMun { get; set; }

        [XmlElement("xMun")]
        public string XMun
        {
            get => XMunField;
            set => XMunField = value == null ? value : XMLUtility.UnescapeReservedCharacters(value).Truncate(60).Trim();
        }

        [XmlElement("CEP")]
        public string CEP { get; set; }

        [XmlElement("UF")]
        public UFBrasil UF { get; set; }

        [XmlElement("cPais")]
        public int CPais { get; set; } = 1058;

        [XmlElement("xPais")]
        public string XPais
        {
            get => XPaisField;
            set => XPaisField = value == null ? value : XMLUtility.UnescapeReservedCharacters(value).Truncate(60).Trim();
        }

        [XmlElement("fone")]
        public string Fone { get; set; }

        [XmlElement("email")]
        public string Email { get; set; }

        #region ShouldSerialize

        public bool ShouldSerializeXCpl() => !string.IsNullOrWhiteSpace(XCpl);
        public bool ShouldSerializeCPais() => CPais > 0;
        public bool ShouldSerializeXPais() => !string.IsNullOrWhiteSpace(XPais);

        public bool ShouldSerializeFone() => !string.IsNullOrWhiteSpace(Fone);
        public bool ShouldSerializeEmail() => !string.IsNullOrWhiteSpace(Email);

        #endregion 
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.Business.DFe.Xml.NFCom.Assinante")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class Assinante
    {
        private string NroTermPrincField;

        [XmlElement("iCodAssinante")]
        public string ICodAssinante { get; set; }

        [XmlElement("tpAssinante")]
        public TipoAssinante TpAssinante { get; set; }

        [XmlElement("tpServUtil")]
        public TipoServicoUtilizado TpServUtil { get; set; }

        [XmlElement("nContrato")]
        public string NContrato { get; set; }

        #region dContratoIni
        [XmlIgnore]
#if INTEROP
        public DateTime DContratoIni { get; set; }
#else
        public DateTimeOffset DContratoIni { get; set; }
#endif

        [XmlElement("dContratoIni")]
        public string DContratoIniField
        {
            get => DContratoIni.ToString("yyyy-MM-dd");
#if INTEROP
            set => DContratoIni = DateTime.Parse(value);
#else
            set => DContratoIni = DateTimeOffset.Parse(value);
#endif
        }
        #endregion

        #region dContratoFim
        [XmlIgnore]
#if INTEROP
        public DateTime DContratoFim { get; set; }
#else
        public DateTimeOffset DContratoFim { get; set; }
#endif

        [XmlElement("dContratoFim")]
        public string DContratoFimField
        {
            get => DContratoFim.ToString("yyyy-MM-dd");
#if INTEROP
            set => DContratoFim = DateTime.Parse(value);
#else
            set => DContratoFim = DateTimeOffset.Parse(value);
#endif
        }
        #endregion

        [XmlElement("NroTermPrinc")]
        public string NroTermPrinc
        {
            get => NroTermPrincField;
            set => NroTermPrincField = value == null ? value : XMLUtility.UnescapeReservedCharacters(value).Truncate(256).Trim();
        }

        [XmlIgnore]
        public UFBrasil CUFPrinc { get; set; }

        [XmlElement("cUFPrinc")]
        public int CUFPrincField
        {
            get => (int)CUFPrinc;
            set => CUFPrinc = (UFBrasil)Enum.Parse(typeof(UFBrasil), value.ToString());
        }

        [XmlIgnore]
        public List<TermAdic> TermAdic { get; set; }

        #region ShouldSerialize

        public bool ShouldSerializeNContrato() => !string.IsNullOrWhiteSpace(NContrato);
        public bool ShouldSerializeDContratoIniField() => DContratoIni.IsNullOrEmpty();
        public bool ShouldSerializeDContratoFimField() => DContratoFim.IsNullOrEmpty();
        public bool ShouldSerializeNroTermPrinc() => !string.IsNullOrWhiteSpace(NroTermPrinc) && CUFPrincField > 0;
        public bool ShouldSerializeCUFPrincField() => !string.IsNullOrWhiteSpace(NroTermPrinc) && CUFPrincField > 0;
        public bool ShouldSerializeTermAdic() => TermAdic != null || TermAdic.Count > 0;

        #endregion
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.Business.DFe.Xml.NFCom.TermAdic")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class TermAdic
    {
        private string NroTermAdicField;

        [XmlElement("NroTermAdic")]
        public string NroTermAdic
        {
            get => NroTermAdicField;
            set => NroTermAdicField = value == null ? value : XMLUtility.UnescapeReservedCharacters(value).Truncate(256).Trim();
        }

        [XmlIgnore]
        public UFBrasil CUFAdic { get; set; }

        [XmlElement("cUFAdic")]
        public int CUFAdicField
        {
            get => (int)CUFAdic;
            set => CUFAdic = (UFBrasil)Enum.Parse(typeof(UFBrasil), value.ToString());
        }

        #region ShouldSerialize

        public bool ShouldSerializeNroTermAdic() => !string.IsNullOrWhiteSpace(NroTermAdic) && CUFAdicField > 0;
        public bool ShouldSerializeCUFAdicField() => !string.IsNullOrWhiteSpace(NroTermAdic) && CUFAdicField > 0;

        #endregion
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.Business.DFe.Xml.NFCom.GSub")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class GSub
    {
        [XmlElement("chNFCom")]
        public string ChNFCom { get; set; }

        [XmlElement("gNF")]
        public GNF GNF { get; set; }

        [XmlElement("motSub")]
        public MotivoSubstituicao MotSub { get; set; }

        #region ShouldSerialize

        public bool ShouldSerializeChNFCom() => !string.IsNullOrWhiteSpace(ChNFCom);
        public bool ShouldSerializeGNF() => GNF != null;

        #endregion
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.Business.DFe.Xml.NFCom.GNF")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class GNF
    {
        [XmlElement("CNPJ")]
        public string CNPJ { get; set; }

        [XmlElement("mod")]
        public ModeloNFImpresso Mod { get; set; }

        [XmlElement("serie")]
        public int Serie { get; set; } = 3;

        [XmlElement("nNF")]
        public int NNF { get; set; }

        [XmlElement("CompetEmis")]
        public string CompetEmis { get; set; }

        [XmlElement("hash115")]
        public string Hash115 { get; set; }

        #region ShouldSerialize

        public bool ShouldSerializeHash115() => !string.IsNullOrWhiteSpace(Hash115);

        #endregion
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.Business.DFe.Xml.NFCom.GCofat")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class GCofat
    {
        [XmlElement("chNFComLocal")]
        public string ChNFComLocal { get; set; }
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.Business.DFe.Xml.NFCom.Det")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class Det
    {
        private string InfAdProdField;

        [XmlAttribute(AttributeName = "nItem")]
        public int NItem { get; set; }

        [XmlAttribute(AttributeName = "chNFComAnt")]
        public string ChNFComAnt { get; set; }

        [XmlAttribute(AttributeName = "nItemAnt")]
        public int NItemAnt { get; set; }

        [XmlElement("prod")]
        public Prod Prod { get; set; }

        [XmlElement("imposto")]
        public Imposto Imposto { get; set; }

        [XmlElement("gProcRef")]
        public GProcRef GProcRef { get; set; }

        [XmlElement("gRessarc")]
        public GRessarc GRessarc { get; set; }

        [XmlElement("infAdProd")]
        public string InfAdProd
        {
            get => string.IsNullOrWhiteSpace(InfAdProdField) ? null : InfAdProdField;
            set => InfAdProdField = value == null ? value : XMLUtility.UnescapeReservedCharacters(value).Truncate(500).Trim();
        }

        #region ShouldSerialize

        public bool ShouldSerializeChNFComAnt() => !string.IsNullOrWhiteSpace(ChNFComAnt);
        public bool ShouldSerializeNItemAnt() => NItemAnt > 0;
        public bool ShouldSerializeGProcRef() => GProcRef != null;
        public bool ShouldSerializeGRessarc() => GRessarc != null;
        public bool ShouldSerializeInfAdProd() => !string.IsNullOrWhiteSpace(InfAdProd);

        #endregion
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.Business.DFe.Xml.NFCom.Prod")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class Prod
    {
        private string XProdField;

        [XmlElement("cProd")]
        public string CProd { get; set; }

        [XmlElement("xProd")]
        public string XProd
        {
            get => XProdField;
            set => XProdField = value == null ? value : XMLUtility.UnescapeReservedCharacters(value).Truncate(120).Trim();
        }

        #region CCLass
        [XmlIgnore]
        public ClassificacaoItem CClass { get; set; }

        [XmlElement("cClass")]
        public string CClassField
        {
            get => ((int)CClass).ToString().PadLeft(7, '0');
            set => CClass = (ClassificacaoItem)Enum.Parse(typeof(ClassificacaoItem), value.ToString());
        }
        #endregion

        [XmlElement("CFOP")]
        public string CFOP { get; set; }

        [XmlElement("CNPJLD")]
        public string CNPJLD { get; set; }

        [XmlElement("uMed")]
        public UnidadeBasicaMedida UMed { get; set; }

        #region QFaturada
        [XmlIgnore]
        public decimal QFaturada { get; set; }

        [XmlElement("qFaturada")]
        public string QFaturadaField
        {
            get => QFaturada.ToString("F4", CultureInfo.InvariantCulture);
            set => QFaturada = (decimal)Converter.ToDouble(value);
        }
        #endregion

        #region VItem
        [XmlIgnore]
        public double VItem { get; set; }

        [XmlElement("vItem")]
        public string VItemField
        {
            get => VItem.ToString("F8", CultureInfo.InvariantCulture);
            set => VItem = Converter.ToDouble(value);
        }
        #endregion

        #region VDesc
        [XmlIgnore]
        public double VDesc { get; set; }

        [XmlElement("vDesc")]
        public string VDescField
        {
            get => VDesc.ToString("F2", CultureInfo.InvariantCulture);
            set => VDesc = Converter.ToDouble(value);
        }
        #endregion

        #region VOutro
        [XmlIgnore]
        public double VOutro { get; set; }

        [XmlElement("vOutro")]
        public string VOutroField
        {
            get => VOutro.ToString("F2", CultureInfo.InvariantCulture);
            set => VOutro = Converter.ToDouble(value);
        }
        #endregion

        #region VProd
        [XmlIgnore]
        public double VProd { get; set; }

        [XmlElement("vProd")]
        public string VProdField
        {
            get => VProd.ToString("F8", CultureInfo.InvariantCulture);
            set => VProd = Converter.ToDouble(value);
        }
        #endregion

        #region dExpiracao
        [XmlIgnore]
#if INTEROP
        public DateTime DExpiracao { get; set; }
#else
        public DateTimeOffset DExpiracao { get; set; }
#endif

        [XmlElement("dExpiracao")]
        public string DExpiracaoField
        {
            get => DExpiracao.ToString("yyyy-MM-dd");
#if INTEROP
            set => DExpiracao = DateTime.Parse(value);
#else
            set => DExpiracao = DateTimeOffset.Parse(value);
#endif
        }
        #endregion

        [XmlElement("indDevolucao")]
        public SimNao IndDevolucao { get; set; }

        #region ShouldSerialize

        public bool ShouldSerializeCFOP() => !string.IsNullOrWhiteSpace(CFOP);
        public bool ShouldSerializeCNPJLD() => !string.IsNullOrWhiteSpace(CNPJLD);
        public bool ShouldSerializeVDescField() => VDesc > 0;
        public bool ShouldSerializeVOutroField() => VOutro > 0;
        public bool ShouldSerializeDExpiracaoField() => DExpiracao.IsNullOrEmpty();
        public bool ShouldSerializeIndDevolucao() => (int)IndDevolucao == 1;

        #endregion
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.Business.DFe.Xml.NFCom.Imposto")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class Imposto
    {
        [XmlElement("ICMS00")]
        public ICMS00 ICMS00 { get; set; }

        [XmlElement("ICMS20")]
        public ICMS20 ICMS20 { get; set; }

        [XmlElement("ICMS40")]
        public ICMS40 ICMS40 { get; set; }

        [XmlElement("ICMS51")]
        public ICMS51 ICMS51 { get; set; }

        [XmlElement("ICMS90")]
        public ICMS90 ICMS90 { get; set; }

        [XmlElement("ICMSSN")]
        public ICMSSN ICMSSN { get; set; }

        [XmlElement("ICMSUFDest")]
        public ICMSUFDest ICMSUFDest { get; set; }

        [XmlElement("indSemCST")]
        public int IndSemCST { get; set; }

        [XmlElement("PIS")]
        public PIS PIS { get; set; }

        [XmlElement("COFINS")]
        public COFINS COFINS { get; set; }

        [XmlElement("FUST")]
        public FUST FUST { get; set; }

        [XmlElement("FUNTTEL")]
        public FUNTTEL FUNTTEL { get; set; }

        [XmlElement("retTrib")]
        public RetTrib RetTrib { get; set; }

        #region ShouldSerialize

        public bool ShouldSerializeIndSemCST() => IndSemCST > 0;
        public bool ShouldSerializeICMS00() => IndSemCST == 0 && ICMS00 != null;
        public bool ShouldSerializeICMS20() => IndSemCST == 0 && ICMS20 != null;
        public bool ShouldSerializeICMS40() => IndSemCST == 0 && ICMS40 != null;
        public bool ShouldSerializeICMS51() => IndSemCST == 0 && ICMS51 != null;
        public bool ShouldSerializeICMS90() => IndSemCST == 0 && ICMS90 != null;
        public bool ShouldSerializeICMSSN() => IndSemCST == 0 && ICMSSN != null;
        public bool ShouldSerializeICMSUFDest() => IndSemCST == 0 && ICMSUFDest != null;
        public bool ShouldSerializePIS() => PIS != null;
        public bool ShouldSerializeCOFINS() => COFINS != null;
        public bool ShouldSerializeFUST() => FUST != null;
        public bool ShouldSerializeFUNTTEL() => FUNTTEL != null;
        public bool ShouldSerializeRetTrib() => RetTrib != null;

        #endregion
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.Business.DFe.Xml.NFCom.ICMS00")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class ICMS00
    {
        [XmlElement("CST")]
        public string CST { get; set; }

        #region VBC
        [XmlIgnore]
        public double VBC { get; set; }

        [XmlElement("vBC")]
        public string VBCField
        {
            get => VBC.ToString("F2", CultureInfo.InvariantCulture);
            set => VBC = Converter.ToDouble(value);
        }
        #endregion

        #region PICMS
        [XmlIgnore]
        public double PICMS { get; set; }

        [XmlElement("pICMS")]
        public string PICMSField
        {
            get => PICMS.ToString("F2", CultureInfo.InvariantCulture);
            set => PICMS = Converter.ToDouble(value);
        }
        #endregion

        #region VICMS
        [XmlIgnore]
        public double VICMS { get; set; }

        [XmlElement("vICMS")]
        public string VICMSField
        {
            get => VICMS.ToString("F2", CultureInfo.InvariantCulture);
            set => VICMS = Converter.ToDouble(value);
        }
        #endregion

        #region PFCP
        [XmlIgnore]
        public double PFCP { get; set; }

        [XmlElement("pFCP")]
        public string PFCPField
        {
            get => PFCP.ToString("F2", CultureInfo.InvariantCulture);
            set => PFCP = Converter.ToDouble(value);
        }
        #endregion

        #region VFCP
        [XmlIgnore]
        public double VFCP { get; set; }

        [XmlElement("vFCP")]
        public string VFCPField
        {
            get => VFCP.ToString("F2", CultureInfo.InvariantCulture);
            set => VFCP = Converter.ToDouble(value);
        }
        #endregion

        #region ShouldSerialize

        public bool ShouldSerializePFCP() => Convert.ToDouble(VFCP) >= 0;
        public bool ShouldSerializeVFCP() => Convert.ToDouble(PFCP) >= 0;

        #endregion
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.Business.DFe.Xml.NFCom.ICMS20")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class ICMS20
    {
        [XmlElement("CST")]
        public string CST { get; set; }

        #region PRedBC
        [XmlIgnore]
        public double PRedBC { get; set; }

        [XmlElement("pRedBC")]
        public string PRedBCField
        {
            get => PRedBC.ToString("F2", CultureInfo.InvariantCulture);
            set => PRedBC = Converter.ToDouble(value);
        }
        #endregion

        #region VBC
        [XmlIgnore]
        public double VBC { get; set; }

        [XmlElement("vBC")]
        public string VBCField
        {
            get => VBC.ToString("F2", CultureInfo.InvariantCulture);
            set => VBC = Converter.ToDouble(value);
        }
        #endregion

        #region PICMS
        [XmlIgnore]
        public double PICMS { get; set; }

        [XmlElement("pICMS")]
        public string PICMSField
        {
            get => PICMS.ToString("F2", CultureInfo.InvariantCulture);
            set => PICMS = Converter.ToDouble(value);
        }
        #endregion

        #region VICMS
        [XmlIgnore]
        public double VICMS { get; set; }

        [XmlElement("vICMS")]
        public string VICMSField
        {
            get => VICMS.ToString("F2", CultureInfo.InvariantCulture);
            set => VICMS = Converter.ToDouble(value);
        }
        #endregion

        #region VICMSDeson
        [XmlIgnore]
        public double VICMSDeson { get; set; }

        [XmlElement("vICMSDeson")]
        public string VICMSDesonField
        {
            get => VICMSDeson.ToString("F2", CultureInfo.InvariantCulture);
            set => VICMSDeson = Converter.ToDouble(value);
        }
        #endregion

        [XmlElement("cBenef")]
        public string CBenef { get; set; }

        #region PFCP
        [XmlIgnore]
        public double PFCP { get; set; }

        [XmlElement("pFCP")]
        public string PFCPField
        {
            get => PFCP.ToString("F2", CultureInfo.InvariantCulture);
            set => PFCP = Converter.ToDouble(value);
        }
        #endregion

        #region VFCP
        [XmlIgnore]
        public double VFCP { get; set; }

        [XmlElement("vFCP")]
        public string VFCPField
        {
            get => VFCP.ToString("F2", CultureInfo.InvariantCulture);
            set => VFCP = Converter.ToDouble(value);
        }
        #endregion

        #region ShouldSerialize

        public bool ShouldSerializeVICMSDeson() => !string.IsNullOrWhiteSpace(CBenef);
        public bool ShouldSerializeCBenef() => Convert.ToDouble(VICMSDeson) >= 0;
        public bool ShouldSerializePFCP() => Convert.ToDouble(VFCP) >= 0;
        public bool ShouldSerializeVFCP() => Convert.ToDouble(PFCP) >= 0;

        #endregion
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.Business.DFe.Xml.NFCom.ICMS40")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class ICMS40
    {
        [XmlElement("CST")]
        public string CST { get; set; }

        #region VICMSDeson
        [XmlIgnore]
        public double VICMSDeson { get; set; }

        [XmlElement("vICMSDeson")]
        public string VICMSDesonField
        {
            get => VICMSDeson.ToString("F2", CultureInfo.InvariantCulture);
            set => VICMSDeson = Converter.ToDouble(value);
        }
        #endregion

        [XmlElement("cBenef")]
        public string CBenef { get; set; }

        #region ShouldSerialize

        public bool ShouldSerializeVICMSDeson() => !string.IsNullOrWhiteSpace(CBenef);
        public bool ShouldSerializeCBenef() => Convert.ToDouble(VICMSDesonField) >= 0;

        #endregion
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.Business.DFe.Xml.NFCom.ICMS51")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class ICMS51
    {
        [XmlElement("CST")]
        public string CST { get; set; }

        #region VICMSDeson
        [XmlIgnore]
        public double VICMSDeson { get; set; }

        [XmlElement("vICMSDeson")]
        public string VICMSDesonField
        {
            get => VICMSDeson.ToString("F2", CultureInfo.InvariantCulture);
            set => VICMSDeson = Converter.ToDouble(value);
        }
        #endregion

        [XmlElement("cBenef")]
        public string CBenef { get; set; }
 
        #region ShouldSerialize

        public bool ShouldSerializeVICMSDesonField() => !string.IsNullOrWhiteSpace(CBenef);
        public bool ShouldSerializeCBenef() => Convert.ToDouble(VICMSDesonField) >= 0;

        #endregion
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.Business.DFe.Xml.NFCom.ICMS90")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class ICMS90
    {
        [XmlElement("CST")]
        public string CST { get; set; }

        #region VBC
        [XmlIgnore]
        public double VBC { get; set; }

        [XmlElement("vBC")]
        public string VBCField
        {
            get => VBC.ToString("F2", CultureInfo.InvariantCulture);
            set => VBC = Converter.ToDouble(value);
        }
        #endregion

        #region PICMS
        [XmlIgnore]
        public double PICMS { get; set; }

        [XmlElement("pICMS")]
        public string PICMSField
        {
            get => PICMS.ToString("F2", CultureInfo.InvariantCulture);
            set => PICMS = Converter.ToDouble(value);
        }
        #endregion

        #region VICMS
        [XmlIgnore]
        public double VICMS { get; set; }

        [XmlElement("vICMS")]
        public string VICMSField
        {
            get => VICMS.ToString("F2", CultureInfo.InvariantCulture);
            set => VICMS = Converter.ToDouble(value);
        }
        #endregion

        #region VICMSDeson
        [XmlIgnore]
        public double VICMSDeson { get; set; }

        [XmlElement("vICMSDeson")]
        public string VICMSDesonField
        {
            get => VICMSDeson.ToString("F2", CultureInfo.InvariantCulture);
            set => VICMSDeson = Converter.ToDouble(value);
        }
        #endregion

        [XmlElement("cBenef")]
        public string CBenef { get; set; }

        #region PFCP
        [XmlIgnore]
        public double PFCP { get; set; }

        [XmlElement("pFCP")]
        public string PFCPField
        {
            get => PFCP.ToString("F2", CultureInfo.InvariantCulture);
            set => PFCP = Converter.ToDouble(value);
        }
        #endregion

        #region VFCP
        [XmlIgnore]
        public double VFCP { get; set; }

        [XmlElement("vFCP")]
        public string VFCPField
        {
            get => VFCP.ToString("F2", CultureInfo.InvariantCulture);
            set => VFCP = Converter.ToDouble(value);
        }
        #endregion

        #region ShouldSerialize

        public bool ShouldSerializePICMS() => Convert.ToDouble(VBC) >= 0 && Convert.ToDouble(VICMS) >= 0;
        public bool ShouldSerializeVBC() => Convert.ToDouble(PICMS) >= 0 && Convert.ToDouble(VICMS) > 0;
        public bool ShouldSerializeVICMS() => Convert.ToDouble(PICMS) > 0 && Convert.ToDouble(VBC) >= 0;
        public bool ShouldSerializeVICMSDeson() => !string.IsNullOrWhiteSpace(CBenef);
        public bool ShouldSerializeCBenef() => Convert.ToDouble(VICMSDeson) >= 0;
        public bool ShouldSerializePFCP() => Convert.ToDouble(VFCP) >= 0;
        public bool ShouldSerializeVFCP() => Convert.ToDouble(PFCP) >= 0;
        
        #endregion
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.Business.DFe.Xml.NFCom.ICMSSN")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class ICMSSN
    {
        [XmlElement("CST")]
        public string CST { get; set; }

        [XmlElement("indSN")]
        public SimNao IndSN { get; set; }
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.Business.DFe.Xml.NFCom.ICMSUFDest")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class ICMSUFDest
    {
        [XmlAttribute("cUFDest")]
        public UFBrasil CUFDest { get; set; }

        #region VBCUFDest
        [XmlIgnore]
        public double VBCUFDest { get; set; }

        [XmlElement("vBCUFDest")]
        public string VBCUFDestField
        {
            get => VBCUFDest.ToString("F2", CultureInfo.InvariantCulture);
            set => Converter.ToDouble(value);
        }
        #endregion

        #region PFCPUFDest
        [XmlIgnore]
        public double PFCPUFDest { get; set; }

        [XmlElement("pFCPUFDest")]
        public string PFCPUFDestField
        {
            get => PFCPUFDest.ToString("F2", CultureInfo.InvariantCulture);
            set => Converter.ToDouble(value);
        }
        #endregion

        #region PICMSUFDest
        [XmlIgnore]
        public double PICMSUFDest { get; set; }

        [XmlElement("pICMSUFDest")]
        public string PICMSUFDestField
        {
            get => PICMSUFDest.ToString("F2", CultureInfo.InvariantCulture);
            set => Converter.ToDouble(value);
        }
        #endregion

        #region VFCPUFDest
        [XmlIgnore]
        public double VFCPUFDest { get; set; }

        [XmlElement("vFCPUFDest")]
        public string VFCPUFDestField
        {
            get => VFCPUFDest.ToString("F2", CultureInfo.InvariantCulture);
            set => Converter.ToDouble(value);
        }
        #endregion

        #region VICMSUFDest
        [XmlIgnore]
        public double VICMSUFDest { get; set; }

        [XmlElement("vICMSUFDest")]
        public string VICMSUFDestField
        {
            get => VICMSUFDest.ToString("F2", CultureInfo.InvariantCulture);
            set => Converter.ToDouble(value);
        }
        #endregion

        #region VICMSUFEmi
        [XmlIgnore]
        public double VICMSUFEmi { get; set; }

        [XmlElement("vICMSUFEmi")]
        public string VICMSUFEmiField
        {
            get => VICMSUFEmi.ToString("F2", CultureInfo.InvariantCulture);
            set => Converter.ToDouble(value);
        }
        #endregion

        [XmlElement("cBenefUFDest")]
        public string CBenefUFDest { get; set; }

        #region ShouldSerialize

        public bool ShouldSerializeCBenefUFDest() => !string.IsNullOrWhiteSpace(CBenefUFDest);

        #endregion
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.Business.DFe.Xml.NFCom.PIS")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class PIS
    {
        [XmlElement("CST")]
        public string CST { get; set; }

        #region VBC
        [XmlIgnore]
        public double VBC { get; set; }

        [XmlElement("vBC")]
        public string VBCField
        {
            get => VBC.ToString("F2", CultureInfo.InvariantCulture);
            set => VBC = Converter.ToDouble(value);
        }
        #endregion

        #region PPIS
        [XmlIgnore]
        public double PPIS { get; set; }

        [XmlElement("pPIS")]
        public string PPISField
        {
            get => PPIS.ToString("F4", CultureInfo.InvariantCulture);
            set => PPIS = Converter.ToDouble(value);
        }
        #endregion

        #region VPIS
        [XmlIgnore]
        public double VPIS { get; set; }

        [XmlElement("vPIS")]
        public string VPISField
        {
            get => VPIS.ToString("F2", CultureInfo.InvariantCulture);
            set => VPIS = Converter.ToDouble(value);
        }
        #endregion
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.Business.DFe.Xml.NFCom.COFINS")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class COFINS
    {
        [XmlElement("CST")]
        public string CST { get; set; }

        #region VBC
        [XmlIgnore]
        public double VBC { get; set; }

        [XmlElement("vBC")]
        public string VBCField
        {
            get => VBC.ToString("F2", CultureInfo.InvariantCulture);
            set => VBC = Converter.ToDouble(value);
        }
        #endregion

        #region PCOFINS
        [XmlIgnore]
        public double PCOFINS { get; set; }

        [XmlElement("pCOFINS")]
        public string PCOFINSField
        {
            get => PCOFINS.ToString("F4", CultureInfo.InvariantCulture);
            set => PCOFINS = Converter.ToDouble(value);
        }
        #endregion

        #region VCOFINS
        [XmlIgnore]
        public double VCOFINS { get; set; }

        [XmlElement("vCOFINS")]
        public string VCOFINSField
        {
            get => VCOFINS.ToString("F2", CultureInfo.InvariantCulture);
            set => VCOFINS = Converter.ToDouble(value);
        }
        #endregion
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.Business.DFe.Xml.NFCom.FUST")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class FUST
    {
        #region VBC
        [XmlIgnore]
        public double VBC { get; set; }

        [XmlElement("vBC")]
        public string VBCField
        {
            get => VBC.ToString("F2", CultureInfo.InvariantCulture);
            set => VBC = Converter.ToDouble(value);
        }
        #endregion

        #region PFUST
        [XmlIgnore]
        public double PFUST { get; set; }

        [XmlElement("pFUST")]
        public string PFUSTField
        {
            get => PFUST.ToString("F4", CultureInfo.InvariantCulture);
            set => PFUST = Converter.ToDouble(value);
        }
        #endregion

        #region VFUST
        [XmlIgnore]
        public double VFUST { get; set; }

        [XmlElement("vFUST")]
        public string VFUSTField
        {
            get => VFUST.ToString("F2", CultureInfo.InvariantCulture);
            set => VFUST = Converter.ToDouble(value);
        }
        #endregion
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.Business.DFe.Xml.NFCom.FUNTTEL")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class FUNTTEL
    {
        #region VBC
        [XmlIgnore]
        public double VBC { get; set; }

        [XmlElement("vBC")]
        public string VBCField
        {
            get => VBC.ToString("F2", CultureInfo.InvariantCulture);
            set => VBC = Converter.ToDouble(value);
        }
        #endregion

        #region PFUNTTEL
        [XmlIgnore]
        public double PFUNTTEL { get; set; }

        [XmlElement("pFUNTTEL")]
        public string PFUNTTELField
        {
            get => PFUNTTEL.ToString("F4", CultureInfo.InvariantCulture);
            set => PFUNTTEL = Converter.ToDouble(value);
        }
        #endregion

        #region VFUNTTEL
        [XmlIgnore]
        public double VFUNTTEL { get; set; }

        [XmlElement("vFUNTTEL")]
        public string VFUNTTELField
        {
            get => VFUNTTEL.ToString("F2", CultureInfo.InvariantCulture);
            set => VFUNTTEL = Converter.ToDouble(value);
        }
        #endregion
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.Business.DFe.Xml.NFCom.RetTrib")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class RetTrib
    {
        #region VRetPIS
        [XmlIgnore]
        public double VRetPIS { get; set; }
        [XmlElement("vRetPIS")]
        public string VRetPISField
        {
            get => VRetPIS.ToString("F2", CultureInfo.InvariantCulture);
            set => VRetPIS = Converter.ToDouble(value);
        }
        #endregion

        #region VRetCOFINS
        [XmlIgnore]
        public double VRetCOFINS { get; set; }
        [XmlElement("vRetCofins")]
        public string VRetCOFINSField
        {
            get => VRetCOFINS.ToString("F2", CultureInfo.InvariantCulture);
            set => VRetCOFINS = Converter.ToDouble(value);
        }
        #endregion

        #region VRetCSLL
        [XmlIgnore]
        public double VRetCSLL { get; set; }
        [XmlElement("vRetCSLL")]
        public string VRetCSLLField
        {
            get => VRetCSLL.ToString("F2", CultureInfo.InvariantCulture);
            set => VRetCSLL = Converter.ToDouble(value);
        }
        #endregion

        #region VBCIRRF
        [XmlIgnore]
        public double VBCIRRF { get; set; }
        [XmlElement("vBCIRRF")]
        public string VBCIRRFField
        {
            get => VBCIRRF.ToString("F2", CultureInfo.InvariantCulture);
            set => VBCIRRF = Converter.ToDouble(value);
        }
        #endregion

        #region VIRRF
        [XmlIgnore]
        public double VIRRF { get; set; }
        [XmlElement("vIRRF")]
        public string VIRRFField
        {
            get => VIRRF.ToString("F2", CultureInfo.InvariantCulture);
            set => VIRRF = Converter.ToDouble(value);
        }
        #endregion
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.Business.DFe.Xml.NFCom.GProcRef")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class GProcRef
    {
        #region VItem
        [XmlIgnore]
        public double VItem { get; set; }

        [XmlElement("vItem")]
        public string VItemField
        {
            get => VItem.ToString("F2", CultureInfo.InvariantCulture);
            set => VItem = Converter.ToDouble(value);
        }
        #endregion

        #region QFaturada
        [XmlIgnore]
        public decimal QFaturada { get; set; }

        [XmlElement("qFaturada")]
        public string QFaturadaField
        {
            get => QFaturada.ToString("F4", CultureInfo.InstalledUICulture);
            set => QFaturada = (decimal)Convert.ToDouble(value);
        }
        #endregion

        #region VProd
        [XmlIgnore]
        public double VProd { get; set; }

        [XmlElement("vProd")]
        public string VProdField
        {
            get => VProd.ToString("F2", CultureInfo.InvariantCulture);
            set => VProd = Converter.ToDouble(value);
        }
        #endregion

        #region VDesc
        [XmlIgnore]
        public double VDesc { get; set; }

        [XmlElement("vDesc")]
        public string VDescField
        {
            get => VDesc.ToString("F2", CultureInfo.InvariantCulture);
            set => VDesc = Converter.ToDouble(value);
        }
        #endregion

        #region VOutro
        [XmlIgnore]
        public double VOutro { get; set; }

        [XmlElement("vOutro")]
        public string VOutroField
        {
            get => VOutro.ToString("F2", CultureInfo.InvariantCulture);
            set => VOutro = Converter.ToDouble(value);
        }
        #endregion

        [XmlElement("indDevolucao")]
        public SimNao IndDevolucao { get; set; }

        #region VBC
        [XmlIgnore]
        public double VBC { get; set; }

        [XmlElement("vBC")]
        public string VBCField
        {
            get => VBC.ToString("F2", CultureInfo.InvariantCulture);
            set => VBC = Converter.ToDouble(value);
        }
        #endregion

        #region PICMS
        [XmlIgnore]
        public double PICMS { get; set; }

        [XmlElement("pICMS")]
        public string PICMSField
        {
            get => PICMS.ToString("F2", CultureInfo.InvariantCulture);
            set => PICMS = Converter.ToDouble(value);
        }
        #endregion

        #region VICMS
        [XmlIgnore]
        public double VICMS { get; set; }

        [XmlElement("vICMS")]
        public string VICMSField
        {
            get => VICMS.ToString("F2", CultureInfo.InvariantCulture);
            set => VICMS = Converter.ToDouble(value);
        }
        #endregion

        #region VPIS
        [XmlIgnore]
        public double VPIS { get; set; }

        [XmlElement("vPIS")]
        public string VPISField
        {
            get => VPIS.ToString("F2", CultureInfo.InvariantCulture);
            set => VPIS = Converter.ToDouble(value);
        }
        #endregion

        #region VCOFINS
        [XmlIgnore]
        public double VCOFINS { get; set; }

        [XmlElement("vCOFINS")]
        public string VCOFINSField
        {
            get => VCOFINS.ToString("F2", CultureInfo.InvariantCulture);
            set => VCOFINS = Converter.ToDouble(value);
        }
        #endregion

        #region VFCP
        [XmlIgnore]
        public double VFCP { get; set; }

        [XmlElement("vFCP")]
        public string VFCPField
        {
            get => VFCP.ToString("F2", CultureInfo.InvariantCulture);
            set => VFCP = Converter.ToDouble(value);
        }
        #endregion
        
        [XmlElement("gProc")]
        public List<GProc> GProc { get; set; }

        #region ShouldSerialize

        public bool ShouldSerializeVDescField() => Convert.ToDouble(VDesc) >= 0;
        public bool ShouldSerializeVOutroField() => Convert.ToDouble(VOutro) >= 0;
        public bool ShouldSerializeIndDevolucao() => (int)IndDevolucao == 1;
        public bool ShouldSerializeVBC() => Convert.ToDouble(VBC) >= 0;
        public bool ShouldSerializePICMS() => Convert.ToDouble(PICMS) >= 0;
        public bool ShouldSerializeVICMS() => Convert.ToDouble(VICMS) >= 0;
        public bool ShouldSerializeVPISField() => Convert.ToDouble(VPIS) >= 0;
        public bool ShouldSerializeVCOFINSField() => Convert.ToDouble(VCOFINS) >= 0;
        public bool ShouldSerializeVFCPField() => Convert.ToDouble(VFCP) >= 0;

        #endregion    
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.Business.DFe.Xml.NFCom.GProc")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class GProc
    {
        private string NProcessoField;

        [XmlElement("tpProc")]
        public IndicadorOrigemProcesso TpProc { get; set; }

        [XmlElement("nProcesso")]
        public string NProcesso
        {
            get => NProcessoField;
            set => NProcessoField = value == null ? value : XMLUtility.UnescapeReservedCharacters(value).Truncate(60).Trim();
        }
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.Business.DFe.Xml.NFCom.GRessarc")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class GRessarc
    {
        private string XObsField;
        private string NProcessoField;
        private string NProtReclamaField;

        [XmlElement("tpRessarc")]
        public TipoRessarcimento TpRessarc { get; set; }

        #region DRef
        [XmlIgnore]
#if INTEROP
        public DateTime DRef { get; set; }
#else
        public DateTimeOffset DRef { get; set; }
#endif

        [XmlElement("dRef")]
        public string DRefField
        {
            get => DRef.ToString("yyyy-MM-dd");
#if INTEROP
            set => DRef = DateTime.Parse(value);
#else
            set => DRef = DateTimeOffset.Parse(value);
#endif
        }
        #endregion

        [XmlElement("nProcesso")]
        public string NProcesso
        {
            get => NProcessoField;
            set => NProcessoField = value == null ? value : XMLUtility.UnescapeReservedCharacters(value).Truncate(60).Trim();
        }

        [XmlElement("nProtReclama")]
        public string NProtReclama
        {
            get => NProtReclamaField;
            set => NProtReclamaField = value == null ? value : XMLUtility.UnescapeReservedCharacters(value).Truncate(60).Trim();
        }

        [XmlElement("xObs")]
        public string XObs
        {
            get => string.IsNullOrWhiteSpace(XObsField) ? null : XObsField;
            set => XObsField = value == null ? value : XMLUtility.UnescapeReservedCharacters(value).Truncate(500).Trim();
        }

        #region ShouldSerialize

        public bool ShouldSerializeNProcesso() => !string.IsNullOrWhiteSpace(NProcesso);
        public bool ShouldSerializeNProtReclama() => !string.IsNullOrWhiteSpace(NProtReclama);
        public bool ShouldSerializeXObs() => !string.IsNullOrWhiteSpace(XObs);

        #endregion
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.Business.DFe.Xml.NFCom.Total")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class Total
    {
        #region VProd
        [XmlIgnore]
        public double VProd { get; set; }

        [XmlElement("vProd")]
        public string VProdField
        {
            get => VProd.ToString("F2", CultureInfo.InvariantCulture);
            set => VProd = Converter.ToDouble(value);
        }
        #endregion

        [XmlElement("ICMSTot")]
        public ICMSTot ICMSTot { get; set; }

        #region VCOFINS
        [XmlIgnore]
        public double VCOFINS { get; set; }
       
        [XmlElement("vCOFINS")]
        public string VCOFINSField
        {
            get => VCOFINS.ToString("F2", CultureInfo.InvariantCulture);
            set => VCOFINS = Converter.ToDouble(value);
        }
        #endregion

        #region PIS
        [XmlIgnore]
        public double VPIS { get; set; }
        
        [XmlElement("vPIS")]
        public string VPISField
        {
            get => VPIS.ToString("F2", CultureInfo.InvariantCulture);
            set => VPIS = Converter.ToDouble(value);
        }
        #endregion

        #region VFUNTTEL
        [XmlIgnore]
        public double VFUNTTEL { get; set; }
        
        [XmlElement("vFUNTTEL")]
        public string VFUNTTELField
        {
            get => VFUNTTEL.ToString("F2", CultureInfo.InvariantCulture);
            set => VFUNTTEL = Converter.ToDouble(value);
        }
        #endregion

        #region VFUST
        [XmlIgnore]
        public double VFUST { get; set; }
        
        [XmlElement("vFUST")]
        public string VFUSTField
        {
            get => VFUST.ToString("F2", CultureInfo.InvariantCulture);
            set => VFUST = Converter.ToDouble(value);
        }
        #endregion

        [XmlElement("vRetTribTot")]
        public VRetTribTot VRetTribTot { get; set; }

        #region VDec
        [XmlIgnore]
        public double VDesc { get; set; }
        
        [XmlElement("vDesc")]
        public string VDescField
        {
            get => VDesc.ToString("F2", CultureInfo.InvariantCulture);
            set => VDesc = Converter.ToDouble(value);
        }
        #endregion

        #region VOutro
        [XmlIgnore]
        public double VOutro { get; set; }
       
        [XmlElement("vOutro")]
        public string VOutroField
        {
            get => VOutro.ToString("F2", CultureInfo.InvariantCulture);
            set => VOutro = Converter.ToDouble(value);
        }
        #endregion

        #region VNF
        [XmlIgnore]
        public double VNF { get; set; }
        
        [XmlElement("vNF")]
        public string VNFField
        {
            get => VNF.ToString("F2", CultureInfo.InvariantCulture);
            set => VNF = Converter.ToDouble(value);
        }
        #endregion
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.Business.DFe.Xml.NFCom.ICMSTot")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class ICMSTot
    {
        #region VBC
        [XmlIgnore]
        public double VBC { get; set; }
       
        [XmlElement("vBC")]
        public string VBCField
        {
            get => VBC.ToString("F2", CultureInfo.InvariantCulture);
            set => VBC = Converter.ToDouble(value);
        }
        #endregion

        #region VICMS
        [XmlIgnore]
        public double VICMS { get; set; }
       
        [XmlElement("vICMS")]
        public string VICMSField
        {
            get => VICMS.ToString("F2", CultureInfo.InvariantCulture);
            set => VICMS = Converter.ToDouble(value);
        }
        #endregion

        #region VICMSDeson
        [XmlIgnore]
        public double VICMSDeson { get; set; }
        
        [XmlElement("vICMSDeson")]
        public string VICMSDesonField
        {
            get => VICMSDeson.ToString("F2", CultureInfo.InvariantCulture);
            set => VICMSDeson = Converter.ToDouble(value);
        }
        #endregion

        #region VFCP
        [XmlIgnore]
        public double VFCP { get; set; }
       
        [XmlElement("vFCP")]
        public string VFCPField
        {
            get => VFCP.ToString("F2", CultureInfo.InvariantCulture);
            set => VFCP = Converter.ToDouble(value);
        }
        #endregion
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.Business.DFe.Xml.NFCom.VRetTribTot")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class VRetTribTot
    {
        #region VRetPIS
        [XmlIgnore]
        public double VRetPIS { get; set; }

        [XmlElement("vRetPIS")]
        public string VRetPISField
        {
            get => VRetPIS.ToString("F2", CultureInfo.InvariantCulture);
            set => VRetPIS = Converter.ToDouble(value);
        }
        #endregion

        #region VRetCOFINS
        [XmlIgnore]
        public double VRetCOFINS { get; set; }
        [XmlElement("vRetCofins")]
        public string VRetCOFINSField
        {
            get => VRetCOFINS.ToString("F2", CultureInfo.InvariantCulture);
            set => VRetCOFINS = Converter.ToDouble(value);
        }
        #endregion

        #region VRetCSLL
        [XmlIgnore]
        public double VRetCSLL { get; set; }
        [XmlElement("vRetCSLL")]
        public string VRetCSLLField
        {
            get => VRetCSLL.ToString("F2", CultureInfo.InvariantCulture);
            set => VRetCSLL = Converter.ToDouble(value);
        }
        #endregion

        #region VIRRF
        [XmlIgnore]
        public double VIRRF { get; set; }
        [XmlElement("vIRRF")]
        public string VIRRFField
        {
            get => VIRRF.ToString("F2", CultureInfo.InvariantCulture);
            set => VIRRF = Converter.ToDouble(value);
        }
        #endregion
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.Business.DFe.Xml.NFCom.GFidelidade")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class GFidelidade
    {
        [XmlElement("qtdSaldoPts")]
        public string QtdSaldoPts { get; set; }

        #region dRefSaldoPts
        [XmlIgnore]
#if INTEROP
        public DateTime DRefSaldoPts { get; set; }
#else
        public DateTimeOffset DRefSaldoPts { get; set; }
#endif

        [XmlElement("dRefSaldoPts")]
        public string DRefSaldoPtsField
        {
            get => DRefSaldoPts.ToString("yyyy-MM-dd");
#if INTEROP
            set => DRefSaldoPts = DateTime.Parse(value);
#else
            set => DRefSaldoPts = DateTimeOffset.Parse(value);
#endif
        }
        #endregion

        [XmlElement("qtdPtsResg")]
        public string QtdPtsResg { get; set; }

        #region dRefResgPts
        [XmlIgnore]
#if INTEROP
        public DateTime DRefResgPts { get; set; }
#else
        public DateTimeOffset DRefResgPts { get; set; }
#endif

        [XmlElement("dRefResgPts")]
        public string DRefResgPtsField
        {
            get => DRefResgPts.ToString("yyyy-MM-dd");
#if INTEROP
            set => DRefResgPts = DateTime.Parse(value);
#else
            set => DRefResgPts = DateTimeOffset.Parse(value);
#endif
        }
        #endregion
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.Business.DFe.Xml.NFCom.GFat")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class GFat
    {
        [XmlElement("CompetFat")]
        public string CompetFat { get; set; }

        #region DVencFat
        [XmlIgnore]
#if INTEROP
        public DateTime DVencFat { get; set; }
#else
        public DateTimeOffset DVencFat { get; set; }
#endif

        [XmlElement("dVencFat")]
        public string DVencFatField
        {
            get => DVencFat.ToString("yyyy-MM-dd");
#if INTEROP
            set => DVencFat = DateTime.Parse(value);
#else
            set => DVencFat = DateTimeOffset.Parse(value);
#endif
        }
        #endregion

        #region DPerUsoIni
        [XmlIgnore]
#if INTEROP
        public DateTime DPerUsoIni { get; set; }
#else
        public DateTimeOffset DPerUsoIni { get; set; }
#endif

        [XmlElement("dPerUsoIni")]
        public string DPerUsoIniField
        {
            get => DPerUsoIni.ToString("yyyy-MM-dd");
#if INTEROP
            set => DPerUsoIni = DateTime.Parse(value);
#else
            set => DPerUsoIni = DateTimeOffset.Parse(value);
#endif
        }
        #endregion

        #region DPerUsoFim
        [XmlIgnore]
#if INTEROP
        public DateTime DPerUsoFim { get; set; }
#else
        public DateTimeOffset DPerUsoFim { get; set; }
#endif

        [XmlElement("dPerUsoFim")]
        public string DPerUsoFimField
        {
            get => DPerUsoFim.ToString("yyyy-MM-dd");
#if INTEROP
            set => DPerUsoFim = DateTime.Parse(value);
#else
            set => DPerUsoFim = DateTimeOffset.Parse(value);
#endif
        }
        #endregion

        [XmlElement("codBarras")]
        public string CodBarras { get; set; }

        [XmlElement("codDebAuto")]
        public string CodDebAuto { get; set; }

        [XmlElement("codBanco")]
        public string CodBanco { get; set; }

        [XmlElement("codAgencia")]
        public string CodAgencia { get; set; }

        [XmlElement("enderCorresp")]
        public EnderCorresp EnderCorresp { get; set; }

        [XmlElement("gPIX")]
        public GPIX GPIX { get; set; }

        #region ShouldSerialize

        public bool ShouldSerializeDPerUsoIni() => DPerUsoIni != null && DPerUsoFim != null;
        public bool ShouldSerializeDPerUsoFim() => DPerUsoIni != null && DPerUsoFim != null;
        public bool ShouldSerializeCodDebAuto() => !string.IsNullOrWhiteSpace(CodDebAuto);
        public bool ShouldSerializeCodBanco() => !string.IsNullOrWhiteSpace(CodBanco) && !string.IsNullOrWhiteSpace(CodAgencia);
        public bool ShouldSerializeCodAgencia() => !string.IsNullOrWhiteSpace(CodBanco) && !string.IsNullOrWhiteSpace(CodAgencia);
        public bool ShouldSerializeEnderCorresp() => EnderCorresp != null;
        public bool ShouldSerializeGPIX() => GPIX != null;

        #endregion
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.NFCom.Xml.NFCom.EnderCorresp")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class EnderCorresp : TEndereco { }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.Business.DFe.Xml.NFCom.GPIX")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class GPIX
    {
        private string UrlQRCodePIXField;

        [XmlElement("urlQRCodePIX")]
        public string UrlQRCodePIX
        {
            get => UrlQRCodePIXField;
            set => UrlQRCodePIXField = value == null ? value : XMLUtility.UnescapeReservedCharacters(value).Truncate(2000).Trim();
        }
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.Business.DFe.Xml.NFCom.GFatCentral")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class GFatCentral
    {
        [XmlElement("CNPJ")]
        public string CNPJ { get; set; }

        #region CUF
        [XmlIgnore]
        public UFBrasil CUF { get; set; }

        [XmlElement("cUF")]
        public int CUFField
        {
            get => (int)CUF;
            set => CUF = (UFBrasil)Enum.Parse(typeof(UFBrasil), value.ToString());
        }
        #endregion
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.Business.DFe.Xml.NFCom.AutXML")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class AutXML
    {
        [XmlElement("CNPJ")]
        public string CNPJ { get; set; }

        [XmlElement("CPF")]
        public string CPF { get; set; }

        #region ShouldSerialize

        public bool ShouldSerializeCNPJ() => !string.IsNullOrWhiteSpace(CNPJ);

        public bool ShouldSerializeCPF() => !string.IsNullOrWhiteSpace(CPF);

        #endregion
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.Business.DFe.Xml.NFCom.InfAdic")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class InfAdic
    {
        private string InfAdFiscoField;
        private string InfCplField;

        [XmlElement("infAdFisco")]
        public string InfAdFisco
        {
            get => InfAdFiscoField;
            set => InfAdFiscoField = value == null ? value : XMLUtility.UnescapeReservedCharacters(value).Truncate(2000).Trim();
        }

        [XmlElement("infCpl")]
        public string InfCpl
        {
            get => InfCplField;
            set => InfCplField = value == null ? value : XMLUtility.UnescapeReservedCharacters(value).Truncate(5000).Trim();
        }

        #region ShouldSerialize

        public bool ShouldSerializeInfAdFisco() => !string.IsNullOrWhiteSpace(InfAdFisco);

        public bool ShouldSerializeInfCpl() => !string.IsNullOrWhiteSpace(InfCpl);

        #endregion
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.Business.DFe.Xml.NFCom.GRespTec")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class GRespTec
    {
        private string XContatoField;

        [XmlElement("CNPJ")]
        public string CNPJ { get; set; }

        [XmlElement("xContato")]
        public string XContato
        {
            get => XContatoField;
            set => XContatoField = value == null ? value : XMLUtility.UnescapeReservedCharacters(value).Truncate(60).Trim();
        }

        [XmlElement("email")]
        public string Email { get; set; }

        [XmlElement("fone")]
        public string Fone { get; set; }

        [XmlElement("idCSRT")]
        public string IdCSRT { get; set; }

        [XmlElement("hashCSRT", DataType = "base64Binary")]
        public byte[] HashCSRT { get; set; }

        #region ShouldSerialize

        public bool ShouldSerializeIdCSRT() => !string.IsNullOrWhiteSpace(IdCSRT) && HashCSRT != null;
        public bool ShouldSerializeHashCSRT() => !string.IsNullOrWhiteSpace(IdCSRT) && HashCSRT != null;

        #endregion
    }

    #endregion InfNFCom

    #region InfNFComSupl

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.Business.DFe.Xml.NFCom.InfNFComSupl")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class InfNFComSupl
    {
        [XmlElement("qrCodNFCom")]
        public string QrCodNFCom { get; set; }
    }

    #endregion InfNFComSupl

    #region Tipos Abstratos

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.Business.DFe.Xml.NFCom.TEndereco")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public abstract class TEndereco
    {
        private string XLgrField;
        private string XCplField;
        private string NroField;
        private string XBairroField;
        private string XMunField;
        private string XPaisField;

        [XmlElement("xLgr")]
        public string XLgr
        {
            get => XLgrField;
            set => XLgrField = value == null ? value : XMLUtility.UnescapeReservedCharacters(value).Truncate(60).Trim();
        }

        [XmlElement("nro")]
        public string Nro
        {
            get => NroField;
            set => NroField = value == null ? value : XMLUtility.UnescapeReservedCharacters(value).Truncate(60).Trim();
        }

        [XmlElement("xCpl")]
        public string XCpl
        {
            get => XCplField;
            set => XCplField = value == null ? value : XMLUtility.UnescapeReservedCharacters(value).Truncate(60).Trim();
        }

        [XmlElement("xBairro")]
        public string XBairro
        {
            get => XBairroField;
            set => XBairroField = value == null ? value : XMLUtility.UnescapeReservedCharacters(value).Truncate(60).Trim();
        }

        [XmlElement("cMun")]
        public int CMun { get; set; }

        [XmlElement("xMun")]
        public string XMun
        {
            get => XMunField;
            set => XMunField = value == null ? value : XMLUtility.UnescapeReservedCharacters(value).Truncate(60).Trim();
        }

        [XmlElement("CEP")]
        public string CEP { get; set; }

        [XmlElement("UF")]
        public UFBrasil UF { get; set; }

        [XmlElement("cPais")]
        public int CPais { get; set; }

        [XmlElement("xPais")]
        public string XPais
        {
            get => XPaisField;
            set => XPaisField = value == null ? value : XMLUtility.UnescapeReservedCharacters(value).Truncate(60).Trim();
        }

        [XmlElement("fone")]
        public string Fone { get; set; }

        [XmlElement("email")]
        public string Email { get; set; }

        #region ShouldSerialize

        public bool ShouldSerializeXCpl() => !string.IsNullOrWhiteSpace(XCpl);
        public bool ShouldSerializeCPais() => CPais > 0;
        public bool ShouldSerializeXPais() => !string.IsNullOrWhiteSpace(XPais);
        public bool ShouldSerializeFone() => !string.IsNullOrWhiteSpace(Fone);
        public bool ShouldSerializeEmail() => !string.IsNullOrWhiteSpace(Email);

        #endregion
    }

    #endregion Tipos Abstratos

}
