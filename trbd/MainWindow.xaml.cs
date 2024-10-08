﻿using Microsoft.Win32;
using System;
using System.Data;
using System.Diagnostics;
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
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "All Files (*.*)|*.*";
            bool? result = saveFileDialog.ShowDialog();

            if (result == true)
            {
                string filePath = saveFileDialog.FileName;
                if (filePath == null || filePath == "")
                {
                    filePath = "./stupid_data.xml";
                }
                try
                {
                    app.stupid_data.WriteXml(filePath);
                }
                catch (NullReferenceException exception)
                {
                    app.on_load_error("Нет данных", "Отсутствуют данные для сохранения, загрузите данные, чтобы начать изменения.", exception);
                }
                
            }
            else
            {
                return;
            }
            MessageBox.Show("Файл сохранен");
            
        }

        private void On_Load_Button_Click(object sender, RoutedEventArgs e)
        {
            app.load_data(txtFilePath.Text);

            MaterialGrid.ItemsSource = app.stupid_data.Tables["Material"]!.DefaultView;
            UsageGrid.ItemsSource = app.stupid_data.Tables["Usage"]!.DefaultView;
        }

        private void On_New_Button_Click(object sender, RoutedEventArgs e)
        {
            var tab = Nikita.SelectedIndex;
            if (app.stupid_data == null)
            {
                app.on_load_error("Нет данных", "Пожалуйста загрузите данные.", new System.NullReferenceException());
                return;

            }
            var table = app.stupid_data.Tables[tab];
            var grid = new ListView();
            if (tab == 0)
            {
                grid = MaterialGrid;
            }
            else if (tab == 1)
            {
                grid = UsageGrid;

            }
            List<string> cols = new List<string>();
            if (grid.View is GridView gridView)
            {
                foreach (var col in gridView.Columns)
                {
                    cols.Add(col.Header.ToString());
                }
            }
            var form = new EditForm(cols, tab);
            form.ShowDialog(table);
        }
        private void On_Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            var tab = Nikita.SelectedIndex;
            if (app.stupid_data == null)
            {
                app.on_load_error("Нет данных", "Пожалуйста загрузите данные.", new System.NullReferenceException());
                return;
            }
            var table = app.stupid_data.Tables[tab];
            int n_row = -1;
            var grid = new ListView();
            if (tab == 0)
            {
                grid = MaterialGrid;
            }
            else if (tab == 1)
            {
                grid = UsageGrid;

            }
            n_row = grid.SelectedIndex;
            List<string> cols = new List<string>();
            if (grid.View is GridView gridView)
            {
                foreach(var col in gridView.Columns)
                {
                    cols.Add(col.Header.ToString());
                }
            }
            var form = new EditForm(cols, tab);
            if (table.Rows.Count <= 0)
            {
                app.on_load_error("Нет данных", "Пожалуйста добавьте данные.", new System.NullReferenceException());
                return;

            }
            if (n_row == -1)
            {
                n_row = table.Rows.Count - 1;
            }
                form.ShowDialog(table, n_row);

        }
        private void On_Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            var tab = Nikita.SelectedIndex;
            var table = app.stupid_data.Tables[tab];
            int n_row = -1;
            var grid = new ListView();
            if (tab == 0)
            {
                grid = MaterialGrid;
            }
            else if (tab == 1)
            {
                grid = UsageGrid;

            }
            n_row = grid.SelectedIndex;
            //List<string> cols = new List<string>();
            //if (grid.View is GridView gridView)
            //{
            //    foreach (var col in gridView.Columns)
            //    {
            //        cols.Add(col.Header.ToString());
            //    }
            //}
            if (table.Rows.Count == 0)
            {
                return;
            }
            if (n_row == -1 && table.Rows.Count != 0)
            {
                n_row = table.Rows.Count - 1;
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