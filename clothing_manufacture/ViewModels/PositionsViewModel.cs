using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using clothing_manufacture.Commands;
using clothing_manufacture.Data;
using clothing_manufacture.Models;
using Microsoft.EntityFrameworkCore;

namespace clothing_manufacture.ViewModels;

public class PositionsViewModel : BaseViewModel
{
    private ObservableCollection<Position> _items = new();
    private Position? _selectedItem;
    private string _statusMessage = "Готово к работе.";
    private bool _isBusy;

    private string _formName = string.Empty;
    private string? _formDescription = null;

    public PositionsViewModel()
    {
        LoadCommand = new RelayCommand(_ => LoadItems());
        AddCommand = new RelayCommand(_ => AddItem(), _ => CanSave());
        UpdateCommand = new RelayCommand(_ => UpdateItem(), _ => CanUpdateOrDelete());
        DeleteCommand = new RelayCommand(_ => DeleteItem(), _ => CanUpdateOrDelete());
        ClearCommand = new RelayCommand(_ => ClearForm());

        LoadItems();
    }

    public ObservableCollection<Position> Positions
    {
        get => _items;
        set
        {
            _items = value;
            OnPropertyChanged();
        }
    }

    public Position? SelectedPosition
    {
        get => _selectedItem;
        set
        {
            _selectedItem = value;
            OnPropertyChanged();

            if (_selectedItem != null)
            {
                FormName = _selectedItem.Name;
                FormDescription = _selectedItem.Description;
                StatusMessage = "Выбрана запись.";
            }

            RefreshCommands();
        }
    }


    public string FormName
    {
        get => _formName;
        set
        {
            _formName = value;
            OnPropertyChanged();
            RefreshCommands();
        }
    }


    public string? FormDescription
    {
        get => _formDescription;
        set
        {
            _formDescription = value;
            OnPropertyChanged();
            RefreshCommands();
        }
    }


    public string StatusMessage
    {
        get => _statusMessage;
        set
        {
            _statusMessage = value;
            OnPropertyChanged();
        }
    }

    public bool IsBusy
    {
        get => _isBusy;
        set
        {
            _isBusy = value;
            OnPropertyChanged();
            RefreshCommands();
        }
    }

    public ICommand LoadCommand { get; }
    public ICommand AddCommand { get; }
    public ICommand UpdateCommand { get; }
    public ICommand DeleteCommand { get; }
    public ICommand ClearCommand { get; }

    private void LoadItems()
    {
        try
        {
            IsBusy = true;

            using ClothingManufactureDbContext db = new();

            Positions = new ObservableCollection<Position>(
                db.Positions
                    .AsNoTracking()
                    .OrderBy(e => e.Id)
                    .ToList()
            );

            StatusMessage = $"Загружено записей: {Positions.Count}";
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
        try
        {
            IsBusy = true;

            using ClothingManufactureDbContext db = new();

            Position item = new();
            item.Name = FormName.Trim();
            item.Description = string.IsNullOrWhiteSpace(FormDescription) ? null : FormDescription.Trim();

            db.Positions.Add(item);
            db.SaveChanges();

            StatusMessage = "Запись добавлена.";
            ClearForm();
            LoadItems();
        }
        catch (Exception ex)
        {
            StatusMessage = "Ошибка добавления записи.";
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }

    private void UpdateItem()
    {
        if (SelectedPosition == null)
            return;

        try
        {
            IsBusy = true;

            using ClothingManufactureDbContext db = new();

            Position? item = db.Positions.FirstOrDefault(e => e.Id == SelectedPosition.Id);

            if (item == null)
            {
                MessageBox.Show("Выбранная запись не найдена в базе данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                LoadItems();
                return;
            }

            item.Name = FormName.Trim();
            item.Description = string.IsNullOrWhiteSpace(FormDescription) ? null : FormDescription.Trim();

            db.SaveChanges();

            StatusMessage = "Запись изменена.";
            ClearForm();
            LoadItems();
        }
        catch (Exception ex)
        {
            StatusMessage = "Ошибка изменения записи.";
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }

    private void DeleteItem()
    {
        if (SelectedPosition == null)
            return;

        MessageBoxResult result = MessageBox.Show(
            "Удалить выбранную запись?",
            "Подтверждение удаления",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question
        );

        if (result != MessageBoxResult.Yes)
            return;

        try
        {
            IsBusy = true;

            using ClothingManufactureDbContext db = new();

            Position? item = db.Positions.FirstOrDefault(e => e.Id == SelectedPosition.Id);

            if (item == null)
            {
                MessageBox.Show("Выбранная запись не найдена в базе данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                LoadItems();
                return;
            }

            db.Positions.Remove(item);
            db.SaveChanges();

            StatusMessage = "Запись удалена.";
            ClearForm();
            LoadItems();
        }
        catch (DbUpdateException)
        {
            StatusMessage = "Нельзя удалить запись, которая используется в других таблицах.";
            MessageBox.Show("Эту запись нельзя удалить, потому что она связана с другими данными.", "Удаление невозможно", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        catch (Exception ex)
        {
            StatusMessage = "Ошибка удаления записи.";
            MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }

    private void ClearForm()
    {
        SelectedPosition = null;
        FormName = string.Empty;
        FormDescription = null;
        StatusMessage = "Форма очищена.";
    }

    private bool CanSave()
    {
        return !IsBusy
               && !string.IsNullOrWhiteSpace(FormName);
    }

    private bool CanUpdateOrDelete()
    {
        return !IsBusy && SelectedPosition != null;
    }

    private void RefreshCommands()
    {
        if (LoadCommand is RelayCommand loadCommand)
            loadCommand.RaiseCanExecuteChanged();

        if (AddCommand is RelayCommand addCommand)
            addCommand.RaiseCanExecuteChanged();

        if (UpdateCommand is RelayCommand updateCommand)
            updateCommand.RaiseCanExecuteChanged();

        if (DeleteCommand is RelayCommand deleteCommand)
            deleteCommand.RaiseCanExecuteChanged();

        if (ClearCommand is RelayCommand clearCommand)
            clearCommand.RaiseCanExecuteChanged();
    }
}
