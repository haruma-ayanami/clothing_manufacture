using System.Windows.Controls;
using clothing_manufacture.ViewModels;

namespace clothing_manufacture.Views;

public partial class OrderItemsPage : Page
{
    public OrderItemsPage()
    {
        InitializeComponent();
        DataContext = new OrderItemsViewModel();
    }
}
