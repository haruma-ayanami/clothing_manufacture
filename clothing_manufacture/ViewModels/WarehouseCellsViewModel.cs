using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using clothing_manufacture.Commands;
using clothing_manufacture.Data;
using clothing_manufacture.Models;
using Microsoft.EntityFrameworkCore;

namespace clothing_manufacture.ViewModels;

public class WarehouseCellsViewModel : BaseViewModel
{
    private ObservableCollection<WarehouseCell> _warehouseCells = new();
    private ObservableCollection<Warehouse> _warehouses = new();
    private WarehouseCell? _selectedWarehouseCell;
    private Warehouse? _selectedWarehouse;
    private string _formCellCode = string.Empty;
    private string? _formStorageZone = null;
    private string? _formDescription = null;
    private string _statusMessage = "Готово к работе.";
    private bool _isBusy;

    public WarehouseCellsViewModel()
    {
        LoadCommand = new RelayCommand(_ => LoadData());
        AddCommand = new RelayCommand(_ => AddItem());
        UpdateCommand = new RelayCommand(_ => UpdateItem());
        DeleteCommand = new RelayCommand(_ => DeleteItem());
        ClearCommand = new RelayCommand(_ => ClearForm());

        LoadData();
    }


    public ObservableCollection<WarehouseCell> WarehouseCells
    {
        get => _warehouseCells;
        set { _warehouseCells = value; OnPropertyChanged(); }
    }


    public ObservableCollection<Warehouse> Warehouses
    {
        get => _warehouses;
        set { _warehouses = value; OnPropertyChanged(); }
    }


    public WarehouseCell? SelectedWarehouseCell
    {
        get => _selectedWarehouseCell;
        set { _selectedWarehouseCell = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public Warehouse? SelectedWarehouse
    {
        get => _selectedWarehouse;
        set { _selectedWarehouse = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string FormCellCode
    {
        get => _formCellCode;
        set { _formCellCode = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string? FormStorageZone
    {
        get => _formStorageZone;
        set { _formStorageZone = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string? FormDescription
    {
        get => _formDescription;
        set { _formDescription = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string StatusMessage
    {
        get => _statusMessage;
        set { _statusMessage = value; OnPropertyChanged(); }
    }

    public bool IsBusy
    {
        get => _isBusy;
        set { _isBusy = value; OnPropertyChanged(); RefreshCommands(); }
    }

    public ICommand LoadCommand { get; }
    public ICommand AddCommand { get; }
    public ICommand UpdateCommand { get; }
    public ICommand DeleteCommand { get; }
    public ICommand ClearCommand { get; }

    private void LoadData()
    {
        try
        {
            IsBusy = true;
            using ClothingManufactureDbContext db = new();

            // Эта ViewModel уже подготовлена для подключения страницы.
            // При создании соответствующей страницы мы быстро дополним здесь точную загрузку и CRUD-логику.
            StatusMessage = "Раздел подготовлен. Логику экрана подключим на соответствующем этапе.";
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
        MessageBox.Show("Добавление для этого раздела будет подключено на этапе создания страницы.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private void UpdateItem()
    {
        MessageBox.Show("Изменение для этого раздела будет подключено на этапе создания страницы.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private void DeleteItem()
    {
        MessageBox.Show("Удаление для этого раздела будет подключено на этапе создания страницы.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private void ClearForm()
    {
        StatusMessage = "Форма очищена.";
    }

    private void RefreshCommands()
    {
        if (LoadCommand is RelayCommand loadCommand) loadCommand.RaiseCanExecuteChanged();
        if (AddCommand is RelayCommand addCommand) addCommand.RaiseCanExecuteChanged();
        if (UpdateCommand is RelayCommand updateCommand) updateCommand.RaiseCanExecuteChanged();
        if (DeleteCommand is RelayCommand deleteCommand) deleteCommand.RaiseCanExecuteChanged();
        if (ClearCommand is RelayCommand clearCommand) clearCommand.RaiseCanExecuteChanged();
    }
}
