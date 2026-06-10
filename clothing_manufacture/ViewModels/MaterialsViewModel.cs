using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using clothing_manufacture.Commands;
using clothing_manufacture.Data;
using clothing_manufacture.Models;
using Microsoft.EntityFrameworkCore;

namespace clothing_manufacture.ViewModels;

public class MaterialsViewModel : BaseViewModel
{
    private ObservableCollection<Material> _items = new();
    private Material? _selectedItem;
    private string _statusMessage = "Готово к работе.";
    private bool _isBusy;

    private string _formName = string.Empty;
    private string _formMaterialType = string.Empty;
    private string _formUnitOfMeasure = string.Empty;
    private string? _formDescription = null;

    public MaterialsViewModel()
    {
        LoadCommand = new RelayCommand(_ => LoadItems());
        AddCommand = new RelayCommand(_ => AddItem(), _ => CanSave());
        UpdateCommand = new RelayCommand(_ => UpdateItem(), _ => CanUpdateOrDelete());
        DeleteCommand = new RelayCommand(_ => DeleteItem(), _ => CanUpdateOrDelete());
        ClearCommand = new RelayCommand(_ => ClearForm());

        LoadItems();
    }

    public ObservableCollection<Material> Materials
    {
        get => _items;
        set
        {
            _items = value;
            OnPropertyChanged();
        }
    }

    public Material? SelectedMaterial
    {
        get => _selectedItem;
        set
        {
            _selectedItem = value;
            OnPropertyChanged();

            if (_selectedItem != null)
            {
                FormName = _selectedItem.Name;
                FormMaterialType = _selectedItem.MaterialType;
                FormUnitOfMeasure = _selectedItem.UnitOfMeasure;
                FormDescription = _selectedItem.Description;
                StatusMessage = "Выбрана запись.";
            }

            RefreshCommands();
        }
    }


    public string FormName
    {
        get => _formName;
        set
        {
            _formName = value;
            OnPropertyChanged();
            RefreshCommands();
        }
    }


    public string FormMaterialType
    {
        get => _formMaterialType;
        set
        {
            _formMaterialType = value;
            OnPropertyChanged();
            RefreshCommands();
        }
    }


    public string FormUnitOfMeasure
    {
        get => _formUnitOfMeasure;
        set
        {
            _formUnitOfMeasure = value;
            OnPropertyChanged();
            RefreshCommands();
        }
    }


    public string? FormDescription
    {
        get => _formDescription;
        set
        {
            _formDescription = value;
            OnPropertyChanged();
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

    private void LoadItems()
    {
        try
        {
            IsBusy = true;

            using ClothingManufactureDbContext db = new();

            Materials = new ObservableCollection<Material>(
                db.Materials
                    .AsNoTracking()
                    .OrderBy(e => e.Id)
                    .ToList()
            );

            StatusMessage = $"Загружено записей: {Materials.Count}";
        }
        catch (Exception ex)
        {
            StatusMessage = "Ошибка загрузки данных.";
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }

    private void AddItem()
    {
        try
        {
            IsBusy = true;

            using ClothingManufactureDbContext db = new();

            Material item = new();
            item.Name = FormName.Trim();
            item.MaterialType = FormMaterialType.Trim();
            item.UnitOfMeasure = FormUnitOfMeasure.Trim();
            item.Description = string.IsNullOrWhiteSpace(FormDescription) ? null : FormDescription.Trim();

            db.Materials.Add(item);
            db.SaveChanges();

            StatusMessage = "Запись добавлена.";
            ClearForm();
            LoadItems();
        }
        catch (Exception ex)
        {
            StatusMessage = "Ошибка добавления записи.";
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }

    private void UpdateItem()
    {
        if (SelectedMaterial == null)
            return;

        try
        {
            IsBusy = true;

            using ClothingManufactureDbContext db = new();

            Material? item = db.Materials.FirstOrDefault(e => e.Id == SelectedMaterial.Id);

            if (item == null)
            {
                MessageBox.Show("Выбранная запись не найдена в базе данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                LoadItems();
                return;
            }

            item.Name = FormName.Trim();
            item.MaterialType = FormMaterialType.Trim();
            item.UnitOfMeasure = FormUnitOfMeasure.Trim();
            item.Description = string.IsNullOrWhiteSpace(FormDescription) ? null : FormDescription.Trim();

            db.SaveChanges();

            StatusMessage = "Запись изменена.";
            ClearForm();
            LoadItems();
        }
        catch (Exception ex)
        {
            StatusMessage = "Ошибка изменения записи.";
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }

    private void DeleteItem()
    {
        if (SelectedMaterial == null)
            return;

        MessageBoxResult result = MessageBox.Show(
            "Удалить выбранную запись?",
            "Подтверждение удаления",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question
        );

        if (result != MessageBoxResult.Yes)
            return;

        try
        {
            IsBusy = true;

            using ClothingManufactureDbContext db = new();

            Material? item = db.Materials.FirstOrDefault(e => e.Id == SelectedMaterial.Id);

            if (item == null)
            {
                MessageBox.Show("Выбранная запись не найдена в базе данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                LoadItems();
                return;
            }

            db.Materials.Remove(item);
            db.SaveChanges();

            StatusMessage = "Запись удалена.";
            ClearForm();
            LoadItems();
        }
        catch (DbUpdateException)
        {
            StatusMessage = "Нельзя удалить запись, которая используется в других таблицах.";
            MessageBox.Show("Эту запись нельзя удалить, потому что она связана с другими данными.", "Удаление невозможно", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        catch (Exception ex)
        {
            StatusMessage = "Ошибка удаления записи.";
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }

    private void ClearForm()
    {
        SelectedMaterial = null;
        FormName = string.Empty;
        FormMaterialType = string.Empty;
        FormUnitOfMeasure = string.Empty;
        FormDescription = null;
        StatusMessage = "Форма очищена.";
    }

    private bool CanSave()
    {
        return !IsBusy
               && !string.IsNullOrWhiteSpace(FormName)
               && !string.IsNullOrWhiteSpace(FormMaterialType)
               && !string.IsNullOrWhiteSpace(FormUnitOfMeasure);
    }

    private bool CanUpdateOrDelete()
    {
        return !IsBusy && SelectedMaterial != null;
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
}
