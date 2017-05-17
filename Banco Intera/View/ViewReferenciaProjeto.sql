create view vReferenciaProjeto
as
SELECT r.IdReferencia , r.Projeto_id ,r.Descricao Descricao_Referencia, p.IdProjeto, p.NomeProjeto, p.Professor_id, p.Descricao Descricao_Projeto FROM Referencia AS r FULL OUTER JOIN Projeto AS p ON (r.Projeto_id = p.IdProjeto)
			
--teste
select * from vReferenciaProjeto

