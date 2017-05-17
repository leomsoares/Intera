create view ViewPessoa
as
select	  p.IdPessoa	Id
		, p.Nome		Nome
		, p.Email		Email
		, a.Ra		 	Ra  
		, pr.Rs			Rs
from Pessoa p full outer join Aluno a on a.Pessoa_id = p.IdPessoa
full outer join Professor pr on pr.Pessoa_id = p.IdPessoa
where p.Status = 1

--teste
select * from ViewPessoa