* ---------------------------------------------------------------------------------
* Consumindo o servi�o de consulta status da NFe
* ---------------------------------------------------------------------------------
Function ConsultaStatusNfe()
   Local oConfig
   Local oConsStatServ, oErro, oExceptionInterop
   Local oStatusServico
   
 * Criar configura��o b�sica para consumir o servi�o
   oConfig = CreateObject("Uni.Business.DFe.Servicos.Configuracao")
   oConfig.TipoDFe = 0 && 0=NFe
   oConfig.CertificadoArquivo = "C:\Projetos\certificados\UnimakePV.pfx"
   oConfig.CertificadoSenha = "12345678"
   
*   oConfig.CertificadoDigital = oCertSel3

*   oConfig.CertificadoSerialNumberOrThumbPrint = serialNumber

*   oConfig.CertificadoBase64 = certBase64
*   oConfig.CertificadoSenha = "12345678"

 * Criar XML   
   oConsStatServ = CreateObject("Uni.Business.DFe.Xml.NFe.ConsStatServ")
   oConsStatServ.Versao = "4.00"
   oConsStatServ.TpAmb = 2 && 2=Homologa��o
   oConsStatServ.CUF = 41 && 41=Paran�
   
 * Criar objeto para pegar exce��o do lado do CSHARP
   oExceptionInterop = CreateObject("Uni.Exceptions.ThrowHelper")
   
   Try
    * Consumir o servi�o
      oStatusServico = CreateObject("Uni.Business.DFe.Servicos.NFe.StatusServico")
	  oStatusServico.Executar(oConsStatServ, oConfig)

    * XML retornado pela SEFAZ
	  MessageBox(oStatusServico.RetornoWSString)
    
    * C�digo de Status e Motivo 	
	  MessageBox(Alltrim(Str(oStatusServico.Result.CStat,5))+" - "+oStatusServico.Result.XMotivo)
	  
    Catch To oErro
    * Exce��o do FOXPRO
	* Mais sobre exce��o em FOXPRO
	* http://www.yaldex.com/fox_pro_tutorial/html/2344b71b-14c0-4125-b001-b5fbb7bd1f05.htm
	
	  MessageBox(oErro.ErrorNo)
	  MessageBox("Exce��o foxpro: " + oErro.Message)
	  
    * Exce��o do CSHARP
      MessageBox(oExceptionInterop.GetMessage())
      MessageBox(oExceptionInterop.GetErrorCode())
   EndTry   
Return