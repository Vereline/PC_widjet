using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableWidget.Models
{
    public enum SignalQuality {
        GOOD,
        LOW,
        NORMAL
    }

    public class Ethernet
    {
        public float SendPerSecond { get; set; }
        public float ReceivePerSecond { get; set; }
        public string SSID { get; set; }
        public string AdapterName { get; set; }
        public string ConnectionType { get; set; }
        public SignalQuality SignalStrength { get; set; }
        public string IPv4 { get; set; }
        public string IPv6 { get; set; }
    }
}