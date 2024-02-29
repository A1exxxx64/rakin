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
    /// Логика взаимодействия для AdminPays.xaml
    /// </summary>
    public partial class AdminPays : Window
    {
        Entities entities = new Entities();
        public AdminPays()
        {
            InitializeComponent();
            foreach(var pay in entities.Pays)
            {
                PaysList.Items.Add(pay);
            }
            foreach(var date in entities.Arends)
            {
                cbArend.Items.Add(date);
            }
            UpdateItemCount();
        }

        private void PaysList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var pay = PaysList.SelectedItem as Pays;

            if (pay != null)
            {
                tbSumm.Text = pay.Summ.ToString();

                cbArend.SelectedItem = (from arend in entities.Arends
                                         where arend.Id == pay.ID_arend
                                         select arend).Single<Arends>();

                dpPayDate.Text = pay.Date_Pay.ToString();
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
            var x = PaysList.SelectedItem as Pays;
            if ( tbSumm.Text == "" || cbArend.SelectedIndex == -1 || dpPayDate.Text=="")
            {
                MessageBox.Show("Заполните поля и проверьте дату начала и конца аренды!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                int chislo1 = 0;

                try
                {
                    chislo1 = int.Parse(tbSumm.Text);

                }
                catch (FormatException ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                if (x == null)
                {
                    x = new Pays();
                    entities.Pays.Add(x);
                    PaysList.Items.Add(x);
                }
                x.Summ = chislo1;
                x.ID_arend = (cbArend.SelectedItem as Arends).Id;
                x.Date_Pay = dpPayDate.SelectedDate.Value;
                entities.SaveChanges();
                PaysList.Items.Refresh();
                UpdateItemCount();
                MessageBox.Show("Сохранение успешно!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        private void bDelete_Click(object sender, RoutedEventArgs e)
        {
            var vikl = PaysList.SelectedItem as Pays;
            if (vikl != null)
            {
                var ques = MessageBox.Show("Вы действительно желаете удалить запись?", "Quess", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (ques == MessageBoxResult.Yes)
                {
                    try
                    {
                        var exist_ = (from structure in entities.Arends where structure.Id == vikl.Id select structure).First();
                        MessageBox.Show("Запись нельзя удалить!",
                                    "Удаление",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                    }
                    catch
                    {
                        entities.Pays.Remove(vikl);
                        entities.SaveChanges();

                        PaysList.Items.Remove(vikl);
                        PaysList.Items.Refresh();

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
            int count = PaysList.Items.Count;
            tbZapisi.Text = count.ToString();
        }
        private void ClearPole()
        {
            PaysList.SelectedIndex = -1;
            tbSumm.Clear();
            cbArend.SelectedIndex = -1;
            dpPayDate.Text = null;
            UpdateItemCount();
        }

        
    }
}
