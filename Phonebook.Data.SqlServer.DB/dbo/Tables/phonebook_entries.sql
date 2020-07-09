CREATE TABLE [dbo].[phonebook_entries] (
    [entry_id]     INT IDENTITY (1, 1) NOT NULL,
    [phonebook_id] INT NOT NULL,
    [contact_id]   INT NOT NULL,
    CONSTRAINT [PK_phonebook_entries] PRIMARY KEY CLUSTERED ([entry_id] ASC)
);

