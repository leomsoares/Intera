create procedure AddReference
(
	  @projeto int
	 ,@descricao varchar(100)	
)
as
begin
	insert into Referencia (Projeto_id, Descricao)
					values (@projeto,@descricao)
end
exec AddReference 1,'Teste Referencia'

