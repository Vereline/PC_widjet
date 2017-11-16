using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableWidget.Models
{
    class ETHERNET_MODEL
    {
        //private Enum Current_state {good = 1, bad, normal, low };
        float Send { get; set; } //send in Kbps (current traffic)
        float Receive { get; set; } //receive in Kbps (current traffic)
        string SSID { get; set; } //router name
        string Adapter_name { get; set; } //adapter name
        string Connection_type { get; set; } //802.11n for example
        string Signal_strength { get; set; } //use good, low, normal
        string IPv4 { get; set; }
        string IPv6 { get; set; }
    }
}