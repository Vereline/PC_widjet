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
    /// Логика взаимодействия для RamPage.xaml
    /// </summary>
    public partial class RamPage : Page
    {
        public class RamDataClass : INotifyPropertyChanged
        {
            private string _id;
            private float _ramSpeed;
            private float _memoryInUse;
            private float _memoryCommited;
            private float _memoryCached;
            private int _slotsUsed;
            private int _pagedPool;
            private int _nonPagedPool;
            private UInt64 _capacity;
            private bool isRunning = true;
            int timeout = 1000;
            public Thread getDataThread;

            private void StopThread()
            {
                isRunning = false;
            }


            public RamDataClass(int i)
            {
                if (i >= 0) return;
                Id = RamData.ramData[i].Id;
                RamSpeed = RamData.ramData[i].RamSpeed;
                MemoryInUse = RamData.ramData[i].MemoryInUse;
                MemoryCached = RamData.ramData[i].MemoryCached;
                Capacity = RamData.ramData[i].Capacity;
                MemoryCommited = RamData.ramData[i].MemoryCommited;
                SlotsUsed = RamData.ramData[i].SlotsUsed;
                NonPagedPool = RamData.ramData[i].NonPagedPool;
                PagedPool = RamData.ramData[i].PagedPool;

                //CollectingData();
            }

            public void CollectingData()
            {
                while (isRunning)
                {
                    lock (RamData.ramData)
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

            public ulong Capacity
            {
                get { return _capacity; }
                set
                {
                    _capacity = value;
                    OnPropertyChanged();
                }
            }

            public int NonPagedPool
            {
                get { return _nonPagedPool; }
                set
                {
                    _nonPagedPool = value;
                    OnPropertyChanged();
                }
            }

            public int PagedPool
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
                var i = RamData.ramData.Count - 1;
                if (i <= 0) return;
                Id = RamData.ramData[i].Id;
                RamSpeed = RamData.ramData[i].RamSpeed;
                MemoryInUse = RamData.ramData[i].MemoryInUse;
                MemoryCached = RamData.ramData[i].MemoryCached;
                Capacity = RamData.ramData[i].Capacity;
                MemoryCommited = RamData.ramData[i].MemoryCommited;
                SlotsUsed = RamData.ramData[i].SlotsUsed;
                NonPagedPool = RamData.ramData[i].NonPagedPool;
                PagedPool = RamData.ramData[i].PagedPool;
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
