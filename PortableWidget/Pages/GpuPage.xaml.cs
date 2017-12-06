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
        public class GpuDataClass : INotifyPropertyChanged
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


            public GpuDataClass(int i)
            {
                if (i >= 0) return;
                Id = GpuData.gpuData[i].Id;
                GpuType = GpuData.gpuData[i].GpuType;
                Temperature = GpuData.gpuData[i].Temperature;
                Speed = GpuData.gpuData[i].Speed;
                MemoryUsage = GpuData.gpuData[i].MemoryUsage;
                CountOfThreads = GpuData.gpuData[i].CountOfThreads;
                FanDutyPercentage = GpuData.gpuData[i].FanDutyPercentage;


                //CollectingData();
            }

            public void CollectingData()
            {
                while (isRunning)
                {
                    lock (GpuData.gpuData)
                    {
                        RefreshBinding();
                        Thread.Sleep(timeout);
                    }

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
                var i = GpuData.gpuData.Count - 1;
                if (i <= 0) return;
                Id = GpuData.gpuData[i].Id;
                GpuType = GpuData.gpuData[i].GpuType;
                Temperature = GpuData.gpuData[i].Temperature;
                Speed = GpuData.gpuData[i].Speed;
                MemoryUsage = GpuData.gpuData[i].MemoryUsage;
                CountOfThreads = GpuData.gpuData[i].CountOfThreads;
                FanDutyPercentage = GpuData.gpuData[i].FanDutyPercentage;
            }

            public event PropertyChangedEventHandler PropertyChanged;

            public void OnPropertyChanged([CallerMemberName]string prop = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
            }
        }


        private GpuDataClass _GpuDataClass;

        public GpuPage()
        {
            InitializeComponent();
            _GpuDataClass = new GpuDataClass(0);
            ContentRoot.DataContext = _GpuDataClass;
            _GpuDataClass.getDataThread = new Thread(_GpuDataClass.CollectingData);
            _GpuDataClass.getDataThread.Start();
        }
    }
}
