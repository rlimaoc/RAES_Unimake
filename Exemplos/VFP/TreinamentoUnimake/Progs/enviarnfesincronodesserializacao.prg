* ---------------------------------------------------------------------------------
* Gerar XML da NFe e enviar no modo s�ncrono com desserializa��o do XML
*
* - Desserializa��o do XML da NFe/NFCe (J� tenho o arquivo do XML pronto e quero 
*   envi�-lo para SEFAZ sem precisar alimentar as propriedades da classe do XML)
* - Finalizando o envio da NFe/NFCe pela consulta situa��o (Enviei o XML da nota 
*   e n�o consegui pegar o retorno, como fa�o para finalizar a nota e 
*   gerar o XML de distribui��o?)
* - Enviei a nota e deu duplicidade, como fa�o para, somente, gerar o XML de 
*   distribui��o da NFe/NFCe?
* ---------------------------------------------------------------------------------
Function EnviarNfeSincronoDesserializacao()
   Local oConfig
   Local oEnviNFe, oNfe, oInfNFe, oIde, oEmit, oEnderEmit, oDest, oEnderDest
   Local oDet, oProd
   Local oImposto, oICMS, oICMSSN101, oPIS, oPISOutr, oCOFINS, oCOFINSOutr
   Local oTotal, oICMSTot, oImpostoDevol
   Local oTransp, oVol
   Local oCobr, oFat, oDup
   Local oPag, oDetPag
   Local oInfAdic, oInfRespTec
   Local oAutorizacao, oRetAutorizacao, oXmlRec, oConfigRec
   Local I, oErro, notaAssinada
   Local oXmlConsSitNFe, oConteudoNFe, oConteudoInfNFe, chaveNFe, oConfigConsSitNFe, oConsultaProtocolo

 * Criar configuracao basica para consumir o servico
   oConfig = CreateObject("Uni.Business.DFe.Servicos.Configuracao")
   oConfig.TipoDfe = 0 && 0=nfe
   oConfig.TipoEmissao = 1 && 1=Normal
   oConfig.CertificadoArquivo = "C:\Projetos\certificados\UnimakePV.pfx"
   oConfig.CertificadoSenha = "12345678"
   
 * Criar a tag <enviNFe>
   oEnviNFe = CreateObject("Uni.Business.DFe.Xml.NFe.EnviNFe")
   oEnviNFe.Versao = "4.00"
   oEnviNFe.IdLote = "000000000000001"
   oEnviNFe.IndSinc = 1 && 1=Sim 0=Nao
  
 * Criar a tag NFe e deserializar o XML j� gravado no HD para j� preencher o objeto para envio
   onfe = CreateObject("Uni.Business.DFe.Xml.NFe.NFe")
   
   xmlstring = [<?xml version="1.0" encoding="UTF-8" ?><NFe xmlns="http://www.portalfiscal.inf.br/nfe"><infNFe Id="NFe43230492797901000174650200000000021000000577" versao="4.00"><ide><cUF>43</cUF><cNF>00000057</cNF>]
   xmlstring = xmlstring + [<natOp>VENDA DE MERCADORIA</natOp><mod>65</mod><serie>20</serie><nNF>2</nNF><dhEmi>2023-04-26T10:08:03-03:00</dhEmi><tpNF>1</tpNF><idDest>1</idDest><cMunFG>4314902</cMunFG><tpImp>4</tpImp><tpEmis>1</tpEmis>]
   xmlstring = xmlstring + [<cDV>7</cDV><tpAmb>2</tpAmb><finNFe>1</finNFe><indFinal>1</indFinal><indPres>1</indPres><procEmi>0</procEmi><verProc>1.4.229</verProc></ide><emit><CNPJ>92797901000174</CNPJ>]
   xmlstring = xmlstring + [<xNome>GREMIO FOOT-BALL PORTO ALEGRENSE</xNome><xFant>GREMIOMANIA ARENA</xFant><enderEmit><xLgr>AV PADRE LEOPOLDO BRENTANO</xLgr><nro>110/2100</nro><xBairro>HUMAITA</xBairro>]
   xmlstring = xmlstring + [<cMun>4314902</cMun><xMun>PORTO ALEGRE</xMun><UF>RS</UF><CEP>90250590</CEP><cPais>1058</cPais><xPais>BRASIL</xPais><fone>5132182000</fone></enderEmit><IE>0960578617</IE><CRT>3</CRT></emit>]
   xmlstring = xmlstring + [<det nItem="1"><prod><cProd>2050000017377</cProd><cEAN>SEM GTIN</cEAN><xProd>NOTA FISCAL EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL</xProd><NCM>61052000</NCM><CFOP>5102</CFOP>]
   xmlstring = xmlstring + [<uCom>UN</uCom><qCom>1.0000</qCom><vUnCom>169.9000</vUnCom><vProd>169.90</vProd>]
   xmlstring = xmlstring + [<cEANTrib>SEM GTIN</cEANTrib><uTrib>UN</uTrib><qTrib>1.0000</qTrib><vUnTrib>169.9000</vUnTrib><indTot>1</indTot></prod><imposto><vTotTrib>37.72</vTotTrib><ICMS><ICMS00><orig>0</orig>]
   xmlstring = xmlstring + [<CST>00</CST><modBC>0</modBC><vBC>169.90</vBC><pICMS>17.00</pICMS><vICMS>28.88</vICMS></ICMS00></ICMS><PIS><PISNT><CST>07</CST></PISNT></PIS><COFINS><COFINSNT><CST>07</CST></COFINSNT>]
   xmlstring = xmlstring + [</COFINS></imposto></det><total><ICMSTot><vBC>169.90</vBC><vICMS>28.88</vICMS><vICMSDeson>0.00</vICMSDeson><vFCP>0.00</vFCP><vBCST>0.00</vBCST><vST>0.00</vST><vFCPST>0.00</vFCPST>]
   xmlstring = xmlstring + [<vFCPSTRet>0.00</vFCPSTRet><vProd>169.90</vProd><vFrete>0.00</vFrete><vSeg>0.00</vSeg><vDesc>0.00</vDesc><vII>0.00</vII><vIPI>0.00</vIPI><vIPIDevol>0.00</vIPIDevol><vPIS>0.00</vPIS>]
   xmlstring = xmlstring + [<vCOFINS>0.00</vCOFINS><vOutro>0.00</vOutro><vNF>169.90</vNF><vTotTrib>37.72</vTotTrib></ICMSTot></total><transp><modFrete>9</modFrete></transp><pag><detPag><tPag>01</tPag><vPag>169.90</vPag>]
   xmlstring = xmlstring + [</detPag></pag><infAdic><infCpl>OPERADOR: EDUARDO PERES CX-020;VENDEDOR: MEGASTORE *ARENA;</infCpl></infAdic></infNFe></NFe>]
   
