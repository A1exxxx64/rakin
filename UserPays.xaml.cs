using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
using DataGrid = System.Windows.Controls.DataGrid;

namespace Ракин_Курсовая
{
    /// <summary>
    /// Логика взаимодействия для UserPays.xaml
    /// </summary>
    public partial class UserPays : System.Windows.Window
    {
        Entities entities = new Entities();
        public UserPays()
        {
            InitializeComponent();
            var query = from q in entities.Pays
                        join w in entities.Arends on q.ID_arend equals w.Id
                        select new
                        {
                            Id = "№"+q.Id,
                            Arends = q.Arends.Date_start,
                            Date_Pay = q.Date_Pay,
                            Summ = q.Summ

                        };

            var result = query.ToList();

            if (query.Count() == 0)
            {
                MessageBox.Show("Ошибка компиляции");
                return;
            }
            foreach (var item in query)
            {
                dgPays.Items.Add(item);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var window = new UserMenu();
            Close();
            window.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = workbook.Sheets[1];
            app.Visible = true;
            worksheet = workbook.ActiveSheet;
            for (int i = 1; i < dgPays.Columns.Count + 1; i++)
            {
                worksheet.Cells[1, i] = dgPays.Columns[i - 1].Header;
            }
            for (int i = 0; i < dgPays.Items.Count; i++)
            {
                for (int j = 0; j < dgPays.Columns.Count; j++)
                {
                    var value = (dgPays.Columns[j].GetCellContent(dgPays.Items[i]) as TextBlock)?.Text;
                    worksheet.Cells[i + 2, j + 1] = value != null ? value : "";
                }
            }

        }
        
    }
}
