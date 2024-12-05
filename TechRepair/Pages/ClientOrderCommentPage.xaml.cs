using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TechRepair.DbModel;

namespace TechRepair.Pages
{
    public partial class ClientOrderCommentPage : Page
    {
        private ClientsOrder currentOrder;

        public ClientOrderCommentPage(ClientsOrder order)
        {
            InitializeComponent();
            currentOrder = order;
            LoadComments();
        }


        private void LoadComments()
        {
            var comments = App.Db.OrderMasterComment
                .Where(c => c.OrderId == currentOrder.Id)
                .OrderByDescending(c => c.Id)
                .ToList();
            CommentsGrid.ItemsSource = comments;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}

