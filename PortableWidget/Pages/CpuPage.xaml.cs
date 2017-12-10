using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Configurations;
using PortableWidget.Data;

namespace PortableWidget.Pages
{
    /// <summary>
    /// Логика взаимодействия для Cpu_page.xaml
    /// </summary>
    public partial class CpuPage : Page
    {
        public class MeasureModelCpu
        {
            public DateTime DateTime { get; set; }
            public double Value { get; set; }
        }

        public class CpuDataClass: INotifyPropertyChanged
        {
            private string _id;
            private float _usagePercentage;
            private float _speed;
            private int _countOfProcesses;
            private int _countOfThreads;
            private bool isRunning = true;
            int timeout = 1000;
            public Thread getDataThread;

            private void StopThread()
            {
                isRunning = false;
            }

            private double _axisMax;
            private double _axisMin;
            private double _trend;

            public ChartValues<MeasureModelCpu> ChartValuesCpu { get; set; }
            public Func<double, string> DateTimeFormatter { get; set; }
            public double AxisStep { get; set; }
            public double AxisUnit { get; set; }

            public double AxisMax
            {
                get { return _axisMax; }
                set
                {
                    _axisMax = value;
                    OnPropertyChanged("AxisMax");
                }
            }
            public double AxisMin
            {
                get { return _axisMin; }
                set
                {
                    _axisMin = value;
                    OnPropertyChanged("AxisMin");
                }
            }

            private void SetAxisLimits(DateTime now)
            {
                AxisMax = now.Ticks + TimeSpan.FromSeconds(1).Ticks; // lets force the axis to be 1 second ahead
                AxisMin = now.Ticks - TimeSpan.FromSeconds(8).Ticks; // and 8 seconds behind
            }



            public CpuDataClass(int i)
            {
                var mapper = Mappers.Xy<MeasureModelCpu>()
                .X(model => model.DateTime.Ticks)
                .Y(model => model.Value);

                Charting.For<MeasureModelCpu>(mapper);

                ChartValuesCpu = new ChartValues<MeasureModelCpu>();

                DateTimeFormatter = value => new DateTime((long)value).ToString("hh:mm:ss");

                AxisStep = TimeSpan.FromSeconds(1).Ticks;
                AxisUnit = TimeSpan.TicksPerSecond;

                SetAxisLimits(DateTime.Now);

                if (i >= 0)
                {
                    return;
                }

                Id = CoreData.cpuData[i].Id;
                UsagePercentage = CoreData.cpuData[i].UsagePercentage;
                Speed = CoreData.cpuData[i].Speed;
                CountOfProcesses = CoreData.cpuData[i].CountOfProcesses;
                CountOfThreads = CoreData.cpuData[i].CountOfThreads;

                //CollectingData();
            }

            public void CollectingData()
            {
                while (isRunning)
                {
                    lock (CoreData.cpuData)
                    {
                        RefreshBinding();
                    }
                    var now = DateTime.Now;
                    _trend = _usagePercentage;
                    try
                    {
                        ChartValuesCpu.Add(new MeasureModelCpu
                        {
                            DateTime = now,
                            Value = _trend
                        });
                    }
                    catch (Exception e)
                    {
                        System.Console.Write(e);
                    }


                    SetAxisLimits(now);
                    //System.Console.Write("now {0}", now);
                    //System.Console.Write("value {0}", _trend);
                    ////lets only use the last 150 values
                    if (ChartValuesCpu.Count > 150) ChartValuesCpu.RemoveAt(0);
                    Thread.Sleep(timeout);
                }
                
            }

            public string Id
            {
                get { return _id; }
                set {
                    _id = value;
                    OnPropertyChanged();
                }
            }

            public float UsagePercentage
            {
                get { return _usagePercentage; }
                set
                {
                    _usagePercentage = value;
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

            public int CountOfProcesses
            {
                get { return _countOfProcesses; }
                set
                {
                    _countOfProcesses = value;
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

            public void RefreshBinding()
            {
                var i = CoreData.cpuData.Count-1;
                if (i <= 0)
                {
                    return;
                }

                Id = CoreData.cpuData[i].Id;
                UsagePercentage = CoreData.cpuData[i].UsagePercentage;
                Speed = CoreData.cpuData[i].Speed;
                CountOfProcesses = CoreData.cpuData[i].CountOfProcesses;
                CountOfThreads = CoreData.cpuData[i].CountOfThreads;
            }

            public event PropertyChangedEventHandler PropertyChanged;

            public void OnPropertyChanged([CallerMemberName]string prop = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
            }
        }


        private CpuDataClass _cpuDataClass;

        public CpuPage()
        {
            InitializeComponent();
            _cpuDataClass = new CpuDataClass(0);
            ContentRoot.DataContext = _cpuDataClass;
            _cpuDataClass.getDataThread = new Thread(_cpuDataClass.CollectingData);
            _cpuDataClass.getDataThread.Start();
        }

        

        private void CartesianChart_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //_cpuDataClass.StopThread();
        }
    }
}
