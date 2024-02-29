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

namespace Ракин_Курсовая
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Entities entities = new Entities();
        public MainWindow()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (TextBox_Login.Text == "" || PasswordBox_password.Password == "")
            {
                MessageBox.Show("Заполните все поля!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                bool flag = false;
                foreach (var login in entities.Users)
                {


                    if (TextBox_Login.Text == login.login && login.role == "admin")
                    {
                        if (PasswordBox_password.Password == login.password)
                        {
                            flag = true;
                            var i = MessageBox.Show("Добро пожаловать администратор", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);
                            var window_ = new MenuAdmin();
                            Close();
                            window_.ShowDialog();
                            break;
                        }
                    }
                    else if ((TextBox_Login.Text == login.login && login.role != "admin"))
                    {
                        if (PasswordBox_password.Password == login.password)
                        {
                            flag = true;
                            var i = MessageBox.Show("Добро пожаловать пользователь", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);
                            var window_ = new UserMenu();
                            Close();
                            window_.ShowDialog();
                            break;
                        }

                    }


                }
                if (!flag)
                {
                    MessageBox.Show("Неверный логин или пароль!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                }

            }
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            var window_ = new Regist();
            Close();
            window_.ShowDialog();
        }
    }
}

