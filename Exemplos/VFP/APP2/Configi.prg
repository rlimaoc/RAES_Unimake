SET READBORDER ON
SET TALK OFF			&& Elimina a resposta dos comandos para o V�deo
SET NOTIFY OFF
SET CONSOLE OFF
SET DATE TO DMY			&& Formato de Data para Dia/Mes/Ano
SET POINT TO ','		&& Separador Decimal
SET SEPARATOR TO '.'	&& Separador do Milhar
** Configura��es de Rede (MultiUsu�rio)
SET EXCLUSIVE OFF		&& Permite o compartilhamento dos arquivos de dados
SET REPROCESS TO 1 		&& Quantas tentativas para bloquear um registro
SET REFRESH TO 5		&& Tempo de atualiza��o dos dados no Browse
SET DELETED ON			&& N�o apresenta os registros marcados para exclus�o
SET CENTURY ON			&& Mostra o ano com 4 d�gitos
SET CENTURY TO 19 ROLLOVER 10	&& 05 ou maior � entendido como 19xx. De 0 a 4 como 20xx
SET CURRENCY TO 'R$'	&& S�mbolo monet�rio
SET SAFETY OFF			&& N�o avisa para sobreescrever um arquivo

ON KEY LABEL ALT+1 ACTIVATE WINDOW CALCULATOR
ON KEY LABEL ALT+2 ACTIVATE WINDOW CALENDAR