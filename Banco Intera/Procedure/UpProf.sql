Create procedure UpProf
(
	  @nome		varchar(50)
	 ,@email	varchar(50)
	 ,@rs		varchar(15)
	 ,@idPessoa	int
)
as
begin
	UPDATE Pessoa		SET		Nome = @nome, Email = @email WHERE IdPessoa = @idPessoa
	UPDATE Professor	SET		Rs = @rs WHERE Pessoa_id = @idPessoa
end

--Teste
exec UpProf 'Sergio', 'sergio@fatec.edu.br', '123', '123124', 3