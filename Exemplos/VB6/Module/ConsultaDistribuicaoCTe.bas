Attribute VB_Name = "ConsultaDistribuicaoCTe"
Option Explicit

Public Sub ConsultarDistribuicaoCTe()
On Error GoTo erro
Dim DistDFeInt, DistNSU, DistribuicaoDFe
Dim nsu     As String: nsu = "000000000000000"
Dim folder  As String: folder = "C:\temp\uninfe" ''<<<altere para um paste existente em sua m�quina

Log.ClearLog

Set DistDFeInt = CreateObject("Uni.Business.DFe.xml.CTe.DistDFeInt")
Set DistNSU = CreateObject("Uni.Business.DFe.xml.CTe.DistNSU")
Set DistribuicaoDFe = CreateObject("Uni.Business.DFe.Servicos.CTe.DistribuicaoDFe")


Do While True
    Log.EscreveLog "Aguarde, consultando NSU n�mero " & nsu, False
    DistNSU.UltNSU = nsu
    
    With DistDFeInt
        .Versao = "1.00"
        .TpAmb = TpAmb
        .CNPJ = "06117473000150"
        .CUFAutor = UFBrasil.PR
        Set .DistNSU = DistNSU
    End With

    DistribuicaoDFe.Executar (DistDFeInt), (Config.InicializarConfiguracao(TipoDFe.CTe))
    
    If (DistribuicaoDFe.result.CStat = 138) Then ''Documentos localizados
    
        If Not DirExists(folder) Then MkDir folder
        DistribuicaoDFe.GravarXMLDocZIP folder, True
        nsu = DistribuicaoDFe.result.UltNSU

        If CInt(DistribuicaoDFe.result.UltNSU) >= CInt(DistribuicaoDFe.result.MaxNSU) Then Exit Do
    Else
        Log.EscreveLog DistribuicaoDFe.result.XMotivo, False
        Exit Do
    End If
Loop

Log.EscreveLog "Consulta de distribui��o conclu�da com sucesso", False

Exit Sub
erro:
Utility.TrapException

End Sub

