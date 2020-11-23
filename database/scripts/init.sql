CREATE DATABASE TesteT2S;
GO
USE TesteT2S;
GO

IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Containers] (
    [Id] int NOT NULL IDENTITY,
    [Number] nvarchar(11) NOT NULL,
    [Customer] nvarchar(50) NOT NULL,
    [Type] tinyint NOT NULL,
    [Status] tinyint NOT NULL,
    [Category] tinyint NOT NULL,
    CONSTRAINT [PK_Containers] PRIMARY KEY ([Id]),
    CONSTRAINT [AK_Containers_Number] UNIQUE ([Number])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20201120192334_Container', N'5.0.0');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Handlings] (
    [Id] int NOT NULL IDENTITY,
    [ContainerId] int NOT NULL,
    [Ship] nvarchar(50) NOT NULL,
    [HandlingType] tinyint NOT NULL,
    [Start] datetime2 NOT NULL,
    [End] datetime2 NOT NULL,
    CONSTRAINT [PK_Handlings] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Handlings_Containers_ContainerId] FOREIGN KEY ([ContainerId]) REFERENCES [Containers] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Handlings_ContainerId] ON [Handlings] ([ContainerId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20201122220537_addShipHandling', N'5.0.0');
GO

insert into Containers ([Number], [Customer], [Type], [Status], [Category]) values ('abde0123456', 'Fernando', 0, 0, 0);
insert into Containers ([Number], [Customer], [Type], [Status], [Category]) values ('abde1123456', 'Fernando', 1, 0, 1);
insert into Containers ([Number], [Customer], [Type], [Status], [Category]) values ('abde2123456', 'Fernando', 1, 1, 0);
insert into Containers ([Number], [Customer], [Type], [Status], [Category]) values ('abde3123456', 'Fernando', 1, 1, 1);
insert into Containers ([Number], [Customer], [Type], [Status], [Category]) values ('abde4123456', 'Fernando', 0, 1, 0);
insert into Containers ([Number], [Customer], [Type], [Status], [Category]) values ('abde5123456', 'Fernando', 0, 0, 1);
insert into Containers ([Number], [Customer], [Type], [Status], [Category]) values ('abde6123456', 'Fernando', 1, 0, 0);
insert into Containers ([Number], [Customer], [Type], [Status], [Category]) values ('abde7123456', 'Fernando', 0, 1, 1);
insert into Containers ([Number], [Customer], [Type], [Status], [Category]) values ('abde8123456', 'Fernando', 1, 1, 0);
insert into Containers ([Number], [Customer], [Type], [Status], [Category]) values ('abde9123456', 'Fernando', 0, 0, 1);
insert into Containers ([Number], [Customer], [Type], [Status], [Category]) values ('abdf0123456', 'Fernando', 1, 1, 1);
insert into Containers ([Number], [Customer], [Type], [Status], [Category]) values ('bbde0123456', 'Lawrence Blaese', 0, 0, 0);
insert into Containers ([Number], [Customer], [Type], [Status], [Category]) values ('bbde1123456', 'Lawrence Blaese', 1, 0, 1);
insert into Containers ([Number], [Customer], [Type], [Status], [Category]) values ('bbde2123456', 'Lawrence Blaese', 1, 1, 0);
insert into Containers ([Number], [Customer], [Type], [Status], [Category]) values ('bbde3123456', 'Brenda Burris', 1, 1, 1);
insert into Containers ([Number], [Customer], [Type], [Status], [Category]) values ('bbde4123456', 'Lawrence Blaese', 0, 1, 0);
insert into Containers ([Number], [Customer], [Type], [Status], [Category]) values ('bbde5123456', 'Brenda Burris', 0, 0, 1);
insert into Containers ([Number], [Customer], [Type], [Status], [Category]) values ('bbde6123456', 'Brenda Burris', 1, 0, 0);
insert into Containers ([Number], [Customer], [Type], [Status], [Category]) values ('bbde7123456', 'Brenda Burris', 0, 1, 1);
insert into Containers ([Number], [Customer], [Type], [Status], [Category]) values ('bbde8123456', 'Brenda Burris', 1, 1, 0);
insert into Containers ([Number], [Customer], [Type], [Status], [Category]) values ('bbde9123456', 'Lawrence Blaese', 0, 0, 1);
insert into Containers ([Number], [Customer], [Type], [Status], [Category]) values ('bbdf0123456', 'Brenda Burris', 1, 1, 1);

insert into Handlings ([ContainerId], [Ship], [HandlingType], [Start], [End]) values (1, 'Navio N', 0, '10-10-2020', '10-11-2020');
insert into Handlings ([ContainerId], [Ship], [HandlingType], [Start], [End]) values (1, 'Navio N', 1, '11-10-2020', '10-12-2020');
insert into Handlings ([ContainerId], [Ship], [HandlingType], [Start], [End]) values (1, 'Navio N', 2, '12-10-2020', '10-13-2020');
insert into Handlings ([ContainerId], [Ship], [HandlingType], [Start], [End]) values (1, 'Navio N', 3, '10-13-2020', '10-14-2020');
insert into Handlings ([ContainerId], [Ship], [HandlingType], [Start], [End]) values (2, 'N Danika', 6, '9-20-2020', '9-21-2020');
insert into Handlings ([ContainerId], [Ship], [HandlingType], [Start], [End]) values (13, 'N Danika', 5, '9-21-2020', '9-22-2020');
insert into Handlings ([ContainerId], [Ship], [HandlingType], [Start], [End]) values (13, 'N Danika', 1, '9-22-2020', '9-23-2020');
insert into Handlings ([ContainerId], [Ship], [HandlingType], [Start], [End]) values (13, 'N Danika', 3, '9-23-2020', '9-24-2020');
insert into Handlings ([ContainerId], [Ship], [HandlingType], [Start], [End]) values (13, 'N Rivers', 4, '9-20-2020', '9-21-2020');
insert into Handlings ([ContainerId], [Ship], [HandlingType], [Start], [End]) values (13, 'N Rivers', 7, '9-21-2020', '9-22-2020');
insert into Handlings ([ContainerId], [Ship], [HandlingType], [Start], [End]) values (13, 'N Rivers', 3, '9-22-2020', '9-23-2020');
insert into Handlings ([ContainerId], [Ship], [HandlingType], [Start], [End]) values (12, 'N Rivers', 2, '9-23-2020', '9-24-2020');
insert into Handlings ([ContainerId], [Ship], [HandlingType], [Start], [End]) values (13, 'N Mccartney', 2, '9-23-2020', '9-24-2020');
insert into Handlings ([ContainerId], [Ship], [HandlingType], [Start], [End]) values (14, 'N Mccartney', 3, '9-23-2020', '9-24-2020');
insert into Handlings ([ContainerId], [Ship], [HandlingType], [Start], [End]) values (15, 'N Mccartney', 4, '9-23-2020', '9-24-2020');
insert into Handlings ([ContainerId], [Ship], [HandlingType], [Start], [End]) values (16, 'N Mccartney', 6, '9-23-2020', '9-24-2020');
insert into Handlings ([ContainerId], [Ship], [HandlingType], [Start], [End]) values (17, 'N Mccartney', 7, '9-23-2020', '9-24-2020');
insert into Handlings ([ContainerId], [Ship], [HandlingType], [Start], [End]) values (8, 'N Mccartney', 1, '9-23-2020', '9-24-2020');
insert into Handlings ([ContainerId], [Ship], [HandlingType], [Start], [End]) values (9, 'N Mccartney', 0, '9-23-2020', '9-24-2020');
insert into Handlings ([ContainerId], [Ship], [HandlingType], [Start], [End]) values (10, 'N Mccartney', 2, '9-23-2020', '9-24-2020');
insert into Handlings ([ContainerId], [Ship], [HandlingType], [Start], [End]) values (11, 'N Mccartney', 2, '9-23-2020', '9-24-2020');

COMMIT;
GO
