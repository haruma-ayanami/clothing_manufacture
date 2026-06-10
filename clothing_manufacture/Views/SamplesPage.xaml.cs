using System.Windows.Controls;
using clothing_manufacture.ViewModels;

namespace clothing_manufacture.Views;

public partial class SamplesPage : Page
{
    public SamplesPage()
    {
        InitializeComponent();
        DataContext = new SamplesViewModel();
    }
}
