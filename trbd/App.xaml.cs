using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;

namespace trbd
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public StupidDataSet stupid_data = new StupidDataSet();

        App()
        {
        }

        public void load_data()
        {
            try
            {
                stupid_data.ReadXml("./stupid_data.xml");
            }
            catch (FileNotFoundException e)
            {
                this.on_load_error("Нет файла", "Я не могу прочитать дазу банных, файла нет.", e);
            }
            catch (System.Data.ConstraintException e)
            {
                this.on_load_error("Файл битый", "Я не могу прочитать дазу банных, файл битый.", e);
            }
        }

        private void on_load_error(string title, string message, Exception exception)
        {
            MessageBox.Show(message + "\n" + exception.Message, title, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
