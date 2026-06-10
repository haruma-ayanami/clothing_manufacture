using System.Collections.ObjectModel;
using clothing_manufacture.Services;

namespace clothing_manufacture.ViewModels;

public class DashboardViewModel : BaseViewModel
{
    private string _connectionStatus = "Проверка подключения...";

    public DashboardViewModel()
    {
        Load();
    }

    public string ConnectionStatus
    {
        get => _connectionStatus;
        set
        {
            _connectionStatus = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<string> TableCounts { get; } = new();

    public void Load()
    {
        TableCounts.Clear();
        ConnectionStatus = DatabaseTestService.CheckConnection();

        try
        {
            foreach (var item in DatabaseTestService.GetTableCounts())
                TableCounts.Add($"{item.Key}: {item.Value}");
        }
        catch (Exception ex)
        {
            TableCounts.Add("Не удалось загрузить статистику: " + DatabaseTestService.GetFullError(ex));
        }
    }
}
