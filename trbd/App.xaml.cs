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
            load_data();
        }

        private void load_data()
        {
            try
            {
                stupid_data.ReadXml("./stupid_data.xml");
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Я не могу прочитать дазу банных, файла нет.", "Нет файла");
            }
            catch (System.Data.ConstraintException)
            {
                MessageBox.Show("Я не могу прочитать дазу банных, файл битый.", "Файл битый");
                throw;
            }
        }
    }
}
