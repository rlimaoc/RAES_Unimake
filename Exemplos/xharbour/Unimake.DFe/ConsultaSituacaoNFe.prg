* ---------------------------------------------------------------------------------
* Consumindo o servi�o de consulta a situacao da NFe
* ---------------------------------------------------------------------------------
#IfNdef __XHARBOUR__
   #xcommand TRY => BEGIN SEQUENCE WITH {| oErr | Break( oErr ) }
   #xcommand CATCH [<!oErr!>] => RECOVER [USING <oErr>] <-oErr->
#endif

Function ConsultaSituacaoNfe()
   Local oConfig
   Local oConsSitNfe, oErro, oExceptionInterop
   Local oConsultaProtocolo

 * Criar configura�ao b�sica para consumir o servi�o
   oConfig = CreateObject("Uni.Business.DFe.Servicos.Configuracao")
   oConfig:TipoDfe = 0 // 0=nfe
   oConfig:Servico = 1 // 1=Situacao da NFE
   oConfig:CertificadoSenha = "12345678"
   oConfig:CertificadoArquivo = "C:\Projetos\certificados\UnimakePV.pfx"

 * Criar XML
   oConsSitNfe = CreateObject("Uni.Business.DFe.Xml.NFe.ConsSitNfe")
   oConsSitNfe:Versao = "4.00"
   oConsSitNfe:TpAmb  = 2  // Homologa��o
   oConsSitNfe:ChNfe  = "412001061174730001505500100006066414037532101" // Chave da NFE 
   
   //Criar objeto para pegar exce��o do CSHARP
   oExceptionInterop = CreateObject("Uni.Exceptions.ThrowHelper")
   
   Try   
    * Consumir o servi�o
      oConsultaProtocolo = CreateObject("Uni.Business.DFe.Servicos.NFe.ConsultaProtocolo")
      oConsultaProtocolo:Executar(oConsSitNfe,oConfig)

      ? "XML Retornado pela SEFAZ"
      ? "========================"
      ? oConsultaProtocolo:RetornoWSString
      ?
      ? "Codigo de Status e Motivo"
      ? "========================="
      ? AllTrim(Str(oConsultaProtocolo:Result:CStat,5)), oConsultaProtocolo:Result:XMotivo
      ?
	  
   Catch oErro
      //Demonstrar exce��es geradas no proprio Harbour, se existir.
	  ? "ERRO"
	  ? "===="
	  ? "Falha ao tentar consultar o status do servico."
      ? oErro:Description
      ? oErro:Operation
	  
	  //Demonstrar a exce��o do CSHARP
	  ?
      ? "Excecao do CSHARP - Message: ", oExceptionInterop:GetMessage()
      ? "Excecao do CSHARP - Codigo: ", oExceptionInterop:GetErrorCode()
      ?
   End	  
   
   Wait
Return

