using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TechRepair.DbModel;
using TechRepair.Windows;

namespace TechRepair.Pages
{
    public partial class ManagerPage : Page
    {
        public ManagerPage()
        {
            InitializeComponent();
            LoadTechTypes();
            RefreshOrders();
        }

        private void LoadTechTypes()
        {
            var techTypes = App.Db.Tech.ToList();
            TechTypeFilter.ItemsSource = techTypes;
            TechTypeFilter.DisplayMemberPath = "TechName";
        }

        private void RefreshOrders()
        {
            var orders = App.Db.ClientsOrder
                .OrderByDescending(o => o.StartDate)
                .ToList();

            if (!string.IsNullOrEmpty(SearchBox.Text))
            {
                int.TryParse(SearchBox.Text, out int searchId);
                orders = orders.Where(o => o.Id == searchId).ToList();
            }

            if (TechTypeFilter.SelectedItem != null)
            {
                var selectedTech = TechTypeFilter.SelectedItem as Tech;
                orders = orders.Where(o => o.TechModel.TechId == selectedTech.Id).ToList();
            }

            OrdersGrid.ItemsSource = orders;
        }

        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new ManagerOrderAddWindow();
            if (addWindow.ShowDialog() == true)
            {
                RefreshOrders();
            }
        }

        private void UpdateOrder_Click(object sender, RoutedEventArgs e)
        {
            var order = (sender as Button).DataContext as ClientsOrder;
            var updateWindow = new ManagerOrderUpdateWindow(order);
            if (updateWindow.ShowDialog() == true)
            {
                RefreshOrders();
            }
        }

        private void DeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            var order = (sender as Button).DataContext as ClientsOrder;

            if (MessageBox.Show("Вы уверены, что хотите удалить этот заказ?",
                "Подтверждение удаления",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                App.Db.ClientsOrder.Remove(order);
                App.Db.SaveChanges();
                RefreshOrders();
            }
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshOrders();
        }

        private void TechTypeFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshOrders();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshOrders();
        }
    }
}
