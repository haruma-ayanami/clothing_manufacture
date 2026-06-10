using System.Windows.Controls;
using clothing_manufacture.ViewModels;

namespace clothing_manufacture.Views;

public partial class PatternsPage : Page
{
    public PatternsPage()
    {
        InitializeComponent();
        DataContext = new PatternsViewModel();
    }
}
