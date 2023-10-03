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
                var experimentExist = session.Experiments.Any(x => x.ExperimentValue.ExperimentKey.Key == EXPERIMENT_KEY);
                if (experimentExist)
                    return null;
            }

            var experimentKey = await _repository.GetExperimentKeyAsync(EXPERIMENT_KEY);

            if (experimentKey == null)
                throw new Exception("Wrong experiment key");
            if (experimentKey?.Created > session.Created)
                return null;

            var experimentValues = await _repository.GetExperimentValues(EXPERIMENT_KEY);

            var randomButtonColor = Randomizer.GetRandomExperimentValue(experimentValues);

            await _repository.AddExperiment(new Experiment(randomButtonColor, session));

            return new KeyValuePair<string, string>(experimentKey.Key, randomButtonColor.Value);
        }
    }
}
