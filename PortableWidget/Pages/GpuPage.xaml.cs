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
            private string _gpuType; 
            private int _temperature; 
            private float _speed; 
            private float _memoryUsage; 
            private int _countOfThreads; 
            private int _fanDutyPercentage;
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
                //GpuType = CoreData.gpuData[i].GpuType;
                //Temperature = CoreData.gpuData[i].Temperature;
                //Speed = CoreData.gpuData[i].Speed;
                MemoryUsage = CoreData.gpuData[i].MemoryUsage;
                //CountOfThreads = CoreData.gpuData[i].CountOfThreads;
                //FanDutyPercentage = CoreData.gpuData[i].FanDutyPercentage;


                //CollectingData();
            }

            public void CollectingData()
            {
                while (isRunning)
                {
                    lock (CoreData.gpuData)
                    {
                        RefreshBinding();
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

            public string GpuType
            {
                get { return _gpuType; }
                set
                {
                    _gpuType = value;
                    OnPropertyChanged();
                }
            }

            public int Temperature
            {
                get { return _temperature; }
                set
                {
                    _temperature = value;
                    OnPropertyChanged();
                }
            }

            public float Speed
            {
                get { return _speed; }
                set
                {
                    _speed = value;
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

            public int CountOfThreads
            {
                get { return _countOfThreads; }
                set
                {
                    _countOfThreads = value;
                    OnPropertyChanged();
                }
            }

            public int FanDutyPercentage
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
                //GpuType = CoreData.gpuData[i].GpuType;
                //Temperature = CoreData.gpuData[i].Temperature;
                //Speed = CoreData.gpuData[i].Speed;
                MemoryUsage = CoreData.gpuData[i].MemoryUsage;
                //CountOfThreads = CoreData.gpuData[i].CountOfThreads;
                //FanDutyPercentage = CoreData.gpuData[i].FanDutyPercentage;
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
