create procedure UpTipoRedeSocial
(
	  @redeSocial_id int
	, @nomeRedeSocial varchar(100)	
)
as
begin
	update TipoRedeSocial 
	set NomeRedeSocial = @nomeRedeSocial
	where IdRedeSocial = @redeSocial_id;
end
exec UpdateTipoRedeSocial 1,'Teste Social'
