using EmisTracking.Services.Database.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace EmisTracking.Services.Database.Factories
{
    public class StudentDbContextFactory : IDesignTimeDbContextFactory<EmissionDbContext>
    {
        public EmissionDbContext CreateDbContext(string[] args)
        {
            var assemblyPath = Path.Combine(Directory.GetCurrentDirectory(), "..\\EmisTracking.WebApi");

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(assemblyPath)
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("EmissionDbConnectionString");

            var optionsBuilder = new DbContextOptionsBuilder<EmissionDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new EmissionDbContext(optionsBuilder.Options);
        }
    }
}
