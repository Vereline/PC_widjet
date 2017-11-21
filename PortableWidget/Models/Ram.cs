using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableWidget.Models
{
    public class Ram
    {
        public float RamSpeed { get; set; }
        public float MemoryInUse { get; set; }
        public float MemoryCommited { get; set; }
        public float MemoryCached { get; set; }
        public int SlotsUsed { get; set; }
        public int PanedPool { get; set; }
        public int NonPagedPool { get; set; }
    }
}
