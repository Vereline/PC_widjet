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

        public Analyser(int timeout) {
            this.timeout = timeout;
        }

        public void Start()
        {
            isRun = true;
            _cpuAnalyseThread = new Thread(AnalyseCpu);
//<<<<<<< Updated upstream
            _ramAnalyseThread = new Thread(AnalyseRam);
//=======
            //ramAnalizeThread = new Thread(AnalyseRam);
//>>>>>>> Stashed changes
            _cpuAnalyseThread.Start();
            _ramAnalyseThread.Start();
            
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
                        //CountOfThreads = (uint)random.Next(100) //for test
                    });
                }
                Thread.Sleep(timeout);
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
                        WriteSpeed = disk.GetWriteCurrentSpeed()
                    });
                }
            }
        }

        private void AnalyseRam()
        {
        }
    }
}
