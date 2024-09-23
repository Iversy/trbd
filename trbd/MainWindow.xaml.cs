using System;
using System.IO;
using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        App app = (App)Application.Current;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void On_Save_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                app.stupid_data.WriteXml("./stupid_data.xml");
            } 
            catch (NullReferenceException exception)
            {
                MessageBox.Show("Отсутствуют данные для сохранения, загрузите данные, чтобы начать изменения.\n" + exception.Message, "Нет данных.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void On_Load_Button_Click(object sender, RoutedEventArgs e)
        {
            app.load_data();
            MaterialGrid.ItemsSource = app.stupid_data.Tables["Material"].DefaultView;
            MaterialGrid.Columns[4].Visibility = Visibility.Collapsed;
            UsageGrid.ItemsSource = app.stupid_data.Tables["Usage"].DefaultView;
        }
    }
}