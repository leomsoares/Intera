create view ViewProfessor
as
	select	  p.IdPessoa	Id
			, p.Nome		Nome
			, p.Email		Email
			, prof.Rs		Rs
			, p.Status		Status
	from Pessoa p, Professor prof
	where p.IdPessoa = prof.Pessoa_id

--teste
select * from ViewProfessor