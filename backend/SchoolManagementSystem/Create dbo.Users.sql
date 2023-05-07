USE [master]
GO

/****** Object: Table [dbo].[Users] Script Date: 2/3/2022 04:11:05 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users] (
    [Id]        UNIQUEIDENTIFIER NOT NULL,
    [CreatedAt] DATETIME2 (7)    NOT NULL,
    [UpdatedAt] DATETIME2 (7)    NOT NULL,
    [Username]  NVARCHAR (MAX)   NULL,
    [FirstName] NVARCHAR (MAX)   NULL,
    [LastName]  NVARCHAR (MAX)   NULL,
    [Email]     NVARCHAR (MAX)   NOT NULL,
    [RoleId]    UNIQUEIDENTIFIER NOT NULL,
    [Password]  NVARCHAR (MAX)   NULL,
    [IsActive]  BIT              NOT NULL,
    [IsDeleted] BIT              NOT NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_Users_RoleId]
    ON [dbo].[Users]([RoleId] ASC);


GO
ALTER TABLE [dbo].[Users]
    ADD CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC);


GO
ALTER TABLE [dbo].[Users]
    ADD CONSTRAINT [FK_Users_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([Id]) ON DELETE CASCADE;

	select * from Roles;


	update Users set Password = 'H5XYLcZ0gbRVcUI2K35SQT5fn6U2itdDAyCyjs7PSvg=';

	select * from Users where Email = 'admin@gmail.com' and Password = 'fE3up9cRdqiw3rxWoKwjYSxYVrsVTtU+DFL/cx2cftM=';
	delete from Users where Id = '09F13C10-C0AA-4F9D-8A7D-50CE2F92F6AD';