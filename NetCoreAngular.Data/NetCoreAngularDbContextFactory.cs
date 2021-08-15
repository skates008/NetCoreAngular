using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace NetCoreAngular.Data
{
    public class NetCoreAngularDbContextFactory : DesignTimeDbContextFactoryBase<NetCoreAngularDbContext>
    {
        

        protected override NetCoreAngularDbContext CreateNewInstance(DbContextOptions<NetCoreAngularDbContext> options)
        {
            return new NetCoreAngularDbContext(options);
        }

        protected override NetCoreAngularDbContext CreateNewInstance(DbContextOptions<NetCoreAngularDbContext> options, IConfiguration configuration)
        {
            return new NetCoreAngularDbContext(options);
        }
    }
}