using System.Windows.Controls;
using clothing_manufacture.ViewModels;

namespace clothing_manufacture.Views;

public partial class AssortmentPlansPage : Page
{
    public AssortmentPlansPage()
    {
        InitializeComponent();
        DataContext = new AssortmentPlansViewModel();
    }
}
