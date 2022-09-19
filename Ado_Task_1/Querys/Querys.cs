using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADO_Lesson_1.Querys
{
    public abstract class QuerysStorage
    {
        public static string CheckDB = @"
                    USE [master];
                    
                    IF db_id('StudentsDB') IS  NULL
                    BEGIN
                    	create database StudentsDB;
                    END";

        public static string BackUpDifferent = @"
                    BACKUP DATABASE [StudentsDB]
                    TO DISK = 'C:\Users\Public\StudentsDB.bak'
                    WITH DIFFERENTIAL,NAME = N'Full_Backup_StudentsDB';";

        public static string BackUpFull = @"
                    BACKUP DATABASE [StudentsDB]
                    TO DISK = 'C:\Users\Public\StudentsDB.bak'
                    WITH NAME = N'Full_Backup_StudentsDB';";

        public static string CreateTable = @"
                    USE [StudentsDB]
                    IF OBJECT_ID('Students', 'U') IS NULL
                    BEGIN
                    	CREATE TABLE Students (
                    			Id INT PRIMARY KEY,
                    			[Name] VARCHAR(20) not null,
                    			[Surname] VARCHAR(20) not null,
                    			[Password] VARCHAR(20) not null
                    	)
                    END
                    ELSE
                    BEGIN
                    	DROP TABLE Students
                    	CREATE TABLE Students (
                    			Id INT PRIMARY KEY,
                    			[Name] VARCHAR(20) not null,
                    			[Surname] VARCHAR(20) not null,
                    			[Password] VARCHAR(20) not null
                    	)
                    
                    END";

        public static string CheckTable = @"
                    USE [StudentsDB];
                    DECLARE @last_id INT =  (
                    							SELECT	
                    								CASE
                    									WHEN (SELECT TOP(1) Id FROM Students ORDER BY Id DESC) IS NULL THEN 0
                                                        ELSE (SELECT TOP(1) Id FROM Students ORDER BY Id DESC) 
                                                    END
                    						)
                    SET @last_id = @last_id + 1
                    INSERT INTO Students VALUES(@last_id,'aaaaaaaaaaaaaaaaaaaa','aaaaaaaaaaaaaaaaaaaa','aaaaaaaaaaaaaaaaaaaa')
                    DELETE FROM Students WHERE Id = @last_id";
    }
}
