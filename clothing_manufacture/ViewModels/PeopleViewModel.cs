using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using clothing_manufacture.Commands;
using clothing_manufacture.Data;
using clothing_manufacture.Models;
using Microsoft.EntityFrameworkCore;

namespace clothing_manufacture.ViewModels;

public class PeopleViewModel : BaseViewModel
{
    private ObservableCollection<Person> _items = new();
    private Person? _selectedItem;
    private string _statusMessage = "Готово к работе.";
    private bool _isBusy;

    private string _formLastName = string.Empty;
    private string _formFirstName = string.Empty;
    private string? _formMiddleName = null;
    private string? _formPhone = null;
    private string? _formEmail = null;

    public PeopleViewModel()
    {
        LoadCommand = new RelayCommand(_ => LoadItems());
        AddCommand = new RelayCommand(_ => AddItem(), _ => CanSave());
        UpdateCommand = new RelayCommand(_ => UpdateItem(), _ => CanUpdateOrDelete());
        DeleteCommand = new RelayCommand(_ => DeleteItem(), _ => CanUpdateOrDelete());
        ClearCommand = new RelayCommand(_ => ClearForm());

        LoadItems();
    }

    public ObservableCollection<Person> People
    {
        get => _items;
        set { _items = value; OnPropertyChanged(); }
    }

    public Person? SelectedPerson
    {
        get => _selectedItem;
        set
        {
            _selectedItem = value;
            OnPropertyChanged();

            if (_selectedItem != null)
            {
                FormLastName = _selectedItem.LastName;
                FormFirstName = _selectedItem.FirstName;
                FormMiddleName = _selectedItem.MiddleName;
                FormPhone = _selectedItem.Phone;
                FormEmail = _selectedItem.Email;
                StatusMessage = "Выбрана запись.";
            }

            RefreshCommands();
        }
    }


    public string FormLastName
    {
        get => _formLastName;
        set { _formLastName = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string FormFirstName
    {
        get => _formFirstName;
        set { _formFirstName = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string? FormMiddleName
    {
        get => _formMiddleName;
        set { _formMiddleName = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string? FormPhone
    {
        get => _formPhone;
        set { _formPhone = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string? FormEmail
    {
        get => _formEmail;
        set { _formEmail = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string StatusMessage { get => _statusMessage; set { _statusMessage = value; OnPropertyChanged(); } }
    public bool IsBusy { get => _isBusy; set { _isBusy = value; OnPropertyChanged(); RefreshCommands(); } }

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
            People = new ObservableCollection<Person>(db.People.AsNoTracking().OrderBy(e => e.Id).ToList());
            StatusMessage = $"Загружено записей: {People.Count}";
        }
        catch (Exception ex)
        {
            StatusMessage = "Ошибка загрузки данных.";
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally { IsBusy = false; }
    }

    private void AddItem()
    {
        try
        {
            IsBusy = true;
            using ClothingManufactureDbContext db = new();
            Person item = new();
            item.LastName = FormLastName.Trim();
            item.FirstName = FormFirstName.Trim();
            item.MiddleName = string.IsNullOrWhiteSpace(FormMiddleName) ? null : FormMiddleName.Trim();
            item.Phone = string.IsNullOrWhiteSpace(FormPhone) ? null : FormPhone.Trim();
            item.Email = string.IsNullOrWhiteSpace(FormEmail) ? null : FormEmail.Trim();
            db.People.Add(item);
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
        finally { IsBusy = false; }
    }

    private void UpdateItem()
    {
        if (SelectedPerson == null) return;

        try
        {
            IsBusy = true;
            using ClothingManufactureDbContext db = new();
            Person? item = db.People.FirstOrDefault(e => e.Id == SelectedPerson.Id);

            if (item == null)
            {
                MessageBox.Show("Выбранная запись не найдена в базе данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                LoadItems();
                return;
            }

            item.LastName = FormLastName.Trim();
            item.FirstName = FormFirstName.Trim();
            item.MiddleName = string.IsNullOrWhiteSpace(FormMiddleName) ? null : FormMiddleName.Trim();
            item.Phone = string.IsNullOrWhiteSpace(FormPhone) ? null : FormPhone.Trim();
            item.Email = string.IsNullOrWhiteSpace(FormEmail) ? null : FormEmail.Trim();
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
        finally { IsBusy = false; }
    }

    private void DeleteItem()
    {
        if (SelectedPerson == null) return;

        if (MessageBox.Show("Удалить выбранную запись?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
            return;

        try
        {
            IsBusy = true;
            using ClothingManufactureDbContext db = new();
            Person? item = db.People.FirstOrDefault(e => e.Id == SelectedPerson.Id);

            if (item == null)
            {
                MessageBox.Show("Выбранная запись не найдена в базе данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                LoadItems();
                return;
            }

            db.People.Remove(item);
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
        finally { IsBusy = false; }
    }

    private void ClearForm()
    {
        SelectedPerson = null;
        FormLastName = string.Empty;
        FormFirstName = string.Empty;
        FormMiddleName = null;
        FormPhone = null;
        FormEmail = null;
        StatusMessage = "Форма очищена.";
    }

    private bool CanSave() => !IsBusy && !string.IsNullOrWhiteSpace(FormLastName) && !string.IsNullOrWhiteSpace(FormFirstName);
    private bool CanUpdateOrDelete() => !IsBusy && SelectedPerson != null;

    private void RefreshCommands()
    {
        if (LoadCommand is RelayCommand loadCommand) loadCommand.RaiseCanExecuteChanged();
        if (AddCommand is RelayCommand addCommand) addCommand.RaiseCanExecuteChanged();
        if (UpdateCommand is RelayCommand updateCommand) updateCommand.RaiseCanExecuteChanged();
        if (DeleteCommand is RelayCommand deleteCommand) deleteCommand.RaiseCanExecuteChanged();
        if (ClearCommand is RelayCommand clearCommand) clearCommand.RaiseCanExecuteChanged();
    }
}
