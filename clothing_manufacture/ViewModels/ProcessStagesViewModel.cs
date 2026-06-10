using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using clothing_manufacture.Commands;
using clothing_manufacture.Data;
using clothing_manufacture.Models;
using Microsoft.EntityFrameworkCore;

namespace clothing_manufacture.ViewModels;

public class ProcessStagesViewModel : BaseViewModel
{
    private ObservableCollection<ProcessStage> _processStages = new();
    private ObservableCollection<ProductionProcess> _productionProcesses = new();
    private ProcessStage? _selectedProcessStage;
    private ProductionProcess? _selectedProductionProcess;
    private string _formName = string.Empty;
    private string _formStageNumber = string.Empty;
    private string? _formDescription = null;
    private string? _formInputData = null;
    private string? _formResult = null;
    private string _statusMessage = "Готово к работе.";
    private bool _isBusy;

    public ProcessStagesViewModel()
    {
        LoadCommand = new RelayCommand(_ => LoadData());
        AddCommand = new RelayCommand(_ => AddItem());
        UpdateCommand = new RelayCommand(_ => UpdateItem());
        DeleteCommand = new RelayCommand(_ => DeleteItem());
        ClearCommand = new RelayCommand(_ => ClearForm());

        LoadData();
    }


    public ObservableCollection<ProcessStage> ProcessStages
    {
        get => _processStages;
        set { _processStages = value; OnPropertyChanged(); }
    }


    public ObservableCollection<ProductionProcess> ProductionProcesses
    {
        get => _productionProcesses;
        set { _productionProcesses = value; OnPropertyChanged(); }
    }


    public ProcessStage? SelectedProcessStage
    {
        get => _selectedProcessStage;
        set { _selectedProcessStage = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public ProductionProcess? SelectedProductionProcess
    {
        get => _selectedProductionProcess;
        set { _selectedProductionProcess = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string FormName
    {
        get => _formName;
        set { _formName = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string FormStageNumber
    {
        get => _formStageNumber;
        set { _formStageNumber = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string? FormDescription
    {
        get => _formDescription;
        set { _formDescription = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string? FormInputData
    {
        get => _formInputData;
        set { _formInputData = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string? FormResult
    {
        get => _formResult;
        set { _formResult = value; OnPropertyChanged(); RefreshCommands(); }
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
