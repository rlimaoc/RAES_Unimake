IF VerificarCertificadoSelecionado() = .F. 
	RETURN 0 
ENDIF 
VerificarVencimentoCertificado()
ConfiguracaoAtual(0,1)

consStatServ= CreateObject("Uni.Business.DFe.Xml.NFe.ConsStatServ")
consStatServ.Versao = "4.00"
consStatServ.CUF = 41
consStatServ.TpAmb = 2

statusServico = CreateObject("Uni.Business.DFe.Servicos.NFe.StatusServico")
statusServico.Executar(consStatServ,Aplicativo.Configuracao.Inicializar)

MESSAGEBOX(statusServico.RetornoWSString)
MESSAGEBOX(statusServico.Result.XMotivo)

RELEASE consStatServ
RELEASE statusServico 