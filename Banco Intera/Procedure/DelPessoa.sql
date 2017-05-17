Create procedure DelPessoa
(
	@idPessoa	int
)
as
begin
	UPDATE Pessoa set Status = 0 WHERE IdPessoa = @idPessoa
end

--Teste
exec InativarPessoa 1
