IF VerificarCertificadoSelecionado() = .F. 
	RETURN 0 
ENDIF 
VerificarVencimentoCertificado()
ConfiguracaoAtual(0,1)

ConsReciNFe = CreateObject("Uni.Business.DFe.Xml.NFe.ConsReciNFe")
retAutorizacao = CreateObject("Uni.Business.DFe.Servicos.NFe.RetAutorizacao")
ConsReciNFe.Versao = "4.00"
ConsReciNFe.TpAmb = 2
ConsReciNFe.NRec = "310000069231900"

TRY 
	retAutorizacao.Executar(ConsReciNFe,Aplicativo.Configuracao.Inicializar) 
CATCH TO cErro
	=MESSAGEBOX("N�o foi poss�vel acessar o Servidor " + cErro.Message,0,'')
ENDTRY 	

MESSAGEBOX(retAutorizacao.RetornoWSString)
MESSAGEBOX(retAutorizacao.Result.XMotivo)

RELEASE ConsReciNFe 
RELEASE retAutorizacao