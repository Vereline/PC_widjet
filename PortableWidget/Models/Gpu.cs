using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableWidget.Models
{
    public class Gpu
    {
        public string GpuModel { get; set; }
        public int Temperature { get; set; } 
        public float Speed { get; set; } 
        public float MemoryUsage { get; set; }
        public int CountOfThreads { get; set; }
        public int FanDutyPercentage { get; set; }
    }
}
