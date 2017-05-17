create procedure AddAlunoProject
(
	  @aluno_id int
	, @projeto_id  int
	, @dataInicio date
	, @dataFinal date
	
)
as
begin
	insert into AlunoData (Aluno_id,Projeto_id, DataInicio, DataFinal) 
				 values   (@aluno_id,@projeto_id, @dataInicio, @dataFinal)
end

exec AddAlunoProject 4,1,'2016-11-09','2017-11-09'