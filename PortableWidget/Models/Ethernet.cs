namespace PortableWidget.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class Ethernet : DbContext
    {
        public Ethernet() : base("name=Ethernet") {}
        public virtual DbSet<EthernetModel> EthernetModel { get; set; }
    }

    public enum SignalQuality
    {
        GOOD,
        LOW,
        NORMAL
    }

    public class EthernetModel
    {
        public string Id { get; set; }
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