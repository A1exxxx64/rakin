using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
    /// Логика взаимодействия для UserDevises.xaml
    /// </summary>
    public partial class UserDevises : Window
    {
        Entities entities = new Entities();
        string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
        string text;
        public UserDevises()
        {
            InitializeComponent();
            foreach (var devise in entities.Devises)
            {
                DeviseList.Items.Add(devise);
            }
            cbSort.Items.Add("По возрастанию");
            cbSort.Items.Add("По убыванию"); 
        }

        private void DeviseList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var devise = DeviseList.SelectedItem as Devises;
            if (devise != null)
            {
                BitmapImage myBitmapImage = new BitmapImage();
                myBitmapImage.BeginInit();
                myBitmapImage.UriSource = new Uri(projectDirectory + "\\image\\" + devise.Photo);

                myBitmapImage.DecodePixelWidth = 200;
                myBitmapImage.EndInit();

                imageBox.Source = myBitmapImage;

                imageBox.Source = myBitmapImage;

                tbName.Text = devise.Devise_Name;
                tbInfo.Text = devise.Devise_info;
                tbquantity.Text = devise.Available_quantity.ToString();
            }
            else 
            { 
                imageBox.Source= null;
                tbquantity.Clear();
                tbInfo.Clear();
                tbName.Clear();
            }
        }

        private void bClear_Click(object sender, RoutedEventArgs e)
        {
            imageBox.Source = null;
            tbquantity.Clear();
            tbInfo.Clear();
            tbName.Clear();
            DeviseList.SelectedIndex = -1;

        }

        private void bBack_Click(object sender, RoutedEventArgs e)
        {
            var vindow = new UserMenu();
            Close();
                vindow.Show();  
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cbSort.SelectedIndex)
            {
                case 0:
                    DeviseList.Items.Clear();
                    var zapr = entities.Devises.OrderBy(u => u.Available_quantity);
                    foreach(var devise in zapr)
                    {
                        DeviseList.Items.Add(devise);
                    }
                    DeviseList.SelectedIndex = 0;
                    break;
                case 1:
                    DeviseList.Items.Clear();
                    var zapr1 = entities.Devises.OrderByDescending(u => u.Available_quantity);
                    foreach (var devise in zapr1)
                    {
                        DeviseList.Items.Add(devise);
                    }
                    DeviseList.SelectedIndex = 0;
                    break;

            }
        }
    }
}
