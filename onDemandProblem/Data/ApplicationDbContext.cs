using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.SqlServer.Migrations.Internal;
using Microsoft.Extensions.Configuration;

namespace onDemandProblem.Data
{
    public class CustomAnnotationProvider : SqlServerMigrationsAnnotationProvider
    {
        public CustomAnnotationProvider(MigrationsAnnotationProviderDependencies dependencies)
            : base(dependencies)
        {
        }

        public override IEnumerable<IAnnotation> For(IProperty property)
        {
            var baseAnnotations = base.For(property);

            var annotation = property.FindAnnotation("ColumnOrder");

            return annotation == null
                ? baseAnnotations
                : baseAnnotations.Concat(new[] { annotation });
        }
    }

    public class ApplicationDbContext : IdentityDbContext
    {
        private IConfiguration _config;

        public ApplicationDbContext(IConfiguration config, DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            _config = config;

            string connectionString = _config.GetConnectionString("DefaultConnection");
            if (connectionString.Length > 10)
            {
                try
                {
                    Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connString = _config.GetConnectionString("DefaultConnection"); // Your connection string logic here

            try
            {
                base.OnConfiguring(optionsBuilder);
                optionsBuilder
                    .EnableSensitiveDataLogging(true)
                    .ReplaceService<IMigrationsAnnotationProvider, CustomAnnotationProvider>()
                    .UseSqlServer(connString);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }
    }
}
