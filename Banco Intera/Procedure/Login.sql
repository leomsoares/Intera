create procedure Login
(
	  @email varchar(50)
	, @senha varchar(30)
)
as
begin
	select * from Pessoa
	where Email = @email and Senha = @senha and Status <> 0 
end

--teste
exec Login 'admin@admin.com', '123'