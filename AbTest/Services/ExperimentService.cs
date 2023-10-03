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

            if (session == null)// if there is no session with current deviceId - add it
                session = await _repository.AddSession(deviceToken);
            else
            {
                var existingExperiment = session.Experiments.FirstOrDefault(x => x.ExperimentKey.Key == experimentKey);
                if (existingExperiment != null)// if session has needed experiment - return it
                    return new KeyValuePair<string, string>(existingExperiment.ExperimentKey.Key, existingExperiment.Value);
            }

            var experimentKeyRecord = await _repository.GetExperimentKeyAsync(experimentKey);

            if (experimentKeyRecord == null)
                throw new Exception("Wrong experiment key");
            if (experimentKeyRecord?.Created > session.Created) //experiment key must be older than session
                return null;

            var experimentValues = await _repository.GetExperimentValues(experimentKey);

            var randomExperimentCase = Randomizer.GetRandomExperimentValue(experimentValues);

            await _repository.AddExperiment(randomExperimentCase, session.Id);

            return new KeyValuePair<string, string>(experimentKeyRecord.Key, randomExperimentCase.Value);
        }
    }
}
