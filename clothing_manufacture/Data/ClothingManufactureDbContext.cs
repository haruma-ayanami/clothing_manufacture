using clothing_manufacture.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace clothing_manufacture.Data;

public class ClothingManufactureDbContext : DbContext
{
    public DbSet<Person> People => Set<Person>();
    public DbSet<Position> Positions => Set<Position>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<ProductionProcess> ProductionProcesses => Set<ProductionProcess>();
    public DbSet<ProcessStage> ProcessStages => Set<ProcessStage>();
    public DbSet<StagePosition> StagePositions => Set<StagePosition>();
    public DbSet<Classifier> Classifiers => Set<Classifier>();
    public DbSet<AssortmentPlan> AssortmentPlans => Set<AssortmentPlan>();
    public DbSet<ClothingCollection> ClothingCollections => Set<ClothingCollection>();
    public DbSet<ClothingModel> ClothingModels => Set<ClothingModel>();
    public DbSet<ModelVariant> ModelVariants => Set<ModelVariant>();
    public DbSet<Material> Materials => Set<Material>();
    public DbSet<ModelMaterial> ModelMaterials => Set<ModelMaterial>();
    public DbSet<Pattern> Patterns => Set<Pattern>();
    public DbSet<Sample> Samples => Set<Sample>();
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<CustomerOrder> CustomerOrders => Set<CustomerOrder>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<ProductionTask> ProductionTasks => Set<ProductionTask>();
    public DbSet<ProductionBatch> ProductionBatches => Set<ProductionBatch>();
    public DbSet<Warehouse> Warehouses => Set<Warehouse>();
    public DbSet<WarehouseCell> WarehouseCells => Set<WarehouseCell>();
    public DbSet<WarehouseStock> WarehouseStocks => Set<WarehouseStock>();
    public DbSet<QualityControl> QualityControls => Set<QualityControl>();
    public DbSet<ProductOperation> ProductOperations => Set<ProductOperation>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured)
            return;

        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        string? connectionString = configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrWhiteSpace(connectionString))
            throw new InvalidOperationException("Строка подключения DefaultConnection не найдена в appsettings.json.");

        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>(entity =>
        {
            entity.ToTable("Человек");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("ID_Человек");
            entity.Property(e => e.LastName).HasColumnName("Фамилия").HasMaxLength(100).IsRequired();
            entity.Property(e => e.FirstName).HasColumnName("Имя").HasMaxLength(100).IsRequired();
            entity.Property(e => e.MiddleName).HasColumnName("Отчество").HasMaxLength(100);
            entity.Property(e => e.Phone).HasColumnName("Телефон").HasMaxLength(30);
            entity.Property(e => e.Email).HasColumnName("Email").HasMaxLength(150);
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.ToTable("Должность");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("ID_Должность");
            entity.Property(e => e.Name).HasColumnName("Наименование").HasMaxLength(150).IsRequired();
            entity.Property(e => e.Description).HasColumnName("Описание").HasMaxLength(500);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("Сотрудник");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("ID_Сотрудник");
            entity.Property(e => e.PersonnelNumber).HasColumnName("Табельный_номер").HasMaxLength(50);
            entity.Property(e => e.Status).HasColumnName("Статус").HasMaxLength(50).IsRequired();
            entity.Property(e => e.PersonId).HasColumnName("FK_Человек");
            entity.Property(e => e.PositionId).HasColumnName("FK_Должность");
            entity.HasOne(e => e.Person).WithMany(p => p.Employees).HasForeignKey(e => e.PersonId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(e => e.Position).WithMany(p => p.Employees).HasForeignKey(e => e.PositionId).OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<ProductionProcess>(entity =>
        {
            entity.ToTable("Процесс");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("ID_Процесс");
            entity.Property(e => e.Name).HasColumnName("Наименование").HasMaxLength(200).IsRequired();
            entity.Property(e => e.Description).HasColumnName("Описание").HasMaxLength(1000);
        });

        modelBuilder.Entity<ProcessStage>(entity =>
        {
            entity.ToTable("Этап_процесса");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("ID_Этап_процесса");
            entity.Property(e => e.Name).HasColumnName("Наименование").HasMaxLength(200).IsRequired();
            entity.Property(e => e.StageNumber).HasColumnName("Номер_этапа").IsRequired();
            entity.Property(e => e.Description).HasColumnName("Описание").HasMaxLength(1000);
            entity.Property(e => e.InputData).HasColumnName("Входные_данные").HasMaxLength(1000);
            entity.Property(e => e.Result).HasColumnName("Результат").HasMaxLength(1000);
            entity.Property(e => e.ProcessId).HasColumnName("FK_Процесс");
            entity.HasOne(e => e.Process).WithMany(p => p.Stages).HasForeignKey(e => e.ProcessId).OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<StagePosition>(entity =>
        {
            entity.ToTable("Этап_должность");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("ID_Этап_должность");
            entity.Property(e => e.StageRole).HasColumnName("Роль_на_этапе").HasMaxLength(150).IsRequired();
            entity.Property(e => e.InteractionDescription).HasColumnName("Описание_взаимодействия").HasMaxLength(1000);
            entity.Property(e => e.ProcessStageId).HasColumnName("FK_Этап_процесса");
            entity.Property(e => e.PositionId).HasColumnName("FK_Должность");
            entity.HasOne(e => e.ProcessStage).WithMany(s => s.StagePositions).HasForeignKey(e => e.ProcessStageId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(e => e.Position).WithMany(p => p.StagePositions).HasForeignKey(e => e.PositionId).OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<Classifier>(entity =>
        {
            entity.ToTable("Классификатор");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("ID_Классификатор");
            entity.Property(e => e.ClassifierType).HasColumnName("Тип_классификатора").HasMaxLength(100).IsRequired();
            entity.Property(e => e.Value).HasColumnName("Значение").HasMaxLength(150).IsRequired();
            entity.Property(e => e.Code).HasColumnName("Код").HasMaxLength(50);
            entity.Property(e => e.SortOrder).HasColumnName("Порядковый_номер");
            entity.Property(e => e.Description).HasColumnName("Описание").HasMaxLength(500);
            entity.Property(e => e.ParentClassifierId).HasColumnName("FK_Классификатор_родитель");
            entity.HasOne(e => e.ParentClassifier).WithMany(e => e.ChildClassifiers).HasForeignKey(e => e.ParentClassifierId).OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<AssortmentPlan>(entity =>
        {
            entity.ToTable("Ассортиментный_план");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("ID_Ассортиментный_план");
            entity.Property(e => e.PlanNumber).HasColumnName("Номер_плана").HasMaxLength(100).IsRequired();
            entity.Property(e => e.Period).HasColumnName("Период").HasMaxLength(100).IsRequired();
            entity.Property(e => e.Status).HasColumnName("Статус").HasMaxLength(50).IsRequired();
            entity.Property(e => e.Comment).HasColumnName("Комментарий").HasMaxLength(1000);
            entity.Property(e => e.ResponsibleEmployeeId).HasColumnName("FK_Сотрудник_ответственный");
            entity.Property(e => e.ApprovedEmployeeId).HasColumnName("FK_Сотрудник_утвердивший");
            entity.HasOne(e => e.ResponsibleEmployee).WithMany(e => e.ResponsibleAssortmentPlans).HasForeignKey(e => e.ResponsibleEmployeeId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(e => e.ApprovedEmployee).WithMany(e => e.ApprovedAssortmentPlans).HasForeignKey(e => e.ApprovedEmployeeId).OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<ClothingCollection>(entity =>
        {
            entity.ToTable("Коллекция");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("ID_Коллекция");
            entity.Property(e => e.Name).HasColumnName("Наименование").HasMaxLength(200).IsRequired();
            entity.Property(e => e.Season).HasColumnName("Сезон").HasMaxLength(100);
            entity.Property(e => e.Year).HasColumnName("Год");
            entity.Property(e => e.PriceSegment).HasColumnName("Ценовой_сегмент").HasMaxLength(100);
            entity.Property(e => e.Status).HasColumnName("Статус").HasMaxLength(50).IsRequired();
            entity.Property(e => e.AssortmentPlanId).HasColumnName("FK_Ассортиментный_план");
            entity.HasOne(e => e.AssortmentPlan).WithMany(p => p.Collections).HasForeignKey(e => e.AssortmentPlanId).OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<ClothingModel>(entity =>
        {
            entity.ToTable("Модель");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("ID_Модель");
            entity.Property(e => e.Article).HasColumnName("Артикул").HasMaxLength(100).IsRequired();
            entity.Property(e => e.Name).HasColumnName("Наименование").HasMaxLength(200).IsRequired();
            entity.Property(e => e.Description).HasColumnName("Описание").HasMaxLength(1000);
            entity.Property(e => e.TechnologicalCard).HasColumnName("Технологическая_карта").HasMaxLength(1000);
            entity.Property(e => e.Brand).HasColumnName("Бренд").HasMaxLength(150);
            entity.Property(e => e.Logo).HasColumnName("Логотип").HasMaxLength(250);
            entity.Property(e => e.BrandColor).HasColumnName("Фирменный_цвет").HasMaxLength(100);
            entity.Property(e => e.BrandbookRequirements).HasColumnName("Требования_брендбука").HasMaxLength(1000);
            entity.Property(e => e.BrandingMethod).HasColumnName("Способ_брендирования").HasMaxLength(100);
            entity.Property(e => e.Status).HasColumnName("Статус").HasMaxLength(50).IsRequired();
            entity.Property(e => e.CategoryClassifierId).HasColumnName("FK_Классификатор_категория");
            entity.Property(e => e.CollectionId).HasColumnName("FK_Коллекция");
            entity.Property(e => e.DesignerEmployeeId).HasColumnName("FK_Сотрудник_дизайнер");
            entity.Property(e => e.ConstructorEmployeeId).HasColumnName("FK_Сотрудник_конструктор");
            entity.HasOne(e => e.CategoryClassifier).WithMany(c => c.CategoryModels).HasForeignKey(e => e.CategoryClassifierId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(e => e.Collection).WithMany(c => c.Models).HasForeignKey(e => e.CollectionId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(e => e.DesignerEmployee).WithMany(e => e.DesignerModels).HasForeignKey(e => e.DesignerEmployeeId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(e => e.ConstructorEmployee).WithMany(e => e.ConstructorModels).HasForeignKey(e => e.ConstructorEmployeeId).OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<ModelVariant>(entity =>
        {
            entity.ToTable("Вариант_модели");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("ID_Вариант_модели");
            entity.Property(e => e.Barcode).HasColumnName("Штрихкод").HasMaxLength(100);
            entity.Property(e => e.Status).HasColumnName("Статус").HasMaxLength(50).IsRequired();
            entity.Property(e => e.ModelId).HasColumnName("FK_Модель");
            entity.Property(e => e.ColorClassifierId).HasColumnName("FK_Классификатор_цвет");
            entity.Property(e => e.SizeClassifierId).HasColumnName("FK_Классификатор_размер");
            entity.HasOne(e => e.Model).WithMany(m => m.Variants).HasForeignKey(e => e.ModelId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(e => e.ColorClassifier).WithMany(c => c.ColorModelVariants).HasForeignKey(e => e.ColorClassifierId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(e => e.SizeClassifier).WithMany(c => c.SizeModelVariants).HasForeignKey(e => e.SizeClassifierId).OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.ToTable("Материал");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("ID_Материал");
            entity.Property(e => e.Name).HasColumnName("Наименование").HasMaxLength(200).IsRequired();
            entity.Property(e => e.MaterialType).HasColumnName("Тип_материала").HasMaxLength(100).IsRequired();
            entity.Property(e => e.UnitOfMeasure).HasColumnName("Единица_измерения").HasMaxLength(30).IsRequired();
            entity.Property(e => e.Description).HasColumnName("Описание").HasMaxLength(500);
        });

        modelBuilder.Entity<ModelMaterial>(entity =>
        {
            entity.ToTable("Модель_материал");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("ID_Модель_материал");
            entity.Property(e => e.QuantityPerItem).HasColumnName("Количество_на_изделие").HasColumnType("decimal(10,3)").IsRequired();
            entity.Property(e => e.IsMainMaterial).HasColumnName("Основной_материал").IsRequired();
            entity.Property(e => e.ModelId).HasColumnName("FK_Модель");
            entity.Property(e => e.MaterialId).HasColumnName("FK_Материал");
            entity.HasOne(e => e.Model).WithMany(m => m.ModelMaterials).HasForeignKey(e => e.ModelId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(e => e.Material).WithMany(m => m.ModelMaterials).HasForeignKey(e => e.MaterialId).OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<Pattern>(entity =>
        {
            entity.ToTable("Лекало");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("ID_Лекало");
            entity.Property(e => e.Version).HasColumnName("Версия").HasMaxLength(50).IsRequired();
            entity.Property(e => e.CreationDate).HasColumnName("Дата_создания").HasColumnType("date").IsRequired();
            entity.Property(e => e.PatternFile).HasColumnName("Файл_лекала").HasMaxLength(300);
            entity.Property(e => e.Status).HasColumnName("Статус").HasMaxLength(50).IsRequired();
            entity.Property(e => e.Comment).HasColumnName("Комментарий").HasMaxLength(1000);
            entity.Property(e => e.ModelId).HasColumnName("FK_Модель");
            entity.Property(e => e.ConstructorEmployeeId).HasColumnName("FK_Сотрудник_конструктор");
            entity.HasOne(e => e.Model).WithMany(m => m.Patterns).HasForeignKey(e => e.ModelId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(e => e.ConstructorEmployee).WithMany(e => e.ConstructedPatterns).HasForeignKey(e => e.ConstructorEmployeeId).OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<Sample>(entity =>
        {
            entity.ToTable("Образец");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("ID_Образец");
            entity.Property(e => e.ManufactureDate).HasColumnName("Дата_изготовления").HasColumnType("date").IsRequired();
            entity.Property(e => e.FittingResult).HasColumnName("Результат_примерки").HasMaxLength(1000);
            entity.Property(e => e.Status).HasColumnName("Статус").HasMaxLength(50).IsRequired();
            entity.Property(e => e.RevisionComment).HasColumnName("Комментарий_по_доработке").HasMaxLength(1000);
            entity.Property(e => e.ModelId).HasColumnName("FK_Модель");
            entity.Property(e => e.PatternId).HasColumnName("FK_Лекало");
            entity.Property(e => e.TailorEmployeeId).HasColumnName("FK_Сотрудник_портной");
            entity.Property(e => e.TechnologistEmployeeId).HasColumnName("FK_Сотрудник_технолог");
            entity.HasOne(e => e.Model).WithMany(m => m.Samples).HasForeignKey(e => e.ModelId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(e => e.Pattern).WithMany(p => p.Samples).HasForeignKey(e => e.PatternId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(e => e.TailorEmployee).WithMany(e => e.TailorSamples).HasForeignKey(e => e.TailorEmployeeId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(e => e.TechnologistEmployee).WithMany(e => e.TechnologistSamples).HasForeignKey(e => e.TechnologistEmployeeId).OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.ToTable("Клиент");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("ID_Клиент");
            entity.Property(e => e.Name).HasColumnName("Наименование").HasMaxLength(200).IsRequired();
            entity.Property(e => e.ClientType).HasColumnName("Тип_клиента").HasMaxLength(50).IsRequired();
            entity.Property(e => e.Phone).HasColumnName("Телефон").HasMaxLength(30);
            entity.Property(e => e.Email).HasColumnName("Email").HasMaxLength(150);
            entity.Property(e => e.Address).HasColumnName("Адрес").HasMaxLength(500);
        });

        modelBuilder.Entity<CustomerOrder>(entity =>
        {
            entity.ToTable("Заказ_клиента");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("ID_Заказ_клиента");
            entity.Property(e => e.OrderNumber).HasColumnName("Номер_заказа").HasMaxLength(100).IsRequired();
            entity.Property(e => e.OrderDate).HasColumnName("Дата_заказа").HasColumnType("date").IsRequired();
            entity.Property(e => e.DueDate).HasColumnName("Срок_исполнения").HasColumnType("date");
            entity.Property(e => e.Status).HasColumnName("Статус").HasMaxLength(50).IsRequired();
            entity.Property(e => e.BrandingRequirements).HasColumnName("Требования_к_брендированию").HasMaxLength(1000);
            entity.Property(e => e.Comment).HasColumnName("Комментарий").HasMaxLength(1000);
            entity.Property(e => e.ClientId).HasColumnName("FK_Клиент");
            entity.Property(e => e.ManagerEmployeeId).HasColumnName("FK_Сотрудник_менеджер");
            entity.HasOne(e => e.Client).WithMany(c => c.CustomerOrders).HasForeignKey(e => e.ClientId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(e => e.ManagerEmployee).WithMany(e => e.ManagedOrders).HasForeignKey(e => e.ManagerEmployeeId).OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.ToTable("Позиция_заказа");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("ID_Позиция_заказа");
            entity.Property(e => e.Quantity).HasColumnName("Количество").IsRequired();
            entity.Property(e => e.UnitPrice).HasColumnName("Цена_за_единицу").HasColumnType("decimal(18,2)");
            entity.Property(e => e.Comment).HasColumnName("Комментарий").HasMaxLength(1000);
            entity.Property(e => e.CustomerOrderId).HasColumnName("FK_Заказ_клиента");
            entity.Property(e => e.ModelVariantId).HasColumnName("FK_Вариант_модели");
            entity.HasOne(e => e.CustomerOrder).WithMany(o => o.OrderItems).HasForeignKey(e => e.CustomerOrderId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(e => e.ModelVariant).WithMany(v => v.OrderItems).HasForeignKey(e => e.ModelVariantId).OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<ProductionTask>(entity =>
        {
            entity.ToTable("Производственное_задание");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("ID_Производственное_задание");
            entity.Property(e => e.TaskNumber).HasColumnName("Номер_задания").HasMaxLength(100).IsRequired();
            entity.Property(e => e.CreationDate).HasColumnName("Дата_создания").HasColumnType("date").IsRequired();
            entity.Property(e => e.PlannedStartDate).HasColumnName("Плановая_дата_начала").HasColumnType("date");
            entity.Property(e => e.PlannedEndDate).HasColumnName("Плановая_дата_окончания").HasColumnType("date");
            entity.Property(e => e.LaunchSource).HasColumnName("Источник_запуска").HasMaxLength(100);
            entity.Property(e => e.Status).HasColumnName("Статус").HasMaxLength(50).IsRequired();
            entity.Property(e => e.Comment).HasColumnName("Комментарий").HasMaxLength(1000);
            entity.Property(e => e.PlannerEmployeeId).HasColumnName("FK_Сотрудник_планировщик");
            entity.Property(e => e.CustomerOrderId).HasColumnName("FK_Заказ_клиента");
            entity.Property(e => e.CollectionId).HasColumnName("FK_Коллекция");
            entity.Property(e => e.AssortmentPlanId).HasColumnName("FK_Ассортиментный_план");
            entity.HasOne(e => e.PlannerEmployee).WithMany(e => e.PlannedProductionTasks).HasForeignKey(e => e.PlannerEmployeeId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(e => e.CustomerOrder).WithMany(o => o.ProductionTasks).HasForeignKey(e => e.CustomerOrderId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(e => e.Collection).WithMany(c => c.ProductionTasks).HasForeignKey(e => e.CollectionId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(e => e.AssortmentPlan).WithMany(p => p.ProductionTasks).HasForeignKey(e => e.AssortmentPlanId).OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<ProductionBatch>(entity =>
        {
            entity.ToTable("Производственная_партия");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("ID_Производственная_партия");
            entity.Property(e => e.BatchNumber).HasColumnName("Номер_партии").HasMaxLength(100).IsRequired();
            entity.Property(e => e.Quantity).HasColumnName("Количество").IsRequired();
            entity.Property(e => e.StartDate).HasColumnName("Дата_запуска").HasColumnType("date");
            entity.Property(e => e.EndDate).HasColumnName("Дата_завершения").HasColumnType("date");
            entity.Property(e => e.Status).HasColumnName("Статус").HasMaxLength(50).IsRequired();
            entity.Property(e => e.ProductionTaskId).HasColumnName("FK_Производственное_задание");
            entity.Property(e => e.ModelVariantId).HasColumnName("FK_Вариант_модели");
            entity.HasOne(e => e.ProductionTask).WithMany(t => t.ProductionBatches).HasForeignKey(e => e.ProductionTaskId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(e => e.ModelVariant).WithMany(v => v.ProductionBatches).HasForeignKey(e => e.ModelVariantId).OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<Warehouse>(entity =>
        {
            entity.ToTable("Склад");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("ID_Склад");
            entity.Property(e => e.Name).HasColumnName("Наименование").HasMaxLength(200).IsRequired();
            entity.Property(e => e.WarehouseType).HasColumnName("Тип_склада").HasMaxLength(100).IsRequired();
            entity.Property(e => e.Address).HasColumnName("Адрес").HasMaxLength(500);
        });

        modelBuilder.Entity<WarehouseCell>(entity =>
        {
            entity.ToTable("Складская_ячейка");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("ID_Складская_ячейка");
            entity.Property(e => e.CellCode).HasColumnName("Код_ячейки").HasMaxLength(100).IsRequired();
            entity.Property(e => e.StorageZone).HasColumnName("Зона_хранения").HasMaxLength(100);
            entity.Property(e => e.Description).HasColumnName("Описание").HasMaxLength(500);
            entity.Property(e => e.WarehouseId).HasColumnName("FK_Склад");
            entity.HasOne(e => e.Warehouse).WithMany(w => w.WarehouseCells).HasForeignKey(e => e.WarehouseId).OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<WarehouseStock>(entity =>
        {
            entity.ToTable("Складской_остаток");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("ID_Складской_остаток");
            entity.Property(e => e.Quantity).HasColumnName("Количество").HasColumnType("decimal(10,3)").IsRequired();
            entity.Property(e => e.UpdateDate).HasColumnName("Дата_обновления").HasColumnType("date").IsRequired();
            entity.Property(e => e.WarehouseCellId).HasColumnName("FK_Складская_ячейка");
            entity.Property(e => e.MaterialId).HasColumnName("FK_Материал");
            entity.Property(e => e.ModelVariantId).HasColumnName("FK_Вариант_модели");
            entity.HasOne(e => e.WarehouseCell).WithMany(c => c.WarehouseStocks).HasForeignKey(e => e.WarehouseCellId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(e => e.Material).WithMany(m => m.WarehouseStocks).HasForeignKey(e => e.MaterialId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(e => e.ModelVariant).WithMany(v => v.WarehouseStocks).HasForeignKey(e => e.ModelVariantId).OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<QualityControl>(entity =>
        {
            entity.ToTable("Контроль_качества");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("ID_Контроль_качества");
            entity.Property(e => e.ControlDate).HasColumnName("Дата_контроля").HasColumnType("date").IsRequired();
            entity.Property(e => e.ControlType).HasColumnName("Тип_контроля").HasMaxLength(100).IsRequired();
            entity.Property(e => e.Result).HasColumnName("Результат").HasMaxLength(50).IsRequired();
            entity.Property(e => e.Notes).HasColumnName("Замечания").HasMaxLength(1000);
            entity.Property(e => e.Decision).HasColumnName("Решение").HasMaxLength(100);
            entity.Property(e => e.ProductionBatchId).HasColumnName("FK_Производственная_партия");
            entity.Property(e => e.SampleId).HasColumnName("FK_Образец");
            entity.Property(e => e.ControllerEmployeeId).HasColumnName("FK_Сотрудник_контролер");
            entity.HasOne(e => e.ProductionBatch).WithMany(b => b.QualityControls).HasForeignKey(e => e.ProductionBatchId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(e => e.Sample).WithMany(s => s.QualityControls).HasForeignKey(e => e.SampleId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(e => e.ControllerEmployee).WithMany(e => e.QualityControls).HasForeignKey(e => e.ControllerEmployeeId).OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<ProductOperation>(entity =>
        {
            entity.ToTable("Операция_товара");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("ID_Операция_товара");
            entity.Property(e => e.OperationType).HasColumnName("Тип_операции").HasMaxLength(100).IsRequired();
            entity.Property(e => e.OperationDate).HasColumnName("Дата_операции").HasColumnType("date").IsRequired();
            entity.Property(e => e.Quantity).HasColumnName("Количество").HasColumnType("decimal(10,3)").IsRequired();
            entity.Property(e => e.UnitPrice).HasColumnName("Цена_за_единицу").HasColumnType("decimal(18,2)");
            entity.Property(e => e.DocumentNumber).HasColumnName("Номер_документа").HasMaxLength(100);
            entity.Property(e => e.Recipient).HasColumnName("Получатель").HasMaxLength(200);
            entity.Property(e => e.DeliveryAddress).HasColumnName("Адрес_доставки").HasMaxLength(500);
            entity.Property(e => e.Route).HasColumnName("Маршрут").HasMaxLength(500);
            entity.Property(e => e.Status).HasColumnName("Статус").HasMaxLength(50).IsRequired();
            entity.Property(e => e.ReturnReason).HasColumnName("Причина_возврата").HasMaxLength(500);
            entity.Property(e => e.ReturnDecision).HasColumnName("Решение_по_возврату").HasMaxLength(500);
            entity.Property(e => e.SenderWarehouseId).HasColumnName("FK_Склад_отправитель");
            entity.Property(e => e.ReceiverWarehouseId).HasColumnName("FK_Склад_получатель");
            entity.Property(e => e.MaterialId).HasColumnName("FK_Материал");
            entity.Property(e => e.ModelVariantId).HasColumnName("FK_Вариант_модели");
            entity.Property(e => e.ClientId).HasColumnName("FK_Клиент");
            entity.Property(e => e.ResponsibleEmployeeId).HasColumnName("FK_Сотрудник_ответственный");
            entity.HasOne(e => e.SenderWarehouse).WithMany(w => w.SenderOperations).HasForeignKey(e => e.SenderWarehouseId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(e => e.ReceiverWarehouse).WithMany(w => w.ReceiverOperations).HasForeignKey(e => e.ReceiverWarehouseId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(e => e.Material).WithMany(m => m.ProductOperations).HasForeignKey(e => e.MaterialId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(e => e.ModelVariant).WithMany(v => v.ProductOperations).HasForeignKey(e => e.ModelVariantId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(e => e.Client).WithMany(c => c.ProductOperations).HasForeignKey(e => e.ClientId).OnDelete(DeleteBehavior.NoAction);
            entity.HasOne(e => e.ResponsibleEmployee).WithMany(e => e.ResponsibleProductOperations).HasForeignKey(e => e.ResponsibleEmployeeId).OnDelete(DeleteBehavior.NoAction);
        });
    }
}
