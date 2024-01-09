# Uni.NFCom
DLL desenvolvida apenas para NFCom - Nota Eletrônica Fatura de Seriços de Comunicação
O projeto foi baseado na DLL da Unimake.DFe e foi alterado epenas para comportar a NFCom, não causando conflito com a biblioteca na Unimake.

# Alterações
2024.01.09 - 1.00
    > Alteração do nome do projeto para não entrar em conflito com a DLL Unimake.DFe
    > Alteração dos Enums para NFCom
    > Implementação da consulta de serviço da SEFAZ

# Documentação
https://wiki.unimake.com.br/index.php/Manuais:Unimake.DFe

# Importante
Para os indicadores abaixo, da NFCom, será utilizado o enum SinNao. Pois só precisam ser enviadas se o valor for 1, caso seja 0 (Nao) a TAG não será enviada.

TAG indPrePago - Indicador de serviço pré-pago
TAG indCessaoMeiosRede - Indicador de Sessão de Meios de Rede
TAG indNotaEntrada - Indicador de nota de entrada
TAG indDevolucao - Indicador de devolução do valor do item
TAG indSN - Indica se o contribuinte é Simples Nacional

TAG tpProc - Tipo de Processo
Será usado o enum TipoOrigemProcesso por compatibilidade, pois a NFCom aceita os mesmos três primeiros valores:
0 - SEFAZ
1 - Justiça Federal
2 - Justiça Estadual

TAG tpEmis - Tipo de Emissão
Foi incorporado no enum TipoEmissao o tipo de contingência da NFCom, apenas para facilidade do uso da biblioteca, pois o valor é o mesmo correspondente ao ContingenciaFSIA.
2 - ContingenciaNFCom

# Bug Fixes no Unimake.DFe
Enums
FinalidadeNFe.Auste => Enum FinalidadeNFe.Ajuste



