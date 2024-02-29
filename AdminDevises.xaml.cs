using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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
    /// Логика взаимодействия для AdminDevises.xaml
    /// </summary>
    public partial class AdminDevises : Window
    {
        Entities entities = new Entities();
        string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
        string text;
        public AdminDevises()
        {
            InitializeComponent();
            foreach (var devise in entities.Devises)
            {
                DevisesList.Items.Add(devise);
                UpdateItemCount();
            }

        }

        private void bSave_Click(object sender, RoutedEventArgs e)
        {
            var x = DevisesList.SelectedItem as Devises;
            if (tbName.Text == "" || tbInfo.Text == "" || tbquantity.Text == null || tbPhoto.Text == "")
            {
                MessageBox.Show("Заполните поля!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                int chislo1 = 0;

                try
                {
                    chislo1 = int.Parse(tbquantity.Text);
                }
                catch (FormatException ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                if (x == null)
                {
                    x = new Devises();
                    entities.Devises.Add(x);
                    DevisesList.Items.Add(x);
                    UpdateItemCount();
                }

                x.Available_quantity = chislo1;
                x.Devise_Name = tbName.Text;
                x.Photo = tbPhoto.Text;
                x.Devise_info = tbInfo.Text;
                entities.SaveChanges();
                DevisesList.Items.Refresh();
                UpdateItemCount();
                MessageBox.Show("Сохранение успешно!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void DevisesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var devise = DevisesList.SelectedItem as Devises;

            if (devise != null)
            {
                BitmapImage myBitmapImage = new BitmapImage();
                myBitmapImage.BeginInit();
                myBitmapImage.UriSource = new Uri(projectDirectory + "\\image\\" + devise.Photo);

                myBitmapImage.DecodePixelWidth = 200;
                myBitmapImage.EndInit();

                imageBox.Source = myBitmapImage;

                imageBox.Source = myBitmapImage;

                tbquantity.Text = devise.Available_quantity.ToString();
                tbName.Text = devise.Devise_Name;
                tbInfo.Text = devise.Devise_info;
                tbPhoto.Text = myBitmapImage.UriSource.ToString();
                UpdateItemCount();

            }
            else
            {
                tbquantity.Clear();
                tbPhoto.Clear();
                tbName.Clear();
                tbInfo.Clear();
                

                imageBox.Source = null;
                UpdateItemCount();
            }

        }

        private void bPhoto_Click(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.InitialDirectory = projectDirectory + "\\image\\";
            if (dlg.ShowDialog() == true && !string.IsNullOrWhiteSpace(dlg.FileName))
                tbPhoto.Text = dlg.SafeFileName.ToString();
        }

        private void bDelete_Click(object sender, RoutedEventArgs e)
        {
            var vikl = DevisesList.SelectedItem as Devises;
            if (vikl != null)
            {
                var ques = MessageBox.Show("Вы действительно желаете удалить запись?", "Quess", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (ques == MessageBoxResult.Yes)
                {
                    try
                    {
                        var exist_ = (from structure in entities.Arends where structure.ID_devise == vikl.Id select structure).First();
                        MessageBox.Show("Запись нельзя удалить!",
                                    "Удаление",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                    }
                    catch
                    {
                        entities.Devises.Remove(vikl);
                        entities.SaveChanges();

                        DevisesList.Items.Remove(vikl);
                        DevisesList.Items.Refresh();


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

        

        private void bClear_Click(object sender, RoutedEventArgs e)
        {
            ClearPole();
            UpdateItemCount();
        }
        private void bBack_Click(object sender, RoutedEventArgs e)
        {
            var window=new MenuAdmin();
            Close();
            window.Show();
        }
        private void UpdateItemCount()
        {
            int count = DevisesList.Items.Count;
            tbZapisi.Text = count.ToString();
        }
        private void ClearPole()
        {
            DevisesList.SelectedIndex = -1;
            tbName.Clear();
            tbInfo.Clear();
            tbPhoto.Clear();
            tbquantity.Clear();
            UpdateItemCount();
            tbPhoto.Clear();
            imageBox.Source = null;
        }

        
    }
}
