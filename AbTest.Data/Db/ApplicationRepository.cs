using AbTest.Data.Db.Entites;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AbTest.Data.Db
{
    public class ApplicationRepository
    {
        private readonly AbTestDbContext _dbContext;

        public ApplicationRepository(AbTestDbContext dbContext) 
        { 
            _dbContext = dbContext;
            _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
     
        public async Task<Session?> GetSession(string deviceToken)
        {
            var session = await _dbContext.Sessions
                .AsNoTracking()
                .Include(x => x.Experiments)                
                    .ThenInclude(x => x.ExperimentValue)
                        .ThenInclude(x => x.ExperimentKey)
                            .AsNoTracking()
                .FirstOrDefaultAsync(x => x.DeviceToken == deviceToken);

            return session;               
        }

        public async Task<ExperimentKey> GetExperimentKeyAsync(string experimentKey)
        {
            var key = await _dbContext.ExperimentKeys
                .AsNoTracking()
                .FirstAsync(x => x.Key == experimentKey);

            return key;
        }

        public async Task<Session> AddSession(string deviceToken)
        {
            var session = new Session { DeviceToken = deviceToken, Created = DateTime.Now };
            await _dbContext.Sessions.AddAsync(session);
            await _dbContext.SaveChangesAsync();

            return session;
        }
        
        public async Task<ExperimentValue[]> GetExperimentValues(string experimentKey)
        {
            var experimentValues = await _dbContext.ExperimentValues
                .AsNoTracking()
                .Include(x => x.ExperimentKey)
                    .AsNoTracking()
                .Where(x => x.ExperimentKey.Key == experimentKey)                
                .ToArrayAsync();

            return experimentValues;
        }

        public async Task AddExperiment(Experiment experiment)
        {          
            _dbContext.Experiments.Attach(experiment);

            await _dbContext.SaveChangesAsync();
        }
    }
}
