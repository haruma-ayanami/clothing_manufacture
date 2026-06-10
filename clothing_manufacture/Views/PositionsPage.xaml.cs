using System.Windows.Controls;
using clothing_manufacture.ViewModels;

namespace clothing_manufacture.Views;

public partial class PositionsPage : Page
{
    public PositionsPage()
    {
        InitializeComponent();
        DataContext = new PositionsViewModel();
    }
}
