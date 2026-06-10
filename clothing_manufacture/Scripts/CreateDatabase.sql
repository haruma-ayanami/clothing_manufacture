IF DB_ID(N'clothing_manufacture') IS NULL
BEGIN
    CREATE DATABASE clothing_manufacture;
END
GO
USE clothing_manufacture;
GO
-- Основную схему таблиц выполни в SSMS по своей финальной ERD-схеме.
-- C#-проект ожидает базу clothing_manufacture и русские названия таблиц/колонок.
