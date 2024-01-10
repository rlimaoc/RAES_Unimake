# Breaking changes
Saiba mais sobre as alterações recentes e futuras desse projeto para NFCom (modelo 62) comaptível com a DLL Unimake.DFe.

## Sobre o breaking changes
Sobre mudanças recentes que podem exigir ação dos desenvolvedores que utilizam o projeto.

### Alterações realizadas em 2024-01-10

1. ***INDICADORES***
   
   Foi identificado mais um indicador:

   +TAG indSemCST - Sem Situação Tributária para o ICMS

1. ***ENUMS***

   Foram criados Enums referentes ao código de classificação dos produtos, conforme lista fonecedida pela SEFAZ \(https://dfe-portal.svrs.rs.gov.br/NFCOM/tabelacclass\).

   Por enquanto será necesário converter para string e depois aplicar o padleft, pois o tamanho da TAG é fixa em 7 caracteres, conforme exemplo abaixo:

   **Exemplo do código:**
   ...
   public enum ClassItemGeral
   {
      ...
      /// <summary>
      /// 0400401	- Serviço Não Medido - Provimento à internet
      /// </summary>
      ServicoNaoMedidoInternet = 0400401,
      ...
   }
   ...

   **Exemplo de uso:**
   ...
   Prod = new Prod
   {
      ...
      CClass = ClassItemGeral.ServicoNaoMedidoInternet.ToString().PadLeft(7, '0'),
      ...
   }
   ...


### Alterações realizadas em 2024-01-09

1. ***INDICADORES***

   Para os indicadores abaixo será utilizado o enum SimNao. Pois as TAGs só precisam ser enviadas se o valor for 1, elas não seráo enviadas se o valor for 0.
   Essa validação será realizada na serialização do XML.

   + TAG indPrePago - Indicador de serviço pré-pago
   + TAG indCessaoMeiosRede - Indicador de Sessão de Meios de Rede
   + TAG indNotaEntrada - Indicador de nota de entrada
   + TAG indDevolucao - Indicador de devolução do valor do item
   + TAG indSN - Indica se o contribuinte é Simples Nacional

   **Como fica o código:**

   ```
   var = new Ide
   {
      ...
      VerProc = "1.00",
      IndPrePago = SimNao.Nao,
      IndSessaoMeioRede = SimNao.Sim,
      IndNotaEntrada = SimNao.Nao,
      ...
   }
   ```

   **Como fica o XML:**

   ```
      <ide>
         ...
         <verProc>1.00</verProc>
         <indCessaoMeiosRede>1</indCessaoMeiosRede>
      </ide>
   ```


1. ***TIPO DE PROCESSO***

   Para a TAG tpProc (Tipo de Processo) deve ser usado o Enum TipoOrigemProcesso, pois os valores utilizados pela NFCom (modelo 62) são os mesmos:

   ```
   0 - SEFAZ
   1 - Justiça Federal
   2 - Justiça Estadual
   ```


1. ***TIPO DE EMISSÃO***

   Na TAG tpEmis (Tipo de Emissão) da NFCom (modelo 62) são aceitos apenas dois valores possíveis:
   1 - Normal
   2 - Contingência

   O Enum TipoEmissao da DLL Unimake.DFe o valor 2 corresponde a ContingenciaFSIA: Contingência FS-IA, com impressão do DANFE em formulário de segurança ou Para MDFe é impressão em formulário branco (sulfite).

   Para facilitar o uso da biblioteca e a clareza das informações, o valor 2 foi repetido com o texto: ContingenciaNFCom, deixando o Enum TipoEmissao como abaixo:

   ```
   1 - TipoEmissao.Normal
   2 - TipoEmissao.ContingenciaNFCom
   2 - TipoEmissao.ContingenciaFSIA
   3 - TipoEmissao.RegimeEspecialNFF
   ...   
   ```
