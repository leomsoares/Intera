create procedure BuscarAluno
(
	  @nome  varchar(50)
)
as
begin
	select IdPessoa, Email from Pessoa where Status = 1 and Nome like '%'+@nome
end

--Testes
exec BuscarAluno 'Te'

select * from pessoa

select P.IdPessoa, Email from Pessoa as P left join Aluno as A on P.IdPessoa = A.Pessoa_id left join Professor as R on P.IdPessoa = R.Pessoa_id 
where Nome like '%Te' and Rs is null

