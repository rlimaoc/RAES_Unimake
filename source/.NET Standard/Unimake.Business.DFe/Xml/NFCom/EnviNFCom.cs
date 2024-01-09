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
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Unimake.Business.DFe.Servicos.Enums;
using Unimake.Business.DFe.Utility;
using Unimake.Business.DFe.Xml;

namespace Unimake.Business.DFe.Xml.NFCom
{
#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Unimake.Business.DFe.Xml.NFCom.EnviNFCom")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlRoot("enviNFCom", Namespace = "http://www.portalfiscal.inf.br/nfcom", IsNullable = false)]
    public class EnviNFCom : XMLBase
    {
        [XmlAttribute(AttributeName = "versao", DataType = "token")]
        public string Versao { get; set; }

        [XmlElement("idLote")]
        public string IdLote { get; set; }

        [XmlElement("indSinc")]
        public SimNao IndSinc { get; set; }

        [XmlElement("NFCom")]
        public List<NFCom> NFCom { get; set; }

        public override XmlDocument GerarXML()
        {
            var xmlDoc = base.GerarXML();

            foreach (var nodeEnvNFCom in xmlDoc.GetElementsByTagName("enviNFCom"))
            {
                var elemEnvNFCom = (XmlElement)nodeEnvNFCom;

                foreach (var nodeNFCom in elemEnvNFCom.GetElementsByTagName("NFCom"))
                {
                    var elemNFCom = (XmlElement)nodeNFCom;

                    var attribute = GetType().GetCustomAttribute<XmlRootAttribute>();
                    elemNFCom.SetAttribute("xmlns", attribute.Namespace);
                }
            }

            return xmlDoc;
        }

        /// <summary>
        /// Desserializar o XML EnviNFCom no objeto EnviNFCom
        /// </summary>
        /// <param name="filename">Localização do arquivo XML EnviNFCom</param>
        /// <returns>Objeto do EnviNFCom</returns>
        public EnviNFCom LoadFromFile(string filename)
        {
            var doc = new XmlDocument();
            doc.LoadXml(System.IO.File.ReadAllText(filename, Encoding.UTF8));
            return XMLUtility.Deserializar<EnviNFCom>(doc);
        }

        /// <summary>
        /// Desserializar o XML EnviNFCom no objeto EnviNFCom
        /// </summary>
        /// <param name="xml">string do XML EnviNFCom</param>
        /// <returns>Objeto da EnviNFCom</returns>
        public EnviNFCom LoadFromXML(string xml) => XMLUtility.Deserializar<EnviNFCom>(xml);

#if INTEROP

        /// <summary>
        /// Adicionar novo elemento a lista
        /// </summary>
        /// <param name="nfcom">Elemento</param>
        public void AddNFCom(NFCom nfcom)
        {
            if (NFCom == null)
            {
                NFCom = new List<NFCom>();
            }

            NFCom.Add(nfcom);
        }

        /// <summary>
        /// Retorna o elemento da lista NFCom (Utilizado para linguagens diferentes do CSharp que não conseguem pegar o conteúdo da lista)
        /// </summary>
        /// <param name="index">Índice da lista a ser retornado (Começa com 0 (zero))</param>
        /// <returns>Conteúdo do index passado por parâmetro da NFCom</returns>
        public NFCom GetNFCom(int index)
        {
            if ((NFCom?.Count ?? 0) == 0)
            {
                return default;
            };

            return NFCom[index];
        }

        /// <summary>
        /// Retorna a quantidade de elementos existentes na lista NFCom
        /// </summary>
        public int GetNFComCount => (NFCom != null ? NFCom.Count : 0);

#endif
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Unimake.Business.DFe.Xml.NFCom.NFCom")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    [XmlRoot("NFCom", Namespace = "http://www.portalfiscal.inf.br/nfcom", IsNullable = false)]
    public class NFCom
    {
        [XmlElement("infNFCom")]
        public List<InfNFCom> InfNFCom { get; set; }

        [XmlElement("infNFComSupl")]
        public InfNFComSupl InfNFComSupl { get; set; }

