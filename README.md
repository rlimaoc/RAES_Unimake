# Uni.NFCom
DLL desenvolvida apenas para NFCom - Nota Eletrônica Fatura de Seriços de Comunicação.
O projeto foi baseado na DLL da Unimake.DFe e foi alterado epenas para comportar a NFCom, não causando conflito com a biblioteca na Unimake.


# Alterações
+ 2024.01.10 - 1.10
  + Implementação da consulta da situação da NFCom
  + Implementação do envio da NFCom (ainda em desenvolvimento)

> [!WARING]
> Para mais informações das versões, acesse o [Log de Versões](https://github.com/rlimaoc/Unimake/blob/main/Versions-Log.md)


# Braking changes
Informações importantes sobre a compatibilidade desse projeto com a DLL Unimake.DFe em [Breaking Changes](https://github.com/rlimaoc/Unimake/blob/main/Breaking-Changes.md) 


### Documentações
https://dfe-portal.svrs.rs.gov.br/Nfcom/Servicos
https://wiki.unimake.com.br/index.php/Manuais:Unimake.DFe


### Webservices utilizados da NFCom (SVRS)
+ **Produção**
  + NFComConsulta 1.00 https://nfcom.svrs.rs.gov.br/WS/NFComConsulta/NFComConsulta.asmx
  + NFComRecepcao 1.00 https://nfcom.svrs.rs.gov.br/WS/NFComRecepcao/NFComRecepcao.asmx
  + NFComRecepcaoEvento 1.00 https://nfcom.svrs.rs.gov.br/WS/NFComRecepcaoEvento/NFComRecepcaoEvento.asmx
  + NFComStatusServico 1.00 https://nfcom.svrs.rs.gov.br/WS/NFComStatusServico/NFComStatusServico.asmx

+ **Homologação**
  + NFComConsulta 1.00 https://nfcom-homologacao.svrs.rs.gov.br/WS/NFComConsulta/NFComConsulta.asmx
  + NFComRecepcao 1.00 https://nfcom-homologacao.svrs.rs.gov.br/WS/NFComRecepcao/NFComRecepcao.asmx
  + NFComRecepcaoEvento 1.00 https://nfcom-homologacao.svrs.rs.gov.br/WS/NFComRecepcaoEvento/NFComRecepcaoEvento.asmx
  + NFComStatusServico 1.00 https://nfcom-homologacao.svrs.rs.gov.br/WS/NFComStatusServico/NFComStatusServico.asmx


### Bug Fixes ref. a DLL (Unimake.DFe)
> [!CAUTION]
> **Essas alterações foram feitas SOMENTE neste projeto e NÃO SÃO compatíveis com a DLL Unimake.DFe**

+ Enums
  FinalidadeNFe.Auste => Enum FinalidadeNFe.Ajuste
