using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using PortableWidget.Models;
using PortableWidget.Data;
using System.Diagnostics;

namespace PortableWidget.Core
{
    public class Analyser
    {
        bool isRun = true;
        int timeout = 1000;
        Thread _cpuAnalyseThread;
        Thread _ramAnalyseThread;
        Thread _diskAnalyseThread;
        Thread _gpuAnalyseThread;
        Thread _ethernetThread;
        Thread _processesThread;
        Thread _batteryThread;

        public Analyser(int timeout) {
            this.timeout = timeout;
        }

        public void Start()
        {
            isRun = true;
            _cpuAnalyseThread = new Thread(AnalyseCpu);
            _ramAnalyseThread = new Thread(AnalyseRam);
            _diskAnalyseThread = new Thread(AnalyseDisk);
            _gpuAnalyseThread = new Thread(AnalyseGpu);
            _ethernetThread = new Thread(AnalyseEthernet);
            _processesThread = new Thread(AnalyseProcessses);
            _batteryThread = new Thread(AnalyseBattery);
            _cpuAnalyseThread.Start();
            _ramAnalyseThread.Start();
            _diskAnalyseThread.Start();
            _gpuAnalyseThread.Start();
            _ethernetThread.Start();
            _processesThread.Start();
            _batteryThread.Start();
            
        }

        public void Stop() {
            isRun = false;
        }

        private void AnalyseCpu() {
            Cpu cpu = new Cpu();
            while (isRun) {
                lock (CoreData.cpuData) {
                    CoreData.cpuData.Add(new CpuModel() {
                        UsagePercentage = cpu.GetUsagePercentage(),
                        Id = cpu.GetCpuId(),
                        Speed = cpu.GetCurrentSpeed(),
                        CountOfProcesses = cpu.CountOfProcess(),
                        CountOfThreads = cpu.CountOfThreads()
                        
                    });
                }
                Thread.Sleep(timeout);
            }
        }

        private void AnalyseRam()
        {
            Ram ram = new Ram();
            while (isRun)
            {
                lock (CoreData.ramData)
                {
                    CoreData.ramData.Add(new RamModel()
                    {
                        Id = ram.GetRamId(),
                        RamSpeed = ram.GetSpeed(),
                        Capacity = ram.GetCapacity(),
                        PagedPool = ram.GetPoolPaged(),
                        NonPagedPool = ram.GetPoolNonPaged(),
                        MemoryInUse = ram.GetMemoryInUse(),
                        Available = ram.GetMemoryAvailable(),
                        MemoryCommited = ram.GetMemoryCommitted(),
                        MemoryCached = ram.GetMemoryCached()
                    });
                }
                Thread.Sleep(timeout);
                //Console.Write(" {0} ", CoreData.ramData[CoreData.ramData.Count - 1].MemoryInUse);
            }
        }

        private void AnalyseDisk()
        {
            Disk disk = new Disk();
            while (isRun)
            {
                lock (CoreData.diskData)
                {
                    CoreData.diskData.Add(new DiskModel() {
                        Id = disk.GetDiskId(),
                        Capacity = disk.GetCapacity(),
                        ReadSpeed = disk.GetReadCurrentSpeed(),
                        WriteSpeed = disk.GetWriteCurrentSpeed(),
                        AverageResponseTime = disk.GetAvResponseTime(),
                        ActiveTime = disk.GetActiveTime()
                    });
                    Thread.Sleep(timeout);
                    /*System.Console.Write("disk write {0} | avTime {1} | DiskTime {2} || ",
                        CoreData.diskData[CoreData.diskData.Count - 1].WriteSpeed,
                        CoreData.diskData[CoreData.diskData.Count - 1].AverageResponseTime,
                        CoreData.diskData[CoreData.diskData.Count - 1].ActiveTime
                        );*/
                }
            }
        }

        private void AnalyseProcessses()
        {
            //Processes process = new Processes();
            Cpu cpu = new Cpu();
            while (isRun)
            {
                lock (CoreData.processData)
                {
                    CoreData.processData.Clear();
                    foreach (var process in Process.GetProcesses())
                    {
                        CoreData.processData.Add(new ProcessModel()
                        {
                            Id = process.Id,
                            Name = process.ProcessName,
                            RamUsage = process.WorkingSet64 / (1024 * 1024)
                        });
                    }
                    Thread.Sleep(timeout);
                    //Console.WriteLine("");
                    //CoreData.processData.Clear();
                }
                
            }
        }
        
        private void AnalyseGpu()
        {
            Gpu gpu = new Gpu();
            while (isRun)
            {
                lock (CoreData.gpuData)
                {
                    CoreData.gpuData.Add(new GpuModel()
                    {
                        Id = gpu.GetID(),
                        AdapterRam = gpu.GetAdapterRam(),
                        GpuDriverVersion = gpu.GetDriverVersion(),
                        MemoryUsage = gpu.GetUsage(),
                        Temperature = gpu.GetTemperature(),
                        FanDutyPercentage = gpu.GetFanDuty()
                        //Speed = gpu.GetSpeed()
                    });
                    //Console.WriteLine("gpu temperature {0}", CoreData.gpuData[CoreData.gpuData.Count - 1].Temperature);
                    //Console.WriteLine("gpu utilization {0}", CoreData.gpuData[CoreData.gpuData.Count - 1].FanDutyPercentage);
                }
                Thread.Sleep(timeout);
            }
        }

        private void AnalyseEthernet()
        {
            Ethernet ethernet = new Ethernet();
            while (isRun)
            {
                lock (CoreData.ethernetData)
                {
                    CoreData.ethernetData.Add(new EthernetModel()
                    {
                        //AdapterName = ethernet.GetAdapterName(),
                        //Id = ethernet.GetDeviceId()
                        //ConnectionSpeed = ethernet.GetSentSpeed()
                    });
                }
                Thread.Sleep(timeout);
            }
        }

        private void AnalyseBattery()
        {
            BatteryCore battery = new BatteryCore();
            while (isRun)
            {
                lock (CoreData.batteryData)
                {
                    List<string> datalist = battery.GetBatteryInformation();
                    CoreData.batteryData.Add(new BatteryModel()
                    {
                        Availability = datalist[0],
                        BatteryStatus = datalist[1],
                        DesignVoltage = Double.Parse(datalist[2]),
                        EstimatedChargeRemaining = UInt32.Parse(datalist[3]),
                        EstimatedRunTime = UInt32.Parse(datalist[4])
                    });

                }
                //Console.WriteLine("Av {0}", CoreData.batteryData[CoreData.batteryData.Count - 1].Availability);
                //Console.WriteLine("status {0}", CoreData.batteryData[CoreData.batteryData.Count - 1].BatteryStatus);
                //Console.WriteLine("volt {0}", CoreData.batteryData[CoreData.batteryData.Count - 1].DesignVoltage);
                //Console.WriteLine("ChRem {0}", CoreData.batteryData[CoreData.batteryData.Count - 1].EstimatedChargeRemaining);
                //Console.WriteLine("RunTime {0}", CoreData.batteryData[CoreData.batteryData.Count - 1].EstimatedRunTime);
                Thread.Sleep(timeout);
            }
        }
    }
}
