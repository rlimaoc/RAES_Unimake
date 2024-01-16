* ---------------------------------------------------------------------------------
* Consumindo o servi�o de consulta status da CTe
* ---------------------------------------------------------------------------------
#IfNdef __XHARBOUR__
   #xcommand TRY => BEGIN SEQUENCE WITH {| oErr | Break( oErr ) }
   #xcommand CATCH [<!oErr!>] => RECOVER [USING <oErr>] <-oErr->
#endif

Function ConsultaStatusCTe()
   Local oConfig
   Local oConsStatServCTe, oErro, oExceptionInterop
   Local oStatusServico
   
 * Criar configura��o b�sica para consumir o servi�o
   oConfig = CreateObject("Uni.Business.DFe.Servicos.Configuracao")
   oConfig:TipoDFe = 2 //0=CTe
   oConfig:CertificadoArquivo = "C:\Projetos\certificados\UnimakePV.pfx"
   oConfig:CertificadoSenha = "12345678"
   oConfig:TipoEmissao = 1 //1=Normal
   oConfig:CodigoUF = 41 //Paran� (No caso do CTe temos que definir a UF nas configura��es pois o XML de consulta status n�o tem e n�o temos como saber de qual estado consultar.
   
 * Criar XML   
   oConsStatServCTe = CreateObject("Uni.Business.DFe.Xml.CTe.ConsStatServCTe")
   oConsStatServCTe:Versao = "3.00"
   oConsStatServCTe:TpAmb = 2 //2=Homologa��o
   
   //Criar objeto para pegar exce��o do lado do CSHARP
   oExceptionInterop = CreateObject("Uni.Exceptions.ThrowHelper")
   
   Try
    * Consumir o servi�o
	  oStatusServico = CreateObject("Uni.Business.DFe.Servicos.CTe.StatusServico")
	  oStatusServico:Executar(oConsStatServCTe, oConfig)

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