CREATE proc [dbo].[usp_add_contact]
	@name varchar(100),
	@phone_number varchar(100),
	@associated_phonebooks ust_list_of_string readonly
as
begin

	set nocount on

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

		declare @associated table([name] varchar(100), processed bit)

		insert @associated
		select distinct([value]), 0 from @associated_phonebooks

		declare @phonbook_id int
		declare @phonebook_name varchar(100)

		while(select top 1 1 from @associated where processed = 0) > 0
		begin

			select top 1 @phonebook_name = [name] from @associated where processed = 0

			select @phonbook_id = phonebook_id from phonebooks where [name] = @phonebook_name

			if @phonbook_id is null
			begin

				insert into phonebooks([name]) values (@phonebook_name)
				set @phonbook_id = SCOPE_IDENTITY()
	
			end

			if not exists(select 1 from phonebook_entries where contact_id = @contact_id and phonebook_id = @phonbook_id)
			begin
				
				insert into phonebook_entries(contact_id, phonebook_id) values (@contact_id, @phonbook_id)

			end

			update @associated set processed = 1 where [name] = @phonebook_name
			
		end

		commit tran

	end try
	begin catch

		rollback tran

		select 'There was a problem processing your request. Please contact your administrator.'

	end catch

	set nocount off

end