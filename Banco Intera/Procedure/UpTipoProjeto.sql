create procedure UpTipoProjeto
(
	  @IdTipo	 int
	 ,@descricao varchar(100)	
)
as
begin
	update TipoProjeto set 
	Descricao = @descricao
	where idTipo = @IdTipo;
end
exec UpTipoProjeto 1,'Teste Tipo'

