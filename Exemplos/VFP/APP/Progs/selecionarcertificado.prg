selCertificado = CreateObject("Unimake.Security.Platform.CertificadoDigital")
SelecionarCertificado = selCertificado.Selecionar()

IF TYPE('SelecionarCertificado') = 'O'
	Aplicativo.CertificadoSelecionado.Selecionado = SelecionarCertificado

	lCertVencido = selCertificado.Vencido(SelecionarCertificado)
	Aplicativo.CertificadoSelecionado.Vencido = .f. 
	IF lCertVencido = .t. 
	     Aplicativo.CertificadoSelecionado.Vencido = .t. 
		=MESSAGEBOX("O Certificado est� Vencido")
	ENDIF 
ENDIF 

RELEASE selCertificado 
