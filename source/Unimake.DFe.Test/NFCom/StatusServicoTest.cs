using Unimake.Business.DFe.Servicos;
using Unimake.Business.DFe.Servicos.NFCom;
using Unimake.Business.DFe.Xml.NFCom;
using Xunit;

namespace Unimake.DFe.Test.NFCom
{
    /// <summary>
    /// Testar o serviço de consulta protocolo da NFe
    /// </summary>
    public class StatusServicoTest
    {
        public void ConsultarStatusServico(TipoAmbiente tipoAmbiente)
        {
            var xml = new ConsStatServNFCom
            {
                Versao = "4.00",
                TpAmb = tipoAmbiente
            };

            var configuracao = new Configuracao
            {
                TipoDFe = TipoDFe.NFCom,
                TipoEmissao = TipoEmissao.Normal,
                CertificadoDigital = PropConfig.CertificadoDigital
            };

            var statusServico = new StatusServicoNFCom(xml, configuracao);
            statusServico.Executar();

            Assert.True(configuracao.TipoAmbiente.Equals(tipoAmbiente), "Tipo de ambiente definido nas configurações diferente de " + tipoAmbiente.ToString());
            Assert.True(statusServico.Result.TpAmb.Equals(tipoAmbiente), "Webservice retornou um Tipo de ambiente diferente " + tipoAmbiente.ToString());
            Assert.True(statusServico.Result.CStat.Equals(107) || statusServico.Result.CStat.Equals(656), "Serviço não está em operação - <xMotivo>" + statusServico.Result.XMotivo + "<xMotivo>");
        }
    }
}