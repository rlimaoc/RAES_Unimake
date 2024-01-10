#if INTEROP
using System.Runtime.InteropServices;
using Unimake.Exceptions;
#endif
using System;
using System.Collections.Generic;
using System.Xml;
using Uni.NFCom.Servicos.Enums;
using Uni.NFCom.Servicos.Interop.Contract;
using Uni.NFCom.Utility;
using Uni.NFCom.Xml.NFCom;

namespace Uni.NFCom.Servicos.NFCom
{
    /// <summary>
    /// Enviar o XML de NFCom para o web-service
    /// </summary>
#if INTEROP
    [ClassInterface(ClassInterfaceType.AutoDual)]
    [ProgId("Uni.NFCom.Servicos.NFCom.Autorizacao")]
    [ComVisible(true)]
#endif
    public class Autorizacao : ServicoBase, IInteropService<EnviNFCom>
    {
        #region Private Fields

        private EnviNFCom _enviNFCom;

        #endregion Private Fields

        #region Private Methods

        /// <summary>
        /// Mudar o conteúdo da tag xMotivo caso a nota tenha sido rejeitada por problemas nos itens/produtos da nota. Assim vamos retornar na xMotivo algumas informações a mais para facilitar o entendimento para o usuário.
        /// </summary>
        private void MudarConteudoTagRetornoXMotivo()
        {
            try
            {
                if (RetornoWSXML.GetElementsByTagName("xMotivo") != null)
                {
                    var xMotivo = RetornoWSXML.GetElementsByTagName("xMotivo")[0].InnerText;
                    if (xMotivo.Contains("[nItem:"))
                    {
                        var nItem = Convert.ToInt32((xMotivo.Substring(xMotivo.IndexOf("[nItem:") + 7)).Substring(0, (xMotivo.Substring(xMotivo.IndexOf("[nItem:") + 7)).Length - 1));
                        RetornoWSString = RetornoWSString.Replace(xMotivo, xMotivo + " [cProd:" + EnviNFCom.NFCom[0].InfNFCom[0].Det[nItem - 1].Prod.CProd + "] [xProd:" + EnviNFCom.NFCom[0].InfNFCom[0].Det[nItem - 1].Prod.XProd + "]");

                        RetornoWSXML = new XmlDocument
                        {
                            PreserveWhitespace = false
                        };
                        RetornoWSXML.LoadXml(RetornoWSString);
                    }
                }
            }
            catch { }
        }

        #endregion

        #region Protected Properties

        /// <summary>
        /// Objeto do XML da NFCom
        /// </summary>
        public EnviNFCom EnviNFCom
        {
            get => _enviNFCom ?? (_enviNFCom = new EnviNFCom().LerXML<EnviNFCom>(ConteudoXML));
            protected set => _enviNFCom = value;
        }

        #endregion Protected Properties

        #region Protected Methods

        /// <summary>
        /// Definir o valor de algumas das propriedades do objeto "Configuracoes"
        /// </summary>
        protected override void DefinirConfiguracao()
        {
            if (EnviNFCom == null)
            {
                Configuracoes.Definida = false;
                return;
            }

            var xml = EnviNFCom;

            if (!Configuracoes.Definida)
            {
                Configuracoes.Servico = Servico.NFComAutorizacao;
                Configuracoes.CodigoUF = (int)xml.NFCom[0].InfNFCom[0].Ide.CUF;
                Configuracoes.TipoAmbiente = xml.NFCom[0].InfNFCom[0].Ide.TpAmb;
                Configuracoes.Modelo = xml.NFCom[0].InfNFCom[0].Ide.Mod;
                Configuracoes.TipoEmissao = xml.NFCom[0].InfNFCom[0].Ide.TpEmis;
                Configuracoes.SchemaVersao = xml.Versao;

                base.DefinirConfiguracao();
            }
        }

        /// <summary>
        /// Efetuar ajustes diversos no XML após o mesmo ter sido assinado.
        /// </summary>
        protected override void AjustarXMLAposAssinado() => EnviNFCom = new EnviNFCom().LerXML<EnviNFCom>(ConteudoXML);

        #endregion Protected Methods

        #region Public Properties

        /// <summary>
        /// Propriedade com o conteúdo retornado da consulta recibo
        /// </summary>
        public RetConsReciNFCom RetConsReciNFCom { get; set; }

#if INTEROP

        /// <summary>
        /// Atribuir null para a propriedade RetConsReciNFCom. (Em FOXPRO não conseguimos atribuir NULL diretamente na propriedade, dá erro de OLE)
        /// </summary>
        public void SetNullRetConsReciNFCom() => RetConsReciNFCom = null;

        /// <summary>
        /// Adicionar o retorno da consulta situação da NFCom na lista dos retornos para elaboração do XML de distribuição
        /// </summary>
        /// <param name="item">Resultado da consulta situação do NFCom</param>
        public void AddRetConsSitNFComs(RetConsSitNFCom item) => (RetConsSitNFComs ?? (RetConsSitNFComs = new List<RetConsSitNFCom>())).Add(item);

#endif


        /// <summary>
        /// Lista com o conteúdo retornado das consultas situação do NFComs enviadas
        /// </summary>
        public List<RetConsSitNFCom> RetConsSitNFComs = new List<RetConsSitNFCom>();

        /// <summary>
        /// Conteúdo retornado pelo web-service depois do envio do XML
        /// </summary>
        public RetEnviNFCom Result
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(RetornoWSString))
                {
                    return XMLUtility.Deserializar<RetEnviNFCom>(RetornoWSXML);
                }

                return new RetEnviNFCom
                {
                    CStat = 0,
                    XMotivo = "Ocorreu uma falha ao tentar criar o objeto a partir do XML retornado da SEFAZ."
                };
            }
        }

        #endregion Public Properties

        #region Public Constructors

        /// <summary>
        /// Construtor
        /// </summary>
        public Autorizacao() : base() { }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="enviNFCom">Objeto contendo o XML a ser enviado</param>
        /// <param name="configuracao">Configurações para conexão e envio do XML para o web-service</param>
        public Autorizacao(EnviNFCom enviNFCom, Configuracao configuracao) : this()
        {
            if (configuracao is null)
            {
                throw new ArgumentNullException(nameof(configuracao));
            }

            Inicializar(enviNFCom?.GerarXML() ?? throw new ArgumentNullException(nameof(enviNFCom)), configuracao);
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        /// Executar o serviço
        /// </summary>
#if INTEROP
        [ComVisible(false)]
#endif
        public override void Executar()
        {
            base.Executar();

            MudarConteudoTagRetornoXMotivo();
        }

        #endregion
    }
}