* ---------------------------------------------------------------------------------
* Carregar tela de configura��o do UniDANFE
* ---------------------------------------------------------------------------------
Function TelaConfiguracao()
   Local TelaConfig
  
 * Criar objeto
   TelaConfig = CreateObject("Unimake.Unidanfe.UnidanfeServices")
   
 * Abrir tela de configura��o  
   TelaConfig:ShowConfigurationScreen()
   
   Wait
RETURN nil