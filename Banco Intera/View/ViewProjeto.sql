create view vProjeto
as
select	  IdProjeto       ,
		  Professor_id    ,
		  Coorientador_id ,
		  TipoProjeto_id  ,
		  NomeProjeto     ,
		  Status	      ,
		  Link		      ,
		  DataInicio      ,
		  DataFinal       ,
		  Descricao   
from	 Projeto 
			
--teste
select * from vProjeto

