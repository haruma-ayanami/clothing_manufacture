using System.Windows.Controls;
using System.Windows.Data;
using clothing_manufacture.Services;
using clothing_manufacture.ViewModels;

namespace clothing_manufacture.Views;

public partial class GenericCrudPage : Page
{
    public GenericCrudPage(EntityDescriptor descriptor)
    {
        InitializeComponent();
        DataContext = new GenericCrudViewModel(descriptor);
        BuildColumns(descriptor);
    }

    private void BuildColumns(EntityDescriptor descriptor)
    {
        ItemsDataGrid.Columns.Clear();

        foreach (GridColumnDefinition columnDefinition in descriptor.Columns)
        {
            DataGridTextColumn column = new()
            {
                Header = columnDefinition.Header,
                Binding = new Binding(columnDefinition.BindingPath)
                {
                    TargetNullValue = string.Empty,
                    StringFormat = columnDefinition.BindingPath.Contains("Date") ? "{0:dd.MM.yyyy}" : null
                },
                Width = new DataGridLength(columnDefinition.Width, DataGridLengthUnitType.Pixel),
                MinWidth = columnDefinition.MinWidth
            };

            ItemsDataGrid.Columns.Add(column);
        }
    }
}
