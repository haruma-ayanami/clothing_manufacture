/*
    Скрипт для SQL Server Management Studio.
    Создаёт базу clothing_manufacture и все таблицы, если они ещё не существуют.
*/

IF DB_ID(N'clothing_manufacture') IS NULL
BEGIN
    CREATE DATABASE clothing_manufacture;
END;
GO

USE clothing_manufacture;
GO

IF OBJECT_ID(N'dbo.Человек', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Человек (
        ID_Человек INT IDENTITY(1,1) PRIMARY KEY,
        Фамилия NVARCHAR(100) NOT NULL,
        Имя NVARCHAR(100) NOT NULL,
        Отчество NVARCHAR(100) NULL,
        Телефон NVARCHAR(30) NULL,
        Email NVARCHAR(150) NULL
    );
END;
GO

IF OBJECT_ID(N'dbo.Должность', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Должность (
        ID_Должность INT IDENTITY(1,1) PRIMARY KEY,
        Наименование NVARCHAR(150) NOT NULL,
        Описание NVARCHAR(500) NULL
    );
END;
GO

IF OBJECT_ID(N'dbo.Сотрудник', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Сотрудник (
        ID_Сотрудник INT IDENTITY(1,1) PRIMARY KEY,
        Табельный_номер NVARCHAR(50) NULL,
        Статус NVARCHAR(50) NOT NULL,
        FK_Человек INT NOT NULL,
        FK_Должность INT NOT NULL,
        CONSTRAINT FK_Сотрудник_Человек FOREIGN KEY (FK_Человек) REFERENCES dbo.Человек(ID_Человек),
        CONSTRAINT FK_Сотрудник_Должность FOREIGN KEY (FK_Должность) REFERENCES dbo.Должность(ID_Должность)
    );
END;
GO

IF OBJECT_ID(N'dbo.Процесс', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Процесс (
        ID_Процесс INT IDENTITY(1,1) PRIMARY KEY,
        Наименование NVARCHAR(200) NOT NULL,
        Описание NVARCHAR(1000) NULL
    );
END;
GO

IF OBJECT_ID(N'dbo.Этап_процесса', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Этап_процесса (
        ID_Этап_процесса INT IDENTITY(1,1) PRIMARY KEY,
        Наименование NVARCHAR(200) NOT NULL,
        Номер_этапа INT NOT NULL,
        Описание NVARCHAR(1000) NULL,
        Входные_данные NVARCHAR(1000) NULL,
        Результат NVARCHAR(1000) NULL,
        FK_Процесс INT NOT NULL,
        CONSTRAINT FK_Этап_процесса_Процесс FOREIGN KEY (FK_Процесс) REFERENCES dbo.Процесс(ID_Процесс)
    );
END;
GO

IF OBJECT_ID(N'dbo.Этап_должность', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Этап_должность (
        ID_Этап_должность INT IDENTITY(1,1) PRIMARY KEY,
        Роль_на_этапе NVARCHAR(150) NOT NULL,
        Описание_взаимодействия NVARCHAR(1000) NULL,
        FK_Этап_процесса INT NOT NULL,
        FK_Должность INT NOT NULL,
        CONSTRAINT FK_Этап_должность_Этап FOREIGN KEY (FK_Этап_процесса) REFERENCES dbo.Этап_процесса(ID_Этап_процесса),
        CONSTRAINT FK_Этап_должность_Должность FOREIGN KEY (FK_Должность) REFERENCES dbo.Должность(ID_Должность)
    );
END;
GO

IF OBJECT_ID(N'dbo.Классификатор', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Классификатор (
        ID_Классификатор INT IDENTITY(1,1) PRIMARY KEY,
        Тип_классификатора NVARCHAR(100) NOT NULL,
        Значение NVARCHAR(150) NOT NULL,
        Код NVARCHAR(50) NULL,
        Порядковый_номер INT NULL,
        Описание NVARCHAR(500) NULL,
        FK_Классификатор_родитель INT NULL,
        CONSTRAINT FK_Классификатор_Родитель FOREIGN KEY (FK_Классификатор_родитель) REFERENCES dbo.Классификатор(ID_Классификатор)
    );
END;
GO

IF OBJECT_ID(N'dbo.Ассортиментный_план', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Ассортиментный_план (
        ID_Ассортиментный_план INT IDENTITY(1,1) PRIMARY KEY,
        Номер_плана NVARCHAR(100) NOT NULL,
        Период NVARCHAR(100) NOT NULL,
        Статус NVARCHAR(50) NOT NULL,
        Комментарий NVARCHAR(1000) NULL,
        FK_Сотрудник_ответственный INT NULL,
        FK_Сотрудник_утвердивший INT NULL,
        CONSTRAINT FK_Ассортиментный_план_Ответственный FOREIGN KEY (FK_Сотрудник_ответственный) REFERENCES dbo.Сотрудник(ID_Сотрудник),
        CONSTRAINT FK_Ассортиментный_план_Утвердивший FOREIGN KEY (FK_Сотрудник_утвердивший) REFERENCES dbo.Сотрудник(ID_Сотрудник)
    );
END;
GO

IF OBJECT_ID(N'dbo.Коллекция', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Коллекция (
        ID_Коллекция INT IDENTITY(1,1) PRIMARY KEY,
        Наименование NVARCHAR(200) NOT NULL,
        Сезон NVARCHAR(100) NULL,
        Год INT NULL,
        Ценовой_сегмент NVARCHAR(100) NULL,
        Статус NVARCHAR(50) NOT NULL,
        FK_Ассортиментный_план INT NULL,
        CONSTRAINT FK_Коллекция_Ассортиментный_план FOREIGN KEY (FK_Ассортиментный_план) REFERENCES dbo.Ассортиментный_план(ID_Ассортиментный_план)
    );
END;
GO

IF OBJECT_ID(N'dbo.Модель', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Модель (
        ID_Модель INT IDENTITY(1,1) PRIMARY KEY,
        Артикул NVARCHAR(100) NOT NULL,
        Наименование NVARCHAR(200) NOT NULL,
        Описание NVARCHAR(1000) NULL,
        Технологическая_карта NVARCHAR(1000) NULL,
        Бренд NVARCHAR(150) NULL,
        Логотип NVARCHAR(250) NULL,
        Фирменный_цвет NVARCHAR(100) NULL,
        Требования_брендбука NVARCHAR(1000) NULL,
        Способ_брендирования NVARCHAR(100) NULL,
        Статус NVARCHAR(50) NOT NULL,
        FK_Классификатор_категория INT NOT NULL,
        FK_Коллекция INT NULL,
        FK_Сотрудник_дизайнер INT NULL,
        FK_Сотрудник_конструктор INT NULL,
        CONSTRAINT FK_Модель_Категория FOREIGN KEY (FK_Классификатор_категория) REFERENCES dbo.Классификатор(ID_Классификатор),
        CONSTRAINT FK_Модель_Коллекция FOREIGN KEY (FK_Коллекция) REFERENCES dbo.Коллекция(ID_Коллекция),
        CONSTRAINT FK_Модель_Дизайнер FOREIGN KEY (FK_Сотрудник_дизайнер) REFERENCES dbo.Сотрудник(ID_Сотрудник),
        CONSTRAINT FK_Модель_Конструктор FOREIGN KEY (FK_Сотрудник_конструктор) REFERENCES dbo.Сотрудник(ID_Сотрудник)
    );
    CREATE UNIQUE INDEX UX_Модель_Артикул ON dbo.Модель(Артикул);
END;
GO

IF OBJECT_ID(N'dbo.Вариант_модели', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Вариант_модели (
        ID_Вариант_модели INT IDENTITY(1,1) PRIMARY KEY,
        Штрихкод NVARCHAR(100) NULL,
        Статус NVARCHAR(50) NOT NULL,
        FK_Модель INT NOT NULL,
        FK_Классификатор_цвет INT NOT NULL,
        FK_Классификатор_размер INT NOT NULL,
        CONSTRAINT FK_Вариант_модели_Модель FOREIGN KEY (FK_Модель) REFERENCES dbo.Модель(ID_Модель),
        CONSTRAINT FK_Вариант_модели_Цвет FOREIGN KEY (FK_Классификатор_цвет) REFERENCES dbo.Классификатор(ID_Классификатор),
        CONSTRAINT FK_Вариант_модели_Размер FOREIGN KEY (FK_Классификатор_размер) REFERENCES dbo.Классификатор(ID_Классификатор)
    );
    CREATE UNIQUE INDEX UX_Вариант_модели_Штрихкод ON dbo.Вариант_модели(Штрихкод) WHERE Штрихкод IS NOT NULL;
END;
GO

IF OBJECT_ID(N'dbo.Материал', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Материал (
        ID_Материал INT IDENTITY(1,1) PRIMARY KEY,
        Наименование NVARCHAR(200) NOT NULL,
        Тип_материала NVARCHAR(100) NOT NULL,
        Единица_измерения NVARCHAR(30) NOT NULL,
        Описание NVARCHAR(500) NULL
    );
END;
GO

IF OBJECT_ID(N'dbo.Модель_материал', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Модель_материал (
        ID_Модель_материал INT IDENTITY(1,1) PRIMARY KEY,
        Количество_на_изделие DECIMAL(10,3) NOT NULL,
        Основной_материал BIT NOT NULL,
        FK_Модель INT NOT NULL,
        FK_Материал INT NOT NULL,
        CONSTRAINT FK_Модель_материал_Модель FOREIGN KEY (FK_Модель) REFERENCES dbo.Модель(ID_Модель),
        CONSTRAINT FK_Модель_материал_Материал FOREIGN KEY (FK_Материал) REFERENCES dbo.Материал(ID_Материал)
    );
END;
GO

IF OBJECT_ID(N'dbo.Лекало', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Лекало (
        ID_Лекало INT IDENTITY(1,1) PRIMARY KEY,
        Версия NVARCHAR(50) NOT NULL,
        Дата_создания DATE NOT NULL,
        Файл_лекала NVARCHAR(300) NULL,
        Статус NVARCHAR(50) NOT NULL,
        Комментарий NVARCHAR(1000) NULL,
        FK_Модель INT NOT NULL,
        FK_Сотрудник_конструктор INT NOT NULL,
        CONSTRAINT FK_Лекало_Модель FOREIGN KEY (FK_Модель) REFERENCES dbo.Модель(ID_Модель),
        CONSTRAINT FK_Лекало_Конструктор FOREIGN KEY (FK_Сотрудник_конструктор) REFERENCES dbo.Сотрудник(ID_Сотрудник)
    );
END;
GO

IF OBJECT_ID(N'dbo.Образец', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Образец (
        ID_Образец INT IDENTITY(1,1) PRIMARY KEY,
        Дата_изготовления DATE NOT NULL,
        Результат_примерки NVARCHAR(1000) NULL,
        Статус NVARCHAR(50) NOT NULL,
        Комментарий_по_доработке NVARCHAR(1000) NULL,
        FK_Модель INT NOT NULL,
        FK_Лекало INT NOT NULL,
        FK_Сотрудник_портной INT NULL,
        FK_Сотрудник_технолог INT NULL,
        CONSTRAINT FK_Образец_Модель FOREIGN KEY (FK_Модель) REFERENCES dbo.Модель(ID_Модель),
        CONSTRAINT FK_Образец_Лекало FOREIGN KEY (FK_Лекало) REFERENCES dbo.Лекало(ID_Лекало),
        CONSTRAINT FK_Образец_Портной FOREIGN KEY (FK_Сотрудник_портной) REFERENCES dbo.Сотрудник(ID_Сотрудник),
        CONSTRAINT FK_Образец_Технолог FOREIGN KEY (FK_Сотрудник_технолог) REFERENCES dbo.Сотрудник(ID_Сотрудник)
    );
END;
GO

IF OBJECT_ID(N'dbo.Клиент', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Клиент (
        ID_Клиент INT IDENTITY(1,1) PRIMARY KEY,
        Наименование NVARCHAR(200) NOT NULL,
        Тип_клиента NVARCHAR(50) NOT NULL,
        Телефон NVARCHAR(30) NULL,
        Email NVARCHAR(150) NULL,
        Адрес NVARCHAR(500) NULL
    );
END;
GO

IF OBJECT_ID(N'dbo.Заказ_клиента', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Заказ_клиента (
        ID_Заказ_клиента INT IDENTITY(1,1) PRIMARY KEY,
        Номер_заказа NVARCHAR(100) NOT NULL,
        Дата_заказа DATE NOT NULL,
        Срок_исполнения DATE NULL,
        Статус NVARCHAR(50) NOT NULL,
        Требования_к_брендированию NVARCHAR(1000) NULL,
        Комментарий NVARCHAR(1000) NULL,
        FK_Клиент INT NOT NULL,
        FK_Сотрудник_менеджер INT NULL,
        CONSTRAINT FK_Заказ_клиента_Клиент FOREIGN KEY (FK_Клиент) REFERENCES dbo.Клиент(ID_Клиент),
        CONSTRAINT FK_Заказ_клиента_Менеджер FOREIGN KEY (FK_Сотрудник_менеджер) REFERENCES dbo.Сотрудник(ID_Сотрудник)
    );
    CREATE UNIQUE INDEX UX_Заказ_клиента_Номер ON dbo.Заказ_клиента(Номер_заказа);
END;
GO

IF OBJECT_ID(N'dbo.Позиция_заказа', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Позиция_заказа (
        ID_Позиция_заказа INT IDENTITY(1,1) PRIMARY KEY,
        Количество INT NOT NULL,
        Цена_за_единицу DECIMAL(18,2) NULL,
        Комментарий NVARCHAR(1000) NULL,
        FK_Заказ_клиента INT NOT NULL,
        FK_Вариант_модели INT NOT NULL,
        CONSTRAINT FK_Позиция_заказа_Заказ FOREIGN KEY (FK_Заказ_клиента) REFERENCES dbo.Заказ_клиента(ID_Заказ_клиента),
        CONSTRAINT FK_Позиция_заказа_Вариант FOREIGN KEY (FK_Вариант_модели) REFERENCES dbo.Вариант_модели(ID_Вариант_модели)
    );
END;
GO

IF OBJECT_ID(N'dbo.Производственное_задание', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Производственное_задание (
        ID_Производственное_задание INT IDENTITY(1,1) PRIMARY KEY,
        Номер_задания NVARCHAR(100) NOT NULL,
        Дата_создания DATE NOT NULL,
        Плановая_дата_начала DATE NULL,
        Плановая_дата_окончания DATE NULL,
        Источник_запуска NVARCHAR(100) NULL,
        Статус NVARCHAR(50) NOT NULL,
        Комментарий NVARCHAR(1000) NULL,
        FK_Сотрудник_планировщик INT NOT NULL,
        FK_Заказ_клиента INT NULL,
        FK_Коллекция INT NULL,
        FK_Ассортиментный_план INT NULL,
        CONSTRAINT FK_Производственное_задание_Планировщик FOREIGN KEY (FK_Сотрудник_планировщик) REFERENCES dbo.Сотрудник(ID_Сотрудник),
        CONSTRAINT FK_Производственное_задание_Заказ FOREIGN KEY (FK_Заказ_клиента) REFERENCES dbo.Заказ_клиента(ID_Заказ_клиента),
        CONSTRAINT FK_Производственное_задание_Коллекция FOREIGN KEY (FK_Коллекция) REFERENCES dbo.Коллекция(ID_Коллекция),
        CONSTRAINT FK_Производственное_задание_План FOREIGN KEY (FK_Ассортиментный_план) REFERENCES dbo.Ассортиментный_план(ID_Ассортиментный_план),
        CONSTRAINT CK_Производственное_задание_Источник CHECK (FK_Заказ_клиента IS NOT NULL OR FK_Коллекция IS NOT NULL OR FK_Ассортиментный_план IS NOT NULL)
    );
END;
GO

IF OBJECT_ID(N'dbo.Производственная_партия', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Производственная_партия (
        ID_Производственная_партия INT IDENTITY(1,1) PRIMARY KEY,
        Номер_партии NVARCHAR(100) NOT NULL,
        Количество INT NOT NULL,
        Дата_запуска DATE NULL,
        Дата_завершения DATE NULL,
        Статус NVARCHAR(50) NOT NULL,
        FK_Производственное_задание INT NOT NULL,
        FK_Вариант_модели INT NOT NULL,
        CONSTRAINT FK_Производственная_партия_Задание FOREIGN KEY (FK_Производственное_задание) REFERENCES dbo.Производственное_задание(ID_Производственное_задание),
        CONSTRAINT FK_Производственная_партия_Вариант FOREIGN KEY (FK_Вариант_модели) REFERENCES dbo.Вариант_модели(ID_Вариант_модели)
    );
END;
GO

IF OBJECT_ID(N'dbo.Склад', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Склад (
        ID_Склад INT IDENTITY(1,1) PRIMARY KEY,
        Наименование NVARCHAR(200) NOT NULL,
        Тип_склада NVARCHAR(100) NOT NULL,
        Адрес NVARCHAR(500) NULL
    );
END;
GO

IF OBJECT_ID(N'dbo.Складская_ячейка', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Складская_ячейка (
        ID_Складская_ячейка INT IDENTITY(1,1) PRIMARY KEY,
        Код_ячейки NVARCHAR(100) NOT NULL,
        Зона_хранения NVARCHAR(100) NULL,
        Описание NVARCHAR(500) NULL,
        FK_Склад INT NOT NULL,
        CONSTRAINT FK_Складская_ячейка_Склад FOREIGN KEY (FK_Склад) REFERENCES dbo.Склад(ID_Склад)
    );
END;
GO

IF OBJECT_ID(N'dbo.Складской_остаток', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Складской_остаток (
        ID_Складской_остаток INT IDENTITY(1,1) PRIMARY KEY,
        Количество DECIMAL(10,3) NOT NULL,
        Дата_обновления DATE NOT NULL,
        FK_Складская_ячейка INT NOT NULL,
        FK_Материал INT NULL,
        FK_Вариант_модели INT NULL,
        CONSTRAINT FK_Складской_остаток_Ячейка FOREIGN KEY (FK_Складская_ячейка) REFERENCES dbo.Складская_ячейка(ID_Складская_ячейка),
        CONSTRAINT FK_Складской_остаток_Материал FOREIGN KEY (FK_Материал) REFERENCES dbo.Материал(ID_Материал),
        CONSTRAINT FK_Складской_остаток_Вариант FOREIGN KEY (FK_Вариант_модели) REFERENCES dbo.Вариант_модели(ID_Вариант_модели),
        CONSTRAINT CK_Складской_остаток_Номенклатура CHECK (
            (FK_Материал IS NOT NULL AND FK_Вариант_модели IS NULL) OR
            (FK_Материал IS NULL AND FK_Вариант_модели IS NOT NULL)
        )
    );
END;
GO

IF OBJECT_ID(N'dbo.Контроль_качества', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Контроль_качества (
        ID_Контроль_качества INT IDENTITY(1,1) PRIMARY KEY,
        Дата_контроля DATE NOT NULL,
        Тип_контроля NVARCHAR(100) NOT NULL,
        Результат NVARCHAR(50) NOT NULL,
        Замечания NVARCHAR(1000) NULL,
        Решение NVARCHAR(100) NULL,
        FK_Производственная_партия INT NULL,
        FK_Образец INT NULL,
        FK_Сотрудник_контролер INT NOT NULL,
        CONSTRAINT FK_Контроль_качества_Партия FOREIGN KEY (FK_Производственная_партия) REFERENCES dbo.Производственная_партия(ID_Производственная_партия),
        CONSTRAINT FK_Контроль_качества_Образец FOREIGN KEY (FK_Образец) REFERENCES dbo.Образец(ID_Образец),
        CONSTRAINT FK_Контроль_качества_Контролер FOREIGN KEY (FK_Сотрудник_контролер) REFERENCES dbo.Сотрудник(ID_Сотрудник),
        CONSTRAINT CK_Контроль_качества_Объект CHECK (
            (FK_Производственная_партия IS NOT NULL AND FK_Образец IS NULL) OR
            (FK_Производственная_партия IS NULL AND FK_Образец IS NOT NULL)
        )
    );
END;
GO

IF OBJECT_ID(N'dbo.Операция_товара', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Операция_товара (
        ID_Операция_товара INT IDENTITY(1,1) PRIMARY KEY,
        Тип_операции NVARCHAR(100) NOT NULL,
        Дата_операции DATE NOT NULL,
        Количество DECIMAL(10,3) NOT NULL,
        Цена_за_единицу DECIMAL(18,2) NULL,
        Номер_документа NVARCHAR(100) NULL,
        Получатель NVARCHAR(200) NULL,
        Адрес_доставки NVARCHAR(500) NULL,
        Маршрут NVARCHAR(500) NULL,
        Статус NVARCHAR(50) NOT NULL,
        Причина_возврата NVARCHAR(500) NULL,
        Решение_по_возврату NVARCHAR(500) NULL,
        FK_Склад_отправитель INT NULL,
        FK_Склад_получатель INT NULL,
        FK_Материал INT NULL,
        FK_Вариант_модели INT NULL,
        FK_Клиент INT NULL,
        FK_Сотрудник_ответственный INT NULL,
        CONSTRAINT FK_Операция_товара_Склад_отправитель FOREIGN KEY (FK_Склад_отправитель) REFERENCES dbo.Склад(ID_Склад),
        CONSTRAINT FK_Операция_товара_Склад_получатель FOREIGN KEY (FK_Склад_получатель) REFERENCES dbo.Склад(ID_Склад),
        CONSTRAINT FK_Операция_товара_Материал FOREIGN KEY (FK_Материал) REFERENCES dbo.Материал(ID_Материал),
        CONSTRAINT FK_Операция_товара_Вариант FOREIGN KEY (FK_Вариант_модели) REFERENCES dbo.Вариант_модели(ID_Вариант_модели),
        CONSTRAINT FK_Операция_товара_Клиент FOREIGN KEY (FK_Клиент) REFERENCES dbo.Клиент(ID_Клиент),
        CONSTRAINT FK_Операция_товара_Ответственный FOREIGN KEY (FK_Сотрудник_ответственный) REFERENCES dbo.Сотрудник(ID_Сотрудник),
        CONSTRAINT CK_Операция_товара_Номенклатура CHECK (
            (FK_Материал IS NOT NULL AND FK_Вариант_модели IS NULL) OR
            (FK_Материал IS NULL AND FK_Вариант_модели IS NOT NULL)
        )
    );
END;
GO

PRINT N'База clothing_manufacture готова.';
GO
