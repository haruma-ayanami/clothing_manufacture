using System.Windows.Controls;
using clothing_manufacture.ViewModels;

namespace clothing_manufacture.Views;

public partial class ProductOperationsPage : Page
{
    public ProductOperationsPage()
    {
        InitializeComponent();
        DataContext = new ProductOperationsViewModel();
    }
}
