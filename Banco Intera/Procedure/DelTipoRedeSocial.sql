create procedure DeleteTipoRedeSocial
(
	  @redeSocial_id int
)
as
begin
	delete from TipoRedeSocial where IdRedeSocial = @redeSocial_id;
end
exec DeleteTipoRedeSocial 1
