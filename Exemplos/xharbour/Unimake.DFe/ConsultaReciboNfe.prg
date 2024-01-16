* ---------------------------------------------------------------------------------
* Consumindo o servi�o de consulta recibo da NFe
* ---------------------------------------------------------------------------------
Function ConsultaReciboNfe()
   Local InicializarConfiguracao
   Local ConsReciNFe
   Local retAutorizacao

 * Criar configura�ao b�sica para consumir o servi�o
   InicializarConfiguracao = CreateObject("Uni.Business.DFe.Servicos.Configuracao")
   InicializarConfiguracao:TipoDfe = 0 // 0=nfe
   InicializarConfiguracao:Servico = 2 // 2=Consulta Recibo
   InicializarConfiguracao:CertificadoSenha = "12345678"
   InicializarConfiguracao:CertificadoArquivo = "C:\Projetos\certificados\UnimakePV.pfx"

  * Criar XML
   ConsReciNFe = CreateObject("Uni.Business.DFe.Xml.NFe.ConsReciNFe")
   ConsReciNFe:Versao = "4.00"
   ConsReciNFe:TpAmb  = 2  // Homologa��o
   ConsReciNFe:NRec = "411234567890123" // n�mero do recibo
   
  * Consumir o servi�o
    retAutorizacao = CreateObject("Uni.Business.DFe.Servicos.NFe.RetAutorizacao")
    retAutorizacao:Executar(ConsReciNFe,InicializarConfiguracao)

   ? "XML Retornado pela SEFAZ"
   ? "========================"
   ? retAutorizacao:RetornoWSString
   ?
   ? "Codigo de Status e Motivo"
   ? "========================="
   ? AllTrim(Str(retAutorizacao:Result:CStat,5)),retAutorizacao:Result:XMotivo
   ?
   Wait
Return

