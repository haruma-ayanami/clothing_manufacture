using System.Windows.Controls;
using clothing_manufacture.ViewModels;

namespace clothing_manufacture.Views;

public partial class ClientsPage : Page
{
    public ClientsPage()
    {
        InitializeComponent();
        DataContext = new ClientsViewModel();
    }
}
