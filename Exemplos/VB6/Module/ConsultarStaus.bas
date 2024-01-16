Attribute VB_Name = "ConsultaStatus"
Option Explicit
Public Sub ConsultarStatus()
On Error GoTo erro
Dim consStatServ
Dim statusServico

Log.ClearLog

Set consStatServ = CreateObject("Uni.Business.DFe.Xml.NFe.ConsStatServ")
consStatServ.Versao = "4.00"
consStatServ.CUF = UFBrasil.PR
consStatServ.TpAmb = TpAmb

Set statusServico = CreateObject("Uni.Business.DFe.Servicos.NFe.StatusServico")
statusServico.Executar (consStatServ), (Config.InicializarConfiguracao(TipoDFe.NFe))

Log.EscreveLog statusServico.RetornoWSString, True
Log.EscreveLog statusServico.result.XMotivo, False

Exit Sub
erro:
Utility.TrapException
End Sub
