create procedure AddTipoProjeto
(
	 @descricao varchar(100)	
)
as
begin
	insert into TipoProjeto (Descricao)
					values  (@descricao)
end
exec AddTipoProjeto 'Teste Referencia'

