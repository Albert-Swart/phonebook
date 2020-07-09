CREATE proc [dbo].[usp_retrieve_contacts]
	@phonebook_name varchar(100)
as
begin

	set nocount on

	declare @messages table ([value] varchar(max))

	if not exists(select 1 from	phonebooks where [name] = @phonebook_name)
	begin

		insert into @messages
		select 'Phonebook "' + @phonebook_name + '" does not exist.'

	end

	select [value] from @messages

	if not exists(select 1 from @messages)
	begin

		select
			c.[name],
			c.phone_number
		from 
			phonebook_entries pe
			inner join phonebooks pb on pb.phonebook_id = pe.phonebook_id
			inner join contacts c on c.contact_id = pe.contact_id
		where
			pb.[name] = @phonebook_name

	end

end