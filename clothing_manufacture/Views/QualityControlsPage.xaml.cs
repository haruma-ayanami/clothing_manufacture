using System.Windows.Controls;
using clothing_manufacture.ViewModels;

namespace clothing_manufacture.Views;

public partial class QualityControlsPage : Page
{
    public QualityControlsPage()
    {
        InitializeComponent();
        DataContext = new QualityControlsViewModel();
    }
}
