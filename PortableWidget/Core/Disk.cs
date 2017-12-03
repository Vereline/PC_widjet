using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Diagnostics;

namespace PortableWidget.Core
{
    class Disk
    {
        public string GetDiskId()
        {
            string DiskId = "";
            var searcher = new ManagementObjectSearcher(
              "select DeviceID from Win32_DiskDrive");
            foreach (var item in searcher.Get())
            {
                DiskId = (string)item["DeviceID"];

            }
            return DiskId;
        }

        public UInt64 GetCapacity()
        {
            UInt64 DiskCapacity = 0;
            var searcher = new ManagementObjectSearcher(
              "select Size from Win32_DiskDrive");
            foreach (var item in searcher.Get())
            {
                DiskCapacity = (UInt64)item["Size"];

            }
            return DiskCapacity;
        }

        public long GetReadCurrentSpeed()
        {
            long ReadSpeed = 0;
            PerformanceCounter ReadCounter = new PerformanceCounter();

            ReadCounter.CategoryName = "PhysicalDisk";
            ReadCounter.CounterName = "Disk Reads/sec";
            ReadCounter.InstanceName = "_Total";

            ReadSpeed = ReadCounter.RawValue;
            return ReadSpeed;
        }

        public long GetWriteCurrentSpeed()
        {
            long WriteSpeed = 0;
            PerformanceCounter WriteCounter = new PerformanceCounter();

            WriteCounter.CategoryName = "PhysicalDisk";
            WriteCounter.CounterName = "Disk Reads/sec";
            WriteCounter.InstanceName = "_Total";

            WriteSpeed = WriteCounter.RawValue;
            return WriteSpeed;
        }

    }
}
