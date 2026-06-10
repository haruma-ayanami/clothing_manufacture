using System.Windows.Controls;
using clothing_manufacture.ViewModels;

namespace clothing_manufacture.Views;

public partial class ClothingModelsPage : Page
{
    public ClothingModelsPage()
    {
        InitializeComponent();
        DataContext = new ClothingModelsViewModel();
    }
}
