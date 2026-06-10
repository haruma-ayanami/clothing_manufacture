using System.Windows.Controls;
using clothing_manufacture.ViewModels;

namespace clothing_manufacture.Views;

public partial class WarehousesPage : Page
{
    public WarehousesPage()
    {
        InitializeComponent();
        DataContext = new WarehousesViewModel();
    }
}
