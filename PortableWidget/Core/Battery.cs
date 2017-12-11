using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Management.Instrumentation;
using System.Threading;
using System.Windows.Forms;
namespace PortableWidget.Core
{
    public class BatteryCore
    {

        private string ComputerName { get; set; }
        public List<string> ResultInfo { get; set; }
        //public BatteryCore()
        //{
        //    ComputerName = SystemInformation.ComputerName;
        //    ResultInfo = new List<string>();
        //}
        public List<string> GetBatteryInformation()
        {
            ComputerName = SystemInformation.ComputerName;
            ResultInfo = new List<string>();
            //while (true)
            //{
                string serverName = @"\\" + ComputerName + @"\root\cimv2";
                ManagementScope scope = new ManagementScope(serverName);
                SelectQuery query = new SelectQuery("SELECT * FROM Win32_Battery");
                try
                {
                    using (var searcher = new ManagementObjectSearcher(scope, query))
                    {
                        ManagementObjectCollection info = searcher.Get();
                        var batteryAvailabilities = (from ManagementObject value in info select value["Availability"].ToString()).ToList();
                        foreach (var value in batteryAvailabilities)
                        {
                            //string
                            int status = Int32.Parse(value);
                            string status_value = CheckBatteryAvailability(status);
                            ResultInfo.Add(status_value);
                        }
                        var batteryStatuses = (from ManagementObject value in info select value["BatteryStatus"].ToString()).ToList();
                        foreach (var value in batteryStatuses)
                        {
                            //string
                            int status = Int32.Parse(value);
                            string status_value = CheckBatteryStatus(status);
                            ResultInfo.Add(status_value);
                        }
                        var batteryVolts = (from ManagementObject value in info select value["DesignVoltage"].ToString()).ToList();
                        foreach (var value in batteryVolts)
                        {
                            int millivolt;
                            Int32.TryParse(value, out millivolt);
                            double volt = (double)millivolt / 1000;
                            ResultInfo.Add(volt.ToString());
                        }
                        var batteryCharges = (from ManagementObject value in info select value["EstimatedChargeRemaining"].ToString()).ToList();
                        foreach (var value in batteryCharges)
                        {
                            ResultInfo.Add(value.ToString());
                        }
                        var batteryInfo = (from ManagementObject value in info select value["EstimatedRunTime"].ToString()).ToList();
                        foreach (var value in batteryInfo)
                        {
                            ResultInfo.Add(value.ToString());
                        }
                    }
                }
                catch (ManagementException e)
                {
                    return ResultInfo;
                }
                //foreach (var info in ResultInfo)
                //{
                //    System.Console.WriteLine("INFO {0}", info);
                //}
                //System.Console.WriteLine();
                return ResultInfo;
                ResultInfo.Clear();
                Thread.Sleep(1000);
            //}

        }
        private string CheckBatteryAvailability(int status)
        {
            switch (status)
            {
                case 1:
                    return "Other";
                case 2:
                    return "Unknown";
                case 3:
                    return "Running or Full Power";
                case 4:
                    return "Warning";
                case 5:
                    return "In Test";
                case 6:
                    return "Not Applicable";
                case 7:
                    return "Power Off";
                case 8:
                    return "Off Line";
                case 9:
                    return "Off Duty";
                case 10:
                    return "Degraded";
                case 11:
                    return "Not Installed";
                case 12:
                    return "Install Error";
                case 13:
                    return "The device is known to be in a power save mode, but its exact status is unknown";
                case 14:
                    return "The device is in a power save state but still functioning, and may exhibit degraded performance";
                case 15:
                    return "The device is not functioning, but could be brought to full power quickly. ";
                case 16:
                    return "Power Cycle";
                case 17:
                    return "The device is in a warning state, though also in a power save mode. ";
                default:
                    return "";
            }
        }
        private string CheckBatteryStatus(int status)
        {
            switch (status)
            {
                case 1:
                    return "The battery is discharging";
                case 2:
                    return "The system has access to AC";
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
                default:
                    return "";
            }
        }
    }


    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        Battery battery = new Battery();

    //        Thread thread = new Thread(battery.GetBatteryInformation);
    //        thread.Start();

    //    }
    //}
}

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Management;
//using OpenHardwareMonitor.Collections;
//using OpenHardwareMonitor.Hardware;
//using System.Diagnostics;

//namespace PortableWidget.Core
//{
//    public class Battery
//    {
//        //PerformanceCounter charge = new PerformanceCounter("Battery Status", "Remaining Capacity", true);

//        public string GetDeviceId()
//        {
//            string ID = "";
//            var searcher = new ManagementObjectSearcher(
//              "select DeviceID from Win32_Battery");
//            foreach (var item in searcher.Get())
//            {
//                ID = (string)item["DeviceID"];

//            }
//            return ID;
//        }

//        public string GetDevicePNPId()
//        {
//            string ID = "";
//            var searcher = new ManagementObjectSearcher(
//              "select PNPDeviceID from Win32_Battery");
//            foreach (var item in searcher.Get())
//            {
//                ID = (string)item["PNPDeviceID"];

//            }
//            return ID;
//        }

//        //public float GetCharge()
//        //{
//        //    PerformanceCounter charge = new PerformanceCounter("Battery Status", "Remaining Capacity", true);
//        //    float tmp = charge.NextValue();
//        //    var Charge = (float)(Math.Round(tmp, 1));
//        //    return Charge;
//        //}

//        public uint GetChargeValue()
//        {
//            uint charge = 0;
//            var searcher = new ManagementObjectSearcher(
//              "select BatteryStatus from Win32_Battery");
//            foreach (var item in searcher.Get())
//            {
//                charge += (UInt16)item["BatteryStatus"];

//            }
//            return charge;
//        }

//        public string GetCharge()
//        {
//            uint value = GetChargeValue();
//            switch(value)
//            {
//                case 1:
//                    return "Discharging";
//                case 2:
//                    return "Unknown";
//                case 3:
//                    return "Fully Charged";
//                case 4:
//                    return "Low";
//                case 5:
//                    return "Critical";
//                case 6:
//                    return "Charging";
//                case 7:
//                    return "Charging and High";
//                case 8:
//                    return "Charging and Low";
//                case 9:
//                    return "Charging and Critical";
//                case 10:
//                    return "Undefined";
//                case 11:
//                    return "Partially Charged";
//                case 12:
//                    return "Install error";
//                default:
//                    return "";
//            }

//        }

//        public float GetMaxCharge()
//        {
//            float maxcharge = 0;
//            var searcher = new ManagementObjectSearcher(
//              "select DesignCapacity from Win32_Battery");
//            foreach (var item in searcher.Get())
//            {
//                if (item["DesignCapacity"] != null)
//                    maxcharge = (float)item["DesignCapacity"];
//                else
//                    maxcharge = 0;

//            }
//            return maxcharge;
//        }

//        public float GetRechargeTime()
//        {
//            float recharge = 0;
//            var searcher = new ManagementObjectSearcher(
//              "select BatteryRechargeTime from Win32_Battery");
//            if (searcher.Get() != null)
//            {
//                foreach (var item in searcher.Get())
//                {
//                    if (item["BatteryRechargeTime"] != null)
//                        recharge += (float)item["BatteryRechargeTime"];
//                    else
//                        recharge = -1;
//                }
//            }
//            else
//                recharge = -1;
//            return recharge;
//        }
//    }
//}
