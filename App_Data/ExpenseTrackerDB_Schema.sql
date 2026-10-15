-- Create ExpenseTrackerDB if it doesn't already exist
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'ExpenseTrackerDB')
BEGIN
    CREATE DATABASE ExpenseTrackerDB;
END
GO

USE ExpenseTrackerDB;
GO

-- 1. Users Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = N'Users')
BEGIN
    CREATE TABLE Users (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        FullName NVARCHAR(150) NOT NULL,
        Email NVARCHAR(150) NOT NULL UNIQUE,
        Password NVARCHAR(255) NOT NULL,
        Phone NVARCHAR(50) NULL,
        CreatedAt NVARCHAR(50) NULL
    );
END
GO

-- 2. Transactions Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = N'Transactions')
BEGIN
    CREATE TABLE Transactions (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        UserEmail NVARCHAR(150) NULL,
        Title NVARCHAR(200) NOT NULL,
        Category NVARCHAR(100) NOT NULL,
        CategoryBadgeClass NVARCHAR(100) NULL,
        Date NVARCHAR(50) NOT NULL,
        Amount DECIMAL(18,2) NOT NULL,
        IsIncome BIT NOT NULL,
        IconType NVARCHAR(100) NULL,
        Note NVARCHAR(MAX) NULL
    );
    CREATE INDEX IX_Transactions_UserEmail ON Transactions(UserEmail);
END
GO

-- 3. Budgets Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = N'Budgets')
BEGIN
    CREATE TABLE Budgets (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        UserEmail NVARCHAR(150) NOT NULL,
        Category NVARCHAR(100) NOT NULL,
        Amount DECIMAL(18,2) NOT NULL,
        CONSTRAINT UQ_Budgets_User_Category UNIQUE (UserEmail, Category)
    );
END
GO

-- 4. Subscriptions Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = N'Subscriptions')
BEGIN
    CREATE TABLE Subscriptions (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        UserEmail NVARCHAR(150) NULL,
        Name NVARCHAR(200) NOT NULL,
        Amount DECIMAL(18,2) NOT NULL,
        NextDueDate NVARCHAR(50) NOT NULL,
        StatusBadge NVARCHAR(50) NULL
    );
END
GO

-- 5. Bills Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = N'Bills')
BEGIN
    CREATE TABLE Bills (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        UserEmail NVARCHAR(150) NULL,
        Name NVARCHAR(200) NOT NULL,
        Amount DECIMAL(18,2) NOT NULL,
        DueDate NVARCHAR(50) NOT NULL,
        Status NVARCHAR(50) NULL
    );
END
GO

-- 6. Loans Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = N'Loans')
BEGIN
    CREATE TABLE Loans (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        UserEmail NVARCHAR(150) NULL,
        PersonOrBank NVARCHAR(200) NOT NULL,
        Amount DECIMAL(18,2) NOT NULL,
        DueDate NVARCHAR(50) NOT NULL,
        Type NVARCHAR(50) NOT NULL
    );
END
GO

-- 7. Notes Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = N'Notes')
BEGIN
    CREATE TABLE Notes (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        UserEmail NVARCHAR(150) NULL,
        Content NVARCHAR(MAX) NOT NULL,
        DateFormatted NVARCHAR(50) NULL
    );
END
GO

-- 8. Settings Table
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = N'Settings')
BEGIN
    CREATE TABLE Settings (
        SettingKey NVARCHAR(100) PRIMARY KEY,
        SettingValue NVARCHAR(255) NULL
    );
END
GO

-- Insert default / migrated data if tables are empty
IF NOT EXISTS (SELECT 1 FROM Settings WHERE SettingKey = 'SelectedCurrency')
BEGIN
    INSERT INTO Settings (SettingKey, SettingValue) VALUES ('SelectedCurrency', 'INR');
END
GO

