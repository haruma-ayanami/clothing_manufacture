using System.Windows.Controls;
using clothing_manufacture.ViewModels;

namespace clothing_manufacture.Views;

public partial class ProcessStagesPage : Page
{
    public ProcessStagesPage()
    {
        InitializeComponent();
        DataContext = new ProcessStagesViewModel();
    }
}
