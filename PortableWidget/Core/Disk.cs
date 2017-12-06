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
        PerformanceCounter diskWrite = new PerformanceCounter("PhysicalDisk", "Disk Write Bytes/sec", "_Total");
        PerformanceCounter diskRead = new PerformanceCounter("PhysicalDisk", "Disk Read Bytes/sec", "_Total");
        PerformanceCounter diskAverageTimeRead = new PerformanceCounter("PhysicalDisk", "Avg. Disk sec/Read", "_Total");
        PerformanceCounter diskAverageTimeWrite = new PerformanceCounter("PhysicalDisk", "Avg. Disk sec/Write", "_Total");
        PerformanceCounter diskTime = new PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total");

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
            float tmp = diskRead.NextValue() / 1024;
            var ReadSpeed = (float)(Math.Round(tmp, 1));
            return ReadSpeed;
        }

        public float GetWriteCurrentSpeed()
        {
            float tmp = diskWrite.NextValue() / 1024;
            var WriteSpeed = (float)(Math.Round(tmp, 1));

            return WriteSpeed;
        }

        public float GetAvResponseTime()
        {
            float tmpR = diskAverageTimeRead.NextValue() * 1000;
            var DiskAverageTimeRead = (float)(Math.Round(tmpR, 1));

            float tmpW = diskAverageTimeWrite.NextValue() * 1000;
            var DiskAverageTimeWrite = (float)(Math.Round(tmpW, 1));

            float AvResponseTime = (DiskAverageTimeRead + DiskAverageTimeWrite) / 2;
            return AvResponseTime;
        }

        public float GetActiveTime()
        {
            float tmp = diskTime.NextValue();
            var DiskTime = (float)(Math.Round((double)tmp, 1));
            return DiskTime;
        }

    }
}
