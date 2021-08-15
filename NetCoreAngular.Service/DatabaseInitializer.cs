using NetCoreAngular.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace NetCoreAngular.Service
{
    public interface IDatabaseInitializer
    {
        Task SeedAsync();
    }

    public class DatabaseInitializer : IDatabaseInitializer
    {
        private readonly NetCoreAngularDbContext _context;
        private readonly ILogger _logger;


        public DatabaseInitializer(NetCoreAngularDbContext context, ILogger<DatabaseInitializer> logger)
        {
            _context = context;
            _logger = logger;
        }

        public virtual async Task SeedAsync()
        {
            try
            {
                await _context.Database.MigrateAsync().ConfigureAwait(false);


            }

            catch (Exception ex)
            {

            }
        }



    }
}