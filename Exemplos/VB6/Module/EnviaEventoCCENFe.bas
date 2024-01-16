Attribute VB_Name = "EventoCCENFe"
Option Explicit
Public Sub EnviarEventoCCENFe()
On Error GoTo erro
Dim EnvEvento, RecepcaoEvento, CStat

Log.ClearLog

Set RecepcaoEvento = CreateObject("Uni.Business.DFe.Servicos.NFe.RecepcaoEvento")
Set EnvEvento = CreateObject("Uni.Business.DFe.Xml.NFe.EnvEvento")

EnvEvento.AddEvento (CriarEvento("CFOP errada, segue CFOP correta.", 1))
EnvEvento.AddEvento (CriarEvento("Nome do transportador est� errado, segue nome correto.", 2))

EnvEvento.Versao = "1.00"
EnvEvento.IdLote = "000000000000001"

RecepcaoEvento.Executar (EnvEvento), (Config.InicializarConfiguracao(TipoDFe.NFe))

''Gravar o XML de distribui��o se a inutiliza��o foi homologada
If (RecepcaoEvento.result.CStat = 128) Then ''128 = Lote de evento processado com sucesso
    CStat = RecepcaoEvento.result.GetEvento(0).InfEvento.CStat
    
    '' 135: Evento homologado com vincula��o da respectiva NFe
    '' 136: Evento homologado sem vincula��o com a respectiva NFe (SEFAZ n�o encontrou a NFe na base dela)
    '' 155: Evento de Cancelamento homologado fora do prazo permitido para cancelamento
                        
    Select Case CStat
        Case 135, 136, 155
            RecepcaoEvento.GravarXmlDistribuicao "C:\temp\"
        Case Else ''Evento rejeitado
            Log.EscreveLog "Evento rejeitado", False
    End Select
End If

Log.EscreveLog RecepcaoEvento.RetornoWSString, True

Exit Sub
erro:
Utility.TrapException

End Sub


Function CriarEvento(ByVal XCorrecao As String, ByVal NSeqEvento As Integer)
Dim DetEventoCCE, Evento, InfEvento
Set InfEvento = CreateObject("Uni.Business.DFe.Xml.NFe.InfEvento")
Set DetEventoCCE = CreateObject("Uni.Business.DFe.Xml.NFe.DetEventoCCE")
Set Evento = CreateObject("Uni.Business.DFe.Xml.NFe.Evento")

With DetEventoCCE
    .XCorrecao = XCorrecao
    .Versao = "1.00"
End With
              
With InfEvento
    Set .DetEvento = DetEventoCCE
    .COrgao = UFBrasil.PR
    .ChNFe = "41191006117473000150550010000579281779843610"
    .CNPJ = "06117473000150"
    .DhEvento = DateTime.Now
    .TpEvento = 110110
    .NSeqEvento = NSeqEvento
    .VerEvento = "1.00"
    .TpAmb = TpAmb
End With
    
Evento.Versao = "1.00"
Set Evento.InfEvento = InfEvento
Set CriarEvento = Evento
End Function
