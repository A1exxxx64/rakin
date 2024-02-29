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
    /// Логика взаимодействия для MenuAdmin.xaml
    /// </summary>
    public partial class MenuAdmin : Window
    {
        public MenuAdmin()
        {
            InitializeComponent();
        }

        private void bUsers_Click(object sender, RoutedEventArgs e)
        {
            var window = new AdminUsers();
            Close();
            window.Show();
        }

        private void bArends_Click(object sender, RoutedEventArgs e)
        {
            var window = new AdminArends();
            Close();
            window.Show();
        }

        private void bClients_Click(object sender, RoutedEventArgs e)
        {
            var window = new AdminClients();
            Close();
            window.Show();
        }

        private void bDevises_Click(object sender, RoutedEventArgs e)
        {
            var window = new AdminDevises();
            Close();
            window.Show();
        }

        private void bPays_Click(object sender, RoutedEventArgs e)
        {
            var window = new AdminPays();
            Close();
            window.Show();
        }

        private void bBack_Click(object sender, RoutedEventArgs e)
        {
            var window = new MainWindow();
            Close();
            window.Show();
        }
    }
}
