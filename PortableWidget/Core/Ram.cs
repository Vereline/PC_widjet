using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace PortableWidget.Core
{
    class Ram
    {
        public string GetRamId()
        {
            string RamId = "";
            var searcher = new ManagementObjectSearcher(
              "select Name from Win32_PhysicalMemory");
            foreach (var item in searcher.Get())
            {
                RamId = (string)item["Name"];

            }
            return RamId;
        }

        public UInt64 GetCapacity()
        {
            UInt64 RamCapacity = 0;
            var searcher = new ManagementObjectSearcher(
              "select Capacity from Win32_PhysicalMemory");
            foreach (var item in searcher.Get())
            {
                RamCapacity = (UInt64)item["Capacity"];

            }
            return RamCapacity;
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
