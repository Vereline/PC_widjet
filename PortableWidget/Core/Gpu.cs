using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace PortableWidget.Core
{
    class Gpu
    {
        public string GetID()
        {
            string clockSpeed = "";
            var searcher = new ManagementObjectSearcher(
            "select DeviceID from Win32_VideoController");
            foreach (var item in searcher.Get())
            {
                clockSpeed = (string)item["DeviceID"];
            }
            return clockSpeed;
        }

        public UInt32 GetAdapterRam()
        {
            UInt32 AdapterRAM = 0;
            var searcher = new ManagementObjectSearcher(
                "select AdapterRAM from Win32_VideoController");
            foreach (var item in searcher.Get())
            {
                AdapterRAM = (UInt32)item["DeviceID"];
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
