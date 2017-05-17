create view vMensagemPessoa
as
SELECT m.IdMensagem IdMsg, m.Descricao Msg, p.IdPessoa IdPes, p.Nome NomePes FROM Mensagem AS m FULL OUTER JOIN Pessoa AS p ON (m.Pessoa_id = p.IdPessoa)
			
--teste
select * from vMensagemPessoa

