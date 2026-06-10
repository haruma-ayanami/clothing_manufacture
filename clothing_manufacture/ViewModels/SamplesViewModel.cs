using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using clothing_manufacture.Commands;
using clothing_manufacture.Data;
using clothing_manufacture.Models;
using Microsoft.EntityFrameworkCore;

namespace clothing_manufacture.ViewModels;

public class SamplesViewModel : BaseViewModel
{
    private ObservableCollection<Sample> _samples = new();
    private ObservableCollection<ClothingModel> _models = new();
    private ObservableCollection<Pattern> _patterns = new();
    private ObservableCollection<Employee> _employees = new();
    private Sample? _selectedSample;
    private ClothingModel? _selectedModel;
    private Pattern? _selectedPattern;
    private Employee? _selectedTailorEmployee;
    private Employee? _selectedTechnologistEmployee;
    private DateTime _formManufactureDate = DateTime.Today;
    private string? _formFittingResult = null;
    private string _formStatus = "Новый";
    private string? _formRevisionComment = null;
    private string _statusMessage = "Готово к работе.";
    private bool _isBusy;

    public SamplesViewModel()
    {
        LoadCommand = new RelayCommand(_ => LoadData());
        AddCommand = new RelayCommand(_ => AddItem());
        UpdateCommand = new RelayCommand(_ => UpdateItem());
        DeleteCommand = new RelayCommand(_ => DeleteItem());
        ClearCommand = new RelayCommand(_ => ClearForm());

        LoadData();
    }


    public ObservableCollection<Sample> Samples
    {
        get => _samples;
        set { _samples = value; OnPropertyChanged(); }
    }


    public ObservableCollection<ClothingModel> Models
    {
        get => _models;
        set { _models = value; OnPropertyChanged(); }
    }


    public ObservableCollection<Pattern> Patterns
    {
        get => _patterns;
        set { _patterns = value; OnPropertyChanged(); }
    }


    public ObservableCollection<Employee> Employees
    {
        get => _employees;
        set { _employees = value; OnPropertyChanged(); }
    }


    public Sample? SelectedSample
    {
        get => _selectedSample;
        set { _selectedSample = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public ClothingModel? SelectedModel
    {
        get => _selectedModel;
        set { _selectedModel = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public Pattern? SelectedPattern
    {
        get => _selectedPattern;
        set { _selectedPattern = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public Employee? SelectedTailorEmployee
    {
        get => _selectedTailorEmployee;
        set { _selectedTailorEmployee = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public Employee? SelectedTechnologistEmployee
    {
        get => _selectedTechnologistEmployee;
        set { _selectedTechnologistEmployee = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public DateTime FormManufactureDate
    {
        get => _formManufactureDate;
        set { _formManufactureDate = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string? FormFittingResult
    {
        get => _formFittingResult;
        set { _formFittingResult = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string FormStatus
    {
        get => _formStatus;
        set { _formStatus = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string? FormRevisionComment
    {
        get => _formRevisionComment;
        set { _formRevisionComment = value; OnPropertyChanged(); RefreshCommands(); }
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
