create procedure AddMensagem
(
	  @pessoaId		 int
	 ,@projetoId	 int
	 ,@descricao	 varchar(50)
)
as
begin
	insert into Mensagem (Pessoa_id, Projeto_id, Descricao)
					values  (@pessoaId, @projetoId, @descricao)
end
exec AddMensagem 1,3 ,'Mensagem'


