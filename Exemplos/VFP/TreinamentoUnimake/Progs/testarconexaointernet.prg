FUNCTION TestarConexaoInternet()
   LOCAL oNet
   
   oNet = CREATEOBJECT("Unimake.Business.DFe.Utility.NetInterop")
   
   IF oNet.HasInternetConnection()
      MESSAGEBOX("Internet ok")
   ELSE
      MESSAGEBOX("Sem conex�o com a internet")
   ENDIF   
RETURN
