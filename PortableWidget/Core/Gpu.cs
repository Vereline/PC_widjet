using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Diagnostics;
using OpenHardwareMonitor.Hardware;

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

        public float GetUsage()
        {
            float usage = 0;
            Computer thisComputer = new Computer() { GPUEnabled = true };
            thisComputer.Open();
            foreach (var hardwareItem in thisComputer.Hardware)
            {
                if (hardwareItem.HardwareType == HardwareType.GpuNvidia || hardwareItem.HardwareType == HardwareType.GpuAti)
                {
                    hardwareItem.Update();
                    foreach (IHardware subHardware in hardwareItem.SubHardware)
                    {
                        subHardware.Update();
                    }
                    foreach (var sensor in hardwareItem.Sensors) {
                        if (sensor.SensorType == SensorType.Load)
                        {
                            usage = sensor.Value.Value;
                        }
                    }
                }
            }
            return usage;
        }

        public float GetTemperature()
        {
            float temperature = 0;
            Computer thisComputer = new Computer() { GPUEnabled = true };
            thisComputer.Open();
            foreach (var hardwareItem in thisComputer.Hardware)
            {
                if (hardwareItem.HardwareType == HardwareType.GpuNvidia)
                {
                    hardwareItem.Update();
                    foreach (IHardware subHardware in hardwareItem.SubHardware)
                        subHardware.Update();
                    foreach (var sensor in hardwareItem.Sensors)
                    {
                        if (sensor.SensorType == SensorType.Temperature)
                        {
                            temperature = sensor.Value.Value;
                        }
                    }
                }
            }
            return temperature;
        }

        public float GetFanDuty()
        {
            float FanDuty = 0;
            Computer thisComputer = new Computer() { GPUEnabled = true };
            thisComputer.Open();
            foreach (var hardwareItem in thisComputer.Hardware)
            {
                if (hardwareItem.HardwareType == HardwareType.GpuNvidia)
                {
                    hardwareItem.Update();
                    foreach (IHardware subHardware in hardwareItem.SubHardware)
                        subHardware.Update();
                    foreach (var sensor in hardwareItem.Sensors)
                    {
                        if (sensor.SensorType == SensorType.Fan)
                        {
                            FanDuty = sensor.Value.Value;
                        }
                    }
                }
            }
            return FanDuty;
        }

        //public float GetSpeed()
        //{
        //    float speed = 0;
        //    Computer thisComputer = new Computer() { GPUEnabled = true };
        //    thisComputer.Open();
        //    foreach (var hardwareItem in thisComputer.Hardware)
        //    {
        //        if (hardwareItem.HardwareType == HardwareType.GpuNvidia)
        //        {
        //            hardwareItem.Update();
        //            foreach (IHardware subHardware in hardwareItem.SubHardware)
        //                subHardware.Update();
        //            foreach (var sensor in hardwareItem.Sensors)
        //            {
        //                if (sensor.SensorType == SensorType.Clock)
        //                {
        //                    speed = sensor.Value.Value;
        //                }
        //            }
        //        }
        //    }
        //    return speed;
        //}
    }
}
