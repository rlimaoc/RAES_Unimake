> [!IMPORTANT]
> DLL ainda em desenvolvimento, não está pronta para uso!!!

# DFe
DLL desenvolvida, exclusivamente, para Nota Eletrônica Fatura de Comuicação - NFCom (modelo 62)
Seguindo os padrões, estruturas e modelos de desenvolvimento da DLL original da Unimake (Unimake.DFe)

> [!WARNING]
> Essa DLL é um fork, para usar as outras DFes (NFe, NFCe, CTe, MDFe, ...), utilizem o projeto original Unimake.DFe (link abaixo na documentação)

# Breaking Changes

Antes de utilizar essa DLL, atente-se para as alterações em [Breaking Changes](https://github.com/rlimaoc/Unimake/blob/main/Breaking-Changes.md)

## Alterações

+ 2024-01-24
  1. Criados os arquivos XML de configuração dos estados

+ 2024-01-17
  1. Alterado o enum ModeloNFPapel para ModeloNFImpresso

+ 2024-01-16
  1. Alterado o nome do projeto para Uni.DFe (para não gerar conflitos e futura compatibilidade)
  1. Importados os esquemas mais recentes do site da NFCom
  1. Configurado os enums da NFCom e alterados os compartilhados com outras DFes

### Documentação

https://dfe-portal.svrs.rs.gov.br/Nfcom
https://wiki.unimake.com.br/index.php/Manuais:Uni.DFe