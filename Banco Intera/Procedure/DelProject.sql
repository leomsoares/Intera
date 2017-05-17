create procedure DeleteProject
(
	  @projeto_id int
)
as
begin
	delete from Projeto where IdProjeto = @projeto_id
end
exec DeleteProject 2
/*
insert into Pessoa values ('Luiz Cordeiro',1,'luiz@fatec.sp.gov.br','senha@123',null)

insert into Professor values (2, '9999')

insert into Aluno values(4,'1234','ADS')
insert into TipoProjeto values ('TCC')

select * from pessoa
select * from professor
select * from aluno
select * from pessoa
select * from projeto
select * from AlunoData
select * from tipoprojeto
select * from tiporedesocial

*/