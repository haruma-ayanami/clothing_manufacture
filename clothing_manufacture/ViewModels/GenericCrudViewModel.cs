using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Input;
using clothing_manufacture.Commands;
using clothing_manufacture.Data;
using clothing_manufacture.Services;
using Microsoft.EntityFrameworkCore;

namespace clothing_manufacture.ViewModels;

public class GenericCrudViewModel : BaseViewModel
{
    private object? _selectedItem;
    private string _statusMessage = "Готово к работе.";
    private bool _isBusy;

    public GenericCrudViewModel(EntityDescriptor descriptor)
    {
        Descriptor = descriptor;
        Title = descriptor.Title;
        Description = descriptor.Description;

        LoadCommand = new RelayCommand(_ => LoadData());
        AddCommand = new RelayCommand(_ => AddItem(), _ => CanAdd());
        UpdateCommand = new RelayCommand(_ => UpdateItem(), _ => CanUpdateOrDelete());
        DeleteCommand = new RelayCommand(_ => DeleteItem(), _ => CanUpdateOrDelete());
        ClearCommand = new RelayCommand(_ => ClearForm());

        foreach (FieldDefinition field in descriptor.Fields)
            Fields.Add(new FormFieldViewModel(field));

        LoadData();
    }

    public EntityDescriptor Descriptor { get; }
    public string Title { get; }
    public string Description { get; }
    public ObservableCollection<object> Items { get; } = new();
    public ObservableCollection<FormFieldViewModel> Fields { get; } = new();

    public object? SelectedItem
    {
        get => _selectedItem;
        set
        {
            _selectedItem = value;
            OnPropertyChanged();

            if (_selectedItem != null)
                FillFormFromSelectedItem();

            RefreshCommands();
        }
    }

    public string StatusMessage
    {
        get => _statusMessage;
        set
        {
            _statusMessage = value;
            OnPropertyChanged();
        }
    }

    public bool IsBusy
    {
        get => _isBusy;
        set
        {
            _isBusy = value;
            OnPropertyChanged();
            RefreshCommands();
        }
    }

    public ICommand LoadCommand { get; }
    public ICommand AddCommand { get; }
    public ICommand UpdateCommand { get; }
    public ICommand DeleteCommand { get; }
    public ICommand ClearCommand { get; }

    public void LoadData()
    {
        try
        {
            IsBusy = true;
            using ClothingManufactureDbContext db = new();

            LoadOptions(db);

            IQueryable query = ReflectionHelper.GetQueryable(db, Descriptor.EntityType);
            query = ReflectionHelper.ApplyIncludes(query, Descriptor.EntityType, Descriptor.IncludePaths);

            List<object> loadedItems = query.Cast<object>().ToList();

            var sortedItems = loadedItems
                .OrderBy(item => ReflectionHelper.GetPropertyValue(item, Descriptor.DisplayProperty)?.ToString() ?? item.ToString())
                .ToList();

            Items.Clear();
            foreach (object item in sortedItems)
                Items.Add(item);

            StatusMessage = $"Загружено записей: {Items.Count}";
        }
        catch (Exception ex)
        {
            StatusMessage = "Ошибка загрузки данных.";
            ShowError("Ошибка загрузки данных", ex);
        }
        finally
        {
            IsBusy = false;
        }
    }

    private void LoadOptions(ClothingManufactureDbContext db)
    {
        foreach (FormFieldViewModel field in Fields)
        {
            field.Options.Clear();

            if (field.Definition.Kind == FieldKind.Choice)
            {
                if (!field.Definition.IsRequired)
                    field.Options.Add(new SelectOption { Value = null, Text = "— не выбрано —" });

                foreach (SelectOption option in field.Definition.StaticOptions)
                    field.Options.Add(option);

                continue;
            }

            if (field.Definition.Kind != FieldKind.Lookup || field.Definition.LookupEntityType == null)
                continue;

            if (!field.Definition.IsRequired)
                field.Options.Add(new SelectOption { Value = null, Text = "— не выбрано —" });

            IQueryable query = ReflectionHelper.GetQueryable(db, field.Definition.LookupEntityType);
            query = ReflectionHelper.ApplyIncludes(query, field.Definition.LookupEntityType, field.Definition.LookupIncludePaths);

            List<object> options = query.Cast<object>().ToList();

            foreach (object optionEntity in options.OrderBy(GetOptionText))
            {
                object? id = ReflectionHelper.GetPropertyValue(optionEntity, "Id");
                string text = GetOptionText(optionEntity, field.Definition.LookupDisplayProperty);

                field.Options.Add(new SelectOption { Value = id, Text = text });
            }
        }
    }

    private static string GetOptionText(object entity)
    {
        return GetOptionText(entity, "DisplayName");
    }

