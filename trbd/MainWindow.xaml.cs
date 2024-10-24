using Microsoft.Win32;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace trbd
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        App app = (App)Application.Current;
        public MainWindow()
        {
            InitializeComponent();

            MaterialGrid.ItemsSource = app.stupid_data.Tables["Material"]?.DefaultView;
            UsageGrid.ItemsSource = app.stupid_data.Tables["Usage"]?.DefaultView;   
        }

        private void On_Save_Button_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "All Files (*.*)|*.*";
            bool? result = saveFileDialog.ShowDialog();
            if (result != true)
                return;

            string filePath = saveFileDialog.FileName;
            if (filePath == String.Empty)
            {
                filePath = "./stupid_data.xml";
            }
            app.stupid_data.WriteXml(filePath);
            MessageBox.Show("Файл сохранен");
        }

        private void On_Load_Button_Click(object sender, RoutedEventArgs e)
        {
            app.LoadData(txtFilePath.Text);
        }

        private ListView get_selected_grid()
        {
            switch (Nikita.SelectedIndex)
            {
                case 0:
                    return MaterialGrid;
                default:
                    return UsageGrid;
            }
        }

        private List<string> get_captions()
        {
            var tab = Nikita.SelectedIndex;
            var table = app.stupid_data.Tables[tab];

            List<string> cols = new List<string>();
            foreach (DataColumn col in table.Columns)
            {
                cols.Add(col.Caption);
            }
            return cols;
        }

        private void On_New_Button_Click(object sender, RoutedEventArgs e)
        {
            var tab = Nikita.SelectedIndex;
            var table = app.stupid_data.Tables[tab];

            var form = new EditForm(get_captions(), tab);
            form.ShowDialog(table);
        }
        private void On_Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            var tab = Nikita.SelectedIndex;
            var table = app.stupid_data.Tables[tab];
            
            ListView grid = get_selected_grid();
            int n_row = grid.SelectedIndex;
            if (n_row == -1)
            {
                n_row += table.Rows.Count;
            }
            var form = new EditForm(get_captions(), tab);
            if (n_row == -1)
            {
                form.ShowDialog(table);
            } else
            {
                form.ShowDialog(table, n_row);
            }
        }
        private void On_Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            if (!app.ShowAsk("Подтвердите удаление", "Вы уверены что хотите удалить данные? Это действие будет невозможно отменить! Ваши данные исчезнут насовем и навегда!"))
                return;
            var tab = Nikita.SelectedIndex;
            var table = app.stupid_data.Tables[tab];
            var grid = get_selected_grid();
            int n_row = grid.SelectedIndex;
            if (table.Rows.Count == 0)
            {
                return;
            }
            if (n_row == -1)
            {
                n_row += table.Rows.Count;
            }
            table.Rows[n_row].Delete();
        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All Files (*.*)|*.*"; 

            if (openFileDialog.ShowDialog() == true)
            {
                txtFilePath.Text = openFileDialog.FileName; 
            }
        }
    }
}