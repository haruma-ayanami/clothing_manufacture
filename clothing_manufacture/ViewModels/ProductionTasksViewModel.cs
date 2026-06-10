using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using clothing_manufacture.Commands;
using clothing_manufacture.Data;
using clothing_manufacture.Models;
using Microsoft.EntityFrameworkCore;

namespace clothing_manufacture.ViewModels;

public class ProductionTasksViewModel : BaseViewModel
{
    private ObservableCollection<ProductionTask> _productionTasks = new();
    private ObservableCollection<Employee> _employees = new();
    private ObservableCollection<CustomerOrder> _customerOrders = new();
    private ObservableCollection<ClothingCollection> _collections = new();
    private ObservableCollection<AssortmentPlan> _assortmentPlans = new();
    private ProductionTask? _selectedProductionTask;
    private Employee? _selectedPlannerEmployee;
    private CustomerOrder? _selectedCustomerOrder;
    private ClothingCollection? _selectedCollection;
    private AssortmentPlan? _selectedAssortmentPlan;
    private string _formTaskNumber = string.Empty;
    private DateTime _formCreationDate = DateTime.Today;
    private DateTime? _formPlannedStartDate = null;
    private DateTime? _formPlannedEndDate = null;
    private string? _formLaunchSource = null;
    private string _formStatus = "Новое";
    private string? _formComment = null;
    private string _statusMessage = "Готово к работе.";
    private bool _isBusy;

    public ProductionTasksViewModel()
    {
        LoadCommand = new RelayCommand(_ => LoadData());
        AddCommand = new RelayCommand(_ => AddItem());
        UpdateCommand = new RelayCommand(_ => UpdateItem());
        DeleteCommand = new RelayCommand(_ => DeleteItem());
        ClearCommand = new RelayCommand(_ => ClearForm());

        LoadData();
    }


    public ObservableCollection<ProductionTask> ProductionTasks
    {
        get => _productionTasks;
        set { _productionTasks = value; OnPropertyChanged(); }
    }


    public ObservableCollection<Employee> Employees
    {
        get => _employees;
        set { _employees = value; OnPropertyChanged(); }
    }


    public ObservableCollection<CustomerOrder> CustomerOrders
    {
        get => _customerOrders;
        set { _customerOrders = value; OnPropertyChanged(); }
    }


    public ObservableCollection<ClothingCollection> Collections
    {
        get => _collections;
        set { _collections = value; OnPropertyChanged(); }
    }


    public ObservableCollection<AssortmentPlan> AssortmentPlans
    {
        get => _assortmentPlans;
        set { _assortmentPlans = value; OnPropertyChanged(); }
    }


    public ProductionTask? SelectedProductionTask
    {
        get => _selectedProductionTask;
        set { _selectedProductionTask = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public Employee? SelectedPlannerEmployee
    {
        get => _selectedPlannerEmployee;
        set { _selectedPlannerEmployee = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public CustomerOrder? SelectedCustomerOrder
    {
        get => _selectedCustomerOrder;
        set { _selectedCustomerOrder = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public ClothingCollection? SelectedCollection
    {
        get => _selectedCollection;
        set { _selectedCollection = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public AssortmentPlan? SelectedAssortmentPlan
    {
        get => _selectedAssortmentPlan;
        set { _selectedAssortmentPlan = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string FormTaskNumber
    {
        get => _formTaskNumber;
        set { _formTaskNumber = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public DateTime FormCreationDate
    {
        get => _formCreationDate;
        set { _formCreationDate = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public DateTime? FormPlannedStartDate
    {
        get => _formPlannedStartDate;
        set { _formPlannedStartDate = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public DateTime? FormPlannedEndDate
    {
        get => _formPlannedEndDate;
        set { _formPlannedEndDate = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string? FormLaunchSource
    {
        get => _formLaunchSource;
        set { _formLaunchSource = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string FormStatus
    {
        get => _formStatus;
        set { _formStatus = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string? FormComment
    {
        get => _formComment;
        set { _formComment = value; OnPropertyChanged(); RefreshCommands(); }
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