        [XmlElement(ElementName = "Signature", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public Signature Signature { get; set; }

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
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Unimake.Business.DFe.Xml.NFCom.InfNFCom")]
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
    [ProgId("Unimake.Business.DFe.Xml.NFCom.Ide")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class Ide
    {
        private string CNFField;
        private string XJustField;

        [XmlIgnore]
        public UFBrasil CUF { get; set; }

        [XmlElement("cUF")]
        public int CUFField
        {
            get => (int)CUF;
            set => CUF = (UFBrasil)Enum.Parse(typeof(UFBrasil), value.ToString());
        }

        [XmlElement("tpAmb")]
        public TipoAmbiente TpAmb { get; set; }

        [XmlElement("mod")]
        public ModeloDFe Mod { get; set; }

        [XmlElement("serie")]
        public int Serie { get; set; }

        [XmlElement("nNF")]
        public int NNF { get; set; }

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

                    retorno = XMLUtility.GerarCodigoNumerico(NNF).ToString("0000000");
                }
                else
                {
                    retorno = CNFField;
                }

                return retorno;
            }
            set => CNFField = value;
        }

        [XmlElement("cDV")]
        public int CDV { get; set; }

        #region dhEmi
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
        public string VerProc { get; set; }

        [XmlElement("indPrePago")]
        public SimNao IndPrePago { get; set; } = 0;

        [XmlElement("indCessaoMeiosRede")]
        public SimNao IndCessaoMeiosRede { get; set; } = 0;

        [XmlElement("indNotaEntrada")]
        public SimNao IndNotaEntrada { get; set; } = 0;

        #region dhCont
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

        public bool ShouldSerializeDhContField() => DhCont > DateTime.MinValue;

        public bool ShouldSerializeXJust() => !string.IsNullOrWhiteSpace(XJust);

        public bool ShouldSerializeIndPrePago() => (int)IndPrePago == 1;

        public bool ShouldSerializeIndCessaoMeiosRede() => (int)IndCessaoMeiosRede == 1;

        public bool ShouldSerializeIndNotaEntrada() => (int)IndNotaEntrada == 1;

        #endregion
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Unimake.Business.DFe.Xml.NFCom.Emit")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfcom")]
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
    [ProgId("Unimake.Business.DFe.Xml.NFCom.EnderEmit")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class EnderEmit
    {
        private string XLgrField;
        private string XCplField;
        private string NroField;
        private string XBairroField;
        private string XMunField;

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

        [XmlElement("UF")]
        public UFBrasil UF { get; set; }

        [XmlElement("CEP")]
        public string CEP { get; set; }

        [XmlElement("fone")]
        public string Fone { get; set; }

        [XmlElement("email")]
        public string Email { get; set; }

        #region ShouldSerialize

        public bool ShouldSerializeXCpl() => !string.IsNullOrWhiteSpace(XCpl);

        public bool ShouldSerializeFone() => !string.IsNullOrWhiteSpace(Fone);

        public bool ShouldSerializeEmail() => !string.IsNullOrWhiteSpace(Email);

        #endregion
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Unimake.Business.DFe.Xml.NFCom.Dest")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfcom")]
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

        public bool ShouldSerializeIdOutros() => !string.IsNullOrWhiteSpace(IdOutros) || string.IsNullOrWhiteSpace(CNPJ) && string.IsNullOrWhiteSpace(CPF) && !string.IsNullOrWhiteSpace(XNome);

        public bool ShouldSerializeIE() => !string.IsNullOrWhiteSpace(IE);

        public bool ShouldSerializeIM() => !string.IsNullOrWhiteSpace(IM);

