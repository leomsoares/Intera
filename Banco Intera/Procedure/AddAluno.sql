create procedure AddAluno
(
	  @nome  varchar(50)
	, @email varchar(50)
	, @ra    varchar(15)
	, @curso varchar(50)
)
as
begin
	insert into Pessoa values (@nome, 1, @email, 'fatec123')
	select scope_identity()
	insert into Aluno values (@@IDENTITY, @ra, @curso)
end

--teste
exec AddAluno 'admin', 'admin@admin.com', '123', '123456789', 'ADS'