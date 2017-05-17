create view vMensagem
as
select	  IdMensagem    ,
		  Pessoa_id  ,
		  Projeto_id  ,
		  Descricao   
from	 Mensagem
			
--teste
select * from vMensagem

