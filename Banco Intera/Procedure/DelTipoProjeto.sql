create procedure DelTipoProjeto
(
	  @IdTipo	 int
)
as
begin
	delete from TipoProjeto where idTipo = @IdTipo;
end
exec DelTipoProjeto 1

