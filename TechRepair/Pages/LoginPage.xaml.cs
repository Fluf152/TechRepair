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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TechRepair.DbModel;

namespace TechRepair.Pages
{
    public partial class LoginPage : Page
    {

        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var user = App.Db.SystemUser.FirstOrDefault(u =>
                u.UserLogin == LoginTextBox.Text &&
                u.UserPassword == PasswordBox.Password);

            if (user == null)
            {
                MessageBox.Show("Неверный логин или пароль");
                return;
            }

            Page nextPage = null;
            switch (user.UserRoleId)
            {
                case 1: // Менеджер
                    nextPage = new ManagerPage(user);
                    break;
                case 2: // Техник
                    nextPage = new MasterPage(user);
                    break;
                case 4: // Заказчик
                    nextPage = new ClientPage(user);
                    break;
            }

            if (nextPage != null)
            {
                MainWindow.Instance.Navigate(nextPage);
            }
        }
    }
}
