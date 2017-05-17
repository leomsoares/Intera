create procedure AddSocial
(
	  @pessoaId		 int
	 ,@nick			 varchar(50)	
	 ,@Social_id	 int
)
as
begin
	insert into Social (Pessoa_id, Nick, Social_id)
					values  (@pessoaId, @nick, @Social_id)
end
exec AddSocial 1,'Teste Social',1

