using System.Windows;
using clothing_manufacture.Services;
using clothing_manufacture.Views;

namespace clothing_manufacture;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        MainContentFrame.Navigate(new DashboardPage());
    }

    private void OpenEntity(string key)
    {
        MainContentFrame.Navigate(new GenericCrudPage(EntityRegistry.Get(key)));
    }

    private void DashboardButton_Click(object sender, RoutedEventArgs e) => MainContentFrame.Navigate(new DashboardPage());
    private void PeopleButton_Click(object sender, RoutedEventArgs e) => OpenEntity("People");
    private void PositionsButton_Click(object sender, RoutedEventArgs e) => OpenEntity("Positions");
    private void EmployeesButton_Click(object sender, RoutedEventArgs e) => OpenEntity("Employees");
    private void ProductionProcessesButton_Click(object sender, RoutedEventArgs e) => OpenEntity("ProductionProcesses");
    private void ProcessStagesButton_Click(object sender, RoutedEventArgs e) => OpenEntity("ProcessStages");
    private void StagePositionsButton_Click(object sender, RoutedEventArgs e) => OpenEntity("StagePositions");
    private void ClassifiersButton_Click(object sender, RoutedEventArgs e) => OpenEntity("Classifiers");
    private void ClientsButton_Click(object sender, RoutedEventArgs e) => OpenEntity("Clients");
    private void MaterialsButton_Click(object sender, RoutedEventArgs e) => OpenEntity("Materials");
    private void AssortmentPlansButton_Click(object sender, RoutedEventArgs e) => OpenEntity("AssortmentPlans");
    private void CollectionsButton_Click(object sender, RoutedEventArgs e) => OpenEntity("ClothingCollections");
    private void ClothingModelsButton_Click(object sender, RoutedEventArgs e) => OpenEntity("ClothingModels");
    private void ModelVariantsButton_Click(object sender, RoutedEventArgs e) => OpenEntity("ModelVariants");
    private void ModelMaterialsButton_Click(object sender, RoutedEventArgs e) => OpenEntity("ModelMaterials");
    private void PatternsButton_Click(object sender, RoutedEventArgs e) => OpenEntity("Patterns");
    private void SamplesButton_Click(object sender, RoutedEventArgs e) => OpenEntity("Samples");
    private void CustomerOrdersButton_Click(object sender, RoutedEventArgs e) => OpenEntity("CustomerOrders");
    private void OrderItemsButton_Click(object sender, RoutedEventArgs e) => OpenEntity("OrderItems");
    private void ProductionTasksButton_Click(object sender, RoutedEventArgs e) => OpenEntity("ProductionTasks");
    private void ProductionBatchesButton_Click(object sender, RoutedEventArgs e) => OpenEntity("ProductionBatches");
    private void WarehousesButton_Click(object sender, RoutedEventArgs e) => OpenEntity("Warehouses");
    private void WarehouseCellsButton_Click(object sender, RoutedEventArgs e) => OpenEntity("WarehouseCells");
    private void WarehouseStocksButton_Click(object sender, RoutedEventArgs e) => OpenEntity("WarehouseStocks");
    private void QualityControlsButton_Click(object sender, RoutedEventArgs e) => OpenEntity("QualityControls");
    private void ProductOperationsButton_Click(object sender, RoutedEventArgs e) => OpenEntity("ProductOperations");
}
