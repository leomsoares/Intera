create procedure UpMensagem
(
	  @IdMensagem		 int
	 ,@descricao	 varchar(50)
)
as
begin
	update Mensagem set 
	Descricao = @descricao
	where IdMensagem = @IdMensagem;
end
exec UpMensagem 1,'Mensagem'


