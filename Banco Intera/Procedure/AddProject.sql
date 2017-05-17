create procedure AddProject
(
	  @professor_id  int
	, @coorientador int
	, @tipoProjeto int
	, @nomeProjeto    varchar(50)
	, @status		int
	, @link   varchar(80)
	, @dataInicio date
	, @dataFinal date
	, @descricao varchar (max)
)
as
begin
	insert into Projeto (Professor_id, Coorientador_id, TipoProjeto_id, NomeProjeto , Status, Link, DataInicio, DataFinal, Descricao)
				  values(@professor_id, @coorientador, @tipoProjeto, @nomeProjeto, @status, @link, @dataInicio, @dataFinal, @descricao)
end
exec AddProject 1,2,1,'Projeto 1',null,'2016-11-09', null,'Teste Projeto'
/*
insert into Pessoa values ('Viana',2,'tripleboot@fatec.sp.gov.br','senha@123',null)

insert into Professor values (2, '9999')

insert into Aluno values(1,'1234','ADS')
insert into TipoProjeto values ('TCC')

select * from pessoa
select * from professor
select * from aluno
select * from pessoa
select * from projeto
select * from AlunoData
select * from tipoprojeto
select * from tiporedesocial
delete from projeto
insert into projeto (Professor_id, TipoProjeto_id, NomeProjeto, Status, DataInicio, Descricao) 
			values	(2,2,'Alimentar Gado',1,'2006-05-05','Cano estourou')
*/