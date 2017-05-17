create procedure BuscarProfessor
(
	  @nome  varchar(50)
)
as
begin
	select IdPessoa, Email from Pessoa where Status = 2
end

--Testes
exec BuscarProfessor 'te'

select * from pessoa