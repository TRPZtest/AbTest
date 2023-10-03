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
        }
     
        public async Task<Session?> GetSession(string deviceToken)
        {
            var session = await _dbContext.Sessions
                .AsNoTracking()
                .Include(x => x.Experiments)                                    
                        .ThenInclude(x => x.ExperimentKey)
                                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.DeviceToken == deviceToken);

            return session;               
        }

        public async Task<int> GetExperimentsCount()
        {
            var count = await _dbContext.Experiments
                .AsNoTracking()
                    .CountAsync();
            
            return count;
        }

        public async Task<int> GetDevicesWithExperimentCount()
        {
            var count = await _dbContext.Sessions
                .AsNoTracking()
                .CountAsync(x => x.Experiments.Any());

            return count;
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
        
        public async Task<Experiment[]> GetExperimentValues(string experimentKey)
        {
            var experimentValues = await _dbContext.Experiments
                .AsNoTracking()
                .Where(x => x.ExperimentKey.Key == experimentKey)
                .Include(x => x.ExperimentKey)
                .AsNoTracking()                          
                .ToArrayAsync();

            return experimentValues;
        }

        public async Task AddExperiment(Experiment experiment, long SessionId)
        {
            var session = await _dbContext.Sessions.FirstAsync(x => x.Id == SessionId);
             
            session.Experiments = new Experiment[] { experiment };

            await _dbContext.SaveChangesAsync();
        }
    }
}
