create procedure UpAluno
(
	 @nome		varchar(50)
	,@email		varchar(50)
	,@ra		varchar(15)
	,@curso		varchar(50)
	,@idPessoa	int
)
as
begin
	UPDATE Pessoa set Nome = @nome, Email = @email WHERE IdPessoa = @idPessoa
	UPDATE Aluno set Ra = @ra, Curso = @curso WHERE Pessoa_id = @idPessoa 
end 

--Teste
exec UpAluno 'antonio', 'antonio@ig.com.br', '1234', '12312412', 'INFO', 1 