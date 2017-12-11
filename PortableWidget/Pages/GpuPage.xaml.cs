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
using LiveCharts;
using LiveCharts.Configurations;
using PortableWidget.Data;

namespace PortableWidget.Pages
{
    /// <summary>
    /// Логика взаимодействия для GpuPage.xaml
    /// </summary>
    public partial class GpuPage : Page
    {
        public class MeasureModelGpu
        {
            public DateTime DateTime { get; set; }
            public double Value { get; set; }
        }

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

            private double _axisMax;
            private double _axisMin;
            private double _trend;

            public ChartValues<MeasureModelGpu> ChartValuesGpu { get; set; }
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

            public CoreDataClass(int i)
            {
                var mapper = Mappers.Xy<MeasureModelGpu>()
               .X(model => model.DateTime.Ticks)
               .Y(model => model.Value);

                Charting.For<MeasureModelGpu>(mapper);

                ChartValuesGpu = new ChartValues<MeasureModelGpu>();

                DateTimeFormatter = value => new DateTime((long)value).ToString("hh:mm:ss");

                AxisStep = TimeSpan.FromSeconds(1).Ticks;
                AxisUnit = TimeSpan.TicksPerSecond;

                SetAxisLimits(DateTime.Now);

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

                    var now = DateTime.Now;
                    _trend = _memoryUsage;
                    try
                    {
                        //_trend = 10;
                        ChartValuesGpu.Add(new MeasureModelGpu
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
                    if (ChartValuesGpu.Count > 150) ChartValuesGpu.RemoveAt(0);

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
