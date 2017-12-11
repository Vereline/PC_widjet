using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
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
using PortableWidget.Data;
using PortableWidget.Models;

namespace PortableWidget.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProcessesPage.xaml
    /// </summary>
    public partial class ProcessesPage : Page
    {
        public class ProcessModelBind
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public long RamUsage { get; set; }
        }

        public class ProcessDataClass
        {
          public List<ProcessModelBind> ProcessModels;
          public Thread ProcessDataThread;
          private bool isRunning = true;
          private int timeout = 1000;

          private void StopThread()
            {
                isRunning = false;
            }

            public ProcessDataClass()
          {
              ProcessModels = new List<ProcessModelBind>();
                foreach (var item in CoreData.processData)
                {
                    ProcessModels.Add(new ProcessModelBind()
                    {
                        Id =item.Id,
                        RamUsage = item.RamUsage,
                        Name = item.Name
                    });
                }
          }

            public void CollectingData()
            {
                while (isRunning)
                {
                    lock (CoreData.processData)
                    {
                        RefreshBinding();
                    }
                    Thread.Sleep(timeout);
                }

            }

            public void RefreshBinding()
            {
                ProcessModels.Clear();
                foreach (var item in CoreData.processData)
                {
                    ProcessModels.Add(new ProcessModelBind()
                    {
                        Id = item.Id,
                        RamUsage = item.RamUsage,
                        Name = item.Name
                    });
                }
            }
        }

        private ProcessDataClass processDataClass;

        //public delegate void PageStateHandler();

        //public event PageStateHandler ListChanged;

        public ProcessesPage()
        {
            InitializeComponent();
            processDataClass = new ProcessDataClass();
            LvDataBinding.ItemsSource = processDataClass.ProcessModels;

            processDataClass.ProcessDataThread = new Thread(processDataClass.CollectingData);
            processDataClass.ProcessDataThread.Start();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LvDataBinding.Items.Refresh();
        }

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            ProcessModelBind processModel = new ProcessModelBind();
            processModel = (ProcessModelBind) LvDataBinding.SelectedItem;
            //kill this process
            Process p = Process.GetProcessById(processModel.Id);
            p.Kill();
        }
    }
}
