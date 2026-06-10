using clothing_manufacture.Models;

namespace clothing_manufacture.Services;

public static class EntityRegistry
{
    private static readonly List<EntityDescriptor> Descriptors = CreateDescriptors();

    public static IReadOnlyList<EntityDescriptor> All => Descriptors;

    public static EntityDescriptor Get(string key)
    {
        return Descriptors.First(d => d.Key == key);
    }

    private static GridColumnDefinition Col(string header, string path, double width = 160, double minWidth = 100)
    {
        return new GridColumnDefinition { Header = header, BindingPath = path, Width = width, MinWidth = minWidth };
    }

    private static FieldDefinition Text(string property, string label, bool required = false, int? max = null, string? help = null, object? defaultValue = null)
    {
        return new FieldDefinition { PropertyName = property, Label = label, Kind = FieldKind.Text, IsRequired = required, MaxLength = max, HelpText = help, DefaultValue = defaultValue };
    }

    private static FieldDefinition Multiline(string property, string label, int? max = null)
    {
        return new FieldDefinition { PropertyName = property, Label = label, Kind = FieldKind.Multiline, MaxLength = max };
    }

    private static FieldDefinition Integer(string property, string label, bool required = false, object? defaultValue = null)
    {
        return new FieldDefinition { PropertyName = property, Label = label, Kind = FieldKind.Integer, IsRequired = required, DefaultValue = defaultValue };
    }

    private static FieldDefinition Decimal(string property, string label, bool required = false, object? defaultValue = null)
    {
        return new FieldDefinition { PropertyName = property, Label = label, Kind = FieldKind.Decimal, IsRequired = required, DefaultValue = defaultValue };
    }

    private static FieldDefinition Date(string property, string label, bool required = false, object? defaultValue = null)
    {
        return new FieldDefinition { PropertyName = property, Label = label, Kind = FieldKind.Date, IsRequired = required, DefaultValue = defaultValue };
    }

    private static FieldDefinition Checkbox(string property, string label, object? defaultValue = null)
    {
        return new FieldDefinition { PropertyName = property, Label = label, Kind = FieldKind.Checkbox, DefaultValue = defaultValue };
    }

    private static FieldDefinition Choice(string property, string label, IEnumerable<string> options, bool required = false, object? defaultValue = null)
    {
        return new FieldDefinition
        {
            PropertyName = property,
            Label = label,
            Kind = FieldKind.Choice,
            IsRequired = required,
            DefaultValue = defaultValue,
            StaticOptions = options.Select(o => new SelectOption { Value = o, Text = o }).ToList()
        };
    }

    private static FieldDefinition Lookup<T>(string property, string label, bool required = false, string displayProperty = "DisplayName", IEnumerable<string>? includes = null) where T : class
    {
        return new FieldDefinition
        {
            PropertyName = property,
            Label = label,
            Kind = FieldKind.Lookup,
            IsRequired = required,
            LookupEntityType = typeof(T),
            LookupDisplayProperty = displayProperty,
            LookupIncludePaths = includes?.ToList() ?? new List<string>()
        };
    }

