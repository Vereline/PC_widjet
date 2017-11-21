using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableWidget.Models
{
    public class Cpu
    {
        public float UsagePercentage { get; set; }
        public int Temperature { get; set; }
        public float Speed { get; set; }
        public int CountOfProcesses { get; set; }
        public int CountOfThreads { get; set; }
    }
}
