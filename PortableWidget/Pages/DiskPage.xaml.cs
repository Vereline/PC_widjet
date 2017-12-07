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
using PortableWidget.Core;
using PortableWidget.Data;

namespace PortableWidget.Pages
{
    /// <summary>
    /// Логика взаимодействия для DiskPage.xaml
    /// </summary>
    public partial class DiskPage : Page
    {
        public class MeasureModel
        {
            public DateTime DateTime { get; set; }
            public double Value { get; set; }
        }

        public class DiskDataClass : INotifyPropertyChanged
        {
            private string _id;
            private float _readSpeed;
            private float _writeSpeed;
            private float _averageResponseTime;
            private ulong _capacity;
            private int _formatted;
            private bool isRunning = true;
            int timeout = 1000;
            public Thread getDataThread;

            private double _axisMax;
            private double _axisMin;
            private double _trendRead;
            private double _trendWrite;

            private void StopThread()
            {
                isRunning = false;
            }

            public ChartValues<MeasureModel> ReadValues { get; set; }
            public ChartValues<MeasureModel> WriteValues { get; set; }
            public ChartValues<MeasureModel> DiskChartValues { get; set; }
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


            public DiskDataClass(int i)
            {
                var mapper = Mappers.Xy<MeasureModel>()
                .X(model => model.DateTime.Ticks)
                .Y(model => model.Value);

                Charting.For<MeasureModel>(mapper);

                ReadValues = new ChartValues<MeasureModel>();
                WriteValues = new ChartValues<MeasureModel>();
                DiskChartValues = new ChartValues<MeasureModel>();

                DateTimeFormatter = value => new DateTime((long)value).ToString("hh:mm:ss");

                AxisStep = TimeSpan.FromSeconds(1).Ticks;
                AxisUnit = TimeSpan.TicksPerSecond;

                SetAxisLimits(DateTime.Now);


                if (i >= 0)
                {
                    return;
                }

                Id = CoreData.diskData[i].Id;
                ReadSpeed = CoreData.diskData[i].ReadSpeed;
                WriteSpeed = CoreData.diskData[i].WriteSpeed;
                AverageResponseTime = CoreData.diskData[i].AverageResponseTime;
                Capacity = CoreData.diskData[i].Capacity;
                //Formatted = CoreData.diskData[i].Formatted;

                //CollectingData();
            }

            public void CollectingData()
            {
                while (isRunning)
                {
                    lock (CoreData.diskData)
                    {
                        RefreshBinding();
                    }
                    var now = DateTime.Now;
                    _trendRead = _readSpeed;
                    _trendWrite = _writeSpeed;

                    ReadValues.Add(new MeasureModel
                    {
                        DateTime = now,
                        Value = _trendRead
                    });
                    WriteValues.Add(new MeasureModel
                    {
                        DateTime = now,
                        Value = _trendWrite
                    });

                    DiskChartValues.Add(new MeasureModel
                    {
                        DateTime = now,
                        Value = _averageResponseTime
                    });


                    SetAxisLimits(now);
                    //System.Console.Write("now {0}", now);
                    //System.Console.Write("value {0}", _trend);
                    //lets only use the last 150 values
                    if (ReadValues.Count > 10) ReadValues.RemoveAt(0);
                    if (WriteValues.Count > 10) WriteValues.RemoveAt(0);
                    if (DiskChartValues.Count > 10) DiskChartValues.RemoveAt(0);
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

            public float ReadSpeed
            {
                get { return _readSpeed; }
                set
                {
                    _readSpeed = value;
                    OnPropertyChanged();
                }
            }

            public float WriteSpeed
            {
                get { return _writeSpeed; }
                set
                {
                        _writeSpeed = value;
                        OnPropertyChanged();
                }
            }

            public float AverageResponseTime
            {
                get { return _averageResponseTime; }
                set
                {
                    _averageResponseTime = value;
                    OnPropertyChanged();
                }
            }

            public ulong Capacity
            {
                get { return _capacity; }
                set
                {
                    _capacity = value;
                    OnPropertyChanged();
                }
            }

            public int Formatted
            {
                get { return _formatted; }
                set
                {
                    _formatted = value;
                    OnPropertyChanged();
                }
            }

            public void RefreshBinding()
            {
                var i = CoreData.diskData.Count - 1;
                if (i <= 0)
                {
                    return;
                }
                Id = CoreData.diskData[i].Id;
                ReadSpeed = CoreData.diskData[i].ReadSpeed;
                WriteSpeed = CoreData.diskData[i].WriteSpeed;
                AverageResponseTime = CoreData.diskData[i].AverageResponseTime;
                Capacity = CoreData.diskData[i].Capacity;
                //Formatted = DiskData.diskData[i].Formatted;
            }

            public event PropertyChangedEventHandler PropertyChanged;

            public void OnPropertyChanged([CallerMemberName]string prop = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
            }
        }


        private DiskDataClass _diskDataClass;

        public DiskPage()
        {
            InitializeComponent();
            _diskDataClass = new DiskDataClass(0);
            ContentRoot.DataContext = _diskDataClass;
            _diskDataClass.getDataThread = new Thread(_diskDataClass.CollectingData);
            _diskDataClass.getDataThread.Start();
        }
    }
}
