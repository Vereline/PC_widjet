using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Diagnostics;

namespace PortableWidget.Core
{
    class Ram
    {

        PerformanceCounter memPoolPaged = new PerformanceCounter("Memory", "Pool Paged Bytes", null);
        PerformanceCounter memPoolNonPaged = new PerformanceCounter("Memory", "Pool Nonpaged Bytes", null);
        PerformanceCounter InUse = new PerformanceCounter("Memory", "% Committed Bytes In Use", null);
        PerformanceCounter Available = new PerformanceCounter("Memory", "Available MBytes", null);
        PerformanceCounter Committed = new PerformanceCounter("Memory", "Committed Bytes", null);
        PerformanceCounter Cached = new PerformanceCounter("Memory", "Cache Bytes", null);

        public float GetMemoryCached()
        {
            float tmp = Cached.NextValue() / (1024 * 1024);
            var MemoryCached = (float)(Math.Round(tmp, 1));
            return MemoryCached;
        }

        public float GetMemoryCommitted()
        {
            float tmp = Committed.NextValue() / (1024*1024 *1024);
            var MemoryCommitted = (float)(Math.Round(tmp, 1));
            return MemoryCommitted;
        }

        public float GetMemoryAvailable()
        {
            float tmp = Available.NextValue() / 1024;
            var MemoryAvailable = (float)(Math.Round(tmp, 1));
            return MemoryAvailable;
        }

        public float GetMemoryInUse()
        {
            float MemoryInUse = 0;
            float memAvailable = Available.NextValue() / 1024;
            float memCap = GetCapacity(); // (1024 * 1024 * 1024);
            float memCached = GetMemoryCached() / 1024;
            MemoryInUse = memCap - memAvailable - memCached;
            //float memCommitted = Committed.NextValue();
            //var MemoryInUse = memCommitted - memAvailable;
            //float tmp = InUse.NextValue();
            //var MemoryInUse = (float)(Math.Round(tmp, 1));
            //Console.WriteLine(InUse.NextValue());
            return MemoryInUse;
        }
        public float GetPoolPaged()
        {
            float tmp = memPoolPaged.NextValue() / (1024 * 1024);
            var MemoryPoolPaged = (float)(Math.Round(tmp, 1));
            return MemoryPoolPaged;
        }

        public float GetPoolNonPaged()
        {
            float tmp = memPoolNonPaged.NextValue() / (1024 * 1024);
            var MemoryPoolNonPaged = (float)(Math.Round(tmp, 1));
            return MemoryPoolNonPaged;
        }

        public string GetRamId()
        {
            string RamId = "";
            var searcher = new ManagementObjectSearcher(
              "select DeviceID from Win32_MemoryDevice");
            foreach (var item in searcher.Get())
            {
                RamId = (string)item["DeviceID"];

            }
            return RamId;
        }

        public float GetCapacity()
        {
            float RamCapacity = 0;
            var searcher = new ManagementObjectSearcher(
              "select Capacity from Win32_PhysicalMemory");
            foreach (var item in searcher.Get())
            {
                RamCapacity += (UInt64)item["Capacity"]; // mb with += it will work correctly, i need someone
                                                         // with 2 memory devices to test this

            }
            return RamCapacity / (1024 * 1024 * 1024); // it is incorrect, but it is close. InUse func. needs this
            // i'll fix this later if it is still broken
        }

        public UInt32 GetSpeed()
        {
            UInt32 RamSpeed = 0;
            var searcher = new ManagementObjectSearcher(
              "select Speed from Win32_PhysicalMemory");
            foreach (var item in searcher.Get())
            {
                RamSpeed = (UInt32)item["Speed"];

            }
            return RamSpeed;
        }
    }
}
