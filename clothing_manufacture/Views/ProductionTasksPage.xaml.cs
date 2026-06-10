using System.Windows.Controls;
using clothing_manufacture.ViewModels;

namespace clothing_manufacture.Views;

public partial class ProductionTasksPage : Page
{
    public ProductionTasksPage()
    {
        InitializeComponent();
        DataContext = new ProductionTasksViewModel();
    }
}
