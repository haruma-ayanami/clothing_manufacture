using System.Windows.Controls;
using clothing_manufacture.ViewModels;

namespace clothing_manufacture.Views;

public partial class ModelVariantsPage : Page
{
    public ModelVariantsPage()
    {
        InitializeComponent();
        DataContext = new ModelVariantsViewModel();
    }
}
