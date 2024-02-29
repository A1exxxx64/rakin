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
    /// Логика взаимодействия для Regist.xaml
    /// </summary>
    public partial class Regist : Window
    {
        Entities entities = new Entities();
        public Regist()
        {
            InitializeComponent();
        }
        private void bCreate_Click(object sender, RoutedEventArgs e)
        {
            if (login.Text == "" || password.Text == "")
            {
                MessageBox.Show("Заполните поля!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {

                if (login.Text != "" && password.Text != "")
                {
                    var x = new Users();
                    entities.Users.Add(x);


                    x.login = login.Text;
                    x.password = password.Text;
                    x.role = "user";

                    entities.SaveChanges(); ;
                    MessageBox.Show("Сохранение успешно!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
        }

        private void bBack_Click(object sender, RoutedEventArgs e)
        {
            var vindow = new MainWindow();
            Close();
            vindow.Show();
        }
    }
}
