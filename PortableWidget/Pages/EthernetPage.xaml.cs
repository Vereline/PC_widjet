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
using PortableWidget.Models;

namespace PortableWidget.Pages
{
    /// <summary>
    /// Логика взаимодействия для EthernetPage.xaml
    /// </summary>
    public partial class EthernetPage : Page
    {
        public class EthernetDataClass : INotifyPropertyChanged
        {
            private string _id;
            private float _sendPerSecond;
            private float _receivePerSecond;
            private string _SSID;
            private string _adapterName;
            private string _connectionType;
            private SignalQuality _signalStrength;
            private string _IPv4;
            private string _IPv6;
            private bool isRunning = true;
            int timeout = 1000;
            public Thread getDataThread;

            private void StopThread()
            {
                isRunning = false;
            }


            public EthernetDataClass(int i)
            {
                if (i >= 0)
                {
                    return;
                }

                Id = CoreData.ethernetData[i].Id;
                SendPerSecond = CoreData.ethernetData[i].ConnectionSpeed;
                SSID = CoreData.ethernetData[i].SSID;
                AdapterName = CoreData.ethernetData[i].AdapterName;
                ConnectionType = CoreData.ethernetData[i].ConnectionType;
                SignalStrength = CoreData.ethernetData[i].SignalStrength;
                IPv4 = CoreData.ethernetData[i].IPv4;
                IPv6 = CoreData.ethernetData[i].IPv6;
                //ReceivePerSecond = CoreData.ethernetData[i].ReceivePerSecond;
                //CollectingData();
            }

            public void CollectingData()
            {
                while (isRunning)
                {
                    lock (CoreData.ethernetData)
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

            public float SendPerSecond
            {
                get { return _sendPerSecond; }
                set
                {
                    _sendPerSecond = value;
                    OnPropertyChanged();
                }
            }

            public float ReceivePerSecond
            {
                get { return _receivePerSecond; }
                set
                {
                    _receivePerSecond = value;
                    OnPropertyChanged();
                }
            }

            public string SSID
            {
                get { return _SSID; }
                set
                {
                    _SSID = value;
                    OnPropertyChanged();
                }
            }

            public string AdapterName
            {
                get { return _adapterName; }
                set
                {
                    _adapterName = value;
                    OnPropertyChanged();
                }
            }

            public string ConnectionType
            {
                get { return _connectionType; }
                set
                {
                    _connectionType = value;
                    OnPropertyChanged();
                }
            }

            public SignalQuality SignalStrength
            {
                get { return _signalStrength; }
                set
                {
                    _signalStrength = value;
                    OnPropertyChanged();
                }
            }

            public string IPv4
            {
                get { return _IPv4; }
                set
                {
                    _IPv4 = value;
                    OnPropertyChanged();
                }
            }

            public string IPv6
            {
                get { return _IPv6; }
                set
                {
                    _IPv6 = value;
                    OnPropertyChanged();
                }
            }


            public void RefreshBinding()
            {
                var i = CoreData.ethernetData.Count - 1;
                if (i <= 0)
                {
                    return;
                }

                Id = CoreData.ethernetData[i].Id;
                SendPerSecond = CoreData.ethernetData[i].ConnectionSpeed;
                SSID = CoreData.ethernetData[i].SSID;
                AdapterName = CoreData.ethernetData[i].AdapterName;
                ConnectionType = CoreData.ethernetData[i].ConnectionType;
                SignalStrength = CoreData.ethernetData[i].SignalStrength;
                IPv4 = CoreData.ethernetData[i].IPv4;
                IPv6 = CoreData.ethernetData[i].IPv6;
                //ReceivePerSecond = CoreData.ethernetData[i].ReceivePerSecond;
            }

            public event PropertyChangedEventHandler PropertyChanged;

            public void OnPropertyChanged([CallerMemberName]string prop = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
            }
        }


        private EthernetDataClass _EthernetDataClass;

        public EthernetPage()
        {
            InitializeComponent();
            _EthernetDataClass = new EthernetDataClass(0);
            ContentRoot.DataContext = _EthernetDataClass;
            _EthernetDataClass.getDataThread = new Thread(_EthernetDataClass.CollectingData);
            _EthernetDataClass.getDataThread.Start();
        }
    }
}
