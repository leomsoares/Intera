create procedure UpdateProject
(
	  @projeto_id int
	, @professor_id  int
	, @coorientador int
	, @tipoProjeto int
	, @nomeProjeto    varchar(50)
	, @link   varchar(80)
	, @dataInicio date
	, @dataFinal date
	, @descricao varchar (max)
)
as
begin
	update Projeto 
	set Professor_id = @professor_id,
		Coorientador_id = @coorientador,
		TipoProjeto_id = @tipoProjeto,
		NomeProjeto = @nomeProjeto,
		Status = 1,
		Link = @link,
		DataInicio = @dataInicio,
		DataFinal = @dataFinal,
		Descricao = @descricao
	 where IdProjeto = @projeto_id; 


end
exec UpdateProject 1,1,2,1,'Projeto SEHA','C:/Projetos/Documento/','2016-11-09', null,'Organização de horáiros da faculdade'
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