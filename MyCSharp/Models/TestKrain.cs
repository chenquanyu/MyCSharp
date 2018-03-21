namespace MyCSharp.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TestKrainContext : DbContext
    {
        public TestKrainContext()
            : base("name=TestKrain")
        {
        }

        public virtual DbSet<Product> Product { get; set; }

        public virtual DbSet<Player> Player { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(e => e.varchar20)
                .IsUnicode(false);
        }
    }
}
