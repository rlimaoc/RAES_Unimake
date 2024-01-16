Attribute VB_Name = "ManifestacaoNFe"
Option Explicit
Public Sub ManifestarNFe()
On Error GoTo erro
Dim EnvEvento, RecepcaoEvento, Evento, InfEvento, DetEventoManif, CStat

Log.ClearLog

Set RecepcaoEvento = CreateObject("Uni.Business.DFe.Servicos.NFe.RecepcaoEvento")
Set EnvEvento = CreateObject("Uni.Business.DFe.Xml.NFe.EnvEvento")
Set Evento = CreateObject("Uni.Business.DFe.Xml.NFe.Evento")
Set InfEvento = CreateObject("Uni.Business.DFe.Xml.NFe.InfEvento")
Set DetEventoManif = CreateObject("Uni.Business.DFe.Xml.NFe.DetEventoManif")

With DetEventoManif
    .Versao = "1.00"
    .DescEvento = "Confirmacao da Operacao"
    .XJust = "Justificativa para manifesta��o da NFe de teste"
End With
              
With InfEvento
    Set .DetEvento = DetEventoManif
    .COrgao = AN
    .ChNFe = "41191000563803000154550010000020901551010553"
    .CNPJ = "06117473000150"
    .DhEvento = DateTime.Now
    .TpEvento = 210200
    .NSeqEvento = 1
    .VerEvento = "1.00"
    .TpAmb = TpAmb
End With
    
Evento.Versao = "1.00"
Set Evento.InfEvento = InfEvento

EnvEvento.AddEvento (Evento)
EnvEvento.Versao = "1.00"
EnvEvento.IdLote = "000000000000001"

RecepcaoEvento.Executar (EnvEvento), (Config.InicializarConfiguracao(TipoDFe.NFe))

''Gravar o XML de distribui��o se a inutiliza��o foi homologada
If (RecepcaoEvento.result.CStat = 128) Then ''128 = Lote de evento processado com sucesso
    CStat = RecepcaoEvento.result.GetRetEvento(0).InfEvento.CStat
    
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


