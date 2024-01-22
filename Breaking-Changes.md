# Breaking changes
Saiba mais sobre as alterações recentes e futuras desse projeto.

## Sobre o breaking changes
Sobre mudanças recentes que podem exigir ação dos desenvolvedores que utilizam o projeto.

### Bug Fixes ref. a DLL (Unimake.DFe)
> [!CAUTION]
> **Essas alterações foram feitas SOMENTE & EXCLUSIVAMENTE neste projeto e NÃO SÃO compatíveis com a DLL Unimake.DFe**

+ Enums
  FinalidadeNFe.Auste => Enum FinalidadeNFe.Ajuste

+ Comentários
  Alguns métodos estavam com comentários gerando warnings, foram corrigidos, mas não afetam o uso da DLL.

### Alterações realizadas em 2024-01-22

Até o momento da implementação da DLL Uni.DFe, alguns estados ainda não implementaram a legislação correspondente a Nota Eletrônica Fatura de Comunicação.

Diferentemente da DLL Unimake.DFe os serviços de consulta status de serviço e consulta situação da nota não dependem do estado, obrigando o desenvolvedor a configurar o código do seu estado no momento da configuração para que as informações sejam carregadas automaticamente, conforme abaixo:

**Como é na DLL original (Unimake.DFe), conforme o treinamento:**

```
var xml = new ConsStatServ
{
  Versao = "4.00",
  CUF = UFBrasil.PR,
  TpAmb = TipoAmbiente.Homologacao
};

var configuracao = new Configuracao
{
  TipoDFe = TipoDFe.NFe,
  TipoEmissao = TipoEmissao.Normal,
  CertificadoDigital = CertificadoSelecionado
}

var statusServico = new StatusServico(xml, configuracao);
statusServico.Executar()
```

**Como ficou nessa versão da DLL (Uni.DFe), seguindo o mesmo treinameto:**

```
var xml = new ConsStatServNFCom
{
  Versao = "1.00",
  TpAmb = TipoAmbiente.Homologacao
};

var configuracao = new Configuracao
{
  TipoDFe = TipoDFe.NFe,
  TipoEmissao = TipoEmissao.Normal,
  CertificadoDigital = CertificadoSelecionado,
  CodigoUF = (int)UFBrasil.PR
}

var statusServico = new StatusServico(xml, configuracao);
statusServico.Executar()
```
