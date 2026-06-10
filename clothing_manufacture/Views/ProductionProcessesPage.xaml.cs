using System.Windows.Controls;
using clothing_manufacture.ViewModels;

namespace clothing_manufacture.Views;

public partial class ProductionProcessesPage : Page
{
    public ProductionProcessesPage()
    {
        InitializeComponent();
        DataContext = new ProductionProcessesViewModel();
    }
}
