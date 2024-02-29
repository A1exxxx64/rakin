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
    /// Логика взаимодействия для UserClients.xaml
    /// </summary>
    public partial class UserClients : Window
    {
        Entities entities = new Entities();
        public UserClients()
        {
            InitializeComponent();
            foreach (var client in entities.Clients)
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
        private void bBack_Click(object sender, RoutedEventArgs e)
        {
            var window = new UserMenu();
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

       
        

        private void bClear_Click(object sender, RoutedEventArgs e)
        {
            ClearPole();
        }

        private void tbSurname_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            var rez = entities.Clients.ToList();
            rez = rez.Where(p => p.Client_Surname.ToLower()
            .Contains(tbSurname_Search
            .Text.ToLower())).ToList();
            ClientList.Items.Clear();
            foreach (var client in rez)
                ClientList.Items.Add(client);
            UpdateItemCount();
        }
        private void tbName_Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            var rez = entities.Clients.ToList();
            rez = rez.Where(p => p.Client_Name.ToLower().Contains(tbName_Search.Text.ToLower())).ToList();
            ClientList.Items.Clear();
            foreach (var client in rez)
                ClientList.Items.Add(client);
            UpdateItemCount();
        }
    }
}
