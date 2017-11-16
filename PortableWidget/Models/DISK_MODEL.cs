using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableWidget.Models
{
    class DISK_MODEL
    {
        float Read_speed { get; set; } //current read speed
        float Write_speed { get; set; } //current write speed
        float Average_response_time { get; set; } //average response time in ms
        int Capacity { get; set; } //current disk capacity
        int Formatted { get; set; } //foramatted disk space
    }
}
