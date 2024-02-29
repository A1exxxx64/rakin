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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Ракин_Курсовая
{
    /// <summary>
    /// Логика взаимодействия для AdminArends.xaml
    /// </summary>
    public partial class AdminArends : Window
    {
        Entities entities = new Entities();
        public AdminArends()
        {
            InitializeComponent();
            foreach(var arend in entities.Arends)
            {
                ArendsList.Items.Add(arend);
            }
            foreach(var client in entities.Clients)
            {
                cbClient.Items.Add(client);
            }
            foreach(var device in entities.Devises)
            {
                cbDevise.Items.Add(device);
            }
            UpdateItemCount();
        }

        private void ArendsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var arenda = ArendsList.SelectedItem as Arends;

            if (arenda != null)
            {
                tbProfit.Text = arenda.Profit.ToString();

                cbClient.SelectedItem = (from Client in entities.Clients
                                           where Client.Id == arenda.Id_client
                                           select Client).Single<Clients>();

                cbDevise.SelectedItem = (from devise in entities.Devises
                                            where devise.Id == arenda.ID_devise
                                            select devise).Single<Devises>();

                dpStart.Text = arenda.Date_start.ToString();
                dpEnd.Text = arenda.Date_end.ToString();
                UpdateItemCount();
            }
            else
            {
                ClearPole();
                UpdateItemCount();
            }
        }
        
        private void bSave_Click(object sender, RoutedEventArgs e)
        {
            var x = ArendsList.SelectedItem as Arends;
            if (dpEnd.SelectedDate < dpStart.SelectedDate || tbProfit.Text == ""  ||
                cbClient.SelectedIndex == -1 ||
                cbDevise.SelectedIndex == -1 || dpEnd.SelectedDate == null || dpStart.SelectedDate==null)
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
                if (x == null)
                {
                    x = new Arends();
                    entities.Arends.Add(x);
                    ArendsList.Items.Add(x);
                }
                x.Profit = chislo1;



                x.Id_client = (cbClient.SelectedItem as Clients).Id;
                x.ID_devise = (cbDevise.SelectedItem as Devises).Id;
                x.Date_start = dpStart.SelectedDate.Value;
                x.Date_end = dpEnd.SelectedDate.Value;
                entities.SaveChanges();
                ArendsList.Items.Refresh();
                UpdateItemCount();
                MessageBox.Show("Сохранение успешно!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void bDelete_Click(object sender, RoutedEventArgs e)
        {
            var vikl = ArendsList.SelectedItem as Arends;
            if (vikl != null)
            {
                var ques = MessageBox.Show("Вы действительно желаете удалить запись?", "Quess", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (ques == MessageBoxResult.Yes)
                {
                    try
                    {
                        var exist_2 = (from structure in entities.Pays where structure.ID_arend == vikl.Id select structure).First();
                        MessageBox.Show("Запись нельзя удалить!",
                                    "Удаление",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                    }
                    catch
                    {
                        entities.Arends.Remove(vikl);
                        entities.SaveChanges();

                        ArendsList.Items.Remove(vikl);
                        ArendsList.Items.Refresh();

                        ClearPole();
                        UpdateItemCount();
                        MessageBox.Show("Запись удалена",
                                        "Удаление",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Information);
                    }

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
        private void bBack_Click(object sender, RoutedEventArgs e)
        {
            var window = new MenuAdmin();
            Close();
            window.Show();
        }
        private void UpdateItemCount()
        {
            int count = ArendsList.Items.Count;
            tbZapisi.Text = count.ToString();
        }
        private void ClearPole()
        {
            ArendsList.SelectedIndex = -1;
            tbProfit.Clear();
            cbDevise.SelectedIndex = -1;
            cbClient.SelectedIndex = -1;
            dpEnd.Text = null;
            dpStart.Text = null;
            UpdateItemCount();
        }
    }
}

