using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using PortableWidget.Models;
using PortableWidget.Data;

namespace PortableWidget.Core
{
    public class Analizer
    {
        bool isRun = true;
        int timeout = 1000;
        Thread cpuAnalizeThread;
        Thread ramAnalizeThread;

        public Analizer(int timeout) {
            this.timeout = timeout;
        }

        public void Start()
        {
            isRun = true;
            cpuAnalizeThread = new Thread(AnalizeCpu);
            ramAnalizeThread = new Thread(AnalizeRam);
            cpuAnalizeThread.Start();
            ramAnalizeThread.Start();
            
        }

        public void Stop() {
            isRun = false;
        }

        private void AnalizeCpu() {
            Cpu cpu = new Core.Cpu();
            while (isRun) {
                lock (CpuData.cpuData) {
                    CpuData.cpuData.Add(new CpuModel() {
                        UsagePercentage = cpu.GetUsagePercentage()
                        // TODO - all props
                    });
                }
                Thread.Sleep(timeout);
            }
        }

        private void AnalizeRam()
        {
        }
    }
}
