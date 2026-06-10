using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using clothing_manufacture.Commands;
using clothing_manufacture.Data;
using clothing_manufacture.Models;
using Microsoft.EntityFrameworkCore;

namespace clothing_manufacture.ViewModels;

public class QualityControlsViewModel : BaseViewModel
{
    private ObservableCollection<QualityControl> _qualityControls = new();
    private ObservableCollection<ProductionBatch> _productionBatches = new();
    private ObservableCollection<Sample> _samples = new();
    private ObservableCollection<Employee> _employees = new();
    private QualityControl? _selectedQualityControl;
    private ProductionBatch? _selectedProductionBatch;
    private Sample? _selectedSample;
    private Employee? _selectedControllerEmployee;
    private DateTime _formControlDate = DateTime.Today;
    private string _formControlType = string.Empty;
    private string _formResult = string.Empty;
    private string? _formNotes = null;
    private string? _formDecision = null;
    private string _statusMessage = "Готово к работе.";
    private bool _isBusy;

    public QualityControlsViewModel()
    {
        LoadCommand = new RelayCommand(_ => LoadData());
        AddCommand = new RelayCommand(_ => AddItem());
        UpdateCommand = new RelayCommand(_ => UpdateItem());
        DeleteCommand = new RelayCommand(_ => DeleteItem());
        ClearCommand = new RelayCommand(_ => ClearForm());

        LoadData();
    }


    public ObservableCollection<QualityControl> QualityControls
    {
        get => _qualityControls;
        set { _qualityControls = value; OnPropertyChanged(); }
    }


    public ObservableCollection<ProductionBatch> ProductionBatches
    {
        get => _productionBatches;
        set { _productionBatches = value; OnPropertyChanged(); }
    }


    public ObservableCollection<Sample> Samples
    {
        get => _samples;
        set { _samples = value; OnPropertyChanged(); }
    }


    public ObservableCollection<Employee> Employees
    {
        get => _employees;
        set { _employees = value; OnPropertyChanged(); }
    }


    public QualityControl? SelectedQualityControl
    {
        get => _selectedQualityControl;
        set { _selectedQualityControl = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public ProductionBatch? SelectedProductionBatch
    {
        get => _selectedProductionBatch;
        set { _selectedProductionBatch = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public Sample? SelectedSample
    {
        get => _selectedSample;
        set { _selectedSample = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public Employee? SelectedControllerEmployee
    {
        get => _selectedControllerEmployee;
        set { _selectedControllerEmployee = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public DateTime FormControlDate
    {
        get => _formControlDate;
        set { _formControlDate = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string FormControlType
    {
        get => _formControlType;
        set { _formControlType = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string FormResult
    {
        get => _formResult;
        set { _formResult = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string? FormNotes
    {
        get => _formNotes;
        set { _formNotes = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string? FormDecision
    {
        get => _formDecision;
        set { _formDecision = value; OnPropertyChanged(); RefreshCommands(); }
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
