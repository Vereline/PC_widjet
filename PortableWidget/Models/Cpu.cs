namespace PortableWidget.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class Cpu : DbContext
    {
        public Cpu() : base("name=Cpu") {}
        
        public virtual DbSet<CpuModel> CpuModel { get; set; }
    }
    
    public class CpuModel
    {
        public string Id { get; set; }
        public float UsagePercentage { get; set; }
        //public int Temperature { get; set; } // i am not shure how to get this
        public float Speed { get; set; }
        public int CountOfProcesses { get; set; }
        public int CountOfThreads { get; set; }
    }
}