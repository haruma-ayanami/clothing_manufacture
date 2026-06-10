using System.Windows.Controls;
using clothing_manufacture.ViewModels;

namespace clothing_manufacture.Views;

public partial class WarehouseCellsPage : Page
{
    public WarehouseCellsPage()
    {
        InitializeComponent();
        DataContext = new WarehouseCellsViewModel();
    }
}
