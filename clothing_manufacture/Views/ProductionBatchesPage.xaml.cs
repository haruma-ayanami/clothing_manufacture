using System.Windows.Controls;
using clothing_manufacture.ViewModels;

namespace clothing_manufacture.Views;

public partial class ProductionBatchesPage : Page
{
    public ProductionBatchesPage()
    {
        InitializeComponent();
        DataContext = new ProductionBatchesViewModel();
    }
}
