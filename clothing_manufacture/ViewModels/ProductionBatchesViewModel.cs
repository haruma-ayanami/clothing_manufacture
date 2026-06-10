using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using clothing_manufacture.Commands;
using clothing_manufacture.Data;
using clothing_manufacture.Models;
using Microsoft.EntityFrameworkCore;

namespace clothing_manufacture.ViewModels;

public class ProductionBatchesViewModel : BaseViewModel
{
    private ObservableCollection<ProductionBatch> _productionBatches = new();
    private ObservableCollection<ProductionTask> _productionTasks = new();
    private ObservableCollection<ModelVariant> _modelVariants = new();
    private ProductionBatch? _selectedProductionBatch;
    private ProductionTask? _selectedProductionTask;
    private ModelVariant? _selectedModelVariant;
    private string _formBatchNumber = string.Empty;
    private string _formQuantity = string.Empty;
    private DateTime? _formStartDate = null;
    private DateTime? _formEndDate = null;
    private string _formStatus = "Новая";
    private string _statusMessage = "Готово к работе.";
    private bool _isBusy;

    public ProductionBatchesViewModel()
    {
        LoadCommand = new RelayCommand(_ => LoadData());
        AddCommand = new RelayCommand(_ => AddItem());
        UpdateCommand = new RelayCommand(_ => UpdateItem());
        DeleteCommand = new RelayCommand(_ => DeleteItem());
        ClearCommand = new RelayCommand(_ => ClearForm());

        LoadData();
    }


    public ObservableCollection<ProductionBatch> ProductionBatches
    {
        get => _productionBatches;
        set { _productionBatches = value; OnPropertyChanged(); }
    }


    public ObservableCollection<ProductionTask> ProductionTasks
    {
        get => _productionTasks;
        set { _productionTasks = value; OnPropertyChanged(); }
    }


    public ObservableCollection<ModelVariant> ModelVariants
    {
        get => _modelVariants;
        set { _modelVariants = value; OnPropertyChanged(); }
    }


    public ProductionBatch? SelectedProductionBatch
    {
        get => _selectedProductionBatch;
        set { _selectedProductionBatch = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public ProductionTask? SelectedProductionTask
    {
        get => _selectedProductionTask;
        set { _selectedProductionTask = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public ModelVariant? SelectedModelVariant
    {
        get => _selectedModelVariant;
        set { _selectedModelVariant = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string FormBatchNumber
    {
        get => _formBatchNumber;
        set { _formBatchNumber = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string FormQuantity
    {
        get => _formQuantity;
        set { _formQuantity = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public DateTime? FormStartDate
    {
        get => _formStartDate;
        set { _formStartDate = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public DateTime? FormEndDate
    {
        get => _formEndDate;
        set { _formEndDate = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string FormStatus
    {
        get => _formStatus;
        set { _formStatus = value; OnPropertyChanged(); RefreshCommands(); }
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
