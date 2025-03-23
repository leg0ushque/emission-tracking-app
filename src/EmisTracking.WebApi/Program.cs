using EmisTracking.Mapping;
using EmisTracking.Services.Database.Contexts;
using EmisTracking.Services.Database.Repositories;
using EmisTracking.Services.Entities;
using EmisTracking.Services.Interfaces;
using EmisTracking.Services.JwtAuth;
using EmisTracking.Services.Options;
using EmisTracking.Services.Services;
using EmisTracking.WebApi.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;

namespace EmisTracking.WebApi
{
    public class Program
    {
        public IConfiguration configuration;

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            var userDbConnString = configuration.GetConnectionString("UserDbConnectionString");
            var emissionDbConnString = configuration.GetConnectionString("EmissionDbConnectionString");

            builder.Services.AddDbContext<UserDbContext>(options =>
                options.UseSqlServer(connectionString: userDbConnString));
            builder.Services.AddIdentity<SystemUser, IdentityRole>()
                .AddEntityFrameworkStores<UserDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.RegisterOptions<JwtTokenSettings>(
                JwtTokenSettings.JwtConfigSectionKey);

            ConfigureAuthentication(builder.Services, configuration);

            ConfigureRepositories(builder.Services, emissionDbConnString);
            ConfigureEntityServices(builder.Services);

            builder.Services.AddAutoMapper(options =>
                options.AddProfile<AutomapperProfile>());

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "EmisTracking.API",
                    Version = "v1",
                    Description = "Provides endpoints to interact with services.",
                });

                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Description = "Allows to attach a JWT token to the request to access the endpoints requiring authorization.",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                options.AddSecurityDefinition("Bearer", jwtSecurityScheme);
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });
            });

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        private static void ConfigureRepositories(IServiceCollection services, string emissionDbConnectionString)
        {
            services.AddDbContext<EmissionDbContext>(options =>
                options.UseSqlServer(connectionString: emissionDbConnectionString));

            services.AddScoped(typeof(IPaginatedRepository<>), typeof(PaginatedRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        }

        private static void ConfigureEntityServices(IServiceCollection services)
        {
            services.AddTransient<IEntityService<User>, UsersService>();
            services.AddTransient<IEntityService<Area>, AreasService>();
            services.AddTransient<IEntityService<Department>, DepartmentsService>();
            services.AddTransient<IEntityService<EmissionSource>, EmissionSourcesService>();
            services.AddTransient<IEntityService<EmissionSource>, EmissionSourcesService>();
            services.AddTransient<IEntityService<Regime>, RegimesService>();
        }

        private static void ConfigureAuthentication(IServiceCollection services, IConfiguration configuration)
        {
            var jwtOptionsSection = configuration.GetSection(JwtTokenSettings.JwtConfigSectionKey);
            var jwtIssuer = jwtOptionsSection.GetValue<string>(JwtTokenSettings.JwtIssuerConfigKey);
            var jwtAudience = jwtOptionsSection.GetValue<string>(JwtTokenSettings.JwtAudienceConfigKey);
            var jwtSecretKey = jwtOptionsSection.GetValue<string>(JwtTokenSettings.JwtSecretKeyConfigKey);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtIssuer,
                        ValidateAudience = true,
                        ValidAudience = jwtAudience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey)),
                        ValidateLifetime = false,
                    };
                });

            services.AddScoped<JwtTokenService>();
        }
    }
}
