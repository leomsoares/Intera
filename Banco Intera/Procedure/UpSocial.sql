create procedure UpSocial
(
	  @pessoaId		 int
	 ,@nick			 varchar(50)	
)
as
begin
	update Social set 
	Nick = @nick
	where Pessoa_id = @pessoaId;
end
--exec UpSocial 1,'Teste Social2'

