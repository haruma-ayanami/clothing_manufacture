using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using clothing_manufacture.Commands;
using clothing_manufacture.Data;
using clothing_manufacture.Models;
using Microsoft.EntityFrameworkCore;

namespace clothing_manufacture.ViewModels;

public class ModelVariantsViewModel : BaseViewModel
{
    private ObservableCollection<ModelVariant> _modelVariants = new();
    private ObservableCollection<ClothingModel> _models = new();
    private ObservableCollection<Classifier> _colorClassifiers = new();
    private ObservableCollection<Classifier> _sizeClassifiers = new();
    private ModelVariant? _selectedModelVariant;
    private ClothingModel? _selectedModel;
    private Classifier? _selectedColorClassifier;
    private Classifier? _selectedSizeClassifier;
    private string _formBarcode = string.Empty;
    private string _formStatus = "Активен";
    private string _statusMessage = "Готово к работе.";
    private bool _isBusy;

    public ModelVariantsViewModel()
    {
        LoadCommand = new RelayCommand(_ => LoadData());
        AddCommand = new RelayCommand(_ => AddItem());
        UpdateCommand = new RelayCommand(_ => UpdateItem());
        DeleteCommand = new RelayCommand(_ => DeleteItem());
        ClearCommand = new RelayCommand(_ => ClearForm());

        LoadData();
    }


    public ObservableCollection<ModelVariant> ModelVariants
    {
        get => _modelVariants;
        set { _modelVariants = value; OnPropertyChanged(); }
    }


    public ObservableCollection<ClothingModel> Models
    {
        get => _models;
        set { _models = value; OnPropertyChanged(); }
    }


    public ObservableCollection<Classifier> ColorClassifiers
    {
        get => _colorClassifiers;
        set { _colorClassifiers = value; OnPropertyChanged(); }
    }


    public ObservableCollection<Classifier> SizeClassifiers
    {
        get => _sizeClassifiers;
        set { _sizeClassifiers = value; OnPropertyChanged(); }
    }


    public ModelVariant? SelectedModelVariant
    {
        get => _selectedModelVariant;
        set { _selectedModelVariant = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public ClothingModel? SelectedModel
    {
        get => _selectedModel;
        set { _selectedModel = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public Classifier? SelectedColorClassifier
    {
        get => _selectedColorClassifier;
        set { _selectedColorClassifier = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public Classifier? SelectedSizeClassifier
    {
        get => _selectedSizeClassifier;
        set { _selectedSizeClassifier = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string FormBarcode
    {
        get => _formBarcode;
        set { _formBarcode = value; OnPropertyChanged(); RefreshCommands(); }
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