-- Seed Users
IF NOT EXISTS (SELECT 1 FROM Users)
BEGIN
    SET IDENTITY_INSERT Users ON;
    INSERT INTO Users (Id, FullName, Email, Password, Phone, CreatedAt) VALUES
    (1, N'tushar', N'tushar123@gmail.com', N'password123', N'+91 00000 00000', N'2026-03-20'),
    (2, N'Rohit', N'rohit@gmail.com', N'rohit123', N'+91 00000 00000', N'2026-09-23'),
    (3, N'miral', N'miral@gmail.com', N'12345678', N'+91 00000 00000', N'2026-09-24'),
    (4, N'parth', N'parth@gmail.com', N'123456', N'+91 00000 00000', N'2026-09-30'),
    (5, N'dd', N'dd@gmail.com', N'Tushar@123', N'+91 00000 00000', N'2026-10-12'),
    (6, N'tushar', N'tushar@gmail.com', N'Tushar@123', N'+91 00000 00000', N'2026-10-12');
    SET IDENTITY_INSERT Users OFF;
END
GO

-- Seed Budgets
IF NOT EXISTS (SELECT 1 FROM Budgets)
BEGIN
    INSERT INTO Budgets (UserEmail, Category, Amount) VALUES
    (N'tushar123@gmail.com', N'Utilities', 2000.00),
    (N'tushar123@gmail.com', N'Health', 4000.00),
    (N'tushar123@gmail.com', N'Shopping', 7000.00),
    (N'tushar123@gmail.com', N'Entertainment', 2000.00),
    (N'tushar123@gmail.com', N'Food', 8000.00),
    (N'tushar123@gmail.com', N'Transport', 3000.00),
    (N'tushar@gmail.com', N'Utilities', 2000.00),
    (N'tushar@gmail.com', N'Health', 4000.00),
    (N'tushar@gmail.com', N'Shopping', 7000.00),
    (N'tushar@gmail.com', N'Entertainment', 2000.00),
    (N'tushar@gmail.com', N'Food', 8000.00),
    (N'tushar@gmail.com', N'Transport', 3000.00),
    (N'parth@gmail.com', N'Food', 5000.00),
    (N'parth@gmail.com', N'Shopping', 5000.00),
    (N'parth@gmail.com', N'Transport', 6500.00),
    (N'parth@gmail.com', N'Entertainment', 2500.00),
    (N'parth@gmail.com', N'Health', 6000.00),
    (N'parth@gmail.com', N'Utilities', 5500.00),
    (N'parth@gmail.com', N'Bills', 7500.00),
    (N'parth@gmail.com', N'Gift', 1500.00),
    (N'parth@gmail.com', N'Other', 5000.00);
END
GO

-- Seed Bills
IF NOT EXISTS (SELECT 1 FROM Bills)
BEGIN
    SET IDENTITY_INSERT Bills ON;
    INSERT INTO Bills (Id, UserEmail, Name, Amount, DueDate, Status) VALUES
    (1, N'tushar123@gmail.com', N'Electricity', 1450.00, N'20/03/2026', N'Overdue'),
    (2, N'tushar123@gmail.com', N'Internet', 699.00, N'25/03/2026', N'Upcoming'),
    (3, N'tushar123@gmail.com', N'House Rent', 14000.00, N'01/04/2026', N'Upcoming'),
    (4, N'tushar123@gmail.com', N'Water & Gas', 380.00, N'05/04/2026', N'Upcoming');
    SET IDENTITY_INSERT Bills OFF;
END
GO

-- Seed Loans
IF NOT EXISTS (SELECT 1 FROM Loans)
BEGIN
    SET IDENTITY_INSERT Loans ON;
    INSERT INTO Loans (Id, UserEmail, PersonOrBank, Amount, DueDate, Type) VALUES
    (1, N'tushar123@gmail.com', N'HDFC Bank', 50000.00, N'10/04/2026', N'I Borrowed'),
    (2, N'tushar123@gmail.com', N'Rahul', 5000.00, N'15/04/2026', N'I Lent'),
    (3, N'tushar123@gmail.com', N'Car Loan EMI', 8500.00, N'05/04/2026', N'I Borrowed');
    SET IDENTITY_INSERT Loans OFF;
END
GO

-- Seed Subscriptions
IF NOT EXISTS (SELECT 1 FROM Subscriptions)
BEGIN
    SET IDENTITY_INSERT Subscriptions ON;
    INSERT INTO Subscriptions (Id, UserEmail, Name, Amount, NextDueDate, StatusBadge) VALUES
    (1, N'tushar123@gmail.com', N'Netflix', 199.00, N'10/8/2026', N'Upcoming'),
    (2, N'tushar123@gmail.com', N'Spotify', 119.00, N'14/8/2026', N'Upcoming'),
    (3, N'tushar123@gmail.com', N'Amazon Prime', 299.00, N'20/8/2026', N'Upcoming'),
    (4, N'tushar123@gmail.com', N'YouTube Premium', 139.00, N'25/8/2026', N'Upcoming');
    SET IDENTITY_INSERT Subscriptions OFF;
