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
        public DataSet stupid_data;

        App()
        {
            load_data();
        }

        private void load_data()
        {
            stupid_data = new DataSet();
            try
            {
                stupid_data.ReadXml("./stupid_data.xml");
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Я не могу прочитать дазу банных, фала нет.", "Нет файла");
            }
        }

        public void serialize()
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(StupidDataSet));
            var oleg = new StupidDataSet();
            var xml = "";

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, oleg);
                    xml = sww.ToString(); // Your XML
                    File.WriteAllText("./oleg.xml", xml);
                }
            }
        }
    }
}
