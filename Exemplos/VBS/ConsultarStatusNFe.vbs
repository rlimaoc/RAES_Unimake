Dim config
Dim consStatServ
Dim exceptionInterop
Dim statusServico

'----------------------------------------------------
'Criar configura��o b�sica para consumir o servi�o
'----------------------------------------------------

Set config = CreateObject("Unimake.Business.DFe.Servicos.Configuracao")
config.CertificadoSenha = "12345678"
config.CertificadoArquivo = "d:\projetos\UnimakePV.pfx"
config.TipoDfe = 0

'----------------------------------------------------
' Criar XML
'----------------------------------------------------
Set consStatServ = CreateObject("Unimake.Business.DFe.Xml.NFe.ConsStatServ")
consStatServ.Versao = "4.00"
consStatServ.TpAmb  = 2 'Homologa��o
consStatServ.CUF    = 41 ' PR

Set exceptionInterop = CreateObject("Unimake.Exceptions.ThrowHelper")
MsgBox "Exce��o do CSHARP: " + exceptionInterop.GetMessage()

'Consumir o servi�o
Set statusServico = CreateObject("Unimake.Business.DFe.Servicos.NFe.StatusServico")

On Error Resume Next
statusServico.Executar (consStatServ), (config)

if	Err.Number <> 0 then
	MsgBox "Exce��o do VBScript: " + Err.Description
	MsgBox "Exce��o do CSHARP: " + exceptionInterop.GetMessage()
end if 

