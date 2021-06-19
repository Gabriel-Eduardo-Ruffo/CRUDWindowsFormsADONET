# CRUDWindowsFormsADONET
Aplicación de escritorio. CRUD. Se conecta a SQL por ADO. NET

##SCRIPT PARA ARMAR TABLA EN SQL SERVER##

IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'TestCRUD')
BEGIN
	CREATE DATABASE [TestCRUD];
END

GO
	USE [TestCRUD];
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Persons')
BEGIN
    CREATE TABLE [TestCRUD].[dbo].[Persons]
	(
		Id INT PRIMARY KEY IDENTITY (1, 1),
		FirstName VARCHAR(100) NOT NULL,
		LastName VARCHAR(100) NOT NULL,
		Age INT NOT NULL
	);
END

GO
	INSERT INTO [TestCRUD].[dbo].[Persons] (FirstName, LastName, Age) VALUES
	('Gabriel','Smith',38),
	('Jose','Perez',25),
	('John','Doe',50);
