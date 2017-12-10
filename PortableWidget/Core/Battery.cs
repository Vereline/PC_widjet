using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using OpenHardwareMonitor.Collections;
using OpenHardwareMonitor.Hardware;
using System.Diagnostics;

namespace PortableWidget.Core
{
    public class Battery
    {
        //PerformanceCounter charge = new PerformanceCounter("Battery Status", "Remaining Capacity", true);

        public string GetDeviceId()
        {
            string ID = "";
            var searcher = new ManagementObjectSearcher(
              "select DeviceID from Win32_Battery");
            foreach (var item in searcher.Get())
            {
                ID = (string)item["DeviceID"];

            }
            return ID;
        }

        public string GetDevicePNPId()
        {
            string ID = "";
            var searcher = new ManagementObjectSearcher(
              "select PNPDeviceID from Win32_Battery");
            foreach (var item in searcher.Get())
            {
                ID = (string)item["PNPDeviceID"];

            }
            return ID;
        }

        //public float GetCharge()
        //{
        //    PerformanceCounter charge = new PerformanceCounter("Battery Status", "Remaining Capacity", true);
        //    float tmp = charge.NextValue();
        //    var Charge = (float)(Math.Round(tmp, 1));
        //    return Charge;
        //}

        public uint GetChargeValue()
        {
            uint charge = 0;
            var searcher = new ManagementObjectSearcher(
              "select BatteryStatus from Win32_Battery");
            foreach (var item in searcher.Get())
            {
                charge += (UInt16)item["BatteryStatus"];

            }
            return charge;
        }

        public string GetCharge()
        {
            uint value = GetChargeValue();
            switch(value)
            {
                case 1:
                    return "Other";
                case 2:
                    return "Unknown";
                case 3:
                    return "Fully Charged";
                case 4:
                    return "Low";
                case 5:
                    return "Critical";
                case 6:
                    return "Charging";
                case 7:
                    return "Charging and High";
                case 8:
                    return "Charging and Low";
                case 9:
                    return "Charging and Critical";
                case 10:
                    return "Undefined";
                case 11:
                    return "Partially Charged";
                case 12:
                    return "Install error";
                default:
                    return "";
            }

        }

        public float GetMaxCharge()
        {
            float maxcharge = 0;
            var searcher = new ManagementObjectSearcher(
              "select DesignCapacity from Win32_Battery");
            foreach (var item in searcher.Get())
            {
                if (item["DesignCapacity"] != null)
                    maxcharge = (float)item["DesignCapacity"];
                else
                    maxcharge = 0;

            }
            return maxcharge;
        }

        public float GetRechargeTime()
        {
            float recharge = 0;
            var searcher = new ManagementObjectSearcher(
              "select BatteryRechargeTime from Win32_Battery");
            if (searcher.Get() != null)
            {
                foreach (var item in searcher.Get())
                {
                    if (item["BatteryRechargeTime"] != null)
                        recharge += (float)item["BatteryRechargeTime"];
                    else
                        recharge = -1;
                }
            }
            else
                recharge = -1;
            return recharge;
        }
    }
}
