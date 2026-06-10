using clothing_manufacture.Data;
using Microsoft.EntityFrameworkCore;

namespace clothing_manufacture.Services;

public static class DatabaseTestService
{
    public static string CheckConnection()
    {
        try
        {
            using ClothingManufactureDbContext db = new();

            if (!db.Database.CanConnect())
                return "Не удалось подключиться к базе данных.";

            return "Подключение к SQL Server успешно.";
        }
        catch (Exception ex)
        {
            return "Ошибка подключения к базе данных:\n\n" + GetFullError(ex);
        }
    }

    public static string GetFullError(Exception ex)
    {
        List<string> messages = new();
        Exception? current = ex;

        while (current != null)
        {
            messages.Add(current.Message);
            current = current.InnerException;
        }

        return string.Join("\n", messages);
    }

    public static Dictionary<string, int> GetTableCounts()
    {
        using ClothingManufactureDbContext db = new();

        return new Dictionary<string, int>
        {
            ["Люди"] = db.People.Count(),
            ["Должности"] = db.Positions.Count(),
            ["Сотрудники"] = db.Employees.Count(),
            ["Клиенты"] = db.Clients.Count(),
            ["Материалы"] = db.Materials.Count(),
            ["Классификаторы"] = db.Classifiers.Count(),
            ["Коллекции"] = db.ClothingCollections.Count(),
            ["Модели"] = db.ClothingModels.Count(),
            ["Варианты моделей"] = db.ModelVariants.Count(),
            ["Заказы"] = db.CustomerOrders.Count(),
            ["Производственные задания"] = db.ProductionTasks.Count(),
            ["Склады"] = db.Warehouses.Count()
        };
    }
}
