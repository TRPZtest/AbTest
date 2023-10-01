using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
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

        public async Task<bool> IsSessionExistAsync(string deviceId)
        {
            var deviceIdParam = new SqlParameter("@DeviceId", deviceId);
            var countParam = new SqlParameter("@Count", System.Data.SqlDbType.Int) { Direction = System.Data.ParameterDirection.Output };
            _dbContext.Database.ExecuteSqlRaw("SELECT @Count =  count(Id) FROM dbo.Sessions s WHERE s.DeviceId = @DeviceId", deviceIdParam, countParam);

            var isExist = (int)countParam.Value > 0;

            return isExist;
        }

        public async Task AddSession(string deviceId)
        {
            var deviceIdParam = new SqlParameter("@DeviceId", deviceId);
            await _dbContext.Database.ExecuteSqlRawAsync("INSERT INTO dbo.Sessions VALUES (@DeviceId)", deviceIdParam);
        }

        public async Task AddExperiment(string deviceId, long experimentKeyId, long experimentValueId)
        {
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@DeviceId", deviceId),
                new SqlParameter("@ExperimentKeyId", experimentKeyId),
                new SqlParameter("@ExperimentValueId", experimentValueId)
            };

            await _dbContext.Database.ExecuteSqlRawAsync("INSERT INTO dbo.Experiments VALUES()", parameters);
        }
    }
}
