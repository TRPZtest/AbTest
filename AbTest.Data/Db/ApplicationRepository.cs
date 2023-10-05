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
    public class ApplicationRepository /*: IApplicationRepository*/
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
                .FirstOrDefaultAsync(x => x.Key == experimentKey);

            return key;
        }

        public async Task<ExperimentKey[]> GetAllExperimentKeysAsync()
        {
            var keys = await _dbContext.ExperimentKeys
                .AsNoTracking()
                .ToArrayAsync();

            return keys;
        }
     
        public async Task AddExperimentToSession(long experimentId, Session session)
        {
            var experiment = await _dbContext.Experiments.FirstAsync(x => x.Id == experimentId);
             experiment.Sessions.Add(session);
        }

        public async Task AddExperimentToSession(long experimentId, long sessionId)
        {
            var experiment = await _dbContext.Experiments.FirstAsync(x => x.Id == experimentId);
            var session = await _dbContext.Sessions.FirstAsync(x => x.Id == sessionId);

            experiment.Sessions.Add(session);
        }

        public async Task<Experiment[]> GetExperimentValues(string experimentKey, bool includeSession)
        {
            var experimentValues = _dbContext.Experiments
                .AsNoTracking()
                .Where(x => x.ExperimentKey.Key == experimentKey)
                .Include(x => x.ExperimentKey)
                .AsNoTracking();

            if (includeSession)
                experimentValues = experimentValues.Include(x => x.Sessions);

            return await experimentValues.ToArrayAsync(); ;
        }
       
        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }       

        public async Task<long> AddSession(Session session)
        {
            var result = await _dbContext.Sessions.AddAsync(session);

            return result.Entity.Id;
        }
    }
}
