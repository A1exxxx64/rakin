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
    /// Логика взаимодействия для AdminClients.xaml
    /// </summary>
    public partial class AdminClients : Window
    {
        Entities entities = new Entities();
        public AdminClients()
        {
            InitializeComponent();
            foreach(var client in entities.Clients)
            {
                ClientList.Items.Add(client);
                UpdateItemCount();
            }
        }

        private void ClientList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var client = ClientList.SelectedItem as Clients;

            if (client != null)
            {
                tbName.Text = client.Client_Name;
                tbSurname.Text = client.Client_Surname;
                tbContact.Text = client.Client_Contakt.ToString();

                UpdateItemCount();
            }
            else
            {
                ClearPole();
            }
        }
        private void bSave_Click(object sender, RoutedEventArgs e)
        {
            var x = ClientList.SelectedItem as Clients;
            if (tbName.Text == "" || tbSurname.Text == "" || tbContact.Text == null )
            {
                MessageBox.Show("Заполните поля!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                int chislo1 = 0;

                try
                {
                    chislo1 = int.Parse(tbContact.Text);
                }
                catch (FormatException ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                if (x == null)
                {
                    x = new Clients();
                    entities.Clients.Add(x);
                    ClientList.Items.Add(x);
                    UpdateItemCount();
                }

                x.Client_Contakt = chislo1;
                x.Client_Name = tbName.Text;
                x.Client_Surname = tbSurname.Text;
                
                entities.SaveChanges();
                ClientList.Items.Refresh();
                UpdateItemCount();
                MessageBox.Show("Сохранение успешно!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void bDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var vikl = ClientList.SelectedItem as Clients;
                if (vikl != null)
                {
                    var ques = MessageBox.Show("Вы действительно желаете удалить запись?", "Quess", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (ques == MessageBoxResult.Yes)
                    {
                        try
                        {
                            var exist_ = (from structure in entities.Arends where structure.Id_client == vikl.Id select structure).First();
                            MessageBox.Show("Запись нельзя удалить!",
                                        "Удаление",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Error);
                        }
                        catch
                        {

                            entities.Clients.Remove(vikl);
                            entities.SaveChanges();

                            ClientList.Items.Remove(vikl);
                            ClientList.Items.Refresh();


                            ClearPole();

                            MessageBox.Show("Запись удалена",
                                            "Удаление",
                                            MessageBoxButton.OK,
                                            MessageBoxImage.Information);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Выбирите удаляемую запись", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception )
            {
                MessageBox.Show("Нельзя удалить родительские данные в котором есть дочерние", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }

        private void bClear_Click(object sender, RoutedEventArgs e)
        {
            ClearPole();
            UpdateItemCount();
        }

        private void bBack_Click(object sender, RoutedEventArgs e)
        {
            var window = new MenuAdmin();
            Close();
            window.Show();
        }
        private void UpdateItemCount()
        {
            int count = ClientList.Items.Count;
            tbZapisi.Text = count.ToString();
        }
        private void ClearPole()
        {
            ClientList.SelectedIndex = -1;
            tbName.Clear();
            tbSurname.Clear();
            tbContact.Clear();
            
            UpdateItemCount();
        }
    }
}
