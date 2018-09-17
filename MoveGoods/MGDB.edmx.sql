
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/03/2018 10:47:22
-- Generated from EDMX file: C:\Users\a.marchenko\documents\visual studio 2017\Projects\MoveGoods\MoveGoods\MGDB.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [MGDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ManagerMove]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MoveSet] DROP CONSTRAINT [FK_ManagerMove];
GO
IF OBJECT_ID(N'[dbo].[FK_StoreManager_Store]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StoreManager] DROP CONSTRAINT [FK_StoreManager_Store];
GO
IF OBJECT_ID(N'[dbo].[FK_StoreManager_Manager]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[StoreManager] DROP CONSTRAINT [FK_StoreManager_Manager];
GO
IF OBJECT_ID(N'[dbo].[FK_ManagerManager]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ManagerSet] DROP CONSTRAINT [FK_ManagerManager];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[StoreSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StoreSet];
GO
IF OBJECT_ID(N'[dbo].[ManagerSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ManagerSet];
GO
IF OBJECT_ID(N'[dbo].[MoveSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MoveSet];
GO
IF OBJECT_ID(N'[dbo].[StoreManager]', 'U') IS NOT NULL
    DROP TABLE [dbo].[StoreManager];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'StoreSet'
CREATE TABLE [dbo].[StoreSet] (
    [Id] nvarchar(16)  NOT NULL,
    [Name] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'ManagerSet'
CREATE TABLE [dbo].[ManagerSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [SAM] nvarchar(max)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [ParentManager_Id] int  NULL
);
GO

-- Creating table 'MoveSet'
CREATE TABLE [dbo].[MoveSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [MoveDT] datetime  NOT NULL,
    [Source] nvarchar(16)  NOT NULL,
    [Destination] nvarchar(16)  NOT NULL,
    [Capacity] nvarchar(max)  NULL,
    [Manager_Id] int  NOT NULL
);
GO

-- Creating table 'StoreManager'
CREATE TABLE [dbo].[StoreManager] (
    [Stores_Id] nvarchar(16)  NOT NULL,
    [Managers_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'StoreSet'
ALTER TABLE [dbo].[StoreSet]
ADD CONSTRAINT [PK_StoreSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ManagerSet'
ALTER TABLE [dbo].[ManagerSet]
ADD CONSTRAINT [PK_ManagerSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MoveSet'
ALTER TABLE [dbo].[MoveSet]
ADD CONSTRAINT [PK_MoveSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Stores_Id], [Managers_Id] in table 'StoreManager'
ALTER TABLE [dbo].[StoreManager]
ADD CONSTRAINT [PK_StoreManager]
    PRIMARY KEY CLUSTERED ([Stores_Id], [Managers_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Manager_Id] in table 'MoveSet'
ALTER TABLE [dbo].[MoveSet]
ADD CONSTRAINT [FK_ManagerMove]
    FOREIGN KEY ([Manager_Id])
    REFERENCES [dbo].[ManagerSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ManagerMove'
CREATE INDEX [IX_FK_ManagerMove]
ON [dbo].[MoveSet]
    ([Manager_Id]);
GO

-- Creating foreign key on [Stores_Id] in table 'StoreManager'
ALTER TABLE [dbo].[StoreManager]
ADD CONSTRAINT [FK_StoreManager_Store]
    FOREIGN KEY ([Stores_Id])
    REFERENCES [dbo].[StoreSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Managers_Id] in table 'StoreManager'
ALTER TABLE [dbo].[StoreManager]
ADD CONSTRAINT [FK_StoreManager_Manager]
    FOREIGN KEY ([Managers_Id])
    REFERENCES [dbo].[ManagerSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_StoreManager_Manager'
CREATE INDEX [IX_FK_StoreManager_Manager]
ON [dbo].[StoreManager]
    ([Managers_Id]);
GO

-- Creating foreign key on [ParentManager_Id] in table 'ManagerSet'
ALTER TABLE [dbo].[ManagerSet]
ADD CONSTRAINT [FK_ManagerManager]
    FOREIGN KEY ([ParentManager_Id])
    REFERENCES [dbo].[ManagerSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ManagerManager'
CREATE INDEX [IX_FK_ManagerManager]
ON [dbo].[ManagerSet]
    ([ParentManager_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------