using System.Windows.Controls;
using clothing_manufacture.ViewModels;

namespace clothing_manufacture.Views;

public partial class WarehouseStocksPage : Page
{
    public WarehouseStocksPage()
    {
        InitializeComponent();
        DataContext = new WarehouseStocksViewModel();
    }
}
