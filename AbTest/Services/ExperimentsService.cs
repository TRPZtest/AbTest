using AbTest.Data.Db;
using AbTest.Data.Db.Entites;
using AbTest.Helpers;
using AbTest.Model;

namespace AbTest.Services
{
    public class ExperimentsService
    {
        private readonly string EXPERIMENT_KEY;
        private readonly ApplicationRepository _repository;

        public ExperimentsService(string experimentKey, ApplicationRepository applicationRepository)
        {
            EXPERIMENT_KEY = experimentKey;
            _repository = applicationRepository;
        }
      
        public  async Task<KeyValuePair<string, string>?> GetExperimentValue(string deviceToken)
        {            
            var session = await _repository.GetSession(deviceToken);

            if (session == null)
                session = await _repository.AddSession(deviceToken);
            else
            {
                var existingExperiment = session.Experiments.FirstOrDefault(x => x.ExperimentValue.ExperimentKey.Key == EXPERIMENT_KEY);
                if (existingExperiment != null)
                    return new KeyValuePair<string, string>(existingExperiment.ExperimentValue.ExperimentKey.Key, existingExperiment.ExperimentValue.Value);
            }

            var experimentKeyRecord = await _repository.GetExperimentKeyAsync(EXPERIMENT_KEY);

            if (experimentKeyRecord == null)
                throw new Exception("Wrong experiment key");
            if (experimentKeyRecord?.Created > session.Created)
                return null;

            var experimentValues = await _repository.GetExperimentValues(EXPERIMENT_KEY);

            var randomValue = Randomizer.GetRandomExperimentValue(experimentValues);

            await _repository.AddExperiment(new Experiment(randomValue, session));

            return new KeyValuePair<string, string>(experimentKeyRecord.Key, randomValue.Value);
        }
    }
}
