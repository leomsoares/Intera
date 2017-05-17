create procedure AddTipoRedeSocial
(
	 @nomeRedeSocial varchar(100)	
)
as
begin
	insert into TipoRedeSocial (NomeRedeSocial)
					values  (@nomeRedeSocial)
end
exec AddTipoRedeSocial'Teste Referencia'