    private static string GetOptionText(object entity, string? displayProperty)
    {
        object? value = null;

        if (!string.IsNullOrWhiteSpace(displayProperty))
            value = ReflectionHelper.GetPropertyValue(entity, displayProperty);

        value ??= ReflectionHelper.GetPropertyValue(entity, "Name");
        value ??= ReflectionHelper.GetPropertyValue(entity, "FullName");
        value ??= ReflectionHelper.GetPropertyValue(entity, "DisplayName");
        value ??= ReflectionHelper.GetPropertyValue(entity, "Id");

        return value?.ToString() ?? entity.ToString() ?? "Запись";
    }

    private void FillFormFromSelectedItem()
    {
        if (SelectedItem == null)
            return;

        foreach (FormFieldViewModel field in Fields)
        {
            object? value = ReflectionHelper.GetPropertyValue(SelectedItem, field.Definition.PropertyName);

            switch (field.Definition.Kind)
            {
                case FieldKind.Text:
                case FieldKind.Multiline:
                case FieldKind.Integer:
                case FieldKind.Decimal:
                    field.TextValue = value?.ToString() ?? string.Empty;
                    break;

                case FieldKind.Date:
                    field.DateValue = value is DateTime dateTime ? dateTime : null;
                    break;

                case FieldKind.Checkbox:
                    field.BoolValue = value is bool boolValue && boolValue;
                    break;

                case FieldKind.Choice:
                case FieldKind.Lookup:
                    field.SelectedValue = value;
                    break;
            }
        }

        StatusMessage = "Выбрана запись для редактирования.";
    }

