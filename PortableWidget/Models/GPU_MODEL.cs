using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableWidget.Models
{
    class GPU_MODEL
    {
        string GPU_name { get; set} //Gpu name and model
        int Temperature { get; set; } //current GPU temperature
        float Speed { get; set; } //current core speed
        float Memory_use { get; set; } //usage of GPU memory
        float Threads { get; set; } //not shure that we'll use it
        int Fan_duty { get; set; } //fan speed in percent
    }
}
