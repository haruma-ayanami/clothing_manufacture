using System.Windows.Controls;
using clothing_manufacture.ViewModels;

namespace clothing_manufacture.Views;

public partial class PeoplePage : Page
{
    public PeoplePage()
    {
        InitializeComponent();
        DataContext = new PeopleViewModel();
    }
}
