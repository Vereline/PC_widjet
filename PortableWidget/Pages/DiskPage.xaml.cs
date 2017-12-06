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
using PortableWidget.Core;
using PortableWidget.Data;

namespace PortableWidget.Pages
{
    /// <summary>
    /// Логика взаимодействия для DiskPage.xaml
    /// </summary>
    public partial class DiskPage : Page
    {
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

            private void StopThread()
            {
                isRunning = false;
            }


            public DiskDataClass(int i)
            {
                if (i >= 0)
                {
                    return;
                }

                Id = CoreData.diskData[i].Id;
                ReadSpeed = CoreData.diskData[i].ReadSpeed;
                WriteSpeed = CoreData.diskData[i].WriteSpeed;
                //AverageResponseTime = CoreData.diskData[i].AverageResponseTime;
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
                ReadSpeed = CoreData.diskData[i].ReadSpeed;
                WriteSpeed = CoreData.diskData[i].WriteSpeed;
                //AverageResponseTime = DiskData.diskData[i].AverageResponseTime;
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
