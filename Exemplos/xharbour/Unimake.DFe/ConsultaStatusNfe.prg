* ---------------------------------------------------------------------------------
* Consumindo o servi�o de consulta status da NFe
* ---------------------------------------------------------------------------------
#IfNdef __XHARBOUR__
   #xcommand TRY => BEGIN SEQUENCE WITH {| oErr | Break( oErr ) }
   #xcommand CATCH [<!oErr!>] => RECOVER [USING <oErr>] <-oErr->
#endif

Function ConsultaStatusNfe()
   Local oConfig
   Local oConsStatServ, oErro, oExceptionInterop
   Local oStatusServico
   
 * Criar configura��o b�sica para consumir o servi�o
   oConfig = CreateObject("Uni.Business.DFe.Servicos.Configuracao")
   oConfig:TipoDFe = 0 //0=NFe
   oConfig:Servico = 0 //0=NFe Status Servi�o
   oConfig:CertificadoArquivo = "C:\Projetos\certificados\UnimakePV.pfx"
   oConfig:CertificadoSenha = "12345678"
   
 * Criar XML   
   oConsStatServ = CreateObject("Uni.Business.DFe.Xml.NFe.ConsStatServ")
   oConsStatServ:Versao = "4.00"
   oConsStatServ:TpAmb = 2 //2=Homologa��o
   oConsStatServ:CUF = 41 //41=Paran�
   
   //Criar objeto para pegar exce��o do lado do CSHARP
   oExceptionInterop = CreateObject("Unimake.Exceptions.ThrowHelper")
   
   Try
    * Consumir o servi�o
	  oStatusServico = CreateObject("Uni.Business.DFe.Servicos.NFe.StatusServico")
	  oStatusServico:Executar(oConsStatServ, oConfig)

	  ? "XML Retornado pela SEFAZ"
      ? "========================"
      ? oStatusServico:RetornoWSString
      ?
      ? "Codigo de Status e Motivo"
      ? "========================="
	  ? Alltrim(Str(oStatusServico:Result:CStat,5)), oStatusServico:Result:XMotivo
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