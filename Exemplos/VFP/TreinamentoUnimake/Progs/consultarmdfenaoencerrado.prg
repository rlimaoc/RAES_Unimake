* ---------------------------------------------------------------------------------
* Consultar MDFe�s N�o Encerrados
* ---------------------------------------------------------------------------------
FUNCTION ConsultarMDFeNaoEncerrado()         
   LOCAL oConfiguracao, oExceptionInterop
   LOCAL oConsMDFeNaoEnc, oConsNaoEnc

 * Criar configura�ao b�sica para consumir o servi�o
   oConfiguracao = CreateObject("Uni.Business.DFe.Servicos.Configuracao")
   oConfiguracao.TipoDfe = 4 && 4=MDFe
   oConfiguracao.CertificadoSenha = "12345678"
   oConfiguracao.CodigoUF = 41 && UFBrasil.PR
   oConfiguracao.CertificadoArquivo = "C:\Projetos\certificados\UnimakePV.pfx"
   
   oConsMDFeNaoEnc = CreateObject("Uni.Business.DFe.Xml.MDFe.ConsMDFeNaoEnc")
   oConsMDFeNaoEnc.Versao = "3.00"
   oConsMDFeNaoEnc.TpAmb = 2 && TipoAmbiente.Homologacao
   oConsMDFeNaoEnc.XServ = "CONSULTAR N�O ENCERRADOS"
   oConsMDFeNaoEnc.CNPJ = "10859283000185"
   
 * Criar objeto para pegar exce��o do lado do CSHARP
   oExceptionInterop = CreateObject("Unimake.Exceptions.ThrowHelper")

   TRY 
    * Enviar a consulta
      oConsNaoEnc = CreateObject("Uni.Business.DFe.Servicos.MDFe.ConsNaoEnc")
      oConsNaoEnc.Executar(oConsMDFeNaoEnc, oConfiguracao)
      
      MESSAGEBOX(oConsNaoEnc.RetornoWSString)
      
   CATCH TO oErro
    * Excecao do FOXPRO
	* Mais sobre excecao em FOXPRO
	* http://www.yaldex.com/fox_pro_tutorial/html/2344b71b-14c0-4125-b001-b5fbb7bd1f05.htm
	
	  MESSAGEBOX("FOXPRO - ErrorCode: " + ALLTRIM(STR(oErro.ErrorNo,10))+ " - Message: " + oErro.Message)
	  
    * Excecao do CSHARP
      MESSAGEBOX("CSHARP - ErrorCode: " + ALLTRIM(STR(oExceptionInterop.GetErrorCode(),20)) + " - Message: " + oExceptionInterop.GetMessage())   
   ENDTRY      
RETURN      