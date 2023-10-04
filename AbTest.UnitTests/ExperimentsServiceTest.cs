using AbTest.Data.Db;
using AbTest.Data.Db.Entites;
using AbTest.Helpers;
using AbTest.Services;
using AbTest.UnitTests.Helpers;
using AbTest.UnitTests.SqlLiteDb;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace AbTest.UnitTests
{
    public class ExperimentsServiceTest
    {                 
        [Fact]
        public async Task CheckOldDivceId()
        {
            var service = GetService();

            var result = await service.GetExperimentValue("old_token1", "button_color");

            Assert.Null(result); //must be null because of old token
        }

        [Fact]
        public async Task CheckAlwaysTheSameResults()
        {
            var service = GetService();

            var token = "new_token";

            var firstResult = await service.GetExperimentValue(token, "button_color");
            for (int i = 0; i < 5; i++)
            {
                var currentResult = await service.GetExperimentValue(token, "button_color");
                Assert.Equal(firstResult.Value, currentResult.Value); //result should always be the same
            }         
        }

        [Fact]
        public async Task CheckTokenWithExistedExperimentResults()
        {
            var service = GetService();

            var token = "token_with_experiment"; // should has price: 20

            var firstResult = await service.GetExperimentValue(token, "price");

            Assert.Equal("20", firstResult.Value.Value);
            Assert.Equal("price", firstResult.Value.Key);

            for (int i = 0; i < 5; i++)
            {
                var currentResult = await service.GetExperimentValue(token, "price");
                Assert.Equal(firstResult.Value, currentResult.Value); //result should always be the same
            }
        }

        private ExperimentService GetService()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();

            // These options will be used by the context instances in this test suite, including the connection opened above.
            var contextOptions = new DbContextOptionsBuilder<AbTestDbContext>()
                .UseSqlite(connection)
                .Options;

            // Create the schema and seed some data
            var context = new AbTestDbContext(contextOptions);

            context.Database.EnsureCreated();

            context.Sessions.AddRange(
                new Session { DeviceToken = "old_token1", Created = DateTime.Now.AddDays(-1) },
                new Session { DeviceToken = "old_token2", Created = DateTime.Now.AddDays(-1) }
            ); //some old tokens

            context.ExperimentKeys.Add(new ExperimentKey { Created = DateTime.Now, Key = "button_color", Experiments = TestDataHelper.GetButtonExperiments() });
            context.ExperimentKeys.Add(new ExperimentKey { Created = DateTime.Now.AddMinutes(-30), Key = "price", Experiments = TestDataHelper.GetPriceExperiments() });

            context.SaveChanges();

            var experiment = context.ExperimentKeys.First(x => x.Key == "price").Experiments.First(x => x.Value == "20");
            experiment.Sessions = new Session[] { new Session { DeviceToken = "token_with_experiment", Created = DateTime.Now }, };

            context.SaveChanges();

            context.ChangeTracker.Clear();

            return new ExperimentService(new ApplicationRepository(context));
        }
    }
}
