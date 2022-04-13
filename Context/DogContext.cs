using Microsoft.EntityFrameworkCore;
using MicroSvc01Core.Models;

namespace MicroSvc01Core.Context
{
    public class DogContext : DbContext
    {
        public DogContext()
        {
        }

        public DogContext(DbContextOptions<DogContext> options)
            : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //        optionsBuilder.UseSqlServer("server=(localdb)\\MSSQLLocalDB; database=MicroSvc01Core; Integrated Security=true; MultipleActiveResultSets=true; Application Name=MicroSvc01Core;");
        //}

        public virtual DbSet<Dog> Dog { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dog>(entity =>
            {
                entity.ToTable("Dog", "dbo");

                entity.HasKey(e => e.ID);

                entity.Property(e => e.Breed)
                    .IsRequired(true)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired(true)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sex)
                    .IsRequired(true)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Age)
                    .IsUnicode(false);
            });
        }
    }
}
