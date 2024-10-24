using System;
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
        public StupidDataSet stupid_data = new();

        public void LoadData(string file_path)
        {
            stupid_data.Clear();
            try
            {
                stupid_data.ReadXml(file_path);
            }
            catch (FileNotFoundException e)
            {
                this.ShowError("Нет файла", "Я не могу прочитать дазу банных, файла нет.", e);
            }
            catch (System.Data.ConstraintException e)
            {
                this.ShowError("Файл битый", "Я не могу прочитать дазу банных, файл битый.", e);
            }
            catch (System.Xml.XmlException e)
            {
                this.ShowError("Неправильный формат", "Поддерживаемые файлы: .xml", e);
            }
            catch (Exception e) 
            {
                this.ShowError("Ошибка загрузки", "Не получилось открыть файл", e);
            }
        }

        public void ShowError(string title, string message, Exception exception)
        {
            MessageBox.Show(message + "\n" + exception.Message, title, MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public void ShowError(string title, string message)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);
        }

    }
}
