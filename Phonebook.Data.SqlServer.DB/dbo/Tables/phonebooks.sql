CREATE TABLE [dbo].[phonebooks] (
    [phonebook_id] INT           IDENTITY (1, 1) NOT NULL,
    [name]         VARCHAR (100) NOT NULL,
    [created]      DATETIME      CONSTRAINT [DF_PhoneBook_created] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_PhoneBook] PRIMARY KEY CLUSTERED ([phonebook_id] ASC)
);

