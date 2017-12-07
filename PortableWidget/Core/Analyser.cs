using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using PortableWidget.Models;
using PortableWidget.Data;

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
            _cpuAnalyseThread.Start();
            _ramAnalyseThread.Start();
            _diskAnalyseThread.Start();
            _gpuAnalyseThread.Start();
            _ethernetThread.Start();
            
        }

        public void Stop() {
            isRun = false;
        }

        private void AnalyseCpu() {
            Cpu cpu = new Cpu();
            Random random = new Random(); // for test
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

        }
        
        private void AnalyseGpu()
        {
            Gpu gpu = new Gpu();
            //Random random = new Random(); // for test
            while (isRun)
            {
                lock (CoreData.gpuData)
                {
                    CoreData.gpuData.Add(new GpuModel()
                    {
                        Id = gpu.GetID(),
                        AdapterRam = gpu.GetAdapterRam(),
                        GpuDriverVersion = gpu.GetDriverVersion()
                    });
                }
                Thread.Sleep(timeout);
            }
        }

        private void AnalyseEthernet()
        {
            Ethernet ethernet = new Ethernet();
            //Random random = new Random(); // for test
            while (isRun)
            {
                lock (CoreData.ethernetData)
                {
                    CoreData.ethernetData.Add(new EthernetModel()
                    {
                        AdapterName = ethernet.GetAdapterName(),
                        SendPerSecond = ethernet.GetSentSpeed()
                    });
                }
                Thread.Sleep(timeout);
            }
        }
    }
}
