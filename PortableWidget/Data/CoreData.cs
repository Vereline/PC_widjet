using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortableWidget.Models;

namespace PortableWidget.Data
{
    public static class CoreData
    {
        public static List<CpuModel> cpuData { get; set; } = new List<CpuModel>();
        public static List<DiskModel> diskData { get; set; } = new List<DiskModel>();
        public static List<EthernetModel> ethernetData { get; set; } = new List<EthernetModel>();
        public static List<GpuModel> gpuData { get; set; } = new List<GpuModel>();
        public static List<RamModel> ramData { get; set; } = new List<RamModel>();
        public static List<ProcessModel> processData { get; set; } = new List<ProcessModel>();
        public static List<ProcessModel> processNameData { get; set; } = new List<ProcessModel>();
        public static List<ProcessModel> processRamData { get; set; } = new List<ProcessModel>();
    }
}
