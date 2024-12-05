using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TechRepair.DbModel;

namespace TechRepair.Pages
{
    public partial class MasterPage : Page
    {
        private SystemUser currentMaster;

        public MasterPage(SystemUser master)
        {
            InitializeComponent();
            currentMaster = master;
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
            var availableOrders = App.Db.ClientsOrder
                .Where(o => o.MasterId == null)
                .OrderByDescending(o => o.StartDate)
                .ToList();

            var myOrders = App.Db.ClientsOrder
                .Where(o => o.MasterId == currentMaster.Id)
                .OrderByDescending(o => o.StartDate)
                .ToList();

            // Применяем поиск по ID
            if (!string.IsNullOrEmpty(SearchBox.Text))
            {
                int.TryParse(SearchBox.Text, out int searchId);
                availableOrders = availableOrders.Where(o => o.Id == searchId).ToList();
                myOrders = myOrders.Where(o => o.Id == searchId).ToList();
            }

            // Применяем фильтр по типу техники
            if (TechTypeFilter.SelectedItem != null)
            {
                var selectedTech = TechTypeFilter.SelectedItem as Tech;
                availableOrders = availableOrders.Where(o => o.TechModel.TechId == selectedTech.Id).ToList();
                myOrders = myOrders.Where(o => o.TechModel.TechId == selectedTech.Id).ToList();
            }

            AvailableOrdersGrid.ItemsSource = availableOrders;
            MyOrdersGrid.ItemsSource = myOrders;
        }

        private void TakeOrder_Click(object sender, RoutedEventArgs e)
        {
            var order = (sender as Button).DataContext as ClientsOrder;
            order.MasterId = currentMaster.Id;
            App.Db.SaveChanges();
            RefreshOrders();
        }

        private void ChangeStatus_Click(object sender, RoutedEventArgs e)
        {
            var order = (sender as Button).DataContext as ClientsOrder;
            var statusWindow = new OrderStatusChangeWindow(order);
            if (statusWindow.ShowDialog() == true)
            {
                RefreshOrders();
            }
        }

        private void ViewComments_Click(object sender, RoutedEventArgs e)
        {
            var order = (sender as Button).DataContext as ClientsOrder;
            var commentWindow = new MasterCommentWindow(order);
            commentWindow.ShowDialog();
            RefreshOrders();
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
