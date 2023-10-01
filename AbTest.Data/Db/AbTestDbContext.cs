using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbTest.Data.Db
{
    public class AbTestDbContext : DbContext
    {
        public AbTestDbContext(DbContextOptions options) : base(options) { }
    }
}
