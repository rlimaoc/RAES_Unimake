* ---------------------------------------------------------------------------------
* Enviar evento de manifestacao da NFe
* ---------------------------------------------------------------------------------
Function EnviarEventoManifestacaoNFe()
   Local oErro, oExceptionInterop 
   Local oConfiguracao, I, eventoAssinado
   Local oEnvEvento, oEvento, oDetEventoManif, oInfEvento, oRecepcaoEvento, oRetEvento 
     
 * Criar configura�ao b�sica para consumir o servi�o
   oConfiguracao = CreateObject("Uni.Business.DFe.Servicos.Configuracao")
   oConfiguracao.TipoDfe = 0 && 0=nfe
   oConfiguracao.CertificadoSenha = "12345678"
   oConfiguracao.CertificadoArquivo = "C:\Projetos\certificados\UnimakePV.pfx"

 * Criar tag EnvEvento
   oEnvEvento = CreateObject("Uni.Business.DFe.Xml.NFe.EnvEvento")
   oEnvEvento.Versao = "1.00"
   oEnvEvento.IdLote = "000000000000001"

 * -------------------------------------------------
 * Criar tags do evento sequencia 1
 * -------------------------------------------------
 * Criar tag Evento
   oEvento = CreateObject("Uni.Business.DFe.Xml.NFe.Evento")
   oEvento.Versao = "1.00"
 
 * Criar tag DetEventoCanc
   oDetEventoManif = CreateObject("Uni.Business.DFe.Xml.NFe.DetEventoManif") && ###
   oDetEventoManif.Versao = "1.00"
   oDetEventoManif.DescEvento = "Ciencia da Operacao"
 * oDetEventoManif.XJust = "Justificativa para cancelamento da NFe de teste" && S� � necess�rio se for "Opera��o n�o realizada" ou "Desconhecimento da Opera��o"

 * Criar tag InfEvento
   oInfEvento = CreateObject("Uni.Business.DFe.Xml.NFe.InfEvento")
 
 * Adicionar a tag DetEventoCanc dentro da Tag DetEvento
   oInfEvento.DetEvento = oDetEventoManif
 
 * Atualizar propriedades da oInfEvento
 * IMPORTANTE: Atualiza��o da propriedade TpEvento deve acontecer depois que o DetEvento recebeu o oDetEventoCanc para que funcione sem erro
   oInfEvento.COrgao = 91 && UFBrasil.AN (Ambiente Nacional) ###
   oInfEvento.ChNFe = "41191006117473000150550010000579281779843610"
   oInfEvento.CNPJ = "06117473000150"
   oInfEvento.DhEvento = DateTime()
   oInfEvento.TpEvento = 210210 && 210240 = TipoEventoNFe.ManifestacaoOperacaoNaoRealizada ###
                                && 210220 = TipoEventoNFe.ManifestacaoDesconhecimentoOperacao
                                && 210210 = TipoEventoNFe.ManifestacaoCienciaOperacao
                                && 210200 = TipoEventoNFe.ManifestacaoConfirmacaoOperacao
   oInfEvento.NSeqEvento = 1
   oInfEvento.VerEvento = "1.00"
   oInfEvento.TpAmb = 2 && TipoAmbiente.Homologacao

 * Adicionar a tag InfEvento dentro da tag Evento
   oEvento.InfEvento = oInfEvento

 * Adicionar a tag Evento dentro da tag EnvEvento
   oEnvEvento.AddEvento(oEvento)   
   
 * Resgatando alguns dados do objeto do XML do evento
   MESSAGEBOX("Versao schema: " + oEnvEvento.Versao + " - LOTE: " + oEnvEvento.IdLote)
   MESSAGEBOX("Qde eventos: " + ALLTRIM(STR(oEnvEvento.GetEventoCount(),10)))
  
   For I = 1 To oEnvEvento.GetEventoCount()
       oTagEvento = oEnvEvento.GetEvento(I - 1) 
       MESSAGEBOX("SEQUENCIA EVENTO: " + ALLTRIM(STR(oTagEvento.InfEvento.NSeqEvento,10)) + " - ORGAO: " + ALLTRIM(STR(oTagEvento.InfEvento.COrgao,10)))
   Next I
  
 * Criar objeto para pegar exce��o do lado do CSHARP
   oExceptionInterop = CreateObject("Uni.Exceptions.ThrowHelper")   
   
   Try
    * Enviar evento
      oRecepcaoEvento = CreateObject("Uni.Business.DFe.Servicos.NFe.RecepcaoEvento")
      oRecepcaoEvento.Executar(oEnvEvento, oConfiguracao)

      eventoAssinado = oRecepcaoEvento.GetConteudoXMLAssinado()
      MESSAGEBOX(eventoAssinado)
      
    * Gravar o XML assinado no HD, antes de enviar.
      DELETE FILE 'd:\testenfe\ManifestacaoNFe.xml'
	  StrToFile(eventoAssinado, 'd:\testenfe\ManifestacaoNFe.xml', 0)    

      MESSAGEBOX("CStat do Lote Retornado: " + ALLTRIM(STR(oRecepcaoEvento.Result.CStat,10)) + " - XMotivo: " + oRecepcaoEvento.Result.XMotivo)
      MESSAGEBOX(oRecepcaoEvento.RetornoWSString)
      
      if oRecepcaoEvento.Result.CStat == 128 && 128 = Lote de evento processado com sucesso.
       * Como pode existir v�rios eventos no XML (Caso da carta de corre��o que posso enviar v�rias sequencias de evento)
       * � necess�rio fazer um loop para ver a autoriza��o de cada um deles
         For I = 1 To oRecepcaoEvento.Result.GetRetEventoCount()
             oRetEvento = oRecepcaoEvento.Result.GetRetEvento(I - 1)
   		  
             DO CASE
                CASE oRetEvento.InfEvento.CStat = 135 && Evento homologado com vincula��o da respectiva NFe
                CASE oRetEvento.InfEvento.CStat = 136 && Evento homologado sem vincula��o com a respectiva NFe (SEFAZ n�o encontrou a NFe na base dela)
                CASE oRetEvento.InfEvento.CStat = 155 && Evento de Cancelamento homologado fora do prazo permitido para cancelamento 
                     oRecepcaoEvento.GravarXmlDistribuicao("d:\testenfe") && Grava o XML de distribui��o
   				 
               OTHERWISE    
                    * Evento rejeitado
                    * Realizar as a��es necess�rias
             ENDCASE
   		
             MESSAGEBOX("CStat do evento " + AllTrim(Str(I,10)) + ": " + ALLTRIM(STR(oRetEvento.InfEvento.CStat,10)) + " - xMotivo: " + oRetEvento.InfEvento.XMotivo)
         Next
      EndIf 

   Catch To oErro
    * Excecao do FOXPRO
	* Mais sobre excecao em FOXPRO
	* http://www.yaldex.com/fox_pro_tutorial/html/2344b71b-14c0-4125-b001-b5fbb7bd1f05.htm
	
	  MessageBox("FOXPRO - ErrorCode: " + ALLTRIM(STR(oErro.ErrorNo,10))+ " - Message: " + oErro.Message)
	  
    * Excecao do CSHARP
      MessageBox("CSHARP - ErrorCode: " + ALLTRIM(STR(oExceptionInterop.GetErrorCode(),20)) + " - Message: " + oExceptionInterop.GetMessage())
   ENDTRY
RETURN
