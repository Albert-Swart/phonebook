if not exists(select 1 from phonebooks where [name] = 'My Phonebook')
begin
	exec usp_add_contact 'Albert', '081 8289111', 'My Phonebook'
end
go
