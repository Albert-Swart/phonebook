CREATE proc [dbo].[usp_retrieve_contacts]
	@phonebook_name_filter ust_list_of_string readonly,
	@contact_name_filter ust_list_of_string readonly
as
begin

	set nocount on

	declare @eligable_contacts table(contact_id int)
	declare @eligable_phonebooks table(phonebook_id int)

	if(select count(1) from @contact_name_filter) = 0
	begin
		
		insert @eligable_contacts
		select contact_id from contacts

	end
	else
	begin

		
		insert @eligable_contacts
		select x.* 
		from
		(
			select [value] from @contact_name_filter
		) as v (name_part)
		cross apply
		(
			select contact_id from contacts where [name] like v.name_part + '%'
		)
		as x

	end

	if(select count(1) from @phonebook_name_filter) = 0
	begin

		insert @eligable_phonebooks
		select phonebook_id from phonebooks

	end
	else
	begin
	
		insert @eligable_phonebooks
		select phonebook_id from phonebooks where [name] in (select [value] from @phonebook_name_filter)

	end

	select
		c.[name],
		c.phone_number,
		pb.[name]
	from 
		phonebook_entries pe
		join phonebooks pb on pb.phonebook_id = pe.phonebook_id
		join contacts c on c.contact_id = pe.contact_id
		join @eligable_contacts ec on ec.contact_id = c.contact_id
		join @eligable_phonebooks ep on ep.phonebook_id = pb.phonebook_id

end