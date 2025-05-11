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
                entity.HasOne<Area>()
                    .WithMany()
                    .HasForeignKey(m => m.AreaId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<EmissionSource>(entity =>
            {
                entity.HasOne<Subdivision>()
                    .WithMany()
                    .HasForeignKey(m => m.SubdivisionId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne<Mode>()
                    .WithMany()
                    .HasForeignKey(m => m.ModeId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne<Methodology>()
                    .WithMany()
                    .HasForeignKey(m => m.MethodologyId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<OperatingTime>(entity =>
            {
                entity.HasOne<EmissionSource>()
                    .WithMany()
                    .HasForeignKey(m => m.EmissionSourceId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<SourceSubstance>(entity =>
            {
                entity.HasOne<EmissionSource>()
                    .WithMany()
                    .HasForeignKey(m => m.EmissionSourceId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne<Pollutant>()
                    .WithMany()
                    .HasForeignKey(m => m.PollutantId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<MethodologyParameter>(entity =>
            {
                entity.HasOne<Methodology>()
                    .WithMany()
                    .HasForeignKey(m => m.MethodologyId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<SpecificIndicator>(entity =>
            {
                entity.HasOne<ConsumptionGroup>()
                    .WithMany()
                    .HasForeignKey(m => m.ConsumptionGroupId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne<Pollutant>()
                    .WithMany()
                    .HasForeignKey(m => m.PollutantId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Consumption>(entity =>
            {
                entity.HasOne<ConsumptionGroup>()
                    .WithMany()
                    .HasForeignKey(m => m.ConsumptionGroupId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ParameterValue>(entity =>
            {
                entity.HasOne<MethodologyParameter>()
                    .WithMany()
                    .HasForeignKey(m => m.MethodologyParameterId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne<GrossEmission>()
                    .WithMany()
                    .HasForeignKey(m => m.GrossEmissionId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<GrossEmission>(entity =>
            {
                entity.HasOne<SourceSubstance>()
                    .WithMany()
                    .HasForeignKey(m => m.SourceSubstanceId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne<Methodology>()
                    .WithMany()
                    .HasForeignKey(m => m.MethodologyId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne<Tax>()
                    .WithMany()
                    .HasForeignKey(m => m.TaxId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Tax>(entity =>
            {
                entity.HasOne<TaxRate>()
                    .WithMany()
                    .HasForeignKey(m => m.TaxRateId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}