using AbTest.Data.Db;
using AbTest.Data.Db.Entites;
using AbTest.UnitTests.Helpers;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace AbTest.UnitTests.SqlLiteDb
{
    public static class DbHelper
    {           
        public static DbContextOptions<AbTestDbContext> GetDbContextOptions()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();

            // These options will be used by the context instances in this test suite, including the connection opened above.
            var contextOptions = new DbContextOptionsBuilder<AbTestDbContext>()
                .UseSqlite(connection)
                .Options;

            return contextOptions;
        }
    }
}
