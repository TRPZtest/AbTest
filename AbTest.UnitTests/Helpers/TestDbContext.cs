using AbTest.Data.Db;
using AbTest.Data.Db.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbTest.UnitTests.Helpers
{
    public class TestDbContext : AbTestDbContext
    {
        public TestDbContext(DbContextOptions options) : base(options)
        {
        }

        
    }
}
