using System.Windows.Controls;
using clothing_manufacture.ViewModels;

namespace clothing_manufacture.Views;

public partial class CustomerOrdersPage : Page
{
    public CustomerOrdersPage()
    {
        InitializeComponent();
        DataContext = new CustomerOrdersViewModel();
    }
}
