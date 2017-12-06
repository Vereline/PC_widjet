using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using PortableWidget.Core;
using System.Diagnostics;
using System.IO;


namespace PortableWidget.Pages
{
    /// <summary>
    /// Логика взаимодействия для FileUnlockerPage.xaml
    /// </summary>
    public partial class FileUnlockerPage : Page
    {
        private String fileName;
        private List<Process> processList;

        public FileUnlockerPage()
        {
            InitializeComponent();
        }

        private void OnSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Processes.ItemsSource = null;
                Processes.ItemsSource = GetBindList(openFileDialog.FileName);
                fileField.Text = fileName;
            }
        }

        private void OnKillBtn_Click(object sender, RoutedEventArgs e)
        {
            if (processList == null) {
                return;
            }

            foreach (var item in processList) {
                item.Kill();
                item.WaitForExit(1000);
            }
           
            Processes.ItemsSource = null;
            Processes.ItemsSource = GetBindList(fileName);
        }

        private List<string> GetBindList(string name) {
            List<string> bindList = new List<string>();
            processList = Unlocker.GetProcessesLockingFile(name);
            foreach (var item in processList)
            {
                bindList.Add(String.Format("Id: {0} | Name: {1} ", item.Id, item.ToString()));
            }
            return bindList;
        }
    }
}
