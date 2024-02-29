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
    /// Логика взаимодействия для AdminUsers.xaml
    /// </summary>
    public partial class AdminUsers : Window
    {
        Entities entities= new Entities();
        public AdminUsers()
        {
            InitializeComponent();
            foreach (var user in entities.Users)
            { UserList.Items.Add(user); }
            UpdateItemCount();
        }

        private void UserList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var user = UserList.SelectedItem as Users;

            if (user != null)
            {
                tbLogin.Text = user.login;
                tbPassword.Text = user.password;
                tbRole.Text = user.role;

                UpdateItemCount();
            }
            else
            {
                ClearPole();
                UpdateItemCount();
            }

        }
        private void bBack_Click(object sender, RoutedEventArgs e)
        {
            var window = new MenuAdmin();
            Close();
            window.Show();
        }
        private void UpdateItemCount()
        {
            int count = UserList.Items.Count;
            tbZapisi.Text = count.ToString();
        }
        private void ClearPole()
        {
            UserList.SelectedIndex = -1;
            tbRole.Clear();
            tbLogin.Clear();
            tbPassword.Clear();
            UpdateItemCount();
        }

        private void bSave_Click(object sender, RoutedEventArgs e)
        {
            var x = UserList.SelectedItem as Users;
            if (tbLogin.Text == "" || tbPassword.Text == "" || tbRole.Text == "")
            {
                MessageBox.Show("Заполните поля и проверьте дату начала и конца аренды!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (x == null)
                {
                    x = new Users();
                    entities.Users.Add(x);
                    UserList.Items.Add(x);
                }
                x.login = tbLogin.Text;
                x.password=tbPassword.Text;
                x.role = tbRole.Text;
                entities.SaveChanges();
                UserList.Items.Refresh();
                UpdateItemCount();
                MessageBox.Show("Сохранение успешно!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void bDelete_Click(object sender, RoutedEventArgs e)
        {
            var vikl = UserList.SelectedItem as Users;
            if (vikl != null)
            {
                var ques = MessageBox.Show("Вы действительно желаете удалить запись?", "Quess", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (ques == MessageBoxResult.Yes)
                {
                    entities.Users.Remove(vikl);
                    entities.SaveChanges();
                    UserList.Items.Remove(vikl);
                    UserList.Items.Refresh();

                    ClearPole();
                    UpdateItemCount();
                    MessageBox.Show("Запись удалена",
                                    "Удаление",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Выбирите удаляемую запись", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void bClear_Click(object sender, RoutedEventArgs e)
        {
            ClearPole();
            UpdateItemCount();
        }

        private void bBack_Click_1(object sender, RoutedEventArgs e)
        {
            var vindow = new MenuAdmin();
            Close();
            vindow.Show();
        }
    }
}
