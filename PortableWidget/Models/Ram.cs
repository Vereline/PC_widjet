namespace PortableWidget.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class Ram : DbContext
    {
        public Ram() : base("name=Ram") {}

        public virtual DbSet<RamModel> RamModel { get; set; }
    }

    public class RamModel
    {
        public string Id { get; set; }
        public float RamSpeed { get; set; }
        public float MemoryInUse { get; set; }
        public float MemoryCommited { get; set; }
        public float MemoryCached { get; set; }
        public int SlotsUsed { get; set; }
        public float PagedPool { get; set; }
        public float NonPagedPool { get; set; }
        public float Capacity { get; set; }
        public float Available { get; set; }
    }
}