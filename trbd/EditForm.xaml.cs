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
        int tab;

        App app = (App)Application.Current;

        public EditForm(List<string> cols, int tab)
        {
            this.tab = tab;
            if (tab == 0)
            {
                this.Title = "Материалы";
            } else if (tab == 1)
            {
                this.Title = "Использование";
            }
            InitializeComponent();
            first.Content = cols[0];
            second.Content = cols[1];
            third.Content = cols[2];
            fourth.Content = cols[3];
        }

        private List<string> ref_names()
        {
            List<string> oleg = new();
            foreach (DataRow row in app.stupid_data.Tables[0].Rows)
            {
                oleg.Add($"{row[0]}, {row[1]}");
            }
            return oleg;
        }

        private int get_index_by_id(int id)
        {
            foreach (string vova in ref_names())
                if (vova.Split(",")[0] == id.ToString())
                    return ref_names().IndexOf(vova);
            return -1;
        }

        private void load_data()
        {
            id_box.Text = row[0].ToString();
            units.Text = row[2].ToString();
            if (this.tab == 1)
            {
                name.Visibility = Visibility.Hidden;
                ref_combo.Visibility = Visibility.Visible;
                ref_combo.ItemsSource = ref_names();
                ref_combo.SelectedIndex = row[1] is DBNull ? -1 : get_index_by_id((int)row[1]);

                datepicker.SelectedDate = row[3] is DBNull ? DateTime.Today : (DateTime)row[3];
                datepicker.Visibility = Visibility.Visible;
                hst.Visibility = Visibility.Hidden;
            }
            else
            {
                name.Visibility = Visibility.Visible;
                ref_combo.Visibility = Visibility.Hidden;
                name.Text = row[1].ToString();

                hst.Text = row[3].ToString();
                datepicker.Visibility = Visibility.Hidden;
                hst.Visibility = Visibility.Visible;
            }
        }
        private void save_data()
        {
            if (tab == 1) 
            {
                DateTime? dateValue = datepicker.SelectedDate;
                if (dateValue > DateTime.Today)
                {
                    MessageBox.Show("Дата не может быть позже сегодняшнего дня.", "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Error);
                    throw new Exception(); 
                }
            }
            else if (tab == 0) 
            {
                if (decimal.TryParse(hst.Text, out decimal value))
                {
                    //System.Diagnostics.Debug.Write(value.ToString());
                    if (value < 0)
                    {
                        MessageBox.Show("Значение должно быть больше нуля.", "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Error);
                        throw new Exception(); 
                    }
                }
                else
                {
                    //System.Diagnostics.Debug.Write(value.ToString());
                    //System.Diagnostics.Debug.Write(hst.Text);
                    MessageBox.Show("Некорректный формат значения.", "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Error);
                    throw new Exception();
                }
            }

            if (tab == 1)
            {
                row[1] = ref_combo.SelectedItem.ToString().Split(",")[0];
                //row[1] = ref_combo.SelectedIndex+1;
                row[3] = datepicker.Text;
            }
            else
            {
                row[1] = name.Text;
                row[3] = hst.Text;
            }
            row[2] = units.Text;
            try
            {
                table.Rows.Add(row);
            }
            catch (System.ArgumentException e)
            {
                System.Diagnostics.Debug.WriteLine("It's okay");
            }
        }
        public bool ShowDialog(DataTable table)
        {
            this.table = table;
            this.row = table.NewRow();
            this.n_row = table.Rows.Count;
            //table.Rows.Add(row);
            load_data();

            this.ShowDialog();
            return true;
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
                app.ShowError("Ошибка данных", "Введены неверные данные", e);
            }
            catch (System.Data.InvalidConstraintException e)
            {
                app.ShowError("Ошибка данных", "Введены неверные данные", e);
            }
            catch (System.ArgumentException e)
            {
                app.ShowError("Ошибка данных", "Введен неправильный тип данных или пустое значение", e);
            }
            catch (Exception e)
            {
            }
        }
        private void Button_Click_Cancel(object sender, RoutedEventArgs _)
        {
            this.Close();
        }
    }
}
