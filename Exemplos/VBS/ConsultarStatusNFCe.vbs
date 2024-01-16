Dim consStatServ
Dim statusServico
Dim configuracao

'Montar o objeto de configuração com informações mínimas 
'para ser utilizado na hora de consumir o serviço
Set configuracao = CreateObject("Uni.Business.DFe.Servicos.Configuracao")
configuracao.TipoDFe = 1
configuracao.CertificadoSenha = "12345678"
configuracao.CertificadoArquivo = "D:\projetos\UnimakePV.pfx"

'Montar o XML de consulta status do serviço
Set consStatServ = CreateObject("Uni.Business.DFe.Xml.NFe.ConsStatServ")
consStatServ.Versao = "4.00"
consStatServ.CUF = 41 'Paraná
consStatServ.TpAmb = 1 '1-Produção 2-Homologação

'Consumir o serviço
Set statusServico = CreateObject("Uni.Business.DFe.Servicos.NFCe.StatusServico")
statusServico.Executar (consStatServ),(configuracao)

'Demonstrar mensagens na tela com o retorno da SEFAZ
MsgBox statusServico.Result.CStat 'Recuperar o conteúdo da tag <cStat> retornada pela SEFAZ
MsgBox statusServico.Result.XMotivo 'Recuperar o conteúdo da tag <xMotivo> retornada pela SEFAZ