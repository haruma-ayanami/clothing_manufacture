using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using clothing_manufacture.Commands;
using clothing_manufacture.Data;
using clothing_manufacture.Models;
using Microsoft.EntityFrameworkCore;

namespace clothing_manufacture.ViewModels;

public class ProductOperationsViewModel : BaseViewModel
{
    private ObservableCollection<ProductOperation> _productOperations = new();
    private ObservableCollection<Warehouse> _warehouses = new();
    private ObservableCollection<Material> _materials = new();
    private ObservableCollection<ModelVariant> _modelVariants = new();
    private ObservableCollection<Client> _clients = new();
    private ObservableCollection<Employee> _employees = new();
    private ProductOperation? _selectedProductOperation;
    private Warehouse? _selectedSenderWarehouse;
    private Warehouse? _selectedReceiverWarehouse;
    private Material? _selectedMaterial;
    private ModelVariant? _selectedModelVariant;
    private Client? _selectedClient;
    private Employee? _selectedResponsibleEmployee;
    private string _formOperationType = string.Empty;
    private DateTime _formOperationDate = DateTime.Today;
    private string _formQuantity = string.Empty;
    private string? _formUnitPrice = null;
    private string? _formDocumentNumber = null;
    private string? _formRecipient = null;
    private string? _formDeliveryAddress = null;
    private string? _formRoute = null;
    private string _formStatus = "Новая";
    private string? _formReturnReason = null;
    private string? _formReturnDecision = null;
    private string _statusMessage = "Готово к работе.";
    private bool _isBusy;

    public ProductOperationsViewModel()
    {
        LoadCommand = new RelayCommand(_ => LoadData());
        AddCommand = new RelayCommand(_ => AddItem());
        UpdateCommand = new RelayCommand(_ => UpdateItem());
        DeleteCommand = new RelayCommand(_ => DeleteItem());
        ClearCommand = new RelayCommand(_ => ClearForm());

        LoadData();
    }


    public ObservableCollection<ProductOperation> ProductOperations
    {
        get => _productOperations;
        set { _productOperations = value; OnPropertyChanged(); }
    }


    public ObservableCollection<Warehouse> Warehouses
    {
        get => _warehouses;
        set { _warehouses = value; OnPropertyChanged(); }
    }


    public ObservableCollection<Material> Materials
    {
        get => _materials;
        set { _materials = value; OnPropertyChanged(); }
    }


    public ObservableCollection<ModelVariant> ModelVariants
    {
        get => _modelVariants;
        set { _modelVariants = value; OnPropertyChanged(); }
    }


    public ObservableCollection<Client> Clients
    {
        get => _clients;
        set { _clients = value; OnPropertyChanged(); }
    }


    public ObservableCollection<Employee> Employees
    {
        get => _employees;
        set { _employees = value; OnPropertyChanged(); }
    }


    public ProductOperation? SelectedProductOperation
    {
        get => _selectedProductOperation;
        set { _selectedProductOperation = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public Warehouse? SelectedSenderWarehouse
    {
        get => _selectedSenderWarehouse;
        set { _selectedSenderWarehouse = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public Warehouse? SelectedReceiverWarehouse
    {
        get => _selectedReceiverWarehouse;
        set { _selectedReceiverWarehouse = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public Material? SelectedMaterial
    {
        get => _selectedMaterial;
        set { _selectedMaterial = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public ModelVariant? SelectedModelVariant
    {
        get => _selectedModelVariant;
        set { _selectedModelVariant = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public Client? SelectedClient
    {
        get => _selectedClient;
        set { _selectedClient = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public Employee? SelectedResponsibleEmployee
    {
        get => _selectedResponsibleEmployee;
        set { _selectedResponsibleEmployee = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string FormOperationType
    {
        get => _formOperationType;
        set { _formOperationType = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public DateTime FormOperationDate
    {
        get => _formOperationDate;
        set { _formOperationDate = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string FormQuantity
    {
        get => _formQuantity;
        set { _formQuantity = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string? FormUnitPrice
    {
        get => _formUnitPrice;
        set { _formUnitPrice = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string? FormDocumentNumber
    {
        get => _formDocumentNumber;
        set { _formDocumentNumber = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string? FormRecipient
    {
        get => _formRecipient;
        set { _formRecipient = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string? FormDeliveryAddress
    {
        get => _formDeliveryAddress;
        set { _formDeliveryAddress = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string? FormRoute
    {
        get => _formRoute;
        set { _formRoute = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string FormStatus
    {
        get => _formStatus;
        set { _formStatus = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string? FormReturnReason
    {
        get => _formReturnReason;
        set { _formReturnReason = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string? FormReturnDecision
    {
        get => _formReturnDecision;
        set { _formReturnDecision = value; OnPropertyChanged(); RefreshCommands(); }
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
