create procedure UpAlunoProject
(
	  @aluno_id int
	, @projeto_id  int
	, @dataInicio date
	, @dataFinal date
	
)
as
begin
	 UPDATE AlunoData 
	 set 
	 DataInicio = @dataInicio
	,DataFinal  = @dataFinal
	 WHERE 	 Aluno_id   = @aluno_id AND  Projeto_id = @projeto_id;		 
end

exec UpAlunoProject 1,3,'2016-11-09','2017-11-09'

