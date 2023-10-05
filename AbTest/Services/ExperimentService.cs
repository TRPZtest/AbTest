using AbTest.Data.Db;
using AbTest.Data.Db.Entites;
using AbTest.Helpers;
using AbTest.Model;

namespace AbTest.Services
{
    public class ExperimentService
    {      
        private readonly ApplicationRepository _repository;

        public ExperimentService(ApplicationRepository applicationRepository)
        {    
            _repository = applicationRepository;
        }
      
        public  async Task<KeyValuePair<string, string>?> GetExperimentValue(string deviceToken, string experimentKey)
        {           
            var session = await _repository.GetSession(deviceToken);

            var experimentKeyRecord = await _repository.GetExperimentKeyAsync(experimentKey);

            if (experimentKeyRecord == null)
                throw new Exception("Wrong experiment key");

            if (session != null)
            {
                if (experimentKeyRecord?.Created >= session?.Created) //experiment key must be older than session
                    return null;

                var existingExperiment = session?.Experiments.FirstOrDefault(x => x.ExperimentKey.Key == experimentKey);
                if (existingExperiment != null)// if session has needed experiment - return it
                    return new KeyValuePair<string, string>(existingExperiment.ExperimentKey.Key, existingExperiment.Value);

                var randomExperiment = await GetRandomExperimentAsync(experimentKey);

                await _repository.AddExperimentToSession(randomExperiment.Id, session.Id);

                await _repository.SaveChangesAsync();

                return new KeyValuePair<string, string>(experimentKey, randomExperiment.Value);
            }
            else // else there is no session with current deviceId - add it
            {
                var randomExperiment = await GetRandomExperimentAsync(experimentKey);
                             
                await _repository.AddExperimentToSession(randomExperiment.Id, new Session { Created = DateTime.Now, DeviceToken = deviceToken });

                await _repository.SaveChangesAsync();

                return new KeyValuePair<string, string>(experimentKey, randomExperiment.Value);
            }                                                                      
        }

        private async Task<Experiment> GetRandomExperimentAsync(string key)
        {
            var experimentValues = await _repository.GetExperimentValues(key, includeSession: false);

            var randomExperimentCase = Randomizer.GetRandomExperimentValue(experimentValues);

            return randomExperimentCase;
        }
    }
}
