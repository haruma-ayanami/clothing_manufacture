using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using clothing_manufacture.Commands;
using clothing_manufacture.Data;
using clothing_manufacture.Models;
using Microsoft.EntityFrameworkCore;

namespace clothing_manufacture.ViewModels;

public class ModelMaterialsViewModel : BaseViewModel
{
    private ObservableCollection<ModelMaterial> _modelMaterials = new();
    private ObservableCollection<ClothingModel> _models = new();
    private ObservableCollection<Material> _materials = new();
    private ModelMaterial? _selectedModelMaterial;
    private ClothingModel? _selectedModel;
    private Material? _selectedMaterial;
    private string _formQuantityPerItem = string.Empty;
    private bool _formIsMainMaterial = false;
    private string _statusMessage = "Готово к работе.";
    private bool _isBusy;

    public ModelMaterialsViewModel()
    {
        LoadCommand = new RelayCommand(_ => LoadData());
        AddCommand = new RelayCommand(_ => AddItem());
        UpdateCommand = new RelayCommand(_ => UpdateItem());
        DeleteCommand = new RelayCommand(_ => DeleteItem());
        ClearCommand = new RelayCommand(_ => ClearForm());

        LoadData();
    }


    public ObservableCollection<ModelMaterial> ModelMaterials
    {
        get => _modelMaterials;
        set { _modelMaterials = value; OnPropertyChanged(); }
    }


    public ObservableCollection<ClothingModel> Models
    {
        get => _models;
        set { _models = value; OnPropertyChanged(); }
    }


    public ObservableCollection<Material> Materials
    {
        get => _materials;
        set { _materials = value; OnPropertyChanged(); }
    }


    public ModelMaterial? SelectedModelMaterial
    {
        get => _selectedModelMaterial;
        set { _selectedModelMaterial = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public ClothingModel? SelectedModel
    {
        get => _selectedModel;
        set { _selectedModel = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public Material? SelectedMaterial
    {
        get => _selectedMaterial;
        set { _selectedMaterial = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public string FormQuantityPerItem
    {
        get => _formQuantityPerItem;
        set { _formQuantityPerItem = value; OnPropertyChanged(); RefreshCommands(); }
    }


    public bool FormIsMainMaterial
    {
        get => _formIsMainMaterial;
        set { _formIsMainMaterial = value; OnPropertyChanged(); RefreshCommands(); }
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
