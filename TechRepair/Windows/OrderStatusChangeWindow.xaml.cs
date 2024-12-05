using System.Linq;
using System.Windows;
using TechRepair.DbModel;

namespace TechRepair.Windows
{
    public partial class OrderStatusChangeWindow : Window
    {
        private ClientsOrder currentOrder;

        public OrderStatusChangeWindow(ClientsOrder order)
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
            if (StatusComboBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите статус");
                return;
            }

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
