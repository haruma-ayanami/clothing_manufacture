using System.Windows.Controls;
using clothing_manufacture.ViewModels;

namespace clothing_manufacture.Views;

public partial class EmployeesPage : Page
{
    public EmployeesPage()
    {
        InitializeComponent();
        DataContext = new EmployeesViewModel();
    }
}
