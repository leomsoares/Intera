create procedure DelReference
(
	  @projeto int
	 ,@idReferencia int
)
as
begin
	delete from Referencia where idReferencia = @idReferencia and Projeto_id = @projeto;				
end
exec DelReference 3,1

