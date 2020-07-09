CREATE proc [dbo].[usp_add_contact]
	@name varchar(100),
	@phone_number varchar(100),
	@phonebook_name varchar(100)
as
begin

	set nocount on

	declare @messages table ([value] varchar(max))

	insert into @messages
	select 'The contact ' + @name + ' with phone number ' + @phone_number + ' is already associated with phonebook ' + @phonebook_name
	from 
		phonebook_entries pe
		inner join phonebooks pb on pb.phonebook_id = pe.phonebook_id
		inner join contacts c on c.contact_id = pe.contact_id
	where 
		c.[name] = @name
		and c.phone_number = @phone_number
		and pb.[name] = @phonebook_name

	if not exists(select 1 from @messages)
	begin

		begin try

			begin tran

			declare @contact_id int

			select
				@contact_id = contact_id
			from
				contacts
			where
				[name] = @name
				and [phone_number]= @phone_number

			if @contact_id is null
			begin

				insert into contacts([name], phone_number) values(@name, @phone_number)
				set @contact_id = SCOPE_IDENTITY()

			end

			declare @phonbook_id int
			select @phonbook_id = phonebook_id from phonebooks where [name] = @phonebook_name

			if @phonbook_id is null
			begin

				insert into phonebooks([name]) values (@phonebook_name)
				set @phonbook_id = SCOPE_IDENTITY()
	
			end

			insert into phonebook_entries(contact_id, phonebook_id) values (@contact_id, @phonbook_id)

			commit tran
		end try
		begin catch

			rollback tran

			insert into @messages
			select 'There was a problem processing your request. Please contact your administrator to resolve.'

		end catch

	end

	select [value] from @messages

	set nocount off

end