using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using onDemandProblem.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace onDemandProblem.Models
{
    public class ContextSeedData
    {
        public static async Task ensureSeedData(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var _config = serviceScope.ServiceProvider.GetRequiredService<IConfiguration>();
                var _context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                _context.Database.EnsureCreated();

                return;
            }
        }
    }
}
