create procedure DelMensagem
(
	  @IdMensagem		 int
)
as
begin
	delete from Mensagem where IdMensagem = @IdMensagem;
end
exec DelMensagem 1


