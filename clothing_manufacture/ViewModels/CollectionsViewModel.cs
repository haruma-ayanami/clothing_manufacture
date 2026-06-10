using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using clothing_manufacture.Commands;
using clothing_manufacture.Data;
using clothing_manufacture.Models;
using Microsoft.EntityFrameworkCore;

namespace clothing_manufacture.ViewModels;

public class CollectionsViewModel : BaseViewModel
{
    private ObservableCollection<ClothingCollection> _collections = new();
    private ObservableCollection<AssortmentPlan> _assortmentPlans = new();
    private ClothingCollection? _selectedCollection;
    private AssortmentPlan? _selectedAssortmentPlan;
    private string _formName = string.Empty;
    private string? _formSeason = null;
    private string? _formYear = null;
    private string? _formPriceSegment = null;
    private string _formStatus = "Новая";
    private string _statusMessage = "Готово к работе.";
    private bool _isBusy;

    public CollectionsViewModel()
    {
        LoadCommand = new RelayCommand(_ => LoadData());
        AddCommand = new RelayCommand(_ => AddItem());
        UpdateCommand = new RelayCommand(_ => UpdateItem());
        DeleteCommand = new RelayCommand(_ => DeleteItem());
        ClearCommand = new RelayCommand(_ => ClearForm());

        LoadData();
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


    public string FormName
    {
        get => _formName;
        set { _formName = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string? FormSeason
    {
        get => _formSeason;
        set { _formSeason = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string? FormYear
    {
        get => _formYear;
        set { _formYear = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string? FormPriceSegment
    {
        get => _formPriceSegment;
        set { _formPriceSegment = value; OnPropertyChanged(); RefreshCommands(); }
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
