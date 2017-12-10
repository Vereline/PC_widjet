using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace PortableWidget.Pages
{
    /// <summary>
    /// Логика взаимодействия для GpuPage.xaml
    /// </summary>
    public partial class GpuPage : Page
    {
        public class CoreDataClass : INotifyPropertyChanged
        {
            private string _id; 
            private string _gpuDriverVersion; 
            private float _temperature; 
            private float _adapterRam; 
            private float _memoryUsage; 
            private float _fanDutyPercentage;
            private bool isRunning = true;
            int timeout = 1000;
            public Thread getDataThread;

            private void StopThread()
            {
                isRunning = false;
            }


            public CoreDataClass(int i)
            {
                if (i >= 0)
                {
                    return;
                }
                Id = CoreData.gpuData[i].Id;
                Temperature = CoreData.gpuData[i].Temperature;
                AdapterRam = CoreData.gpuData[i].AdapterRam;
                MemoryUsage = CoreData.gpuData[i].MemoryUsage;
                GpuDriverVersion = CoreData.gpuData[i].GpuDriverVersion;
                FanDutyPercentage = CoreData.gpuData[i].FanDutyPercentage;
            }

            public void CollectingData()
            {
                while (isRunning)
                {
                    lock (CoreData.gpuData)
                    {
                        RefreshBinding();
                        //System.Console.Write(" fan ={0}", FanDutyPercentage);
                        //System.Console.Write("temp ={0}", Temperature);
                    }
                    Thread.Sleep(timeout);
                }

            }

            public string Id
            {
                get { return _id; }
                set
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }

            public float Temperature
            {
                get { return _temperature; }
                set
                {
                    _temperature = value;
                    OnPropertyChanged();
                }
            }

            public float AdapterRam
            {
                get { return _adapterRam; }
                set
                {
                    _adapterRam = value;
                    OnPropertyChanged();
                }
            }

            public float MemoryUsage
            {
                get { return _memoryUsage; }
                set
                {
                    _memoryUsage = value;
                    OnPropertyChanged();
                }
            }

            public string GpuDriverVersion
            {
                get { return _gpuDriverVersion; }
                set
                {
                    _gpuDriverVersion = value;
                    OnPropertyChanged();
                }
            }

            public float FanDutyPercentage
            {
                get { return _fanDutyPercentage; }
                set
                {
                    _fanDutyPercentage = value;
                    OnPropertyChanged();
                }
            }

            public void RefreshBinding()
            {
                var i = CoreData.gpuData.Count - 1;
                if (i <= 0)
                {
                    return;
                }
                Id = CoreData.gpuData[i].Id;
                Temperature = CoreData.gpuData[i].Temperature;
                AdapterRam = CoreData.gpuData[i].AdapterRam;
                MemoryUsage = CoreData.gpuData[i].MemoryUsage;
                GpuDriverVersion = CoreData.gpuData[i].GpuDriverVersion;
                FanDutyPercentage = CoreData.gpuData[i].FanDutyPercentage;
            }

            public event PropertyChangedEventHandler PropertyChanged;

            public void OnPropertyChanged([CallerMemberName]string prop = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
            }
        }


        private CoreDataClass _CoreDataClass;

        public GpuPage()
        {
            InitializeComponent();
            _CoreDataClass = new CoreDataClass(0);
            ContentRoot.DataContext = _CoreDataClass;
            _CoreDataClass.getDataThread = new Thread(_CoreDataClass.CollectingData);
            _CoreDataClass.getDataThread.Start();
        }
    }
}
