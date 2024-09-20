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
        App()
        {
            this.serialize();
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
    public class Phone
    {
        public string Title { get; set; }
        public string Company { get; set; }
        public int Price { get; set; }
    }

    public class PhoneList
    {
        public List<Phone> read()
        {
            return new List<Phone>
            {
                new Phone { Title="iPhone 6S", Company="Apple", Price=54990 },
                new Phone {Title="Lumia 950", Company="Microsoft", Price=39990 },
                new Phone {Title="Nexus 5X", Company="Google", Price=29990 }
            };
        }
    }

}
