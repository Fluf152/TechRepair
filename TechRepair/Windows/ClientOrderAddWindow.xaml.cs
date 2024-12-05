using System;
using System.Linq;
using System.Windows;
using TechRepair.DbModel;

namespace TechRepair.Windows
{
    public partial class ClientOrderAddWindow : Window
    {
        private SystemUser currentClient;

        public ClientOrderAddWindow(SystemUser client)
        {
            InitializeComponent();
            currentClient = client;
            LoadTechTypes();
        }

        private void LoadTechTypes()
        {
            TechTypeCombo.ItemsSource = App.Db.Tech.ToList();
            TechTypeCombo.DisplayMemberPath = "TechName";
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
            if (ModelCombo.SelectedItem == null || string.IsNullOrWhiteSpace(ProblemBox.Text))
            {
                MessageBox.Show("Please fill all fields");
                return;
            }

            var order = new ClientsOrder
            {
                StartDate = DateTime.Now,
                ModelId = (ModelCombo.SelectedItem as TechModel).Id,
                ProblemDescription = ProblemBox.Text,
                OrderStatusId = 1, // Assuming 1 is "New" status
                ClientId = currentClient.Id
            };

            App.Db.ClientsOrder.Add(order);
            App.Db.SaveChanges();

            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}

