using Microsoft.EntityFrameworkCore;
using NetCoreAngular.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreAnguar.Tests
{
    public abstract class TestBase
    {
        protected NetCoreAngularDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<NetCoreAngularDbContext>()
                                  .UseInMemoryDatabase(Guid.NewGuid().ToString())
                                  .Options;
            return new NetCoreAngularDbContext(options);
        }
    }
}
