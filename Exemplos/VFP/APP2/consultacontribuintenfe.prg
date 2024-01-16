IF VerificarCertificadoSelecionado() = .F. 
	RETURN 0 
ENDIF 
VerificarVencimentoCertificado()
ConfiguracaoAtual(0,1)

ConsCad = CreateObject("Uni.Business.DFe.Xml.NFe.ConsCad")
InfCons = CreateObject("Uni.Business.DFe.Xml.NFe.InfCons")
ConsultaCadastro = CreateObject("Uni.Business.DFe.Servicos.NFe.ConsultaCadastro")

InfCons.CNPJ = "06117473000150"
InfCons.UF = 41
ConsCad.Versao = "2.00"
ConsCad.InfCons = InfCons

ConsultaCadastro.Executar(ConsCad,Aplicativo.Configuracao.Inicializar)
 
MESSAGEBOX(ConsultaCadastro.result.InfCons.XMotivo)
MESSAGEBOX(ConsultaCadastro.RetornoWSString)
MESSAGEBOX(ConsultaCadastro.result.InfCons.InfCad.Xnome)

RELEASE ConsCad 
RELEASE InfCons 
RELEASE ConsultaCadastro 