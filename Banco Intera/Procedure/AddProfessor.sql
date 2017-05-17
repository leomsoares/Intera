create procedure AddProfessor
(
		  @nome  varchar(50)
		, @email varchar(50)
		, @rs    varchar(15)
)
as
begin
		insert into Pessoa values (@nome, 2, @email, 'fatec123')
		select scope_identity()
		insert into Professor values (@@IDENTITY, @rs)
end

--teste
exec AddProfessor 'admin', 'admin@admin', '123', '987654321'