        #endregion
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Unimake.Business.DFe.Xml.NFCom.EnderDest")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class EnderDest
    {
        private string XLgrField;
        private string NroField;
        private string XCplField;
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

        #region cMun

        private int _CMun;

        [XmlIgnore]
        public int CMun
        {
            get => _CMun;
            set
            {
                if (value <= 0)
                {
                    throw new Exception("Código do município do destinatário (tag <cMun> da <enderDest>) está sem conteúdo. É obrigatório informar o código IBGE do município.");
                }

                _CMun = value;
            }
        }

        [XmlElement("cMun")]
        public string CMunField
        {
            get => CMun.ToString();
            set => CMun = Convert.ToInt32(string.IsNullOrWhiteSpace(value) ? "0" : value);
        }

        #endregion

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
    [ProgId("Unimake.Business.DFe.Xml.NFCom.Assinante")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class Assinante
    {
        private string NroTermPrincField;
        private string NroTermAdicField;

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

        #region NroTermPrinc e cUFPrinc
        [XmlElement("NroTermPrinc")]
        public string NroTermPrinc
        {
            get => NroTermPrincField;
            set => NroTermPrincField = value == null ? value : XMLUtility.UnescapeReservedCharacters(value).Truncate(256).Trim();
        }

        [XmlElement("cUFPrinc")]
        public UFBrasil CUFPrinc { get; set; }
        #endregion

        #region NroTermAdic e cUFAdic
        [XmlElement("NroTermAdic")]
        public string NroTermAdic
        {
            get => NroTermAdicField;
            set => NroTermAdicField = value == null ? value : XMLUtility.UnescapeReservedCharacters(value).Truncate(256).Trim();
        }

        [XmlElement("cUFAdic")]
        public UFBrasil CUFAdic { get; set; }
        #endregion

        #region ShouldSerialize

        public bool ShouldSerializeDContratoIniField() => DContratoIniField != null;

        public bool ShouldSerializeDContratoFimField() => DContratoFimField != null;

        public bool ShouldSerializeNroTermPrinc() => !string.IsNullOrWhiteSpace(NroTermPrinc);

        public bool ShouldSerializeNroTermAdic() => !string.IsNullOrWhiteSpace(NroTermAdic);

        #endregion
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Unimake.Business.DFe.Xml.NFCom.GSub")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class GSub
    {
        [XmlElement("chNFCom")]
        public string ChNFCom { get; set; }

        [XmlElement("gNF")]
        public GNF GNF { get; set; }

        [XmlElement("motSub")]
        public MotivoSubstituicao MotSub { get; set; }
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Unimake.Business.DFe.Xml.NFCom.GNF")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class GNF
    {
        [XmlElement("CNPJ")]
        public string CNPJ { get; set; }

        [XmlElement("mod")]
        public ModeloNFPapel Mod { get; set; }

        [XmlElement("serie")]
        public int Serie { get; set; } = 3;

        [XmlElement("nNF")]
        public int NNF { get; set; }

        #region CompetEmis
        [XmlIgnore]
#if INTEROP
        public DateTime CompetEmis { get; set; }
#else
        public DateTimeOffset CompetEmis { get; set; }
#endif

        [XmlElement("CompetEmis")]
        public string CompetEmisField
        {
            get => CompetEmis.ToString("yyyyMM");
#if INTEROP
            set => CompetEmis = DateTime.Parse(value);
#else
            set => CompetEmis = DateTimeOffset.Parse(value);
#endif
        }
        #endregion

        [XmlElement("hash115")]
        public string Hash115 { get; set; }

        #region ShouldSerialize

        public bool ShouldSerializeHash115() => !string.IsNullOrWhiteSpace(Hash115);

        #endregion
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Unimake.Business.DFe.Xml.NFCom.GCofat")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class GCofat
    {
        [XmlElement("chNFComLocal")]
        public string ChNFComLocal { get; set; }

        #region ShouldSerialize

        public bool ShouldSerializeChNFComLocal() => !string.IsNullOrWhiteSpace(ChNFComLocal);

        #endregion
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Unimake.Business.DFe.Xml.NFCom.Det")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class Det
    {
        private string InfAdProdField;

        [XmlAttribute(AttributeName = "nItem")]
        public int NItem { get; set; }

        [XmlAttribute(AttributeName = "chNFComAnt")]
        public int ChNFComAnt { get; set; }

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

        public bool ShouldSerializeInfAdProd() => !string.IsNullOrWhiteSpace(InfAdProd);

        #endregion
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Unimake.Business.DFe.Xml.NFCom.Prod")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class Prod
    {
        private string XProdField;
        private string CClassField;

        [XmlElement("cProd")]
        public string CProd { get; set; }

        [XmlElement("xProd")]
        public string XProd
        {
            get => XProdField;
            set => XProdField = value == null ? value : XMLUtility.UnescapeReservedCharacters(value).Truncate(120).Trim();
        }

        [XmlElement("cClass")]
        public string CClass
        {
            get => CClassField;
            set => CClassField = value.PadLeft(7, '0');
        }

        [XmlElement("CFOP")]
        public string CFOP { get; set; }

        [XmlElement("CNPJLD")]
        public string CNPJLD { get; set; }

        [XmlElement("uMed")]
        public UnidadeBasicaMedida UMed { get; set; }

        [XmlElement("qFaturada")]
        public decimal QFaturada { get; set; }

        [XmlIgnore]
        public double VItem { get; set; }

        [XmlElement("vItem")]
        public string VItemField
        {
            get => VItem.ToString("F2", CultureInfo.InvariantCulture);
            set => VItem = Converter.ToDouble(value);
        }

        [XmlIgnore]
        public double VDesc { get; set; }

        [XmlElement("vDesc")]
        public string VDescField
        {
            get => VDesc.ToString("F2", CultureInfo.InvariantCulture);
            set => VDesc = Converter.ToDouble(value);
        }

        [XmlIgnore]
        public double VOutro { get; set; }

        [XmlElement("vOutro")]
        public string VOutroField
        {
            get => VOutro.ToString("F2", CultureInfo.InvariantCulture);
            set => VOutro = Converter.ToDouble(value);
        }

        [XmlIgnore]
        public double VProd { get; set; }

        [XmlElement("vProd")]
        public string VProdField
        {
            get => VProd.ToString("F2", CultureInfo.InvariantCulture);
            set => VProd = Converter.ToDouble(value);
        }

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
            get => DExpiracao.ToString("yyyyMM");
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
        public bool ShouldSerializeIndDevolucao() => (int)IndDevolucao == 1;
        public bool ShouldSerializeDExpiracaoField() => !string.IsNullOrWhiteSpace(DExpiracaoField);

        #endregion

#if INTEROP

        /// <summary>
        /// Adicionar novo elemento a lista
        /// </summary>
        /// <param name="nVE">Elemento</param>
        public void AddNVE(string nVE)
        {
            if (NVE == null)
            {
                NVE = new List<string>();
            }

            NVE.Add(nVE);
        }

        /// <summary>
        /// Retorna o elemento da lista NVE (Utilizado para linguagens diferentes do CSharp que não conseguem pegar o conteúdo da lista)
        /// </summary>
        /// <param name="index">Índice da lista a ser retornado (Começa com 0 (zero))</param>
        /// <returns>Conteúdo do index passado por parâmetro da NVE</returns>
        public string GetNVE(int index)
        {
            if ((NVE?.Count ?? 0) == 0)
            {
                return default;
            };

            return NVE[index];
        }

        /// <summary>
        /// Retorna a quantidade de elementos existentes na lista NVE
        /// </summary>
        public int GetNVECount => (NVE != null ? NVE.Count : 0);

        /// <summary>
        /// Adicionar novo elemento a lista
        /// </summary>
        /// <param name="di">Elemento</param>
        public void AddDI(DI di)
        {
            if (DI == null)
            {
                DI = new List<DI>();
            }

            DI.Add(di);
        }

        /// <summary>
        /// Retorna o elemento da lista DI (Utilizado para linguagens diferentes do CSharp que não conseguem pegar o conteúdo da lista)
        /// </summary>
        /// <param name="index">Índice da lista a ser retornado (Começa com 0 (zero))</param>
        /// <returns>Conteúdo do index passado por parâmetro da DI</returns>
        public DI GetDI(int index)
        {
            if ((DI?.Count ?? 0) == 0)
            {
                return default;
            };

            return DI[index];
        }

        /// <summary>
        /// Retorna a quantidade de elementos existentes na lista DI
        /// </summary>
        public int GetDICount => (DI != null ? DI.Count : 0);

        /// <summary>
        /// Adicionar novo elemento a lista
        /// </summary>
        /// <param name="detExport">Elemento</param>
        public void AddDetExport(DetExport detExport)
        {
            if (DetExport == null)
            {
                DetExport = new List<DetExport>();
            }

            DetExport.Add(detExport);
        }

        /// <summary>
        /// Retorna o elemento da lista DetExport (Utilizado para linguagens diferentes do CSharp que não conseguem pegar o conteúdo da lista)
        /// </summary>
        /// <param name="index">Índice da lista a ser retornado (Começa com 0 (zero))</param>
        /// <returns>Conteúdo do index passado por parâmetro da DetExport</returns>
        public DetExport GetDetExport(int index)
        {
            if ((DetExport?.Count ?? 0) == 0)
            {
                return default;
            };

            return DetExport[index];
        }

        /// <summary>
        /// Retorna a quantidade de elementos existentes na lista DetExport
        /// </summary>
        public int GetDetExportCount => (DetExport != null ? DetExport.Count : 0);

        /// <summary>
        /// Adicionar novo elemento a lista
        /// </summary>
        /// <param name="rastro">Elemento</param>
        public void AddRastro(Rastro rastro)
        {
            if (Rastro == null)
            {
                Rastro = new List<Rastro>();
            }

            Rastro.Add(rastro);
        }

        /// <summary>
        /// Retorna o elemento da lista Rastro (Utilizado para linguagens diferentes do CSharp que não conseguem pegar o conteúdo da lista)
        /// </summary>
        /// <param name="index">Índice da lista a ser retornado (Começa com 0 (zero))</param>
        /// <returns>Conteúdo do index passado por parâmetro da Rastro</returns>
        public Rastro GetRastro(int index)
        {
            if ((Rastro?.Count ?? 0) == 0)
            {
                return default;
            };

            return Rastro[index];
        }

        /// <summary>
        /// Retorna a quantidade de elementos existentes na lista Rastro
        /// </summary>
        public int GetRastroCount => (Rastro != null ? Rastro.Count : 0);

        /// <summary>
        /// Adicionar novo elemento a lista
        /// </summary>
        /// <param name="arma">Elemento</param>
        public void AddArma(Arma arma)
        {
            if (Arma == null)
            {
                Arma = new List<Arma>();
            }

            Arma.Add(arma);
        }

        /// <summary>
        /// Retorna o elemento da lista Arma (Utilizado para linguagens diferentes do CSharp que não conseguem pegar o conteúdo da lista)
        /// </summary>
        /// <param name="index">Índice da lista a ser retornado (Começa com 0 (zero))</param>
        /// <returns>Conteúdo do index passado por parâmetro da Arma</returns>
        public Arma GetArma(int index)
        {
            if ((Arma?.Count ?? 0) == 0)
            {
                return default;
            };

            return Arma[index];
        }

        /// <summary>
        /// Retorna a quantidade de elementos existentes na lista Arma
        /// </summary>
        public int GetArmaCount => (Arma != null ? Arma.Count : 0);

        /// <summary>
        /// Adicionar novo elemento a lista
        /// </summary>
        /// <param name="comb">Elemento</param>
        public void AddComb(Comb comb)
        {
            if (Comb == null)
            {
                Comb = new List<Comb>();
            }

            Comb.Add(comb);
        }

        /// <summary>
        /// Retorna o elemento da lista Comb (Utilizado para linguagens diferentes do CSharp que não conseguem pegar o conteúdo da lista)
        /// </summary>
        /// <param name="index">Índice da lista a ser retornado (Começa com 0 (zero))</param>
        /// <returns>Conteúdo do index passado por parâmetro da Comb</returns>
        public Comb GetComb(int index)
        {
            if ((Comb?.Count ?? 0) == 0)
            {
                return default;
            };

            return Comb[index];
        }

        /// <summary>
        /// Retorna a quantidade de elementos existentes na lista Comb
        /// </summary>
        public int GetCombCount => (Comb != null ? Comb.Count : 0);

        /// <summary>
        /// Adicionar novo elemento a lista
        /// </summary>
        /// <param name="veicProd">Elemento</param>
        public void AddVeicProd(VeicProd veicProd)
        {
            if (VeicProd == null)
            {
                VeicProd = new List<VeicProd>();
            }

            VeicProd.Add(veicProd);
        }

        /// <summary>
        /// Retorna o elemento da lista VeicProd (Utilizado para linguagens diferentes do CSharp que não conseguem pegar o conteúdo da lista)
        /// </summary>
        /// <param name="index">Índice da lista a ser retornado (Começa com 0 (zero))</param>
        /// <returns>Conteúdo do index passado por parâmetro da VeicProd</returns>
        public VeicProd GetVeicProd(int index)
        {
            if ((VeicProd?.Count ?? 0) == 0)
            {
                return default;
            };

            return VeicProd[index];
        }

        /// <summary>
        /// Retorna a quantidade de elementos existentes na lista VeicProd
        /// </summary>
        public int GetVeicProdCount => (VeicProd != null ? VeicProd.Count : 0);

#endif
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Unimake.Business.DFe.Xml.NFCom.Imposto")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class Imposto
    {
        [XmlElement("TImp")]
        public TImp TImp { get; set; }
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Unimake.Business.DFe.Xml.NFCom.TImp")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class TImp
    {
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
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Unimake.Business.DFe.Xml.NFCom.PIS")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class PIS
    {
        [XmlElement("CST")]
        public string CST { get; set; }

        [XmlIgnore]
        public double VBC { get; set; }

        [XmlElement("vBC")]
        public string VBCField
        {
            get => VBC.ToString("F2", CultureInfo.InvariantCulture);
            set => VBC = Converter.ToDouble(value);
        }

        [XmlIgnore]
        public double PPIS { get; set; }

        [XmlElement("pPIS")]
        public string PPISField
        {
            get => PPIS.ToString("F4", CultureInfo.InvariantCulture);
            set => PPIS = Converter.ToDouble(value);
        }

        [XmlIgnore]
        public double VPIS { get; set; }

        [XmlElement("vPIS")]
        public string VPISField
        {
            get => VPIS.ToString("F2", CultureInfo.InvariantCulture);
            set => VPIS = Converter.ToDouble(value);
        }
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Unimake.Business.DFe.Xml.NFCom.COFINS")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class COFINS
    {
        [XmlElement("CST")]
        public string CST { get; set; }

        [XmlIgnore]
        public double VBC { get; set; }

        [XmlElement("vBC")]
        public string VBCField
        {
            get => VBC.ToString("F2", CultureInfo.InvariantCulture);
            set => VBC = Converter.ToDouble(value);
        }

        [XmlIgnore]
        public double PCOFINS { get; set; }

        [XmlElement("pCOFINS")]
        public string PCOFINSField
        {
            get => PCOFINS.ToString("F4", CultureInfo.InvariantCulture);
            set => PCOFINS = Converter.ToDouble(value);
        }

        [XmlIgnore]
        public double VCOFINS { get; set; }

        [XmlElement("vCOFINS")]
        public string VCOFINSField
        {
            get => VCOFINS.ToString("F2", CultureInfo.InvariantCulture);
            set => VCOFINS = Converter.ToDouble(value);
        }
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Unimake.Business.DFe.Xml.NFCom.FUST")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class FUST
    {
        [XmlIgnore]
        public double VBC { get; set; }

        [XmlElement("vBC")]
        public string VBCField
        {
            get => VBC.ToString("F2", CultureInfo.InvariantCulture);
            set => VBC = Converter.ToDouble(value);
        }

        [XmlIgnore]
        public double PFUST { get; set; }

        [XmlElement("pFUST")]
        public string PFUSTField
        {
            get => PFUST.ToString("F4", CultureInfo.InvariantCulture);
            set => PFUST = Converter.ToDouble(value);
        }

        [XmlIgnore]
        public double VFUST { get; set; }

        [XmlElement("vFUST")]
        public string VFUSTField
        {
            get => VFUST.ToString("F2", CultureInfo.InvariantCulture);
            set => VFUST = Converter.ToDouble(value);
        }
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Unimake.Business.DFe.Xml.NFCom.FUNTTEL")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class FUNTTEL
    {
        [XmlIgnore]
        public double VBC { get; set; }

        [XmlElement("vBC")]
        public string VBCField
        {
            get => VBC.ToString("F2", CultureInfo.InvariantCulture);
            set => VBC = Converter.ToDouble(value);
        }

        [XmlIgnore]
        public double PFUNTTEL { get; set; }

        [XmlElement("pFUNTTEL")]
        public string PFUNTTELField
        {
            get => PFUNTTEL.ToString("F4", CultureInfo.InvariantCulture);
            set => PFUNTTEL = Converter.ToDouble(value);
        }

        [XmlIgnore]
        public double VFUNTTEL { get; set; }

        [XmlElement("vFUNTTEL")]
        public string VFUNTTELField
        {
            get => VFUNTTEL.ToString("F2", CultureInfo.InvariantCulture);
            set => VFUNTTEL = Converter.ToDouble(value);
        }
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Unimake.Business.DFe.Xml.NFCom.RetTrib")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class RetTrib
    {
        [XmlIgnore]
        public double VRetPIS { get; set; }
        [XmlElement("vRetPIS")]
        public string VRetPISField
        {
            get => VRetPIS.ToString("F2", CultureInfo.InvariantCulture);
            set => VRetPIS = Converter.ToDouble(value);
        }

        [XmlIgnore]
        public double VRetCOFINS { get; set; }
        [XmlElement("vRetCofins")]
        public string VRetCOFINSField
        {
            get => VRetCOFINS.ToString("F2", CultureInfo.InvariantCulture);
            set => VRetCOFINS = Converter.ToDouble(value);
        }

        [XmlIgnore]
        public double VRetCSLL { get; set; }
        [XmlElement("vRetCSLL")]
        public string VRetCSLLField
        {
            get => VRetCSLL.ToString("F2", CultureInfo.InvariantCulture);
            set => VRetCSLL = Converter.ToDouble(value);
        }

        [XmlIgnore]
        public double VIRRF { get; set; }
        [XmlElement("vIRRF")]
        public string VIRRFField
        {
            get => VIRRF.ToString("F2", CultureInfo.InvariantCulture);
            set => VIRRF = Converter.ToDouble(value);
        }
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Unimake.Business.DFe.Xml.NFCom.GProcRef")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class GProcRef
    {
        [XmlIgnore]
        public double VItem { get; set; }

        [XmlElement("vItem")]
        public string VItemField
        {
            get => VItem.ToString("F2", CultureInfo.InvariantCulture);
            set => VItem = Converter.ToDouble(value);
        }

        [XmlElement("qFaturada")]
        public decimal QFaturada { get; set; }

        [XmlIgnore]
        public double VProd { get; set; }

        [XmlElement("vProd")]
        public string VProdField
        {
            get => VProd.ToString("F2", CultureInfo.InvariantCulture);
            set => VProd = Converter.ToDouble(value);
        }

        [XmlIgnore]
        public double VDesc { get; set; }

        [XmlElement("vDesc")]
        public string VDescField
        {
            get => VDesc.ToString("F2", CultureInfo.InvariantCulture);
            set => VDesc = Converter.ToDouble(value);
        }

        [XmlIgnore]
        public double VOutro { get; set; }

        [XmlElement("vOutro")]
        public string VOutroField
        {
            get => VOutro.ToString("F2", CultureInfo.InvariantCulture);
            set => VOutro = Converter.ToDouble(value);
        }

        [XmlElement("indDevolucao")]
        public SimNao IndDevolucao { get; set; }

        [XmlIgnore]
        public double VBC { get; set; }

        [XmlElement("vBC")]
        public string VBCField
        {
            get => VBC.ToString("F2", CultureInfo.InvariantCulture);
            set => VBC = Converter.ToDouble(value);
        }

        [XmlIgnore]
        public double PICMS { get; set; }

        [XmlElement("pICMS")]
        public string PICMSField
        {
            get => PICMS.ToString("F2", CultureInfo.InvariantCulture);
            set => PICMS = Converter.ToDouble(value);
        }

        [XmlIgnore]
        public double VICMS { get; set; }

        [XmlElement("vICMS")]
        public string VICMSField
        {
            get => VICMS.ToString("F2", CultureInfo.InvariantCulture);
            set => VICMS = Converter.ToDouble(value);
        }

        [XmlIgnore]
        public double VPIS { get; set; }

        [XmlElement("vPIS")]
        public string VPISField
        {
            get => VPIS.ToString("F2", CultureInfo.InvariantCulture);
            set => VPIS = Converter.ToDouble(value);
        }

        [XmlIgnore]
        public double VCOFINS { get; set; }

        [XmlElement("vCOFINS")]
        public string VCOFINSField
        {
            get => VCOFINS.ToString("F2", CultureInfo.InvariantCulture);
            set => VCOFINS = Converter.ToDouble(value);
        }

        [XmlElement("gProc")]
        public List<GProc> GProc { get; set; }

        #region ShouldSerialize

        public bool ShouldSerializeVDescField() => VDesc > 0;
        public bool ShouldSerializeVOutroField() => VOutro > 0;
        public bool ShouldSerializeIndDevolucao() => (int)IndDevolucao == 1;
        public bool ShouldSerializeVBCField() => VBC > 0;
        public bool ShouldSerializePICMSField() => PICMS > 0;
        public bool ShouldSerializeVICMSField() => VICMS > 0;
        public bool ShouldSerializeVPISField() => VPIS > 0;
        public bool ShouldSerializeVCOFINSField() => VCOFINS > 0;

        #endregion    
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Unimake.Business.DFe.Xml.NFCom.GProc")]
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
    [ProgId("Unimake.Business.DFe.Xml.NFCom.GRessarc")]
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

        #region dRef
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
    [ProgId("Unimake.Business.DFe.Xml.NFCom.Total")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class Total
    {
        [XmlIgnore]
        public double VProd { get; set; }
        [XmlElement("vProd")]
        public string VProdField
        {
            get => VProd.ToString("F2", CultureInfo.InvariantCulture);
            set => VProd = Converter.ToDouble(value);
        }

        [XmlElement("ICMSTot")]
        public ICMSTot ICMSTot { get; set; }

        [XmlIgnore]
        public double VCOFINS { get; set; }
        [XmlElement("vCOFINS")]
        public string VCOFINSField
        {
            get => VCOFINS.ToString("F2", CultureInfo.InvariantCulture);
            set => VCOFINS = Converter.ToDouble(value);
        }

        [XmlIgnore]
        public double VPIS { get; set; }
        [XmlElement("vPIS")]
        public string VPISField
        {
            get => VPIS.ToString("F2", CultureInfo.InvariantCulture);
            set => VPIS = Converter.ToDouble(value);
        }

        [XmlElement("vRetTribTot")]
        public VRetTribTot VRetTribTot { get; set; }

        [XmlIgnore]
        public double VDesc { get; set; }
        [XmlElement("vDesc")]
        public string VDescField
        {
            get => VDesc.ToString("F2", CultureInfo.InvariantCulture);
            set => VDesc = Converter.ToDouble(value);
        }

        [XmlIgnore]
        public double VOutro { get; set; }
        [XmlElement("vOutro")]
        public string VOutroField
        {
            get => VOutro.ToString("F2", CultureInfo.InvariantCulture);
            set => VOutro = Converter.ToDouble(value);
        }

        [XmlIgnore]
        public double VNF { get; set; }
        [XmlElement("vNF")]
        public string VNFField
        {
            get => VNF.ToString("F2", CultureInfo.InvariantCulture);
            set => VNF = Converter.ToDouble(value);
        }
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Unimake.Business.DFe.Xml.NFCom.ICMSTot")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class ICMSTot
    {
        [XmlIgnore]
        public double VBC { get; set; }
        [XmlElement("vBC")]
        public string VBCField
        {
            get => VBC.ToString("F2", CultureInfo.InvariantCulture);
            set => VBC = Converter.ToDouble(value);
        }

        [XmlIgnore]
        public double VICMS { get; set; }
        [XmlElement("vICMS")]
        public string VICMSField
        {
            get => VICMS.ToString("F2", CultureInfo.InvariantCulture);
            set => VICMS = Converter.ToDouble(value);
        }

        [XmlIgnore]
        public double VICMSDeson { get; set; }
        [XmlElement("vICMSDeson")]
        public string VICMSDesonField
        {
            get => VICMSDeson.ToString("F2", CultureInfo.InvariantCulture);
            set => VICMSDeson = Converter.ToDouble(value);
        }

        [XmlIgnore]
        public double VFCP { get; set; }
        [XmlElement("vFCP")]
        public string VFCPField
        {
            get => VFCP.ToString("F2", CultureInfo.InvariantCulture);
            set => VFCP = Converter.ToDouble(value);
        }
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Unimake.Business.DFe.Xml.NFCom.VRetTribTot")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class VRetTribTot
    {
        [XmlIgnore]
        public double VRetPIS { get; set; }
        [XmlElement("vRetPIS")]
        public string VRetPISField
        {
            get => VRetPIS.ToString("F2", CultureInfo.InvariantCulture);
            set => VRetPIS = Converter.ToDouble(value);
        }

        [XmlIgnore]
        public double VRetCOFINS { get; set; }
        [XmlElement("vRetCofins")]
        public string VRetCOFINSField
        {
            get => VRetCOFINS.ToString("F2", CultureInfo.InvariantCulture);
            set => VRetCOFINS = Converter.ToDouble(value);
        }

        [XmlIgnore]
        public double VRetCSLL { get; set; }
        [XmlElement("vRetCSLL")]
        public string VRetCSLLField
        {
            get => VRetCSLL.ToString("F2", CultureInfo.InvariantCulture);
            set => VRetCSLL = Converter.ToDouble(value);
        }

        [XmlIgnore]
        public double VIRRF { get; set; }
        [XmlElement("vIRRF")]
        public string VIRRFField
        {
            get => VIRRF.ToString("F2", CultureInfo.InvariantCulture);
            set => VIRRF = Converter.ToDouble(value);
        }
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Unimake.Business.DFe.Xml.NFCom.GFidelidade")]
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
    [ProgId("Unimake.Business.DFe.Xml.NFCom.GFat")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class GFat
    {
        #region CompetFat
        [XmlIgnore]
#if INTEROP
        public DateTime CompetFat { get; set; }
#else
        public DateTimeOffset CompetFat { get; set; }
#endif

        [XmlElement("CompetFat")]
        public string CompetFatField
        {
            get => CompetFat.ToString("yyyyMM");
#if INTEROP
            set => CompetFat = DateTime.Parse(value);
#else
            set => CompetFat = DateTimeOffset.Parse(value);
#endif
        }
        #endregion

        #region dVencFat
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

        #region dPerUsoIni
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

        #region dPerUsoFim
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

        public bool ShouldSerializeCodDebAuto() => !string.IsNullOrWhiteSpace(CodDebAuto);

        #endregion
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Unimake.Business.DFe.Xml.NFCom.EnderCorresp")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class EnderCorresp
    {
        private string XLgrField;
        private string XCplField;
        private string NroField;
        private string XBairroField;
        private string XMunField;

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

        [XmlElement("UF")]
        public UFBrasil UF { get; set; }

        [XmlElement("CEP")]
        public string CEP { get; set; }

        [XmlElement("fone")]
        public string Fone { get; set; }

        [XmlElement("email")]
        public string Email { get; set; }

        #region ShouldSerialize

        public bool ShouldSerializeXCpl() => !string.IsNullOrWhiteSpace(XCpl);

        public bool ShouldSerializeFone() => !string.IsNullOrWhiteSpace(Fone);

        public bool ShouldSerializeEmail() => !string.IsNullOrWhiteSpace(Email);

        #endregion
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Unimake.Business.DFe.Xml.NFCom.GPIX")]
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
    [ProgId("Unimake.Business.DFe.Xml.NFCom.GFatCentral")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class GFatCentral
    {
        [XmlElement("CNPJ")]
        public string CNPJ { get; set; }

        [XmlElement("cUF")]
        public UFBrasil CUF { get; set; }

        #region ShouldSerialize

        public bool ShouldSerializeCNPJ() => !string.IsNullOrWhiteSpace(CNPJ);

        #endregion
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Unimake.Business.DFe.Xml.NFCom.AutXML")]
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
    [ProgId("Unimake.Business.DFe.Xml.NFCom.InfAdic")]
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

#if INTEROP

        /// <summary>
        /// Adicionar novo elemento a lista
        /// </summary>
        /// <param name="obsCont">Elemento</param>
        public void AddObsCont(ObsCont obsCont)
        {
            if (ObsCont == null)
            {
                ObsCont = new List<ObsCont>();
            }

            ObsCont.Add(obsCont);
        }

        /// <summary>
        /// Retorna o elemento da lista ObsCont (Utilizado para linguagens diferentes do CSharp que não conseguem pegar o conteúdo da lista)
        /// </summary>
        /// <param name="index">Índice da lista a ser retornado (Começa com 0 (zero))</param>
        /// <returns>Conteúdo do index passado por parâmetro da ObsCont</returns>
        public ObsCont GetObsCont(int index)
        {
            if ((ObsCont?.Count ?? 0) == 0)
            {
                return default;
            };

            return ObsCont[index];
        }

        /// <summary>
        /// Retorna a quantidade de elementos existentes na lista ObsCont
        /// </summary>
        public int GetObsContCount => (ObsCont != null ? ObsCont.Count : 0);

        /// <summary>
        /// Adicionar novo elemento a lista
        /// </summary>
        /// <param name="obsFisco">Elemento</param>
        public void AddObsFisco(ObsFisco obsFisco)
        {
            if (ObsFisco == null)
            {
                ObsFisco = new List<ObsFisco>();
            }

            ObsFisco.Add(obsFisco);
        }

        /// <summary>
        /// Retorna o elemento da lista ObsFisco (Utilizado para linguagens diferentes do CSharp que não conseguem pegar o conteúdo da lista)
        /// </summary>
        /// <param name="index">Índice da lista a ser retornado (Começa com 0 (zero))</param>
        /// <returns>Conteúdo do index passado por parâmetro da ObsFisco</returns>
        public ObsFisco GetObsFisco(int index)
        {
            if ((ObsFisco?.Count ?? 0) == 0)
            {
                return default;
            };

            return ObsFisco[index];
        }

        /// <summary>
        /// Retorna a quantidade de elementos existentes na lista ObsFisco
        /// </summary>
        public int GetObsFiscoCount => (ObsFisco != null ? ObsFisco.Count : 0);

        /// <summary>
        /// Adicionar novo elemento a lista
        /// </summary>
        /// <param name="procRef">Elemento</param>
        public void AddProcRef(ProcRef procRef)
        {
            if (ProcRef == null)
            {
                ProcRef = new List<ProcRef>();
            }

            ProcRef.Add(procRef);
        }

        /// <summary>
        /// Retorna o elemento da lista ProcRef (Utilizado para linguagens diferentes do CSharp que não conseguem pegar o conteúdo da lista)
        /// </summary>
        /// <param name="index">Índice da lista a ser retornado (Começa com 0 (zero))</param>
        /// <returns>Conteúdo do index passado por parâmetro da ProcRef</returns>
        public ProcRef GetProcRef(int index)
        {
            if ((ProcRef?.Count ?? 0) == 0)
            {
                return default;
            };

            return ProcRef[index];
        }

        /// <summary>
        /// Retorna a quantidade de elementos existentes na lista ProcRef
        /// </summary>
        public int GetProcRefCount => (ProcRef != null ? ProcRef.Count : 0);

#endif
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Unimake.Business.DFe.Xml.NFCom.GRespTec")]
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

        public bool ShouldSerializeIdCSRT() => !string.IsNullOrWhiteSpace(IdCSRT);

        public bool ShouldSerializeHashCSRT() => HashCSRT != null;

        #endregion
    }

#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Unimake.Business.DFe.Xml.NFCom.InfNFComSupl")]
    [ComVisible(true)]
#endif
    [Serializable()]
    [XmlType(AnonymousType = true, Namespace = "http://www.portalfiscal.inf.br/nfcom")]
    public class InfNFComSupl
    {
        [XmlElement("qrCodNFCom")]
        public string QrCodeNFCom { get; set; }
    }
}