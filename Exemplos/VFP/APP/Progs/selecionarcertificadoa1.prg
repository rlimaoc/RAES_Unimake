dlg = GETFILE('pfx', 'Arquivo PFX (*.pfx)','',1,'Selecionar arquivo A1')
GetPFX = dlg
caminhoPFX = GetPFX
senhaCertificado = InputBox("Informe a senha do certificado", "Senha do certificado")

IF EMPTY(senhaCertificado)
	=MESSAGEBOX("Necess�rio informar correntamente o Certificado Digital")
	RETURN 0
ENDIF 

selCertificado = CreateObject("Unimake.Security.Platform.CertificadoDigital")

TRY 
	SelecionarCertificadoDeArquivo = selCertificado.CarregarCertificadoDigitalA1(caminhoPFX, senhaCertificado)
CATCH TO cErro
	=MESSAGEBOX("N�o foi poss�vel acessar o certificado " + cErro.Message,0,'')
ENDTRY 	

IF VARTYPE(SelecionarCertificadoDeArquivo) <> "U"
	Aplicativo.CertificadoSelecionado.Selecionado = SelecionarCertificadoDeArquivo 
	
	lCertVencido = selCertificado.Vencido(SelecionarCertificadoDeArquivo)
	Aplicativo.CertificadoSelecionado.Vencido = .f. 
	IF lCertVencido = .t. 
	     Aplicativo.CertificadoSelecionado.Vencido = .t. 
		=MESSAGEBOX("O Certificado est� Vencido")
	ENDIF 
ENDIF 

RELEASE selCertificado 