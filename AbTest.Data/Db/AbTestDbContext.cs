using AbTest.Data.Db.Entites;
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
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Experiment> Experiments { get; set; }
        public DbSet<ExperimentKey> ExperimentKeys { get; set; }
        public DbSet<ExperimentValue> ExperimentValues { get; set; }
        public AbTestDbContext(DbContextOptions options) : base(options) { }
    }
}
