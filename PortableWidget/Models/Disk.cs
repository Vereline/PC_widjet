namespace PortableWidget.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class Disk : DbContext
    {
        public Disk() : base("name=Disk") {}
        public virtual DbSet<DiskModel> DiskModel { get; set; }
    }

    public class DiskModel
    {
        public string Id { get; set; }
        public float ReadSpeed { get; set; }
        public float WriteSpeed { get; set; }
        public float AverageResponseTime { get; set; } // it is for some time. I'll turn it back later
        public ulong Capacity { get; set; }
        public float ActiveTime { get; set; }
        //public int Formatted { get; set; }
    }
}