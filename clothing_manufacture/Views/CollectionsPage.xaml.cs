using System.Windows.Controls;
using clothing_manufacture.ViewModels;

namespace clothing_manufacture.Views;

public partial class CollectionsPage : Page
{
    public CollectionsPage()
    {
        InitializeComponent();
        DataContext = new CollectionsViewModel();
    }
}
