using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableWidget.Models
{
    public class ProcessModel
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public long RamUsage { get; set; }
        //public string Path { get; set; } //it is incorrect concept
    }
}
