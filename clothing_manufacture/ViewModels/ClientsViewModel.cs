using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using clothing_manufacture.Commands;
using clothing_manufacture.Data;
using clothing_manufacture.Models;
using Microsoft.EntityFrameworkCore;

namespace clothing_manufacture.ViewModels;

public class ClientsViewModel : BaseViewModel
{
    private ObservableCollection<Client> _items = new();
    private Client? _selectedItem;
    private string _statusMessage = "Готово к работе.";
    private bool _isBusy;

    private string _formName = string.Empty;
    private string _formClientType = string.Empty;
    private string? _formPhone = null;
    private string? _formEmail = null;
    private string? _formAddress = null;

    public ClientsViewModel()
    {
        LoadCommand = new RelayCommand(_ => LoadItems());
        AddCommand = new RelayCommand(_ => AddItem(), _ => CanSave());
        UpdateCommand = new RelayCommand(_ => UpdateItem(), _ => CanUpdateOrDelete());
        DeleteCommand = new RelayCommand(_ => DeleteItem(), _ => CanUpdateOrDelete());
        ClearCommand = new RelayCommand(_ => ClearForm());

        LoadItems();
    }

    public ObservableCollection<Client> Clients
    {
        get => _items;
        set
        {
            _items = value;
            OnPropertyChanged();
        }
    }

    public Client? SelectedClient
    {
        get => _selectedItem;
        set
        {
            _selectedItem = value;
            OnPropertyChanged();

            if (_selectedItem != null)
            {
                FormName = _selectedItem.Name;
                FormClientType = _selectedItem.ClientType;
                FormPhone = _selectedItem.Phone;
                FormEmail = _selectedItem.Email;
                FormAddress = _selectedItem.Address;
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


    public string FormClientType
    {
        get => _formClientType;
        set
        {
            _formClientType = value;
            OnPropertyChanged();
            RefreshCommands();
        }
    }


    public string? FormPhone
    {
        get => _formPhone;
        set
        {
            _formPhone = value;
            OnPropertyChanged();
            RefreshCommands();
        }
    }


    public string? FormEmail
    {
        get => _formEmail;
        set
        {
            _formEmail = value;
            OnPropertyChanged();
            RefreshCommands();
        }
    }


    public string? FormAddress
    {
        get => _formAddress;
        set
        {
            _formAddress = value;
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

            Clients = new ObservableCollection<Client>(
                db.Clients
                    .AsNoTracking()
                    .OrderBy(e => e.Id)
                    .ToList()
            );

            StatusMessage = $"Загружено записей: {Clients.Count}";
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

            Client item = new();
            item.Name = FormName.Trim();
            item.ClientType = FormClientType.Trim();
            item.Phone = string.IsNullOrWhiteSpace(FormPhone) ? null : FormPhone.Trim();
            item.Email = string.IsNullOrWhiteSpace(FormEmail) ? null : FormEmail.Trim();
            item.Address = string.IsNullOrWhiteSpace(FormAddress) ? null : FormAddress.Trim();

            db.Clients.Add(item);
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
        if (SelectedClient == null)
            return;

        try
        {
            IsBusy = true;

            using ClothingManufactureDbContext db = new();

            Client? item = db.Clients.FirstOrDefault(e => e.Id == SelectedClient.Id);

            if (item == null)
            {
                MessageBox.Show("Выбранная запись не найдена в базе данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                LoadItems();
                return;
            }

            item.Name = FormName.Trim();
            item.ClientType = FormClientType.Trim();
            item.Phone = string.IsNullOrWhiteSpace(FormPhone) ? null : FormPhone.Trim();
            item.Email = string.IsNullOrWhiteSpace(FormEmail) ? null : FormEmail.Trim();
            item.Address = string.IsNullOrWhiteSpace(FormAddress) ? null : FormAddress.Trim();

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
        if (SelectedClient == null)
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

            Client? item = db.Clients.FirstOrDefault(e => e.Id == SelectedClient.Id);

            if (item == null)
            {
                MessageBox.Show("Выбранная запись не найдена в базе данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                LoadItems();
                return;
            }

            db.Clients.Remove(item);
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
        SelectedClient = null;
        FormName = string.Empty;
        FormClientType = string.Empty;
        FormPhone = null;
        FormEmail = null;
        FormAddress = null;
        StatusMessage = "Форма очищена.";
    }

    private bool CanSave()
    {
        return !IsBusy
               && !string.IsNullOrWhiteSpace(FormName)
               && !string.IsNullOrWhiteSpace(FormClientType);
    }

    private bool CanUpdateOrDelete()
    {
        return !IsBusy && SelectedClient != null;
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
