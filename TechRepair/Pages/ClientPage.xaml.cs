using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TechRepair.DbModel;
using TechRepair.Windows;

namespace TechRepair.Pages
{
    public partial class ClientPage : Page
    {
        private SystemUser currentClient;

        public ClientPage(SystemUser client)
        {
            InitializeComponent();
            currentClient = client;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
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
                .Where(o => o.ClientId == currentClient.Id)
                .OrderByDescending(o => o.StartDate)
                .ToList();

            // Apply search filter if exists
            if (!string.IsNullOrEmpty(SearchBox.Text))
            {
                int.TryParse(SearchBox.Text, out int searchId);
                orders = orders.Where(o => o.Id == searchId).ToList();
            }

            // Apply tech type filter if selected
            if (TechTypeFilter.SelectedItem != null)
            {
                var selectedTech = TechTypeFilter.SelectedItem as Tech;
                orders = orders.Where(o => o.TechModel.TechId == selectedTech.Id).ToList();
            }

            OrdersGrid.ItemsSource = orders;
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshOrders();
        }

        private void TechTypeFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshOrders();
        }

        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new ClientOrderAddWindow(currentClient);
            addWindow.ShowDialog();
            RefreshOrders();
        }

        private void EditProblem_Click(object sender, RoutedEventArgs e)
        {
            var order = (sender as Button).DataContext as ClientsOrder;
        }

        private void ViewComments_Click(object sender, RoutedEventArgs e)
        {
            var order = (sender as Button).DataContext as ClientsOrder;
            NavigationService.Navigate(new ClientOrderCommentPage(order));
        }
    }
}
