using System;
using System.Linq;
using System.Windows;
using TechRepair.DbModel;

namespace TechRepair.Windows
{
    public partial class ManagerOrderAddWindow : Window
    {
        public ManagerOrderAddWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            ClientCombo.ItemsSource = App.Db.SystemUser
                .Where(u => u.UserRoleId == 1) // Предполагаем, что RoleId=1 для клиентов
                .ToList();
            ClientCombo.DisplayMemberPath = "FullName";

            TechTypeCombo.ItemsSource = App.Db.Tech.ToList();
            TechTypeCombo.DisplayMemberPath = "TechName";

            MasterCombo.ItemsSource = App.Db.SystemUser
                .Where(u => u.UserRoleId == 2) // Предполагаем, что RoleId=2 для мастеров
                .ToList();
            MasterCombo.DisplayMemberPath = "FullName";

            StatusCombo.ItemsSource = App.Db.OrderStatusType.ToList();
            StatusCombo.DisplayMemberPath = "StatusName";
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
                var order = new ClientsOrder
                {
                    StartDate = DateTime.Now,
                    ClientId = (ClientCombo.SelectedItem as SystemUser).Id,
                    ModelId = (ModelCombo.SelectedItem as TechModel).Id,
                    MasterId = MasterCombo.SelectedItem != null ?
                              (MasterCombo.SelectedItem as SystemUser).Id :
                              (int?)null,
                    OrderStatusId = (StatusCombo.SelectedItem as OrderStatusType).Id,
                    ProblemDescription = ProblemBox.Text
                };

                App.Db.ClientsOrder.Add(order);
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
