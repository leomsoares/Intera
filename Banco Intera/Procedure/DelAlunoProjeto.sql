create procedure DelAlunoProject
(
	  @aluno_id int
	, @projeto_id  int
)
as
begin
	 delete from AlunoData WHERE Aluno_id   = @aluno_id AND  Projeto_id = @projeto_id;		 
end

exec DelAlunoProject 1,3