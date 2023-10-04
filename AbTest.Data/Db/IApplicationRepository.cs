using AbTest.Data.Db.Entites;

namespace AbTest.Data.Db
{
    public interface IApplicationRepository : IDisposable
    {
        Task AddExperiment(Experiment experiment, long SessionId);
        Task<Session> AddSession(string deviceToken);
        Task<ExperimentKey[]> GetAllExperimentKeysAsync();
        Task<int> GetDevicesWithExperimentCount();
        Task<ExperimentKey> GetExperimentKeyAsync(string experimentKey);
        Task<int> GetExperimentsCount();
        Task<Experiment[]> GetExperimentValues(string experimentKey, bool includeSession);
        Task<Session?> GetSession(string deviceToken);
        Task<int> SaveChangesAsync();
    }
}