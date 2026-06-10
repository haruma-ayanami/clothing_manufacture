using System.Windows.Controls;
using clothing_manufacture.ViewModels;

namespace clothing_manufacture.Views;

public partial class ClassifiersPage : Page
{
    public ClassifiersPage()
    {
        InitializeComponent();
        DataContext = new ClassifiersViewModel();
    }
}
