use [master];
go

if db_id('StudentsDB') is  null
begin
	create database StudentsDB;
end
go

use [StudentsDB];

go


BACKUP DATABASE [StudentsDB]
TO DISK = 'C:\Users\Public\StudentsDB.bak'
WITH NAME = N'Full_Backup_StudentsDB';

go

IF OBJECT_ID('Students', 'U') IS NULL
BEGIN
	Create Table Students (
			Id int primary key,
			[Name] varchar(20) not null,
			[Surname] varchar(20) not null,
			[Password] varchar(20) not null
	)
END