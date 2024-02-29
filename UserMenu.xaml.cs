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

namespace Ракин_Курсовая
{
    /// <summary>
    /// Логика взаимодействия для UserMenu.xaml
    /// </summary>
    public partial class UserMenu : Window
    {
        public UserMenu()
        {
            InitializeComponent();
        }

       
        private void bDevises_Click(object sender, RoutedEventArgs e)
        {
            var window = new UserDevises();
            Close();
            window.Show();
        }

        private void bPays_Click(object sender, RoutedEventArgs e)
        {
            var window = new UserPays();
            Close();
            window.Show();
        }

        private void bBack_Click(object sender, RoutedEventArgs e)
        {
            var window = new MainWindow();
            Close();
            window.Show();
        }

        private void bArends_Click(object sender, RoutedEventArgs e)
        {
            var window = new UserArends();
            Close();
            window.Show();
        }

        private void bClients_Click(object sender, RoutedEventArgs e)
        {
            var window = new UserClients();
            Close();
            window.Show();
        }
    }
}
