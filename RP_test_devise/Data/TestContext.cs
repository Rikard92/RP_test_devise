using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace RP_test_devise.Models
{
    public partial class TestContext : DbContext
    {
        public TestContext()
        {

        }
        public TestContext(DbContextOptions<TestContext> options) 
            :base(options)
        {
        }
        // this is the DB Context that stores TestDevises
        public virtual DbSet<TestDevise> testDevises { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // for converting to database
            modelBuilder.Entity<TestDevise>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Name).ValueGeneratedNever().HasMaxLength(20).IsFixedLength();                 
                entity.Property(e => e.SecretVal).HasMaxLength(50).IsFixedLength();
            });

        }

    }

}
