CREATE TABLE [dbo].[contacts] (
    [contact_id]   INT           IDENTITY (1, 1) NOT NULL,
    [name]         VARCHAR (100) NOT NULL,
    [phone_number] VARCHAR (50)  NOT NULL,
    [created]      DATETIME      CONSTRAINT [DF_entries_created] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_contacts] PRIMARY KEY CLUSTERED ([contact_id] ASC)
);

