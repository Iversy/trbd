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
        public StupidDataSet stupid_data;
        public MainWindow()
        {
            InitializeComponent();

            stupid_data = new StupidDataSet();
            try
            {
                stupid_data.ReadXml("./stupid_data.xml");
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Я не могу прочитать дазу банных, фала нет.", "Нет файла");
            }
            phonesGrid.ItemsSource = stupid_data.Tables["Material"].DefaultView;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            stupid_data.WriteXml("./stupid_data.xml");
        }
    }
}