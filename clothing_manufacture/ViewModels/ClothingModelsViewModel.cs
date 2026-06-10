using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using clothing_manufacture.Commands;
using clothing_manufacture.Data;
using clothing_manufacture.Models;
using Microsoft.EntityFrameworkCore;

namespace clothing_manufacture.ViewModels;

public class ClothingModelsViewModel : BaseViewModel
{
    private ObservableCollection<ClothingModel> _models = new();
    private ObservableCollection<Classifier> _categoryClassifiers = new();
    private ObservableCollection<ClothingCollection> _collections = new();
    private ObservableCollection<Employee> _employees = new();
    private ClothingModel? _selectedModel;
    private Classifier? _selectedCategoryClassifier;
    private ClothingCollection? _selectedCollection;
    private Employee? _selectedDesignerEmployee;
    private Employee? _selectedConstructorEmployee;
    private string _formArticle = string.Empty;
    private string _formName = string.Empty;
    private string? _formDescription = null;
    private string? _formTechnologicalCard = null;
    private string? _formBrand = null;
    private string? _formLogo = null;
    private string? _formBrandColor = null;
    private string? _formBrandbookRequirements = null;
    private string? _formBrandingMethod = null;
    private string _formStatus = "Активна";
    private string _statusMessage = "Готово к работе.";
    private bool _isBusy;

    public ClothingModelsViewModel()
    {
        LoadCommand = new RelayCommand(_ => LoadData());
        AddCommand = new RelayCommand(_ => AddItem());
        UpdateCommand = new RelayCommand(_ => UpdateItem());
        DeleteCommand = new RelayCommand(_ => DeleteItem());
        ClearCommand = new RelayCommand(_ => ClearForm());

        LoadData();
    }


    public ObservableCollection<ClothingModel> Models
    {
        get => _models;
        set { _models = value; OnPropertyChanged(); }
    }


    public ObservableCollection<Classifier> CategoryClassifiers
    {
        get => _categoryClassifiers;
        set { _categoryClassifiers = value; OnPropertyChanged(); }
    }


    public ObservableCollection<ClothingCollection> Collections
    {
        get => _collections;
        set { _collections = value; OnPropertyChanged(); }
    }


    public ObservableCollection<Employee> Employees
    {
        get => _employees;
        set { _employees = value; OnPropertyChanged(); }
    }


    public ClothingModel? SelectedModel
    {
        get => _selectedModel;
        set { _selectedModel = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public Classifier? SelectedCategoryClassifier
    {
        get => _selectedCategoryClassifier;
        set { _selectedCategoryClassifier = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public ClothingCollection? SelectedCollection
    {
        get => _selectedCollection;
        set { _selectedCollection = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public Employee? SelectedDesignerEmployee
    {
        get => _selectedDesignerEmployee;
        set { _selectedDesignerEmployee = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public Employee? SelectedConstructorEmployee
    {
        get => _selectedConstructorEmployee;
        set { _selectedConstructorEmployee = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string FormArticle
    {
        get => _formArticle;
        set { _formArticle = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string FormName
    {
        get => _formName;
        set { _formName = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string? FormDescription
    {
        get => _formDescription;
        set { _formDescription = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string? FormTechnologicalCard
    {
        get => _formTechnologicalCard;
        set { _formTechnologicalCard = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string? FormBrand
    {
        get => _formBrand;
        set { _formBrand = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string? FormLogo
    {
        get => _formLogo;
        set { _formLogo = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string? FormBrandColor
    {
        get => _formBrandColor;
        set { _formBrandColor = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string? FormBrandbookRequirements
    {
        get => _formBrandbookRequirements;
        set { _formBrandbookRequirements = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string? FormBrandingMethod
    {
        get => _formBrandingMethod;
        set { _formBrandingMethod = value; OnPropertyChanged(); RefreshCommands(); }
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
