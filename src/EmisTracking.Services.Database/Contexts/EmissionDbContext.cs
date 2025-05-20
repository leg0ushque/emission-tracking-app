using EmisTracking.Services.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmisTracking.Services.Database.Contexts
{
    public class EmissionDbContext(DbContextOptions<EmissionDbContext> options)
        : DbContext(options)
    {
        public DbSet<Area> Areas { get; set; }
        public DbSet<Subdivision> Subdivisions { get; set; }
        public DbSet<Mode> Modes { get; set; }
        public DbSet<Methodology> Methodologies { get; set; }
        public DbSet<EmissionSource> EmissionSources { get; set; }
        public DbSet<OperatingTime> OperatingTimes { get; set; }
        public DbSet<Pollutant> Pollutants { get; set; }
        public DbSet<SourceSubstance> SourceSubstances { get; set; }
        public DbSet<MethodologyParameter> MethodologyParameters { get; set; }
        public DbSet<ConsumptionGroup> ConsumptionGroups { get; set; }
        public DbSet<SpecificIndicator> SpecificIndicators { get; set; }
        public DbSet<Consumption> Consumptions { get; set; }
        public DbSet<ParameterValue> ParameterValues { get; set; }
        public DbSet<GrossEmission> GrossEmissions { get; set; }
        public DbSet<TaxRate> TaxRates { get; set; }
        public DbSet<Tax> Taxes { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Subdivision>(entity =>
            {
                entity.HasOne(s => s.Area)
                    .WithMany()
                    .HasForeignKey(m => m.AreaId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<EmissionSource>(entity =>
            {
                entity.HasOne(e => e.Subdivision)
                    .WithMany()
                    .HasForeignKey(m => m.SubdivisionId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Mode)
                    .WithMany()
                    .HasForeignKey(m => m.ModeId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Methodology)
                    .WithMany()
                    .HasForeignKey(m => m.MethodologyId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<OperatingTime>(entity =>
            {
                entity.HasOne(o => o.EmissionSource)
                    .WithMany()
                    .HasForeignKey(m => m.EmissionSourceId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<SourceSubstance>(entity =>
            {
                entity.HasOne(s => s.EmissionSource)
                    .WithMany()
                    .HasForeignKey(m => m.EmissionSourceId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(s => s.Pollutant)
                    .WithMany()
                    .HasForeignKey(m => m.PollutantId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<MethodologyParameter>(entity =>
            {
                entity.HasOne(m => m.Methodology)
                    .WithMany()
                    .HasForeignKey(m => m.MethodologyId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<SpecificIndicator>(entity =>
            {
                entity.HasOne(g => g.ConsumptionGroup)
                    .WithMany()
                    .HasForeignKey(m => m.ConsumptionGroupId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(g => g.Pollutant)
                    .WithMany()
                    .HasForeignKey(m => m.PollutantId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Consumption>(entity =>
            {
                entity.HasOne(x => x.ConsumptionGroup)
                    .WithMany()
                    .HasForeignKey(m => m.ConsumptionGroupId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ParameterValue>(entity =>
            {
                entity.HasOne(p => p.MethodologyParameter)
                    .WithMany()
                    .HasForeignKey(m => m.MethodologyParameterId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(p => p.GrossEmission)
                    .WithMany()
                    .HasForeignKey(m => m.GrossEmissionId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GrossEmission>(entity =>
            {
                entity.HasOne(g => g.SourceSubstance)
                    .WithMany()
                    .HasForeignKey(m => m.SourceSubstanceId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(g => g.Methodology)
                    .WithMany()
                    .HasForeignKey(m => m.MethodologyId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(g => g.Tax)
                    .WithMany()
                    .HasForeignKey(m => m.TaxId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}