    private static List<EntityDescriptor> CreateDescriptors()
    {
        List<EntityDescriptor> list = new();

        list.Add(new EntityDescriptor
        {
            Key = "People",
            Title = "Люди",
            TableName = "Человек",
            Description = "Физические лица, которые могут быть сотрудниками.",
            EntityType = typeof(Person),
            DisplayProperty = "FullName",
            Columns = { Col("ID", "Id", 70), Col("Фамилия", "LastName", 150), Col("Имя", "FirstName", 140), Col("Отчество", "MiddleName", 150), Col("Телефон", "Phone", 140), Col("Email", "Email", 220) },
            Fields = { Text("LastName", "Фамилия", true, 100), Text("FirstName", "Имя", true, 100), Text("MiddleName", "Отчество", false, 100), Text("Phone", "Телефон", false, 30), Text("Email", "Email", false, 150) }
        });

        list.Add(new EntityDescriptor
        {
            Key = "Positions",
            Title = "Должности",
            TableName = "Должность",
            Description = "Справочник должностей сотрудников.",
            EntityType = typeof(Position),
            DisplayProperty = "Name",
            Columns = { Col("ID", "Id", 70), Col("Наименование", "Name", 220), Col("Описание", "Description", 420) },
            Fields = { Text("Name", "Наименование", true, 150), Multiline("Description", "Описание", 500) }
        });

        list.Add(new EntityDescriptor
        {
            Key = "Employees",
            Title = "Сотрудники",
            TableName = "Сотрудник",
            Description = "Сотрудники предприятия с привязкой к человеку и должности.",
            EntityType = typeof(Employee),
            DisplayProperty = "DisplayName",
            IncludePaths = { "Person", "Position" },
            Columns = { Col("ID", "Id", 70), Col("Табельный №", "PersonnelNumber", 130), Col("ФИО", "Person.FullName", 240), Col("Должность", "Position.Name", 200), Col("Статус", "Status", 120) },
            Fields = { Lookup<Person>("PersonId", "Человек", true, "FullName"), Lookup<Position>("PositionId", "Должность", true, "Name"), Text("PersonnelNumber", "Табельный номер", false, 50), Choice("Status", "Статус", new[] { "Активен", "В отпуске", "Уволен", "На больничном" }, true, "Активен") }
        });

        list.Add(new EntityDescriptor
        {
            Key = "ProductionProcesses",
            Title = "Процессы",
            TableName = "Процесс",
            Description = "Описание производственных процессов.",
            EntityType = typeof(ProductionProcess),
            DisplayProperty = "Name",
            Columns = { Col("ID", "Id", 70), Col("Наименование", "Name", 260), Col("Описание", "Description", 500) },
            Fields = { Text("Name", "Наименование", true, 200), Multiline("Description", "Описание", 1000) }
        });

        list.Add(new EntityDescriptor
        {
            Key = "ProcessStages",
            Title = "Этапы процесса",
            TableName = "Этап_процесса",
            Description = "Этапы внутри производственных процессов.",
            EntityType = typeof(ProcessStage),
            DisplayProperty = "DisplayName",
            IncludePaths = { "Process" },
            Columns = { Col("ID", "Id", 70), Col("Процесс", "Process.Name", 220), Col("№", "StageNumber", 80), Col("Наименование", "Name", 220), Col("Входные данные", "InputData", 250), Col("Результат", "Result", 250) },
            Fields = { Lookup<ProductionProcess>("ProcessId", "Процесс", true, "Name"), Integer("StageNumber", "Номер этапа", true, 1), Text("Name", "Наименование", true, 200), Multiline("Description", "Описание", 1000), Multiline("InputData", "Входные данные", 1000), Multiline("Result", "Результат", 1000) }
        });

        list.Add(new EntityDescriptor
        {
            Key = "StagePositions",
            Title = "Этапы и должности",
            TableName = "Этап_должность",
            Description = "Роли должностей на этапах процесса.",
            EntityType = typeof(StagePosition),
            IncludePaths = { "ProcessStage", "ProcessStage.Process", "Position" },
            Columns = { Col("ID", "Id", 70), Col("Этап", "ProcessStage.DisplayName", 280), Col("Должность", "Position.Name", 220), Col("Роль", "StageRole", 180), Col("Описание взаимодействия", "InteractionDescription", 380) },
            Fields = { Lookup<ProcessStage>("ProcessStageId", "Этап процесса", true, "DisplayName", new[] { "Process" }), Lookup<Position>("PositionId", "Должность", true, "Name"), Text("StageRole", "Роль на этапе", true, 150), Multiline("InteractionDescription", "Описание взаимодействия", 1000) }
        });

        list.Add(new EntityDescriptor
        {
            Key = "Classifiers",
            Title = "Классификаторы",
            TableName = "Классификатор",
            Description = "Общие классификаторы: категории, цвета, размеры, сезоны, статусы.",
            EntityType = typeof(Classifier),
            DisplayProperty = "DisplayName",
            IncludePaths = { "ParentClassifier" },
            Columns = { Col("ID", "Id", 70), Col("Тип", "ClassifierType", 180), Col("Значение", "Value", 220), Col("Код", "Code", 120), Col("Порядок", "SortOrder", 100), Col("Родитель", "ParentClassifier.DisplayName", 240), Col("Описание", "Description", 360) },
            Fields = { Choice("ClassifierType", "Тип классификатора", new[] { "Категория модели", "Цвет", "Размер", "Сезон", "Статус", "Ценовой сегмент" }, true, "Категория модели"), Text("Value", "Значение", true, 150), Text("Code", "Код", false, 50), Integer("SortOrder", "Порядковый номер"), Lookup<Classifier>("ParentClassifierId", "Родительский классификатор", false, "DisplayName"), Multiline("Description", "Описание", 500) }
        });

        list.Add(new EntityDescriptor
        {
            Key = "AssortmentPlans",
            Title = "Ассортиментные планы",
            TableName = "Ассортиментный_план",
            Description = "Планы ассортимента и ответственные сотрудники.",
            EntityType = typeof(AssortmentPlan),
            DisplayProperty = "DisplayName",
            IncludePaths = { "ResponsibleEmployee", "ResponsibleEmployee.Person", "ResponsibleEmployee.Position", "ApprovedEmployee", "ApprovedEmployee.Person", "ApprovedEmployee.Position" },
            Columns = { Col("ID", "Id", 70), Col("Номер", "PlanNumber", 160), Col("Период", "Period", 150), Col("Статус", "Status", 130), Col("Ответственный", "ResponsibleEmployee.DisplayName", 260), Col("Утвердивший", "ApprovedEmployee.DisplayName", 260), Col("Комментарий", "Comment", 320) },
            Fields = { Text("PlanNumber", "Номер плана", true, 100), Text("Period", "Период", true, 100), Choice("Status", "Статус", new[] { "Новый", "В работе", "Утверждён", "Закрыт" }, true, "Новый"), Lookup<Employee>("ResponsibleEmployeeId", "Ответственный", false, "DisplayName", new[] { "Person", "Position" }), Lookup<Employee>("ApprovedEmployeeId", "Утвердивший", false, "DisplayName", new[] { "Person", "Position" }), Multiline("Comment", "Комментарий", 1000) }
        });

        list.Add(new EntityDescriptor
        {
            Key = "ClothingCollections",
            Title = "Коллекции",
            TableName = "Коллекция",
            Description = "Коллекции одежды, привязанные к ассортиментному плану.",
            EntityType = typeof(ClothingCollection),
            DisplayProperty = "DisplayName",
            IncludePaths = { "AssortmentPlan" },
            Columns = { Col("ID", "Id", 70), Col("Наименование", "Name", 240), Col("Сезон", "Season", 130), Col("Год", "Year", 90), Col("Ценовой сегмент", "PriceSegment", 160), Col("Статус", "Status", 130), Col("План", "AssortmentPlan.DisplayName", 220) },
            Fields = { Text("Name", "Наименование", true, 200), Text("Season", "Сезон", false, 100), Integer("Year", "Год"), Choice("PriceSegment", "Ценовой сегмент", new[] { "Эконом", "Средний", "Премиум" }), Choice("Status", "Статус", new[] { "Новая", "В работе", "Утверждена", "Закрыта" }, true, "Новая"), Lookup<AssortmentPlan>("AssortmentPlanId", "Ассортиментный план", false, "DisplayName") }
        });

        list.Add(new EntityDescriptor
        {
            Key = "ClothingModels",
            Title = "Модели",
            TableName = "Модель",
            Description = "Модели одежды с категорией, коллекцией, дизайнером и конструктором.",
            EntityType = typeof(ClothingModel),
            DisplayProperty = "DisplayName",
            IncludePaths = { "CategoryClassifier", "Collection", "DesignerEmployee", "DesignerEmployee.Person", "DesignerEmployee.Position", "ConstructorEmployee", "ConstructorEmployee.Person", "ConstructorEmployee.Position" },
            Columns = { Col("ID", "Id", 70), Col("Артикул", "Article", 130), Col("Наименование", "Name", 240), Col("Категория", "CategoryClassifier.Value", 170), Col("Коллекция", "Collection.Name", 180), Col("Бренд", "Brand", 150), Col("Дизайнер", "DesignerEmployee.DisplayName", 240), Col("Конструктор", "ConstructorEmployee.DisplayName", 240), Col("Статус", "Status", 130) },
            Fields = { Text("Article", "Артикул", true, 100), Text("Name", "Наименование", true, 200), Lookup<Classifier>("CategoryClassifierId", "Категория", true, "DisplayName"), Lookup<ClothingCollection>("CollectionId", "Коллекция", false, "DisplayName"), Lookup<Employee>("DesignerEmployeeId", "Дизайнер", false, "DisplayName", new[] { "Person", "Position" }), Lookup<Employee>("ConstructorEmployeeId", "Конструктор", false, "DisplayName", new[] { "Person", "Position" }), Choice("Status", "Статус", new[] { "Активна", "В разработке", "Архив", "Закрыта" }, true, "Активна"), Text("Brand", "Бренд", false, 150), Text("Logo", "Логотип / путь", false, 250), Text("BrandColor", "Фирменный цвет", false, 100), Text("BrandingMethod", "Способ брендирования", false, 100), Multiline("Description", "Описание", 1000), Multiline("TechnologicalCard", "Технологическая карта", 1000), Multiline("BrandbookRequirements", "Требования брендбука", 1000) }
        });

        list.Add(new EntityDescriptor
        {
            Key = "ModelVariants",
            Title = "Варианты моделей",
            TableName = "Вариант_модели",
            Description = "Цвето-размерные варианты моделей.",
            EntityType = typeof(ModelVariant),
            DisplayProperty = "DisplayName",
            IncludePaths = { "Model", "ColorClassifier", "SizeClassifier" },
            Columns = { Col("ID", "Id", 70), Col("Штрихкод", "Barcode", 180), Col("Модель", "Model.DisplayName", 280), Col("Цвет", "ColorClassifier.Value", 140), Col("Размер", "SizeClassifier.Value", 120), Col("Статус", "Status", 130) },
            Fields = { Lookup<ClothingModel>("ModelId", "Модель", true, "DisplayName"), Lookup<Classifier>("ColorClassifierId", "Цвет", true, "DisplayName"), Lookup<Classifier>("SizeClassifierId", "Размер", true, "DisplayName"), Text("Barcode", "Штрихкод", false, 100), Choice("Status", "Статус", new[] { "Активен", "В разработке", "Архив" }, true, "Активен") }
        });

        list.Add(new EntityDescriptor
        {
            Key = "Materials",
            Title = "Материалы",
            TableName = "Материал",
            Description = "Материалы, фурнитура, нитки и упаковка.",
            EntityType = typeof(Material),
            DisplayProperty = "Name",
            Columns = { Col("ID", "Id", 70), Col("Наименование", "Name", 260), Col("Тип", "MaterialType", 160), Col("Ед. изм.", "UnitOfMeasure", 110), Col("Описание", "Description", 420) },
            Fields = { Text("Name", "Наименование", true, 200), Choice("MaterialType", "Тип материала", new[] { "Ткань", "Фурнитура", "Нитки", "Упаковка", "Декор" }, true, "Ткань"), Choice("UnitOfMeasure", "Единица измерения", new[] { "м", "м²", "кг", "шт", "рулон", "комплект" }, true, "м"), Multiline("Description", "Описание", 500) }
        });

        list.Add(new EntityDescriptor
        {
            Key = "ModelMaterials",
            Title = "Материалы модели",
            TableName = "Модель_материал",
            Description = "Состав материалов для модели.",
            EntityType = typeof(ModelMaterial),
            IncludePaths = { "Model", "Material" },
            Columns = { Col("ID", "Id", 70), Col("Модель", "Model.DisplayName", 280), Col("Материал", "Material.Name", 240), Col("Кол-во на изделие", "QuantityPerItem", 160), Col("Основной", "IsMainMaterial", 110) },
            Fields = { Lookup<ClothingModel>("ModelId", "Модель", true, "DisplayName"), Lookup<Material>("MaterialId", "Материал", true, "Name"), Decimal("QuantityPerItem", "Количество на изделие", true, "1"), Checkbox("IsMainMaterial", "Основной материал", false) }
        });

        list.Add(new EntityDescriptor
        {
            Key = "Patterns",
            Title = "Лекала",
            TableName = "Лекало",
            Description = "Лекала моделей и версии файлов.",
            EntityType = typeof(Pattern),
            DisplayProperty = "DisplayName",
            IncludePaths = { "Model", "ConstructorEmployee", "ConstructorEmployee.Person", "ConstructorEmployee.Position" },
            Columns = { Col("ID", "Id", 70), Col("Модель", "Model.DisplayName", 280), Col("Версия", "Version", 120), Col("Дата", "CreationDate", 120), Col("Конструктор", "ConstructorEmployee.DisplayName", 260), Col("Статус", "Status", 130), Col("Файл", "PatternFile", 260) },
            Fields = { Lookup<ClothingModel>("ModelId", "Модель", true, "DisplayName"), Lookup<Employee>("ConstructorEmployeeId", "Конструктор", true, "DisplayName", new[] { "Person", "Position" }), Text("Version", "Версия", true, 50, defaultValue: "1.0"), Date("CreationDate", "Дата создания", true, DateTime.Today), Text("PatternFile", "Файл лекала", false, 300), Choice("Status", "Статус", new[] { "Черновик", "На проверке", "Утверждено", "Архив" }, true, "Черновик"), Multiline("Comment", "Комментарий", 1000) }
        });

        list.Add(new EntityDescriptor
        {
            Key = "Samples",
            Title = "Образцы",
            TableName = "Образец",
            Description = "Пошив и проверка образцов.",
            EntityType = typeof(Sample),
            DisplayProperty = "DisplayName",
            IncludePaths = { "Model", "Pattern", "TailorEmployee", "TailorEmployee.Person", "TailorEmployee.Position", "TechnologistEmployee", "TechnologistEmployee.Person", "TechnologistEmployee.Position" },
            Columns = { Col("ID", "Id", 70), Col("Модель", "Model.DisplayName", 260), Col("Лекало", "Pattern.DisplayName", 220), Col("Дата", "ManufactureDate", 120), Col("Портной", "TailorEmployee.DisplayName", 240), Col("Технолог", "TechnologistEmployee.DisplayName", 240), Col("Статус", "Status", 130), Col("Результат примерки", "FittingResult", 260) },
            Fields = { Lookup<ClothingModel>("ModelId", "Модель", true, "DisplayName"), Lookup<Pattern>("PatternId", "Лекало", true, "DisplayName", new[] { "Model" }), Date("ManufactureDate", "Дата изготовления", true, DateTime.Today), Lookup<Employee>("TailorEmployeeId", "Портной", false, "DisplayName", new[] { "Person", "Position" }), Lookup<Employee>("TechnologistEmployeeId", "Технолог", false, "DisplayName", new[] { "Person", "Position" }), Choice("Status", "Статус", new[] { "Изготовлен", "На доработке", "Утверждён", "Отклонён" }, true, "Изготовлен"), Multiline("FittingResult", "Результат примерки", 1000), Multiline("RevisionComment", "Комментарий по доработке", 1000) }
        });

        list.Add(new EntityDescriptor
        {
            Key = "Clients",
            Title = "Клиенты",
            TableName = "Клиент",
            Description = "Клиенты предприятия.",
            EntityType = typeof(Client),
            DisplayProperty = "Name",
            Columns = { Col("ID", "Id", 70), Col("Наименование", "Name", 260), Col("Тип", "ClientType", 160), Col("Телефон", "Phone", 150), Col("Email", "Email", 220), Col("Адрес", "Address", 350) },
            Fields = { Text("Name", "Наименование", true, 200), Choice("ClientType", "Тип клиента", new[] { "Юридическое лицо", "Физическое лицо", "ИП" }, true, "Юридическое лицо"), Text("Phone", "Телефон", false, 30), Text("Email", "Email", false, 150), Multiline("Address", "Адрес", 500) }
        });

        list.Add(new EntityDescriptor
        {
            Key = "CustomerOrders",
            Title = "Заказы клиентов",
            TableName = "Заказ_клиента",
            Description = "Заказы клиентов и общие требования.",
            EntityType = typeof(CustomerOrder),
            DisplayProperty = "DisplayName",
            IncludePaths = { "Client", "ManagerEmployee", "ManagerEmployee.Person", "ManagerEmployee.Position" },
            Columns = { Col("ID", "Id", 70), Col("Номер", "OrderNumber", 160), Col("Клиент", "Client.Name", 260), Col("Дата", "OrderDate", 120), Col("Срок", "DueDate", 120), Col("Менеджер", "ManagerEmployee.DisplayName", 250), Col("Статус", "Status", 130), Col("Комментарий", "Comment", 300) },
            Fields = { Text("OrderNumber", "Номер заказа", true, 100), Lookup<Client>("ClientId", "Клиент", true, "Name"), Lookup<Employee>("ManagerEmployeeId", "Менеджер", false, "DisplayName", new[] { "Person", "Position" }), Date("OrderDate", "Дата заказа", true, DateTime.Today), Date("DueDate", "Срок исполнения"), Choice("Status", "Статус", new[] { "Новый", "В работе", "Выполнен", "Отменён" }, true, "Новый"), Multiline("BrandingRequirements", "Требования к брендированию", 1000), Multiline("Comment", "Комментарий", 1000) }
        });

        list.Add(new EntityDescriptor
        {
            Key = "OrderItems",
            Title = "Позиции заказов",
            TableName = "Позиция_заказа",
            Description = "Состав заказов: варианты моделей, количество и цена.",
            EntityType = typeof(OrderItem),
            IncludePaths = { "CustomerOrder", "CustomerOrder.Client", "ModelVariant", "ModelVariant.Model", "ModelVariant.ColorClassifier", "ModelVariant.SizeClassifier" },
            Columns = { Col("ID", "Id", 70), Col("Заказ", "CustomerOrder.DisplayName", 260), Col("Вариант", "ModelVariant.DisplayName", 350), Col("Кол-во", "Quantity", 100), Col("Цена", "UnitPrice", 120), Col("Сумма", "LineTotal", 130), Col("Комментарий", "Comment", 300) },
            Fields = { Lookup<CustomerOrder>("CustomerOrderId", "Заказ", true, "DisplayName", new[] { "Client" }), Lookup<ModelVariant>("ModelVariantId", "Вариант модели", true, "DisplayName", new[] { "Model", "ColorClassifier", "SizeClassifier" }), Integer("Quantity", "Количество", true, 1), Decimal("UnitPrice", "Цена за единицу"), Multiline("Comment", "Комментарий", 1000) }
        });

        list.Add(new EntityDescriptor
        {
            Key = "ProductionTasks",
            Title = "Производственные задания",
            TableName = "Производственное_задание",
            Description = "Задания на производство по заказу, коллекции или ассортиментному плану.",
            EntityType = typeof(ProductionTask),
            DisplayProperty = "TaskNumber",
            IncludePaths = { "PlannerEmployee", "PlannerEmployee.Person", "PlannerEmployee.Position", "CustomerOrder", "CustomerOrder.Client", "Collection", "AssortmentPlan" },
            Columns = { Col("ID", "Id", 70), Col("Номер", "TaskNumber", 160), Col("Планировщик", "PlannerEmployee.DisplayName", 260), Col("Дата", "CreationDate", 120), Col("Начало", "PlannedStartDate", 120), Col("Окончание", "PlannedEndDate", 120), Col("Источник", "LaunchSource", 150), Col("Заказ", "CustomerOrder.DisplayName", 240), Col("Коллекция", "Collection.DisplayName", 220), Col("План", "AssortmentPlan.DisplayName", 220), Col("Статус", "Status", 130) },
            Fields = { Text("TaskNumber", "Номер задания", true, 100), Lookup<Employee>("PlannerEmployeeId", "Планировщик", true, "DisplayName", new[] { "Person", "Position" }), Date("CreationDate", "Дата создания", true, DateTime.Today), Date("PlannedStartDate", "Плановая дата начала"), Date("PlannedEndDate", "Плановая дата окончания"), Choice("LaunchSource", "Источник запуска", new[] { "Заказ", "Коллекция", "Ассортиментный план" }), Lookup<CustomerOrder>("CustomerOrderId", "Заказ клиента", false, "DisplayName", new[] { "Client" }), Lookup<ClothingCollection>("CollectionId", "Коллекция", false, "DisplayName"), Lookup<AssortmentPlan>("AssortmentPlanId", "Ассортиментный план", false, "DisplayName"), Choice("Status", "Статус", new[] { "Новое", "Запланировано", "В производстве", "Завершено", "Отменено" }, true, "Новое"), Multiline("Comment", "Комментарий", 1000) },
            CustomValidate = values => (values["CustomerOrderId"] == null && values["CollectionId"] == null && values["AssortmentPlanId"] == null) ? "Укажите хотя бы один источник: заказ, коллекцию или ассортиментный план." : null
        });

        list.Add(new EntityDescriptor
        {
            Key = "ProductionBatches",
            Title = "Производственные партии",
            TableName = "Производственная_партия",
            Description = "Партии выпуска по производственным заданиям.",
            EntityType = typeof(ProductionBatch),
            DisplayProperty = "BatchNumber",
            IncludePaths = { "ProductionTask", "ModelVariant", "ModelVariant.Model", "ModelVariant.ColorClassifier", "ModelVariant.SizeClassifier" },
            Columns = { Col("ID", "Id", 70), Col("Партия", "BatchNumber", 160), Col("Задание", "ProductionTask.TaskNumber", 180), Col("Вариант", "ModelVariant.DisplayName", 350), Col("Кол-во", "Quantity", 100), Col("Запуск", "StartDate", 120), Col("Завершение", "EndDate", 120), Col("Статус", "Status", 130) },
            Fields = { Text("BatchNumber", "Номер партии", true, 100), Lookup<ProductionTask>("ProductionTaskId", "Производственное задание", true, "TaskNumber"), Lookup<ModelVariant>("ModelVariantId", "Вариант модели", true, "DisplayName", new[] { "Model", "ColorClassifier", "SizeClassifier" }), Integer("Quantity", "Количество", true, 1), Date("StartDate", "Дата запуска"), Date("EndDate", "Дата завершения"), Choice("Status", "Статус", new[] { "Новая", "В производстве", "Завершена", "Остановлена" }, true, "Новая") }
        });

        list.Add(new EntityDescriptor
        {
            Key = "Warehouses",
            Title = "Склады",
            TableName = "Склад",
            Description = "Склады материалов и готовой продукции.",
            EntityType = typeof(Warehouse),
            DisplayProperty = "Name",
            Columns = { Col("ID", "Id", 70), Col("Наименование", "Name", 260), Col("Тип", "WarehouseType", 180), Col("Адрес", "Address", 420) },
            Fields = { Text("Name", "Наименование", true, 200), Choice("WarehouseType", "Тип склада", new[] { "Материалы", "Готовая продукция", "Смешанный", "Брак", "Транзитный" }, true, "Материалы"), Multiline("Address", "Адрес", 500) }
        });

        list.Add(new EntityDescriptor
        {
            Key = "WarehouseCells",
            Title = "Складские ячейки",
            TableName = "Складская_ячейка",
            Description = "Ячейки и зоны хранения на складах.",
            EntityType = typeof(WarehouseCell),
            DisplayProperty = "DisplayName",
            IncludePaths = { "Warehouse" },
            Columns = { Col("ID", "Id", 70), Col("Склад", "Warehouse.Name", 240), Col("Код ячейки", "CellCode", 150), Col("Зона", "StorageZone", 150), Col("Описание", "Description", 350) },
            Fields = { Lookup<Warehouse>("WarehouseId", "Склад", true, "Name"), Text("CellCode", "Код ячейки", true, 100), Text("StorageZone", "Зона хранения", false, 100), Multiline("Description", "Описание", 500) }
        });

        list.Add(new EntityDescriptor
        {
            Key = "WarehouseStocks",
            Title = "Складские остатки",
            TableName = "Складской_остаток",
            Description = "Остатки материалов или готовой продукции в ячейках.",
            EntityType = typeof(WarehouseStock),
            IncludePaths = { "WarehouseCell", "WarehouseCell.Warehouse", "Material", "ModelVariant", "ModelVariant.Model", "ModelVariant.ColorClassifier", "ModelVariant.SizeClassifier" },
            Columns = { Col("ID", "Id", 70), Col("Ячейка", "WarehouseCell.DisplayName", 260), Col("Номенклатура", "ItemName", 350), Col("Материал", "Material.Name", 240), Col("Вариант", "ModelVariant.DisplayName", 350), Col("Кол-во", "Quantity", 120), Col("Дата обновления", "UpdateDate", 150) },
            Fields = { Lookup<WarehouseCell>("WarehouseCellId", "Складская ячейка", true, "DisplayName", new[] { "Warehouse" }), Lookup<Material>("MaterialId", "Материал", false, "Name"), Lookup<ModelVariant>("ModelVariantId", "Вариант модели", false, "DisplayName", new[] { "Model", "ColorClassifier", "SizeClassifier" }), Decimal("Quantity", "Количество", true, "0"), Date("UpdateDate", "Дата обновления", true, DateTime.Today) },
            CustomValidate = values => (values["MaterialId"] == null && values["ModelVariantId"] == null) || (values["MaterialId"] != null && values["ModelVariantId"] != null) ? "Выберите либо материал, либо вариант модели. Нельзя выбрать оба значения одновременно." : null
        });

        list.Add(new EntityDescriptor
        {
            Key = "QualityControls",
            Title = "Контроль качества",
            TableName = "Контроль_качества",
            Description = "Контроль качества партий и образцов.",
            EntityType = typeof(QualityControl),
            IncludePaths = { "ProductionBatch", "Sample", "Sample.Model", "ControllerEmployee", "ControllerEmployee.Person", "ControllerEmployee.Position" },
            Columns = { Col("ID", "Id", 70), Col("Дата", "ControlDate", 120), Col("Тип", "ControlType", 170), Col("Результат", "Result", 140), Col("Партия", "ProductionBatch.BatchNumber", 170), Col("Образец", "Sample.DisplayName", 220), Col("Контролёр", "ControllerEmployee.DisplayName", 260), Col("Решение", "Decision", 170), Col("Замечания", "Notes", 320) },
            Fields = { Date("ControlDate", "Дата контроля", true, DateTime.Today), Choice("ControlType", "Тип контроля", new[] { "Входной", "Межоперационный", "Финальный", "Образец" }, true, "Финальный"), Choice("Result", "Результат", new[] { "Пройден", "Не пройден", "С замечаниями" }, true, "Пройден"), Lookup<ProductionBatch>("ProductionBatchId", "Производственная партия", false, "BatchNumber"), Lookup<Sample>("SampleId", "Образец", false, "DisplayName", new[] { "Model" }), Lookup<Employee>("ControllerEmployeeId", "Контролёр", true, "DisplayName", new[] { "Person", "Position" }), Text("Decision", "Решение", false, 100), Multiline("Notes", "Замечания", 1000) },
            CustomValidate = values => (values["ProductionBatchId"] == null && values["SampleId"] == null) || (values["ProductionBatchId"] != null && values["SampleId"] != null) ? "Выберите либо производственную партию, либо образец. Нельзя выбрать оба значения одновременно." : null
        });

        list.Add(new EntityDescriptor
        {
            Key = "ProductOperations",
            Title = "Операции товара",
            TableName = "Операция_товара",
            Description = "Поступления, перемещения, отгрузки, возвраты и списания.",
            EntityType = typeof(ProductOperation),
            DisplayProperty = "DocumentNumber",
            IncludePaths = { "SenderWarehouse", "ReceiverWarehouse", "Material", "ModelVariant", "ModelVariant.Model", "ModelVariant.ColorClassifier", "ModelVariant.SizeClassifier", "Client", "ResponsibleEmployee", "ResponsibleEmployee.Person", "ResponsibleEmployee.Position" },
            Columns = { Col("ID", "Id", 70), Col("Тип", "OperationType", 150), Col("Дата", "OperationDate", 120), Col("Документ", "DocumentNumber", 150), Col("Номенклатура", "ItemName", 350), Col("Кол-во", "Quantity", 110), Col("Цена", "UnitPrice", 110), Col("Сумма", "OperationTotal", 130), Col("Отправитель", "SenderWarehouse.Name", 200), Col("Получатель", "ReceiverWarehouse.Name", 200), Col("Клиент", "Client.Name", 220), Col("Ответственный", "ResponsibleEmployee.DisplayName", 260), Col("Статус", "Status", 130) },
            Fields = { Choice("OperationType", "Тип операции", new[] { "Поступление", "Перемещение", "Отгрузка", "Возврат", "Списание" }, true, "Поступление"), Date("OperationDate", "Дата операции", true, DateTime.Today), Text("DocumentNumber", "Номер документа", false, 100), Lookup<Warehouse>("SenderWarehouseId", "Склад-отправитель", false, "Name"), Lookup<Warehouse>("ReceiverWarehouseId", "Склад-получатель", false, "Name"), Lookup<Material>("MaterialId", "Материал", false, "Name"), Lookup<ModelVariant>("ModelVariantId", "Вариант модели", false, "DisplayName", new[] { "Model", "ColorClassifier", "SizeClassifier" }), Decimal("Quantity", "Количество", true, "1"), Decimal("UnitPrice", "Цена за единицу"), Lookup<Client>("ClientId", "Клиент", false, "Name"), Lookup<Employee>("ResponsibleEmployeeId", "Ответственный", false, "DisplayName", new[] { "Person", "Position" }), Text("Recipient", "Получатель", false, 200), Multiline("DeliveryAddress", "Адрес доставки", 500), Multiline("Route", "Маршрут", 500), Choice("Status", "Статус", new[] { "Создана", "Проведена", "Отменена" }, true, "Создана"), Multiline("ReturnReason", "Причина возврата", 500), Multiline("ReturnDecision", "Решение по возврату", 500) },
            CustomValidate = values => (values["MaterialId"] == null && values["ModelVariantId"] == null) || (values["MaterialId"] != null && values["ModelVariantId"] != null) ? "Выберите либо материал, либо вариант модели. Нельзя выбрать оба значения одновременно." : null
        });

        return list;
    }
}
