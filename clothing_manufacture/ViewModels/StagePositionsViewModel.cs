using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using clothing_manufacture.Commands;
using clothing_manufacture.Data;
using clothing_manufacture.Models;
using Microsoft.EntityFrameworkCore;

namespace clothing_manufacture.ViewModels;

public class StagePositionsViewModel : BaseViewModel
{
    private ObservableCollection<StagePosition> _stagePositions = new();
    private ObservableCollection<ProcessStage> _processStages = new();
    private ObservableCollection<Position> _positions = new();
    private StagePosition? _selectedStagePosition;
    private ProcessStage? _selectedProcessStage;
    private Position? _selectedPosition;
    private string _formStageRole = string.Empty;
    private string? _formInteractionDescription = null;
    private string _statusMessage = "Готово к работе.";
    private bool _isBusy;

    public StagePositionsViewModel()
    {
        LoadCommand = new RelayCommand(_ => LoadData());
        AddCommand = new RelayCommand(_ => AddItem());
        UpdateCommand = new RelayCommand(_ => UpdateItem());
        DeleteCommand = new RelayCommand(_ => DeleteItem());
        ClearCommand = new RelayCommand(_ => ClearForm());

        LoadData();
    }


    public ObservableCollection<StagePosition> StagePositions
    {
        get => _stagePositions;
        set { _stagePositions = value; OnPropertyChanged(); }
    }


    public ObservableCollection<ProcessStage> ProcessStages
    {
        get => _processStages;
        set { _processStages = value; OnPropertyChanged(); }
    }


    public ObservableCollection<Position> Positions
    {
        get => _positions;
        set { _positions = value; OnPropertyChanged(); }
    }


    public StagePosition? SelectedStagePosition
    {
        get => _selectedStagePosition;
        set { _selectedStagePosition = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public ProcessStage? SelectedProcessStage
    {
        get => _selectedProcessStage;
        set { _selectedProcessStage = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public Position? SelectedPosition
    {
        get => _selectedPosition;
        set { _selectedPosition = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string FormStageRole
    {
        get => _formStageRole;
        set { _formStageRole = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string? FormInteractionDescription
    {
        get => _formInteractionDescription;
        set { _formInteractionDescription = value; OnPropertyChanged(); RefreshCommands(); }
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
