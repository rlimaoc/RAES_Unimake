IF VerificarCertificadoSelecionado() = .F. 
	RETURN 0 
ENDIF 
VerificarVencimentoCertificado()
ConfiguracaoAtual(0,1)

RecepcaoEvento = CreateObject("Uni.Business.DFe.Servicos.NFe.RecepcaoEvento")
EnvEvento = CreateObject("Uni.Business.DFe.Xml.NFe.EnvEvento")
Evento = CreateObject("Uni.Business.DFe.Xml.NFe.Evento")
InfEvento = CreateObject("Uni.Business.DFe.Xml.NFe.InfEvento")
DetEventoManif = CreateObject("Uni.Business.DFe.Xml.NFe.DetEventoManif")

DetEventoManif.Versao = "1.00"
DetEventoManif.DescEvento = "Confirmacao da Operacao"
DetEventoManif.XJust = "Justificativa para manifesta��o da NFe de teste"
          
InfEvento.DetEvento = DetEventoManif
InfEvento.COrgao = 91
InfEvento.ChNFe = "41191000563803000154550010000020901551010553"
InfEvento.CNPJ = "06117473000150"
InfEvento.DhEvento = DATETIME()
InfEvento.TpEvento = 210200
InfEvento.NSeqEvento = 1
InfEvento.VerEvento = "1.00"
InfEvento.TpAmb = 2

Evento.Versao = "1.00"
Evento.InfEvento = InfEvento

EnvEvento.AddEvento(Evento)
EnvEvento.Versao = "1.00"
EnvEvento.IdLote = "000000000000001"

RecepcaoEvento.Executar(EnvEvento,Aplicativo.Configuracao.Inicializar)

*Gravar o XML de distribui��o se a inutiliza��o foi homologada
If RecepcaoEvento.result.CStat = 128  && 128 = Lote de evento processado com sucesso

    CStat = RecepcaoEvento.result.GetRetEvento(0).InfEvento.CStat
    
   * 135: Evento homologado com vincula��o da respectiva NFe
   * 136: Evento homologado sem vincula��o com a respectiva NFe (SEFAZ n�o encontrou a NFe na base dela)
   * 155: Evento de Cancelamento homologado fora do prazo permitido para cancelamento
    DO CASE 
        Case CStat = 135 .OR. CStat = 136 .OR. CStat = 155
        
            MESSAGEBOX(recepcaoEvento.Result.GetRetEvento(0).InfEvento.DhRegEvento)
			MESSAGEBOX(recepcaoEvento.Result.GetRetEvento(0).InfEvento.NProt)
			
            RecepcaoEvento.GravarXmlDistribuicao(FULLPATH(CURDIR())+'Retorno\')
        OTHERWISE  && Evento rejeitado
            MESSAGEBOX("Evento Rejeitado")
    ENDCASE 
ENDIF 

MESSAGEBOX(RecepcaoEvento.RetornoWSString)

RELEASE RecepcaoEvento 
RELEASE EnvEvento
RELEASE Evento
RELEASE InfEvento
RELEASE DetEventoManif
