using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Diagnostics;

namespace PortableWidget.Core
{
    public class Cpu
    {
        PerformanceCounter ProcessorUtilization = new PerformanceCounter("Processor Information", "% Processor Utility", "_Total");

        public uint GetCurrentSpeed()
        {
            uint clockSpeed = 0;
            var searcher = new ManagementObjectSearcher(
            "select CurrentClockSpeed from Win32_Processor");
            foreach (var item in searcher.Get())
            {
                clockSpeed = (uint)item["CurrentClockSpeed"];
            }
            return clockSpeed;
        }

        //public uint GetCurrentSpeed()
        //{

        //}

        public float GetUsagePercentage()
        {
            float tmp = ProcessorUtilization.NextValue();
            var ProcUtility = (float)(Math.Round(tmp, 1));
            return ProcUtility;
        }

        /*public uint CountOfThreads()
        {
            uint ThreadCount = 0;
            var searcher = new ManagementObjectSearcher(
              "select ThreadCount from Win32_Processor");
            foreach (var item in searcher.Get())
            {
                ThreadCount = (uint)item["ThreadCount"];

            }
            return ThreadCount;
        }*/

        public int CountOfThreads()
        {
            int TheadCount = 0;
            Process[] ArrProcess = Process.GetProcesses();
            foreach (var process in ArrProcess)
            {
                TheadCount += process.Threads.Count;
            }
            return TheadCount;
        }

        public int CountOfProcess()
        {
            Process[] ArrProcesses = Process.GetProcesses();
            int ProcessCount = ArrProcesses.Length;
            return ProcessCount;
        }

        public string GetCpuId()
        {
            string CpuId = "";
            var searcher = new ManagementObjectSearcher(
              "select DeviceID from Win32_Processor");
            foreach (var item in searcher.Get())
            {
                CpuId = (string)item["DeviceID"];

            }
            return CpuId;
        }
    }
}
