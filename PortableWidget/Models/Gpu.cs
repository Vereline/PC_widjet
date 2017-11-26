namespace PortableWidget.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class Gpu : DbContext
    {
        public Gpu() : base("name=Gpu") {}

        public virtual DbSet<GpuModel> GpuModel { get; set; }
    }

    public class GpuModel
    {
        public string Id { get; set; }
        public string GpuType { get; set; }
        public int Temperature { get; set; }
        public float Speed { get; set; }
        public float MemoryUsage { get; set; }
        public int CountOfThreads { get; set; }
        public int FanDutyPercentage { get; set; }
    }
}