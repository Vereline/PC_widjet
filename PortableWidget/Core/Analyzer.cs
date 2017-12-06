using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using PortableWidget.Models;
using PortableWidget.Data;

namespace PortableWidget.Core
{
    public class Analyzer
    {
        bool isRun = true;
        int timeout = 1000;
        Thread cpuAnalizeThread;
        Thread ramAnalizeThread;

        public Analyzer(int timeout) {
            this.timeout = timeout;
        }

        public void Start()
        {
            isRun = true;
            cpuAnalizeThread = new Thread(AnalyzeCpu);
            ramAnalizeThread = new Thread(AnalizeRam);
            cpuAnalizeThread.Start();
            ramAnalizeThread.Start();
            
        }

        public void Stop() {
            isRun = false;
        }

        private void AnalyzeCpu() {
            Cpu cpu = new Cpu();
            while (isRun) {
                lock (CpuData.cpuData) {
                    CpuData.cpuData.Add(new CpuModel() {
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

        private void AnalyzeDisk()
        {
            Disk disk = new Disk();
            while (isRun)
            {
                lock (DiskData.diskData)
                {
                    DiskData.diskData.Add(new DiskModel() {
                        Id = disk.GetDiskId(),
                        Capacity = disk.GetCapacity(),
                        ReadSpeed = disk.GetReadCurrentSpeed(),
                        WriteSpeed = disk.GetWriteCurrentSpeed()
                    });
                }
            }
        }

        private void AnalizeRam()
        {
        }
    }
}
