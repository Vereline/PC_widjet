using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableWidget.Models
{
    class CPU_MODEL
    {
        float In_use { get; set; } // usage of CPU
        int Temperature { get; set; } //current CPU temperature
        float Speed { get; set; } //current core speed in GHz
        int Processes { get; set; } //current amount of processes
        int Threads { get; set; } //current amount of threads
    }
}
