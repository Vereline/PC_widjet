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
using PortableWidget.Data;
using LiveCharts.Configurations;

namespace PortableWidget.Pages
{
    /// <summary>
    /// Логика взаимодействия для RamPage.xaml
    /// </summary>
    public partial class RamPage : Page
    {
        public class MeasureModel
        {
            public DateTime DateTime { get; set; }
            public double Value { get; set; }
        }

        public class RamDataClass : INotifyPropertyChanged
        {
            private string _id;
            private float _ramSpeed;
            private float _memoryInUse;
            private float _memoryCommited;
            private float _memoryCached;
            private int _slotsUsed;
            private float _pagedPool;
            private float _nonPagedPool;
            private float _capacity;
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

            public ChartValues<MeasureModel> ChartValues { get; set; }
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


            public RamDataClass(int i)
            {
                var mapper = Mappers.Xy<MeasureModel>()
                .X(model => model.DateTime.Ticks)
                .Y(model => model.Value);

                Charting.For<MeasureModel>(mapper);

                ChartValues = new ChartValues<MeasureModel>();

                DateTimeFormatter = value => new DateTime((long)value).ToString("hh:mm:ss");

                AxisStep = TimeSpan.FromSeconds(1).Ticks;
                AxisUnit = TimeSpan.TicksPerSecond;

                SetAxisLimits(DateTime.Now);

                if (i >= 0)
                {
                    return;
                }

                Id = CoreData.ramData[i].Id;
                RamSpeed = CoreData.ramData[i].RamSpeed;
                MemoryInUse = CoreData.ramData[i].MemoryInUse;
                MemoryCached = CoreData.ramData[i].MemoryCached;
                Capacity = CoreData.ramData[i].Capacity;
                MemoryCommited = CoreData.ramData[i].MemoryCommited;
                SlotsUsed = CoreData.ramData[i].SlotsUsed;
                NonPagedPool = CoreData.ramData[i].NonPagedPool;
                PagedPool = CoreData.ramData[i].PagedPool;

                //CollectingData();
            }

            public void CollectingData()
            {
                while (isRunning)
                {
                    lock (CoreData.ramData)
                    {
                        RefreshBinding();
                    }
                    var now = DateTime.Now;
                    _trend = _memoryInUse;

                    ChartValues.Add(new MeasureModel
                    {
                        DateTime = now,
                        Value = _trend
                    });

                    SetAxisLimits(now);
                    //System.Console.Write("now {0}", now);
                    //System.Console.Write("value {0}", _trend);
                    //lets only use the last 150 values
                    if (ChartValues.Count > 10) ChartValues.RemoveAt(0);

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

            public float RamSpeed
            {
                get { return _ramSpeed; }
                set
                {
                    _ramSpeed = value;
                    OnPropertyChanged();
                }
            }

            public float MemoryInUse
            {
                get { return _memoryInUse; }
                set
                {
                    _memoryInUse = value;
                    OnPropertyChanged();
                }
            }

            public float MemoryCached
            {
                get { return _memoryCached; }
                set
                {
                    _memoryCached = value;
                    OnPropertyChanged();
                }
            }

            public float MemoryCommited
            {
                get { return _memoryCommited; }
                set
                {
                    _memoryCommited = value;
                    OnPropertyChanged();
                }
            }

            public int SlotsUsed
            {
                get { return _slotsUsed; }
                set
                {
                    _slotsUsed = value;
                    OnPropertyChanged();
                }
            }

            public float Capacity
            {
                get { return _capacity; }
                set
                {
                    _capacity = value;
                    OnPropertyChanged();
                }
            }

            public float NonPagedPool
            {
                get { return _nonPagedPool; }
                set
                {
                    _nonPagedPool = value;
                    OnPropertyChanged();
                }
            }

            public float PagedPool
            {
                get { return _pagedPool; }
                set
                {
                    _pagedPool = value;
                    OnPropertyChanged();
                }
            }

            public void RefreshBinding()
            {
                var i = CoreData.ramData.Count - 1;
                if (i <= 0)
                {
                    return;
                }

                Id = CoreData.ramData[i].Id;
                RamSpeed = CoreData.ramData[i].RamSpeed;
                MemoryInUse = CoreData.ramData[i].MemoryInUse;
                MemoryCached = CoreData.ramData[i].MemoryCached;
                Capacity = CoreData.ramData[i].Capacity;
                MemoryCommited = CoreData.ramData[i].MemoryCommited;
                SlotsUsed = CoreData.ramData[i].SlotsUsed;
                NonPagedPool = CoreData.ramData[i].NonPagedPool;
                PagedPool = CoreData.ramData[i].PagedPool;
            }

            public event PropertyChangedEventHandler PropertyChanged;

            public void OnPropertyChanged([CallerMemberName]string prop = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
            }
        }


        private RamDataClass _RamDataClass;

        public RamPage()
        {
            InitializeComponent();
            _RamDataClass = new RamDataClass(0);
            ContentRoot.DataContext = _RamDataClass;
            _RamDataClass.getDataThread = new Thread(_RamDataClass.CollectingData);
            _RamDataClass.getDataThread.Start();
        }
    }
}
