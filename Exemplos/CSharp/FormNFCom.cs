using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Uni.NFCom.Servicos;
using Uni.NFCom.Servicos.Enums;
using XmlNFCom = Uni.NFCom.Xml.NFCom;
using ServicoNFCom = Uni.NFCom.Servicos.NFCom;
using System.Security.Cryptography.X509Certificates;
using Uni.NFCom.Security;

namespace TreinamentoDLL
{
    public partial class FormNFCom : Form
    {
        #region Private Fields

        /// <summary>
        /// Field para o Certificado Selecionado
        /// </summary>
        private static X509Certificate2 CertificadoSelecionadoField;

        #endregion Private Fields

        #region Private Properties

        /// <summary>
        /// Caminho do certificado digital A1
        /// </summary>
        private static string PathCertificadoDigital { get; set; } = @"C:\NFeModelo55\Cert\UniNFCom_PV.pfx";

        /// <summary>
        /// Senha de uso do certificado digital A1
        /// </summary>
        private static string SenhaCertificadoDigital { get; set; } = "19601964";

        #endregion

        #region Private Methods

        private void BtnConsultaStatusNFCom_Click(object sender, EventArgs e)
        {
            var xml = new XmlNFCom.ConsStatServNFCom
            {
                Versao = "1.00",
                TpAmb = TipoAmbiente.Homologacao
            };

            var configuracao = new Configuracao
            {
                TipoDFe = TipoDFe.NFe,
                TipoEmissao = TipoEmissao.Normal,
                CertificadoDigital = CertificadoSelecionado,
                WebEnderecoHomologacao = "https://nfcom-homologacao.svrs.rs.gov.br/WS/NFComStatusServico/NFComStatusServico.asmx",
                WebSoapString = "<soap:Envelope xmlns:soap=\"http://www.w3.org/2003/05/soap-envelope\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><soap:Body><nfcomDadosMsg xmlns=\"http://www.portalfiscal.inf.br/nfcom/wsdl/NFComStatusServico\">{xmlBody}</nfcomDadosMsg></soap:Body></soap:Envelope>",
            };

            var statusServico = new ServicoNFCom.StatusServicoNFCom(xml, configuracao);
            statusServico.Executar();

            MessageBox.Show(statusServico.Result.CStat + " " + statusServico.Result.XMotivo);
        }

        private void FormNFCom_Load(object sender, EventArgs e)
        {

        }

        #endregion Private Methods

        #region Public Properties

        /// <summary>
        /// Certificado digital
        /// </summary>
        public static X509Certificate2 CertificadoSelecionado
        {
            get
            {
                if (CertificadoSelecionadoField == null)
                {
                    CertificadoSelecionadoField = new CertificadoDigital().CarregarCertificadoDigitalA1(PathCertificadoDigital, SenhaCertificadoDigital);
                }

                return CertificadoSelecionadoField;
            }

            private set => throw new Exception("Não é possível atribuir um certificado digital! Somente resgate o valor da propriedade que o certificado é definido automaticamente.");
        }

        #endregion Public Properties

        #region Public Constructs

        public FormNFCom() => InitializeComponent();

        #endregion Public Constructs
    }
}
