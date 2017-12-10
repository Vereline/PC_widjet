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
        //public int GpuDriverType { get; set; }
        public string GpuDriverVersion { get; set; }
        public float Temperature { get; set; }
        public float Speed { get; set; }
        public float MemoryUsage { get; set; }
        //public int CountOfThreads { get; set; } //we don't need this (this is not importent)
        public float FanDutyPercentage { get; set; }
        public float AdapterRam { get; set; }
    }
}