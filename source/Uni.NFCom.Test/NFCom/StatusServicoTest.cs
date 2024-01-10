using Uni.NFCom.Servicos;
using Uni.NFCom.Servicos.Enums;
using Uni.NFCom.Servicos.NFCom;
using Uni.NFCom.Xml.NFCom;
using Xunit;

namespace Unimake.DFe.Test.NFCom
{
    /// <summary>
    /// Testar o serviço de consulta protocolo da NFe
    /// </summary>
    public class StatusServicoTest
    {
        /// <summary>
        /// Consultar uma chave de NFe somente para saber se a conexão com o webservice está ocorrendo corretamente e se quem está respondendo é o webservice correto.
        /// Efetua uma consulta por estado + ambiente para garantir que todos estão funcionando.
        /// </summary>
        /// <param name="tipoAmbiente">Ambiente para onde deve ser enviado a consulta situação</param>
        [Theory]
        [Trait("DFe", "NFCom")]
        [InlineData(TipoAmbiente.Homologacao)]
        [InlineData(TipoAmbiente.Producao)]
        public void ConsultarStatusServico(TipoAmbiente tipoAmbiente)
        {
            var xml = new ConsStatServNFCom
            {
                Versao = "1.00",
                TpAmb = tipoAmbiente
            };

            var configuracao = new Configuracao
            {
                TipoDFe = TipoDFe.NFCom,
                TipoEmissao = TipoEmissao.Normal,
                CertificadoDigital = PropConfig.CertificadoDigital,
                WebEnderecoProducao = "https://nfcom.svrs.rs.gov.br/WS/NFComStatusServico/NFComStatusServico.asmx",
                WebEnderecoHomologacao = "https://nfcom-homologacao.svrs.rs.gov.br/WS/NFComStatusServico/NFComStatusServico.asmx",
                WebSoapString = "<soap:Envelope xmlns:soap=\"http://www.w3.org/2003/05/soap-envelope\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"><soap:Body><nfcomDadosMsg xmlns=\"http://www.portalfiscal.inf.br/nfcom/wsdl/NFComStatusServico\">{xmlBody}</nfcomDadosMsg></soap:Body></soap:Envelope>",
            };

            var statusServico = new StatusServico(xml, configuracao);
            statusServico.Executar();

            Assert.True(configuracao.TipoAmbiente.Equals(tipoAmbiente), "Tipo de ambiente definido nas configurações diferente de " + tipoAmbiente.ToString());
            Assert.True(statusServico.Result.TpAmb.Equals(tipoAmbiente), "Webservice retornou um Tipo de ambiente diferente " + tipoAmbiente.ToString());
            Assert.True(statusServico.Result.CStat.Equals(107) || statusServico.Result.CStat.Equals(656), "Serviço não está em operação - <xMotivo>" + statusServico.Result.XMotivo + "<xMotivo>");
        }
    }
}