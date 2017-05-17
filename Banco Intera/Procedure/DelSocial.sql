create procedure DelSocial
(
	  @pessoaId		 int	
)
as
begin
	delete from Social where Pessoa_id = @pessoaId;
end
--exec DelSocial 1

