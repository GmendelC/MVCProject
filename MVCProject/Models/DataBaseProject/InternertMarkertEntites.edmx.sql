
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/20/2017 18:46:29
-- Generated from EDMX file: C:\Users\Artzi\Desktop\MVCProject\MVCProject\Models\DataBaseProject\InternertMarkertEntites.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [InternertMarkert];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_SellingProduct]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Products] DROP CONSTRAINT [FK_SellingProduct];
GO
IF OBJECT_ID(N'[dbo].[FK_ByingProduct]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Products] DROP CONSTRAINT [FK_ByingProduct];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Products]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Products];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int  NOT NULL,
    [FirstName] varchar(50)  NOT NULL,
    [LastName] nvarchar(50)  NOT NULL,
    [BirthDate] datetime  NOT NULL,
    [Email] varchar(50)  NOT NULL,
    [UserName] varchar(50)  NOT NULL,
    [Password] varchar(50)  NOT NULL
);
GO

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(50)  NOT NULL,
    [ShortDescription] nvarchar(500)  NOT NULL,
    [LogDescription] nvarchar(4000)  NOT NULL,
    [Date] datetime  NOT NULL,
    [Price] decimal(18,0)  NOT NULL,
    [InCar] bit  NOT NULL,
    [SellingId] int  NULL,
    [ByingId] int  NULL,
    [Image1_File] varbinary(max)  NULL,
    [Image1_MIME] nvarchar(max)  NULL,
    [Image2_File] varbinary(max)  NULL,
    [Image2_MIME] nvarchar(max)  NULL,
    [Image3_File] varbinary(max)  NULL,
    [Image3_MIME] nvarchar(max)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [SellingId] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_SellingProduct]
    FOREIGN KEY ([SellingId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SellingProduct'
CREATE INDEX [IX_FK_SellingProduct]
ON [dbo].[Products]
    ([SellingId]);
GO

-- Creating foreign key on [ByingId] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_ByingProduct]
    FOREIGN KEY ([ByingId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ByingProduct'
CREATE INDEX [IX_FK_ByingProduct]
ON [dbo].[Products]
    ([ByingId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------