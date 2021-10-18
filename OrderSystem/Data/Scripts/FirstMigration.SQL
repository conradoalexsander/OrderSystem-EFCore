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

CREATE TABLE [Customers] (
    [Id] int NOT NULL IDENTITY,
    [Name] VARCHAR(80) NOT NULL,
    [Telephone] CHAR(11) NULL,
    [CEP] CHAR(8) NOT NULL,
    [State] CHAR(2) NOT NULL,
    [City] nvarchar(60) NOT NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Products] (
    [Id] int NOT NULL IDENTITY,
    [BarCode] VARCHAR(14) NOT NULL,
    [Description] VARCHAR(60) NULL,
    [Value] decimal(18,2) NOT NULL,
    [ProductType] nvarchar(max) NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_Products] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Orders] (
    [Id] int NOT NULL IDENTITY,
    [CustomerId] int NOT NULL,
    [StartedIn] datetime2 NOT NULL DEFAULT (GETDATE()),
    [FinishedIn] datetime2 NOT NULL,
    [DeliveryType] nvarchar(max) NOT NULL,
    [Status] nvarchar(max) NOT NULL,
    [Observation] VARCHAR(512) NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Orders_Customers_CustomerId] FOREIGN KEY ([CustomerId]) REFERENCES [Customers] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [OrderItem] (
    [Id] int NOT NULL IDENTITY,
    [OrderId] int NOT NULL,
    [ProductId] int NOT NULL,
    [Quantity] int NOT NULL DEFAULT 1,
    [Value] decimal(18,2) NOT NULL,
    [Discount] decimal(18,2) NOT NULL,
    CONSTRAINT [PK_OrderItem] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_OrderItem_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_OrderItem_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [idx_customer_phone] ON [Customers] ([Telephone]);
GO

CREATE INDEX [IX_OrderItem_OrderId] ON [OrderItem] ([OrderId]);
GO

CREATE INDEX [IX_OrderItem_ProductId] ON [OrderItem] ([ProductId]);
GO

CREATE INDEX [IX_Orders_CustomerId] ON [Orders] ([CustomerId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20211018160100_FirstMigration', N'5.0.11');
GO

COMMIT;
GO

