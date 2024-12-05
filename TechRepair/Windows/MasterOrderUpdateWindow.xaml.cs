using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TechRepair.DbModel;

namespace TechRepair.Windows
{
    /// <summary>
    /// Interaction logic for MasterOrderUpdateWindow.xaml
    /// </summary>
    public partial class MasterOrderUpdateWindow : Window
    {
        private ClientsOrder currentOrder;

        public MasterOrderUpdateWindow(ClientsOrder order)
        {
            InitializeComponent();
            currentOrder = order;
            LoadStatuses();
        }

        private void LoadStatuses()
        {
            var statuses = App.Db.OrderStatusType.ToList();
            StatusComboBox.ItemsSource = statuses;
            StatusComboBox.DisplayMemberPath = "StatusName";
            StatusComboBox.SelectedItem = statuses.FirstOrDefault(s => s.Id == currentOrder.OrderStatusId);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (StatusComboBox.SelectedItem == null) return;

            var selectedStatus = StatusComboBox.SelectedItem as OrderStatusType;
            currentOrder.OrderStatusId = selectedStatus.Id;
            App.Db.SaveChanges();

            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