END
GO

-- Seed Notes
IF NOT EXISTS (SELECT 1 FROM Notes)
BEGIN
    SET IDENTITY_INSERT Notes ON;
    INSERT INTO Notes (Id, UserEmail, Content, DateFormatted) VALUES
    (1, N'tushar123@gmail.com', N'File ITR before July 31 — carry forward losses.', N'23/03/2026'),
    (2, N'tushar123@gmail.com', N'Review mutual fund SIPs — increase by ₹2000 next month.', N'23/03/2026'),
    (3, N'tushar123@gmail.com', N'Ask Rahul to return ₹5000 by 15 Aug.', N'23/03/2026');
    SET IDENTITY_INSERT Notes OFF;
END
GO

-- Seed Transactions
IF NOT EXISTS (SELECT 1 FROM Transactions)
BEGIN
    SET IDENTITY_INSERT Transactions ON;
    INSERT INTO Transactions (Id, UserEmail, Title, Category, CategoryBadgeClass, Date, Amount, IsIncome, IconType, Note) VALUES
    (1, N'tushar123@gmail.com', N'Monthly Salary', N'Salary', N'tag-salary', N'1/8/2026', 52000.00, 1, N'salary', N'Aug salary'),
    (2, N'tushar123@gmail.com', N'Dinner', N'Food', N'tag-food', N'26/7/2026', 700.00, 0, N'food', N'Zomato order'),
    (3, N'tushar123@gmail.com', N'Mall', N'Shopping', N'tag-shopping', N'26/7/2026', 5000.00, 0, N'shopping', N'Zara clothes'),
    (4, N'tushar123@gmail.com', N'Bus', N'Transport', N'tag-transport', N'26/7/2026', 50.00, 0, N'transport', N'Daily pass'),
    (5, N'tushar123@gmail.com', N'Dividend', N'Investment', N'tag-investment', N'24/7/2026', 3200.00, 1, N'investment', N'Mutual fund'),
    (6, N'tushar123@gmail.com', N'Dinner', N'Food', N'tag-food', N'10/7/2026', 300.00, 0, N'food', N'Restaurant with friends'),
    (7, N'tushar123@gmail.com', N'Freelance Project', N'Freelance', N'tag-freelance', N'5/7/2026', 12500.00, 1, N'freelance', N'UI design work'),
    (8, N'tushar123@gmail.com', N'Online shopping', N'Shopping', N'tag-shopping', N'5/7/2026', 3500.00, 0, N'shopping', N'Electronics'),
    (9, N'tushar123@gmail.com', N'Monthly Salary', N'Salary', N'tag-salary', N'1/7/2026', 52000.00, 1, N'salary', N'July salary'),
    (10, N'tushar123@gmail.com', N'monthly', N'Salary', N'tag-salary', N'12/09/2026', 50000.00, 1, N'salary', N''),
    (11, NULL, N'Calendar Test Expense', N'Shopping', N'tag-shopping', N'15-10-2026', 450.00, 0, N'shopping', N''),
    (12, NULL, N'Calendar Test Expense', N'Shopping', N'tag-shopping', N'15-10-2026', 450.00, 0, N'shopping', N''),
    (13, NULL, N'office', N'Salary', N'tag-salary', N'24-9-2026', 65000.00, 1, N'salary', N''),
    (14, N'parth@gmail.com', N'Monthly', N'Salary', N'tag-salary', N'30-9-2026', 50000.00, 1, N'salary', N''),
    (15, N'parth@gmail.com', N'dinner', N'Food', N'tag-food', N'30-9-2026', 1200.00, 0, N'food', N''),
    (16, N'parth@gmail.com', N'Monthly', N'Investment', N'tag-investment', N'30-8-2026', 23500.00, 1, N'investment', N'');
    SET IDENTITY_INSERT Transactions OFF;
END
GO
