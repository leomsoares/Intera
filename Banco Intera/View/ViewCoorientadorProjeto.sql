create view vCoorientadorProjeto
as
SELECT p.IdPessoa,Nome,p.Status Status_professor, p.Email, pro.IdProjeto, pro.TipoProjeto_id, pro.NomeProjeto, pro.Status Status_Projeto, pro.DataInicio, pro.DataFinal, pro.Descricao from pessoa p inner join Projeto pro on (p.IdPessoa = pro.Coorientador_id)
			
--teste
select * from vCoorientadorProjeto