*   oEnviNFe.AddNFe(oNFe.LoadFromFile("D:\testenfe\41230206117473000150550010000590081999182930-nfe.xml")) 
   
   oEnviNFe.AddNFe(oNFe.LoadFromXml(xmlstring))

 * Como deserializar partindo da string do XML
   && oEnviNFe.AddNFe(oNFe.LoadFromXML("asldkjaslkdjasldjaslkdjasldkjasldksjadas"))   
   
 * Recuperar a chave da NFe:
   oConteudoNFe = oEnviNFe.GetNFe(0)
   oConteudoInfNFe = oConteudoNFe.GetInfNFe(0)
   chaveNFe = oConteudoInfNFe.Chave
		 
   MessageBox("Chave da NFe:" + chaveNFe)

 * Consumir o serviço (Enviar NFE para SEFAZ)
   oAutorizacao = CreateObject("Uni.Business.DFe.Servicos.NFe.Autorizacao")
   
 * Criar objeto para pegar excecao do lado do CSHARP
   oExceptionInterop = CreateObject("Uni.Exceptions.ThrowHelper")   

   Try
      oAutorizacao.SetXMLConfiguracao(oEnviNFe, oConfig)      
	  
    * Pode-se gravar o conteudo do XML assinado na base de dados antes do envio, caso queira recuperar para futuro tratamento, isso da garantias
	  notaAssinada = oAutorizacao.GetConteudoNFeAssinada(0)
      MessageBox(notaAssinada) && Demonstrar o XML da nota assinada na tela

    * Gravar o XML assinado no HD, antes de enviar.
      DELETE FILE 'd:\testenfe\' + chaveNFe + '-nfe.xml'
	  StrToFile(notaAssinada, 'd:\testenfe\' + chaveNFe + '-nfe.xml', 0)  
  
    * Enviar a nota para SEFAZ
	  oAutorizacao.Executar(oEnviNFe, oConfig) 
	  
    * XML Retornado pela SEFAZ
      MessageBox(oAutorizacao.RetornoWSString)

    * Codigo de Status e Motivo
      MessageBox(AllTrim(Str(oAutorizacao.Result.CStat,5)) + " " +oAutorizacao.Result.XMotivo)
  
	  if oAutorizacao.Result.CStat == 104 && 104 = Lote Processado
         if oAutorizacao.Result.ProtNFe.InfProt.CStat == 100 && 100 = Autorizado o uso da NF-e
          * Gravar XML de distribuicao em uma pasta (NFe com o protocolo de autorizacao anexado)
            oAutorizacao.GravarXmlDistribuicao("d:\testenfe")
			
		  * Pegar a string do XML de distribui��o
            docProcNFe = oAutorizacao.GetNFeProcResults(chaveNFe)
			MessageBox(docProcNFe)

          * Como pegar o numero do protocolo de autorizacao para gravar na base
		    MessageBox(oAutorizacao.Result.ProtNFe.InfProt.NProt)
		 else
          * Rejeitada ou Denegada - Fazer devidos tratamentos		 
         ENDIF
      ELSE
         IF oAutorizacao.Result.CStat == 204 && Duplicidade da NFe
          * Finalizar a nota pela consulta situa��o
          * Configura��o M�nima
            oConfigConsSit = CreateObject("Uni.Business.DFe.Servicos.Configuracao")
            oConfigConsSit.TipoDfe = 0 && 0=nfe
            oConfigConsSit.CertificadoSenha = "12345678"
            oConfigConsSit.CertificadoArquivo = "C:\Projetos\certificados\UnimakePV.pfx"          
         
          * Criar XML de consulta situa��o da NFe
            oConsSitNfe = CreateObject("Uni.Business.DFe.Xml.NFe.ConsSitNfe")
            oConsSitNfe.Versao = "4.00"
            oConsSitNfe.TpAmb  = 2  && Homologa��o
            oConsSitNfe.ChNfe  = chaveNFe  && Chave da NFE 
            
          * Consumir o Servi�o de Consulta Situa��o da Nota
            oConsultaProtocolo = CREATEOBJECT("Uni.Business.DFe.Servicos.NFe.ConsultaProtocolo")
            oConsultaProtocolo.Executar(oConsSitNFe, oConfigConsSit)
            
            MESSAGEBOX(oConsultaProtocolo.RetornoWSString)
            MESSAGEBOX(AllTrim(Str(oConsultaProtocolo.Result.CStat,5)) + " " + oConsultaProtocolo.Result.XMotivo)

            IF oConsultaProtocolo.Result.CStat == 100 && Nota Fiscal Autorizada
             * Alimentar a propriedade com o retorno da consulta
               oAutorizacao.AddRetConsSitNFes(oConsultaProtocolo.Result)
               
               oAutorizacao.GravarXmlDistribuicao("d:\testenfe")
               
             * Pegar a string do XML de distribui��o para gravar em uma base de dados, por exemplo.
               docProcNFe = oAutorizacao.GetNFeProcResults(chaveNFe)
    		   MessageBox(docProcNFe)
            ELSE
               MESSAGEBOX(oConsultaProtocolo.Result.CStat)
               MESSAGEBOX(oConsultaProtocolo.Result.XMotivo)         		 
            ENDIF          
            
         ENDIF
	  ENDIF	  
   
   Catch To oErro
    * Excecao do FOXPRO
	* Mais sobre excecao em FOXPRO
	* http://www.yaldex.com/fox_pro_tutorial/html/2344b71b-14c0-4125-b001-b5fbb7bd1f05.htm
	
	  MessageBox("FOXPRO - ErrorCode: " + ALLTRIM(STR(oErro.ErrorNo,10))+ " - Message: " + oErro.Message)
	  
    * Excecao do CSHARP
      MessageBox("CSHARP - ErrorCode: " + ALLTRIM(STR(oExceptionInterop.GetErrorCode(),20)) + " - Message: " + oExceptionInterop.GetMessage())
   EndTry
Return

