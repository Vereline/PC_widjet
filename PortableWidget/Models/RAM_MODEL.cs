using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableWidget.Models
{
    class RAM_MODEL
    {
        float RAM_speed { get; set; } //current RAM speed in MGz
        float Memory_in_use { get; set; } //current memory in use
        float Memory_commited { get; set; }
        float Memory_cached { get; set; }
        int Slots_used { get; set; } //amount of used slots
        int Paned_pool { get; set; }
        int Non_paged_pool { get; set; }
    }
}
