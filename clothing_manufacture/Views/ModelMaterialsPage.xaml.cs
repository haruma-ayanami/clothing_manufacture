using System.Windows.Controls;
using clothing_manufacture.ViewModels;

namespace clothing_manufacture.Views;

public partial class ModelMaterialsPage : Page
{
    public ModelMaterialsPage()
    {
        InitializeComponent();
        DataContext = new ModelMaterialsViewModel();
    }
}
