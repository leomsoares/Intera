create procedure UpReference
(
	  @projeto int
	 ,@idReferencia int
	 ,@descricao varchar(100)	
)
as
begin
	update Referencia set
	Descricao = @descricao
	where idReferencia = @idReferencia and Projeto_id = @projeto;				
end
exec UpReference 3,1,'Teste Referencia'

