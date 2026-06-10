using System.Windows.Controls;
using clothing_manufacture.ViewModels;

namespace clothing_manufacture.Views;

public partial class MaterialsPage : Page
{
    public MaterialsPage()
    {
        InitializeComponent();
        DataContext = new MaterialsViewModel();
    }
}
