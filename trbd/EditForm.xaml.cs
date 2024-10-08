using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
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

namespace trbd
{
    /// <summary>
    /// Interaction logic for EditForm.xaml
    /// </summary>
    public partial class EditForm : Window
    {
        DataTable table;
        int n_row;
        DataRow row;

        App app = (App)Application.Current;

        public EditForm()
        {
            InitializeComponent();
        }

        private void load_data()
        {
            int i = 0;
            foreach (var input in Grid.Children.OfType<TextBox>())
                input.Text = row[i++].ToString();
        }

        private void save_data()
        {
            int i = 0;
            foreach (var input in Grid.Children.OfType<TextBox>())
                row[i++] = input.Text;
        }

        public bool? ShowDialog(DataTable table, int n_row)
        {
            this.table = table;
            this.n_row = n_row;
            this.row = table.Rows[n_row];
            load_data();

            this.ShowDialog();
            return false;
        }

        private void Button_Click(object sender, RoutedEventArgs _)
        {
            try
            {
                save_data();
                this.Close();
            }
            catch (System.Data.ConstraintException e)
            {
                app.on_load_error("Ошибка данных", "Введены неверные данные", e);
            }
            catch (System.Data.InvalidConstraintException e)
            {
                app.on_load_error("Ошибка данных", "Введены неверные данные", e);
            }
        }
    }
}
