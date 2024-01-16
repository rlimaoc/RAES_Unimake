* ---------------------------------------------------------------------------------
* Gerar o XML do evento de comprovante de entrega da NFe e enviar para SEFAZ
* ---------------------------------------------------------------------------------
FUNCTION EnviarEventoComprovanteEntregaNFe()         
   LOCAL oConfiguracao, oExceptionInterop
   LOCAL oEnvEvento, oEvento, oDetEventoCompEntregaNFe, oInfEvento, oRecepcaoEvento    

 * Criar o objeto de configura��o m�nima
   oConfiguracao = CREATEOBJECT("Uni.Business.DFe.Servicos.Configuracao")
   oConfiguracao.TipoDFe = 0 && 0=NFe
   oConfiguracao.CertificadoArquivo = "C:\Projetos\certificados\UnimakePV.pfx"
   oConfiguracao.CertificadoSenha = "12345678"   

 * Criar XML 
  
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
 
 * Criar tag DetEventoCCE ###
   oDetEventoCompEntregaNFe = CreateObject("Uni.Business.DFe.Xml.NFe.DetEventoCompEntregaNFe")
   oDetEventoCompEntregaNFe.Versao = "1.00"
   oDetEventoCompEntregaNFe.COrgaoAutor = 41 && UFBrasil.PR
   oDetEventoCompEntregaNFe.TpAutor = 1 && TipoAutor.EmpresaEmitente
   oDetEventoCompEntregaNFe.VerAplic = "ERP 1.0"
   oDetEventoCompEntregaNFe.DhEntrega = DATETIME()
   oDetEventoCompEntregaNFe.NDoc = "00000000000" && Documento de quem assinou o comprovante
   oDetEventoCompEntregaNFe.XNome = "NOME DE QUEM ASSINOU O COMPROVANTE"
   oDetEventoCompEntregaNFe.LatGPS = "37.774929"
   oDetEventoCompEntregaNFe.LongGPS = "122.419418"
   oDetEventoCompEntregaNFe.HashComprovante = "2eDWGfx2xZJVFTKXGuiGZgzE2W4="
   oDetEventoCompEntregaNFe.DhHashComprovante = DATETIME()

 * Criar tag InfEvento
   oInfEvento = CreateObject("Uni.Business.DFe.Xml.NFe.InfEvento")
 
 * Referenciar o objeto oDetEventoCompEntregaNFe na Tag DetEvento
   oInfEvento.DetEvento = oDetEventoCompEntregaNFe
 
 * Atualizar propriedades da oInfEvento
 * IMPORTANTE: Atualiza��o da propriedade TpEvento deve acontecer depois que o DetEvento recebeu o oDetEventoCancCompEntregaNFe para que funcione sem erro
   oInfEvento.COrgao = 91 && UFBrasil.AN
   oInfEvento.ChNFe = "41191006117473000150550010000579281779843610"
   oInfEvento.CNPJ = "06117473000150"
   oInfEvento.DhEvento = DateTime()
   oInfEvento.TpEvento = 110130 && TipoEventoNFe.ComprovanteEntregaNFe ###
   oInfEvento.NSeqEvento = 1
   oInfEvento.VerEvento = "1.00"
   oInfEvento.TpAmb = 2 && TipoAmbiente.Homologacao   

 * Adicionar a tag InfEvento dentro da tag Evento
   oEvento.InfEvento = oInfEvento

 * Adicionar a tag Evento dentro da tag EnvEvento
   oEnvEvento.AddEvento(oEvento)
   
 * Resgatando alguns dados do objeto do XML do evento
   oConteudoEvento = oEnvEvento.GetEvento(0) && Evento � uma lista, ent�o tem que pegar, neste caso, sempre o primeiro conte�do. N�o teremos mais de um, pois n�o tem como enviar mais de um evento de comprovante de entrega.
 
   MESSAGEBOX(oEnvEvento.Versao)
   MESSAGEBOX(oConteudoEvento.InfEvento.COrgao)
   MESSAGEBOX(oConteudoEvento.InfEvento.CNPJ) 
   MESSAGEBOX(oConteudoEvento.InfEvento.DhEvento)
 
 * Criar objeto para pegar exce��o do lado do CSHARP
   oExceptionInterop = CREATEOBJECT("Unimake.Exceptions.ThrowHelper")

   TRY
    * Enviar evento
      oRecepcaoEvento = CREATEOBJECT("Uni.Business.DFe.Servicos.NFe.RecepcaoEvento")
      oRecepcaoEvento.Executar(oEnvEvento,  oConfiguracao)
      
      eventoAssinado = oRecepcaoEvento.GetConteudoXMLAssinado()
      MESSAGEBOX(eventoAssinado)
      
    * Gravar o XML assinado no HD, antes de enviar.
      DELETE FILE 'd:\testenfe\CartaCorrecao.xml'
	  STRTOFILE(eventoAssinado, 'd:\testenfe\ComprovanteDeEntregaNFe.xml', 0)      

      MESSAGEBOX("CStat do Lote Retornado: " + ALLTRIM(STR(oRecepcaoEvento.Result.CStat,10)) + " - XMotivo: " + oRecepcaoEvento.Result.XMotivo)
 
      IF oRecepcaoEvento.Result.CStat == 128 && 128 = Lote de evento processado com sucesso.
       * Como pode existir v�rios eventos no XML (Caso da carta de corre��o que posso enviar v�rias sequencias de evento)
       * � necess�rio fazer um loop para ver a autoriza��o de cada um deles
         FOR I = 1 TO oRecepcaoEvento.Result.GetRetEventoCount()
             oRetEvento = oRecepcaoEvento.Result.GetRetEvento(I - 1)
             
             DO CASE
                CASE oRetEvento.InfEvento.CStat = 135 && Evento homologado com vincula��o da respectiva NFe
                CASE oRetEvento.InfEvento.CStat = 136 && Evento homologado sem vincula��o com a respectiva NFe (SEFAZ n�o encontrou a NFe na base dela)
                CASE oRetEvento.InfEvento.CStat = 155 && Evento de Cancelamento homologado fora do prazo permitido para cancelamento 
                     oRecepcaoEvento.GravarXmlDistribuicao("tmp\testenfe") && Grava o XML de distribui��o
   				 
                   * Como pegar o nome do arquivo de distribui��o
                     oProcEventoNFe = oRecepcaoEvento.GetProcEventoNFeResult(0)
                     MESSAGEBOX(oProcEventoNFe.NomeArquivoDistribuicao)
   		  
               OTHERWISE    
                    * Evento rejeitado
                    * Realizar as a��es necess�rias
             ENDCASE
   		
             MESSAGEBOX("CStat do evento " + AllTrim(Str(I,10)) + ": " + ALLTRIM(STR(oRetEvento.InfEvento.CStat,10)) + " - xMotivo: " + oRetEvento.InfEvento.XMotivo)
         NEXT
      ENDIF 
       
   CATCH TO oErro
    * Excecao do FOXPRO
	* Mais sobre excecao em FOXPRO
	* http://www.yaldex.com/fox_pro_tutorial/html/2344b71b-14c0-4125-b001-b5fbb7bd1f05.htm
	
	  MESSAGEBOX("FOXPRO - ErrorCode: " + ALLTRIM(STR(oErro.ErrorNo,10))+ " - Message: " + oErro.Message)
	  
    * Excecao do CSHARP
      MESSAGEBOX("CSHARP - ErrorCode: " + ALLTRIM(STR(oExceptionInterop.GetErrorCode(),20)) + " - Message: " + oExceptionInterop.GetMessage())   
   ENDTRY      
RETURN      