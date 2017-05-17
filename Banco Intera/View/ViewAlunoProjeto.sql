create view vAlunoProjeto
as
SELECT ad.Projeto_id Projeto, p.IdPessoa IdAluno, p.Nome Nome, p.Email Email, ad.DataInicio DataInicio, ad.DataFinal DataFinal 
FROM Pessoa as p FULL OUTER JOIN AlunoData as ad ON(p.IdPessoa = ad.Aluno_id) where p.Status = 1
			
--teste
select * from vAlunoProjeto
