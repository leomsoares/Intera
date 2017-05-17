create view ViewAluno
as
	select	  p.IdPessoa	Id
			, p.Nome		Nome
			, p.Email		Email
			, a.Ra			Ra
			, p.Status		Status
	from Pessoa p, Aluno a
	where p.IdPessoa = a.Pessoa_id

	--teste
	select * from ViewAluno