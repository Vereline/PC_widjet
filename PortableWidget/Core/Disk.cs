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

        public float GetReadCurrentSpeed()
        {
            float ReadSpeed = 0;
            PerformanceCounter ReadCounter = new PerformanceCounter();

            ReadCounter.CategoryName = "PhysicalDisk";
            ReadCounter.CounterName = "Disk Read Bytes/sec";
            ReadCounter.InstanceName = "_Total";

            ReadSpeed = ReadCounter.NextValue();
            return ReadSpeed / 1024;
        }

        public float GetWriteCurrentSpeed()
        {
            float WriteSpeed = 0;
            PerformanceCounter WriteCounter = new PerformanceCounter("PhysicalDisk", "Disk Read Bytes/sec", "_Total");

            //WriteCounter.CategoryName = "PhysicalDisk";
            //WriteCounter.CounterName = "Disk Write Bytes/sec";
            //WriteCounter.InstanceName = "_Total";

            WriteSpeed = WriteCounter.NextValue();
            return WriteSpeed;
        }

    }
}
