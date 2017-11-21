using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableWidget.Models
{
    public class Disk
    {
        public float ReadSpeed { get; set; } 
        public float WriteSpeed { get; set; }
        public float AverageResponseTime { get; set; }
        public int Capacity { get; set; } 
        public int Formatted { get; set; }
    }
}