    private void AddItem()
    {
        try
        {
            IsBusy = true;

            if (!TryReadFormValues(out Dictionary<string, object?> values, out string validationError))
            {
                StatusMessage = validationError;
                MessageBox.Show(validationError, "Проверьте форму", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            object entity = Activator.CreateInstance(Descriptor.EntityType)
                ?? throw new InvalidOperationException("Не удалось создать объект.");

            ApplyValuesToEntity(entity, values);

            using ClothingManufactureDbContext db = new();
            db.Add(entity);
            db.SaveChanges();

            StatusMessage = "Запись добавлена.";
            ClearForm();
            LoadData();
        }
        catch (Exception ex)
        {
            StatusMessage = "Ошибка добавления записи.";
            ShowError("Ошибка добавления записи", ex);
        }
        finally
        {
            IsBusy = false;
        }
    }

    private void UpdateItem()
    {
        if (SelectedItem == null)
            return;

        try
        {
            IsBusy = true;

            if (!TryReadFormValues(out Dictionary<string, object?> values, out string validationError))
            {
                StatusMessage = validationError;
                MessageBox.Show(validationError, "Проверьте форму", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            object? key = ReflectionHelper.GetPropertyValue(SelectedItem, Descriptor.KeyPropertyName);

            using ClothingManufactureDbContext db = new();
            object? entity = db.Find(Descriptor.EntityType, key);

            if (entity == null)
            {
                MessageBox.Show("Выбранная запись не найдена в базе данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                LoadData();
                return;
            }

            ApplyValuesToEntity(entity, values);
            db.SaveChanges();

            StatusMessage = "Запись изменена.";
            ClearForm();
            LoadData();
        }
        catch (Exception ex)
        {
            StatusMessage = "Ошибка изменения записи.";
            ShowError("Ошибка изменения записи", ex);
        }
        finally
        {
            IsBusy = false;
        }
    }

    private void DeleteItem()
    {
        if (SelectedItem == null)
            return;

        MessageBoxResult result = MessageBox.Show(
            "Удалить выбранную запись? Если она связана с другими таблицами, база данных не позволит удалить её.",
            "Подтверждение удаления",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question
        );

        if (result != MessageBoxResult.Yes)
            return;

        try
        {
            IsBusy = true;
            object? key = ReflectionHelper.GetPropertyValue(SelectedItem, Descriptor.KeyPropertyName);

            using ClothingManufactureDbContext db = new();
            object? entity = db.Find(Descriptor.EntityType, key);

            if (entity == null)
            {
                MessageBox.Show("Выбранная запись не найдена в базе данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                LoadData();
                return;
            }

            db.Remove(entity);
            db.SaveChanges();

            StatusMessage = "Запись удалена.";
            ClearForm();
            LoadData();
        }
        catch (DbUpdateException ex)
        {
            StatusMessage = "Удаление невозможно: запись используется в других таблицах.";
            ShowError("Удаление невозможно", ex);
        }
        catch (Exception ex)
        {
            StatusMessage = "Ошибка удаления записи.";
            ShowError("Ошибка удаления записи", ex);
        }
        finally
        {
            IsBusy = false;
        }
    }

    private void ClearForm()
    {
        SelectedItem = null;

        foreach (FormFieldViewModel field in Fields)
            field.ResetToDefault();

        StatusMessage = "Форма очищена.";
        RefreshCommands();
    }

    private bool TryReadFormValues(out Dictionary<string, object?> values, out string validationError)
    {
        values = new Dictionary<string, object?>();
        validationError = string.Empty;

        foreach (FormFieldViewModel field in Fields)
        {
            if (!TryReadFieldValue(field, out object? value, out validationError))
                return false;

            values[field.Definition.PropertyName] = value;
        }

        string? customValidation = Descriptor.CustomValidate?.Invoke(values);

        if (!string.IsNullOrWhiteSpace(customValidation))
        {
            validationError = customValidation;
            return false;
        }

        return true;
    }

    private bool TryReadFieldValue(FormFieldViewModel field, out object? value, out string validationError)
    {
        value = null;
        validationError = string.Empty;

        Type? targetType = ReflectionHelper.GetPropertyType(Descriptor.EntityType, field.Definition.PropertyName);
        Type actualType = Nullable.GetUnderlyingType(targetType ?? typeof(string)) ?? targetType ?? typeof(string);

        switch (field.Definition.Kind)
        {
            case FieldKind.Text:
            case FieldKind.Multiline:
            {
                string? text = string.IsNullOrWhiteSpace(field.TextValue) ? null : field.TextValue.Trim();

                if (field.Definition.IsRequired && string.IsNullOrWhiteSpace(text))
                {
                    validationError = $"Поле \"{field.Definition.Label}\" обязательно для заполнения.";
                    return false;
                }

                value = text;
                return true;
            }

            case FieldKind.Integer:
            {
                if (string.IsNullOrWhiteSpace(field.TextValue))
                {
                    if (field.Definition.IsRequired)
                    {
                        validationError = $"Поле \"{field.Definition.Label}\" обязательно для заполнения.";
                        return false;
                    }

                    value = null;
                    return true;
                }

                if (!int.TryParse(field.TextValue.Trim(), out int integerValue))
                {
                    validationError = $"Поле \"{field.Definition.Label}\" должно быть целым числом.";
                    return false;
                }

                value = integerValue;
                return true;
            }

            case FieldKind.Decimal:
            {
                if (string.IsNullOrWhiteSpace(field.TextValue))
                {
                    if (field.Definition.IsRequired)
                    {
                        validationError = $"Поле \"{field.Definition.Label}\" обязательно для заполнения.";
                        return false;
                    }

                    value = null;
                    return true;
                }

                string normalized = field.TextValue.Trim().Replace(',', '.');

                if (!decimal.TryParse(normalized, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal decimalValue))
                {
                    validationError = $"Поле \"{field.Definition.Label}\" должно быть числом.";
                    return false;
                }

                value = decimalValue;
                return true;
            }

            case FieldKind.Date:
            {
                if (field.DateValue == null)
                {
                    if (field.Definition.IsRequired)
                    {
                        validationError = $"Поле \"{field.Definition.Label}\" обязательно для заполнения.";
                        return false;
                    }

                    value = null;
                    return true;
                }

                value = field.DateValue.Value.Date;
                return true;
            }

            case FieldKind.Checkbox:
                value = field.BoolValue;
                return true;

            case FieldKind.Choice:
            {
                if (field.Definition.IsRequired && field.SelectedValue == null)
                {
                    validationError = $"Поле \"{field.Definition.Label}\" обязательно для выбора.";
                    return false;
                }

                value = field.SelectedValue?.ToString();
                return true;
            }

            case FieldKind.Lookup:
            {
                if (field.Definition.IsRequired && field.SelectedValue == null)
                {
                    validationError = $"Поле \"{field.Definition.Label}\" обязательно для выбора.";
                    return false;
                }

                if (field.SelectedValue == null)
                {
                    value = null;
                    return true;
                }

                value = Convert.ChangeType(field.SelectedValue, actualType, CultureInfo.InvariantCulture);
                return true;
            }

            default:
                value = null;
                return true;
        }
    }

    private void ApplyValuesToEntity(object entity, IReadOnlyDictionary<string, object?> values)
    {
        foreach (var item in values)
            ReflectionHelper.SetPropertyValue(entity, item.Key, item.Value);
    }

    private bool CanAdd()
    {
        return !IsBusy;
    }

    private bool CanUpdateOrDelete()
    {
        return !IsBusy && SelectedItem != null;
    }

    private void RefreshCommands()
    {
        if (LoadCommand is RelayCommand loadCommand)
            loadCommand.RaiseCanExecuteChanged();
        if (AddCommand is RelayCommand addCommand)
            addCommand.RaiseCanExecuteChanged();
        if (UpdateCommand is RelayCommand updateCommand)
            updateCommand.RaiseCanExecuteChanged();
        if (DeleteCommand is RelayCommand deleteCommand)
            deleteCommand.RaiseCanExecuteChanged();
        if (ClearCommand is RelayCommand clearCommand)
            clearCommand.RaiseCanExecuteChanged();
    }

    private static void ShowError(string title, Exception ex)
    {
        MessageBox.Show(
            DatabaseTestService.GetFullError(ex),
            title,
            MessageBoxButton.OK,
            MessageBoxImage.Error
        );
    }
}
