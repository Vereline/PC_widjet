using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Diagnostics;

namespace PortableWidget.Core
{
    class Gpu
    {
        //PerformanceCounter GpuUtilization = new PerformanceCounter("GPU Engine", "Utilization Percentage", "*");

        //public float GetUtilization()
        //{
        //    float tmp = GpuUtilization.NextValue();
        //    var GpuUtility = (float)(Math.Round(tmp, 1));
        //    return GpuUtility;
        //}
        public string GetID()
        {
            string DeviceID = "";
            var searcher = new ManagementObjectSearcher(
            "select DeviceID from Win32_VideoController");
            foreach (var item in searcher.Get())
            {
                DeviceID = (string)item["DeviceID"];
            }
            return DeviceID;
        }

        public UInt32 GetAdapterRam()
        {
            UInt32 AdapterRAM = 0;
            var searcher = new ManagementObjectSearcher(
                "select AdapterRAM from Win32_VideoController");
            foreach (var item in searcher.Get())
            {
                AdapterRAM = (UInt32)item["AdapterRam"];
            }
            return AdapterRAM;
        }

        public string GetDriverVersion()
        {
            string DriverVersion = "";
            var searcher = new ManagementObjectSearcher(
                "select DriverVersion from Win32_VideoController");
            foreach (var item in searcher.Get())
            {
                DriverVersion = (string)item["DriverVersion"];
            }
            return DriverVersion;
        }
    }
}
