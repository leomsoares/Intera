create view vScientificResearch
as
SELECT IdProjeto, Professor_id, NomeProjeto, TipoProjeto_id ,P.Status, ISNULL(Link,'') AS Link, DataInicio, ISNULL(DataFinal,'') AS DataFinal, Descricao , Nome 
FROM Projeto as P inner join Pessoa as Pes on (Professor_id = IdPessoa) where TipoProjeto_id = 2 
			
--teste
select * from vScientificResearch

