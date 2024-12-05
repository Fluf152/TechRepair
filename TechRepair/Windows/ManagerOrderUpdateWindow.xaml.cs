using System.Linq;
using System.Windows;
using TechRepair.DbModel;

namespace TechRepair.Windows
{
    public partial class ManagerOrderUpdateWindow : Window
    {
        private ClientsOrder currentOrder;

        public ManagerOrderUpdateWindow(ClientsOrder order)
        {
            InitializeComponent();
            currentOrder = order;
            LoadData();
            SetCurrentValues();
        }

        private void LoadData()
        {
            ClientCombo.ItemsSource = App.Db.SystemUser
                .Where(u => u.UserRoleId == 1)
                .ToList();
            ClientCombo.DisplayMemberPath = "FullName";

            TechTypeCombo.ItemsSource = App.Db.Tech.ToList();
            TechTypeCombo.DisplayMemberPath = "TechName";

            MasterCombo.ItemsSource = App.Db.SystemUser
                .Where(u => u.UserRoleId == 2)
                .ToList();
            MasterCombo.DisplayMemberPath = "FullName";

            StatusCombo.ItemsSource = App.Db.OrderStatusType.ToList();
            StatusCombo.DisplayMemberPath = "StatusName";
        }

        private void SetCurrentValues()
        {
            ClientCombo.SelectedItem = ClientCombo.ItemsSource
                .Cast<SystemUser>()
                .FirstOrDefault(c => c.Id == currentOrder.ClientId);

            var techType = App.Db.TechModel
                .Where(m => m.Id == currentOrder.ModelId)
                .Select(m => m.Tech)
                .FirstOrDefault();

            TechTypeCombo.SelectedItem = techType;

            ModelCombo.ItemsSource = App.Db.TechModel
                .Where(m => m.TechId == techType.Id)
                .ToList();
            ModelCombo.DisplayMemberPath = "TechModelName";
            ModelCombo.SelectedItem = ModelCombo.ItemsSource
                .Cast<TechModel>()
                .FirstOrDefault(m => m.Id == currentOrder.ModelId);

            MasterCombo.SelectedItem = MasterCombo.ItemsSource
                .Cast<SystemUser>()
                .FirstOrDefault(m => m.Id == currentOrder.MasterId);

            StatusCombo.SelectedItem = StatusCombo.ItemsSource
                .Cast<OrderStatusType>()
                .FirstOrDefault(s => s.Id == currentOrder.OrderStatusId);

            ProblemBox.Text = currentOrder.ProblemDescription;
        }

        private void TechTypeCombo_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (TechTypeCombo.SelectedItem is Tech selectedTech)
            {
                ModelCombo.ItemsSource = App.Db.TechModel
                    .Where(m => m.TechId == selectedTech.Id)
                    .ToList();
                ModelCombo.DisplayMemberPath = "TechModelName";
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                currentOrder.ClientId = (ClientCombo.SelectedItem as SystemUser).Id;
                currentOrder.ModelId = (ModelCombo.SelectedItem as TechModel).Id;
                currentOrder.MasterId = MasterCombo.SelectedItem != null ?
                                      (MasterCombo.SelectedItem as SystemUser).Id :
                                      (int?)null;
                currentOrder.OrderStatusId = (StatusCombo.SelectedItem as OrderStatusType).Id;
                currentOrder.ProblemDescription = ProblemBox.Text;

                App.Db.SaveChanges();
                DialogResult = true;
            }
        }

        private bool ValidateInput()
        {
            if (ClientCombo.SelectedItem == null ||
                TechTypeCombo.SelectedItem == null ||
                ModelCombo.SelectedItem == null ||
                StatusCombo.SelectedItem == null ||
                string.IsNullOrWhiteSpace(ProblemBox.Text))
            {
                MessageBox.Show("Заполните все обязательные поля");
                return false;
            }
            return true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
