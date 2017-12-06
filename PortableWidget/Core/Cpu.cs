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

        public float GetUsagePercentage()
        {
            uint ClockSpeed = 0;
            uint MaxClockSpeed = 0;
            float Percentage = 0;
            var searcher = new ManagementObjectSearcher(
            "select * from Win32_Processor");
            foreach (var item in searcher.Get())
            {
                ClockSpeed = (uint)item["CurrentClockSpeed"];
                MaxClockSpeed = (uint)item["MaxClockSpeed"];
            }
            Percentage = ClockSpeed / MaxClockSpeed * 100;
            return Percentage;
        }

        public uint CountOfThreads()
        {
            uint ThreadCount = 0;
            var searcher = new ManagementObjectSearcher(
              "select ThreadCount from Win32_Processor");
            foreach (var item in searcher.Get())
            {
                ThreadCount = (uint)item["ThreadCount"];

            }
            return ThreadCount;
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
