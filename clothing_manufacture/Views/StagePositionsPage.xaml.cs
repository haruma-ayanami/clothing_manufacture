using System.Windows.Controls;
using clothing_manufacture.ViewModels;

namespace clothing_manufacture.Views;

public partial class StagePositionsPage : Page
{
    public StagePositionsPage()
    {
        InitializeComponent();
        DataContext = new StagePositionsViewModel();
    }
}
