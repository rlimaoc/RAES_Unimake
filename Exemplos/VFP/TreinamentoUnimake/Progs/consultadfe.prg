* -----------------------------------------------------------------------------------
* Consulta de documentos fiscais destinados / Consulta NFe�s emitidas contra meu CNPJ
* -----------------------------------------------------------------------------------
FUNCTION ConsultaDFe()
   LOCAL oConfig
   LOCAL oDistDFeInt, oDistNSU
   LOCAL oErro, oExceptionInterop
   LOCAL oDistribuicaoDFe
   LOCAL nsu, folder
   
 * Criar configura��o b�sica para consumir o servi�o
   oConfig = CREATEOBJECT("Uni.Business.DFe.Servicos.Configuracao")
   oConfig.CertificadoArquivo = "C:\Projetos\certificados\UnimakePV.pfx"
   oConfig.CertificadoSenha = "12345678"  
   
   nsu = "000000000000000"
   
 * Criar objeto para pegar exce��o do lado do CSHARP
   oExceptionInterop = CREATEOBJECT("Unimake.Exceptions.ThrowHelper")
   
   TRY
      DO WHILE .T.
       * Criar XML da consulta DFe
         oDistDFeInt = CREATEOBJECT("Uni.Business.DFe.Xml.NFe.DistDFeInt")
         oDistDFeInt.Versao = "1.35" && ou 1.01
         oDistDFeInt.TpAmb= 1 && 1=Homologa��o
         oDistDFeInt.CNPJ = "06117473000150"
         oDistDFeInt.CUFAutor = 41 && UFBrasil.PR
         
         oDistNSU = CREATEOBJECT("Uni.Business.DFe.Xml.NFe.DistNSU")
         oDistNSU.UltNSU = nsu
         
         oDistDFeInt.DistNSU = oDistNSU

       * Consumir o servi�o (enviar o XML de consulta e tratar o retorno do webservice)
         oDistribuicaoDFe = CREATEOBJECT("Uni.Business.DFe.Servicos.NFe.DistribuicaoDFe")
         oDistribuicaoDFe.Executar(oDistDFeInt, oConfig)
         
         MESSAGEBOX(oDistribuicaoDFe.RetornoWSString)
         
       * Gravar o XML retornado da receita federal no HD
         DELETE FILE 'd:\testenfe\' + 'RetornoDfe-' + nsu + '.xml'
	     StrToFile(oDistribuicaoDFe.RetornoWSString, 'd:\testenfe\' + 'RetornoDfe-' + nsu + '.xml', 0)
         
         IF oDistribuicaoDFe.Result.CStat = 138 && 138=Documentos localizados
          * Pasta onde vamos gravar os XMLs retornados pela SEFAZ
            folder = "d:\testenfe\doczip"

          * Salvar o XML retornados na consulta            
          * <param name="folder">Nome da pasta onde � para salvar os XML</param>
          * <param name="saveXMLSummary">Salvar os arquivos de resumo da NFe e Eventos?</param>
          * <param name="fileNameWithNSU">true=Salva os arquivos da NFe e seus eventos com o NSU no nome do arquivo / false=Salva os arquivos da NFe e seus eventos com o CHAVE da NFe no nome do arquivo</param>
            oDistribuicaoDFe.GravarXMLDocZIP(folder, .T., .T.) 
            
          * Como pegar o conte�do retornado na consulta no formato string
            FOR I = 1 TO oDistribuicaoDFe.Result.LoteDistDFeInt.GetDocZipCount()
                oDocZip = oDistribuicaoDFe.Result.LoteDistDFeInt.GetDocZip(I-1)
                
              * Conteudo do XML retornado no formato string
                MESSAGEBOX(oDocZip.ConteudoXML)
                
              * Tipo do XML:
              * 1 = XML de resumo de eventos
              * 2 = XML de resumo da NFe
              * 3 = XML de distribui��o de eventos da NFe (XML completo do evento)
              * 4 = XML de distribui��o da NFe (XML completo da NFe)
              * 5 = XML de distribui��o de eventos da CTe (XML completo do evento)
              * 6 = XML de distribui��o do CTe (XML completo do CTe)
              * 0 = XML desconhecido
                MESSAGEBOX(oDocZip.TipoXML)
            NEXT I
            
          * Como pegar os retornos dos resumos de eventos em objeto
            FOR I = 1 TO oDistribuicaoDFe.GetResEventosCount()
                oResEvento = oDistribuicaoDFe.GetResEvento(I-1)
                
                MESSAGEBOX(oResEvento.ChNFe)
                MESSAGEBOX(oResEvento.CNPJ)
            NEXT I   
            
          * Como pegar os retornos dos resumos de NFe em objeto
            FOR I = 1 TO oDistribuicaoDFe.GetResNFeCount()
                oResNFe = oDistribuicaoDFe.GetResNFe(I-1)
                
                MESSAGEBOX(oResNFe.ChNFe)
                MESSAGEBOX(oResNFe.CNPJ)
            NEXT I
            
          * Como pegar os retornos dos XML de Distribui��o dos Eventos (XML completos dos eventos)
            FOR I = 1 TO oDistribuicaoDFe.GetProcEventoNFesCount()
                oProcEventoNFe = oDistribuicaoDFe.GetProcEventoNFes(I-1)
                
                MESSAGEBOX(oProcEventoNFe.Evento.InfEvento.CNPJ)
                MESSAGEBOX(oProcEventoNFe.Evento.InfEvento.ChNFe)
            NEXT I   

          * Como pegar os retornos dos XML de Distribui��o das NFes (XML completos das NFes)
            FOR I = 1 TO oDistribuicaoDFe.GetProcNFesCount()
                oNfeProc = oDistribuicaoDFe.GetProcNFes(I-1)
                
                oInfNFe = oNfeProc.NFe.GetInfNFe(0)

                MESSAGEBOX(oInfNFe.Id)
                MESSAGEBOX(oInfNFe.IDE.CUF)
                MESSAGEBOX(oInfNFe.IDE.CNF)                
                MESSAGEBOX(oNFeProc.ProtNFe.InfProt.ChNFe)
                MESSAGEBOX(oNFeProc.ProtNFe.InfProt.NProt)
             NEXT I        
         ELSE 
            IF oDistribuicaoDFe.Result.CStat = 656 && 656 = Consumo indevido
               * Abortar a opera��o e s� voltar a consultar novamente ap�s 1 hora (nossa experiencia nos levou a usar 1h10m)
               EXIT
            ENDIF 
         ENDIF      

       * Salvar o conteudo da vari�vel "nsu" na base de dados para que na proxima consulta continue de 
       * onde parou para n�o gerar consumo indevido
         nsu = oDistribuicaoDFe.Result.UltNSU
         
       * Se atingiu o maxNSU tem que parar as consultas e iniciar novamente ap�s 1 hora (para n�o gerar consumo indevido e nossa experiencia nos levou a usar 1h10m), o mesmo processo, s� que n�o mais
       * do nsu ZERO e sim do que ficou na vari�vel "nsu" acima, ou seja, vai dar sequencia.  
         IF STR(oDistribuicaoDFe.Result.UltNSU,15) >= STR(oDistribuicaoDFe.Result.MaxNSU)  
            EXIT
         ENDIF
      ENDDO
	  
    CATCH TO oErro
    * Exce��o do FOXPRO
	* Mais sobre exce��o em FOXPRO
	* http://www.yaldex.com/fox_pro_tutorial/html/2344b71b-14c0-4125-b001-b5fbb7bd1f05.htm
	
	  MESSAGEBOX(oErro.ErrorNo)
	  MESSAGEBOX("Exce��o foxpro: " + oErro.Message)
	  
    * Exce��o do CSHARP
      MESSAGEBOX(oExceptionInterop.GetMessage())
      MESSAGEBOX(oExceptionInterop.GetErrorCode())
   ENDTRY   
RETURN