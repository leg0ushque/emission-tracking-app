using EmisTracking.Services.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmisTracking.Services.Database.Contexts
{
    public class EmissionDbContext(DbContextOptions<EmissionDbContext> options)
        : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasOne<Area>()
                    .WithMany()
                    .HasForeignKey(m => m.AreaId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<EmissionSource>(entity =>
            {
                entity.HasOne<Department>()
                    .WithMany()
                    .HasForeignKey(m => m.DepartmentId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne<Regime>()
                    .WithMany()
                    .HasForeignKey(m => m.RegimeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

        public DbSet<Area> Areas { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<EmissionSource> EmissionSources { get; set; }

        public DbSet<Regime> Regimes { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
