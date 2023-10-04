using AbTest.Data.Db;
using AbTest.Data.Db.Entites;
using AbTest.Helpers;
using AbTest.Model;

namespace AbTest.Services
{
    public class ExperimentService
    {      
        private readonly IApplicationRepository _repository;

        public ExperimentService(IApplicationRepository applicationRepository)
        {    
            _repository = applicationRepository;
        }
      
        public  async Task<KeyValuePair<string, string>?> GetExperimentValue(string deviceToken, string experimentKey)
        {            
            var session = await _repository.GetSession(deviceToken);

            var experimentKeyRecord = await _repository.GetExperimentKeyAsync(experimentKey);

            if (session != null)
            {
                if (experimentKeyRecord?.Created >= session?.Created) //experiment key must be older than session
                    return null;

                var existingExperiment = session?.Experiments.FirstOrDefault(x => x.ExperimentKey.Key == experimentKey);
                if (existingExperiment != null)// if session has needed experiment - return it
                    return new KeyValuePair<string, string>(existingExperiment.ExperimentKey.Key, existingExperiment.Value);
            }
            else // else there is no session with current deviceId - add it
            {
                session = await _repository.AddSession(deviceToken);
                await _repository.SaveChangesAsync();
            }
              
            if (experimentKeyRecord == null)
                throw new Exception("Wrong experiment key");
          
            var experimentValues = await _repository.GetExperimentValues(experimentKey, includeSession: false);

            var randomExperimentCase = Randomizer.GetRandomExperimentValue(experimentValues);

            await _repository.AddExperiment(randomExperimentCase, session.Id);
            await _repository.SaveChangesAsync();

            return new KeyValuePair<string, string>(experimentKeyRecord.Key, randomExperimentCase.Value);
        }
    }
}
