using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortableWidget.Models
{

    using System.Data.Entity;

    public class Battery : DbContext
    {
        public Battery() : base("name=Gpu") { }

        public virtual DbSet<BatteryModel> BatteryModel { get; set; }
    }

    public class BatteryModel
    {
        public string ID { get; set; }
        public string Charge { get; set; }
        public float FullCharge { get; set; }
        public float RechargeTime { get; set; }

    }
}
