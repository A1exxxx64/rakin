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
    /// Логика взаимодействия для UserArends.xaml
    /// </summary>
    public partial class UserArends : Window
    {
        Entities entities = new Entities(); 
        public UserArends()
        {
            InitializeComponent();
            foreach (var client in entities.Clients)
            {
                cbClient.Items.Add(client);
            }
            foreach (var device in entities.Devises)
            {
                cbDevise.Items.Add(device);
            }
           
        }

        private void bSave_Click(object sender, RoutedEventArgs e)
        {
           
            if (dpEnd.SelectedDate < dpStart.SelectedDate || tbProfit.Text == "" || cbClient.SelectedIndex == -1 || cbDevise.SelectedIndex == -1 || dpEnd.SelectedDate == null || dpStart.SelectedDate == null)
            {
                MessageBox.Show("Заполните поля и проверьте дату начала и конца аренды!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                int chislo1 = 0;

                try
                {
                    chislo1 = int.Parse(tbProfit.Text);

                }
                catch (FormatException ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                
                   var x = new Arends();
                    entities.Arends.Add(x);
  
                
                x.Profit = chislo1;



                x.Id_client = (cbClient.SelectedItem as Clients).Id;
                x.ID_devise = (cbDevise.SelectedItem as Devises).Id;
                x.Date_start = dpStart.SelectedDate.Value;
                x.Date_end = dpEnd.SelectedDate.Value;
                entities.SaveChanges();

                MessageBox.Show("Сохранение успешно!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }
        
        private void ClearPole()
        {
           
            tbProfit.Clear();
            cbDevise.SelectedIndex = -1;
            cbClient.SelectedIndex = -1;
            dpEnd.Text = null;
            dpStart.Text = null;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var vindow = new UserMenu();
            Close();
            vindow.Show();

        }
    }
}
