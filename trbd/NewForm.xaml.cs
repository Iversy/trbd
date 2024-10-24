using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace trbd
{
    /// <summary>
    /// Логика взаимодействия для NewForm.xaml
    /// </summary>
    public partial class NewForm : Window
    {
        DataTable table;
        DataRow row;


        App app = (App)Application.Current;


        public NewForm()
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
            this.row = table.NewRow();
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
                app.ShowError("Ошибка данных", "Введены неверные данные", e);
            }
            catch (System.Data.InvalidConstraintException e)
            {
                app.ShowError("Ошибка данных", "Введены неверные данные", e);
            }
        }

    }
}
