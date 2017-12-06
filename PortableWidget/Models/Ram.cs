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
        public int PagedPool { get; set; }
        public int NonPagedPool { get; set; }
        public UInt64 Capacity { get; set; }
    }
}