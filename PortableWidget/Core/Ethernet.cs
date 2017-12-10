using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using System.Management;
using System.Diagnostics;

namespace PortableWidget.Core
{
    class Ethernet
    {

        PerformanceCounter EthernetSent = new PerformanceCounter("Network Adapter", "Bytes Sent/sec", "");
        //public long GetSentSpeed()
        //{
        //    long SentSpeed = 0;
        //    var searcher = new ManagementObjectSearcher(
        //      "select Speed from Win32_NetwokAdapter");
        //    foreach (var item in searcher.Get())
        //    {
        //        SentSpeed += (long)item["Speed"];
        //        //EthernetSent.InstanceName = SentSpeed;
        //        //SentSpeed += EthernetSent.NextValue();


        //    }
        //    //Console.WriteLine(SentSpeed);
        //    return SentSpeed;
        //}

        //public string GetDeviceId()
        //{
        //    string AdapterId = "";
        //    var searcher = new ManagementObjectSearcher(
        //      "select DeviceID from Win32_NetwokAdapter");
        //    foreach (var item in searcher.Get())
        //    {
        //        AdapterId = (string)item["DeviceID"];

        //    }
        //    return AdapterId;
        //}

        public string GetAdapterName()
        {
            string AdapterName = "";
            var searcher = new ManagementObjectSearcher(
              "select Name from Win32_NetworkAdapter");
            foreach (var item in searcher.Get())
            {
                AdapterName = (string)item["Name"];

            }
            return AdapterName;
        }

        public float GetNetworkSendSpeed()
        {
            float SendSpeed = 0;
            return SendSpeed;
        }
    }
}
