using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using clothing_manufacture.Commands;
using clothing_manufacture.Data;
using clothing_manufacture.Models;
using Microsoft.EntityFrameworkCore;

namespace clothing_manufacture.ViewModels;

public class AssortmentPlansViewModel : BaseViewModel
{
    private ObservableCollection<AssortmentPlan> _assortmentPlans = new();
    private ObservableCollection<Employee> _employees = new();
    private AssortmentPlan? _selectedAssortmentPlan;
    private Employee? _selectedResponsibleEmployee;
    private Employee? _selectedApprovedEmployee;
    private string _formPlanNumber = string.Empty;
    private string _formPeriod = string.Empty;
    private string _formStatus = "Новый";
    private string? _formComment = null;
    private string _statusMessage = "Готово к работе.";
    private bool _isBusy;

    public AssortmentPlansViewModel()
    {
        LoadCommand = new RelayCommand(_ => LoadData());
        AddCommand = new RelayCommand(_ => AddItem());
        UpdateCommand = new RelayCommand(_ => UpdateItem());
        DeleteCommand = new RelayCommand(_ => DeleteItem());
        ClearCommand = new RelayCommand(_ => ClearForm());

        LoadData();
    }


    public ObservableCollection<AssortmentPlan> AssortmentPlans
    {
        get => _assortmentPlans;
        set { _assortmentPlans = value; OnPropertyChanged(); }
    }


    public ObservableCollection<Employee> Employees
    {
        get => _employees;
        set { _employees = value; OnPropertyChanged(); }
    }


    public AssortmentPlan? SelectedAssortmentPlan
    {
        get => _selectedAssortmentPlan;
        set { _selectedAssortmentPlan = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public Employee? SelectedResponsibleEmployee
    {
        get => _selectedResponsibleEmployee;
        set { _selectedResponsibleEmployee = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public Employee? SelectedApprovedEmployee
    {
        get => _selectedApprovedEmployee;
        set { _selectedApprovedEmployee = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string FormPlanNumber
    {
        get => _formPlanNumber;
        set { _formPlanNumber = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string FormPeriod
    {
        get => _formPeriod;
        set { _formPeriod = value; OnPropertyChanged(); RefreshCommands(); }
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
