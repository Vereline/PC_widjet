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
    /// Логика взаимодействия для BatteryPage.xaml
    /// </summary>
    public partial class BatteryPage : Page
    {
        public class MeasureModelBattery
        {
            public DateTime DateTime { get; set; }
            public double Value { get; set; }
        }

        public class BatteryDataClass : INotifyPropertyChanged
        {
            private string _id;
            private string _charge;
            private float _fullCharge;
            private float _rechargeTime;
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

            public ChartValues<MeasureModelBattery> ChartValuesBattery { get; set; }
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



            public BatteryDataClass(int i)
            {
                var mapper = Mappers.Xy<MeasureModelBattery>()
                .X(model => model.DateTime.Ticks)
                .Y(model => model.Value);

                Charting.For<MeasureModelBattery>(mapper);

                ChartValuesBattery = new ChartValues<MeasureModelBattery>();

                DateTimeFormatter = value => new DateTime((long)value).ToString("hh:mm:ss");

                AxisStep = TimeSpan.FromSeconds(1).Ticks;
                AxisUnit = TimeSpan.TicksPerSecond;

                SetAxisLimits(DateTime.Now);

                if (i >= 0)
                {
                    return;
                }

                ID = CoreData.batteryData[i].ID;
                RechargeTime = CoreData.batteryData[i].RechargeTime;
                Charge = CoreData.batteryData[i].Charge;
                FullCharge = CoreData.batteryData[i].FullCharge;
                
                //CollectingData();
            }

            public void CollectingData()
            {
                while (isRunning)
                {
                    lock (CoreData.batteryData)
                    {
                        RefreshBinding();
                    }
                    var now = DateTime.Now;
                    _trend = _rechargeTime;
                    try
                    {
                        ChartValuesBattery.Add(new MeasureModelBattery
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
                    if (ChartValuesBattery.Count > 150) ChartValuesBattery.RemoveAt(0);
                    Thread.Sleep(timeout);
                }

            }

            public string ID
            {
                get { return _id; }
                set
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }

            public float FullCharge
            {
                get { return _fullCharge; }
                set
                {
                    _fullCharge = value;
                    OnPropertyChanged();
                }
            }

            public float RechargeTime
            {
                get { return _rechargeTime; }
                set
                {
                    _rechargeTime = value;
                    OnPropertyChanged();
                }
            }

            public string Charge
            {
                get { return _charge; }
                set
                {
                    _charge = value;
                    OnPropertyChanged();
                }
            }

            public void RefreshBinding()
            {
                var i = CoreData.batteryData.Count - 1;
                if (i <= 0)
                {
                    return;
                }

                ID = CoreData.batteryData[i].ID;
                RechargeTime = CoreData.batteryData[i].RechargeTime;
                Charge = CoreData.batteryData[i].Charge;
                FullCharge = CoreData.batteryData[i].FullCharge;
            }

            public event PropertyChangedEventHandler PropertyChanged;

            public void OnPropertyChanged([CallerMemberName]string prop = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
            }
        }


        private BatteryDataClass _batteryDataClass;


        public BatteryPage()
        {
            InitializeComponent();
            InitializeComponent();
            _batteryDataClass = new BatteryDataClass(0);
            ContentRoot.DataContext = _batteryDataClass;
            _batteryDataClass.getDataThread = new Thread(_batteryDataClass.CollectingData);
            _batteryDataClass.getDataThread.Start();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
