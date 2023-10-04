using AbTest.Data.Db;
using AbTest.Model;
using Microsoft.AspNetCore.Authentication;

namespace AbTest.RequestHandlers
{
    public class ExperimentsReportHandler : HandlerBase<RequestBase?, ExperimentsReportDto>
    {
        private readonly IApplicationRepository _repository;

        public ExperimentsReportHandler(IApplicationRepository repository)
        {
            _repository = repository;
        }

        public override async Task<ExperimentsReportDto> RequestLogic(RequestBase? requestDto = null)
        {
            var report = new ExperimentsReportDto();

            report.DeviceWithExperimentCount = await _repository.GetDevicesWithExperimentCount(); //Total number of devices with experiment

            var keys = await _repository.GetAllExperimentKeysAsync();

            report.Experiments = new List<ExperimentListItem>();
            report.ExperimentsDetails = new List<ExperimentsDetail>();

            foreach ( var key in keys)
            {
                var experimentValues = await _repository.GetExperimentValues(key.Key, includeSession: true);
                var experimentDictionary = experimentValues.ToDictionary(x => x.Value, x => x.Probability); 
                report.Experiments.Add(new ExperimentListItem { ExperimentKey = key.Key, ValueProbability = experimentDictionary } ); //Experiment name and dictionary Value - Probability
                report.ExperimentsDetails.Add(
                        new ExperimentsDetail
                        {
                            ExperimentKey = key.Key,
                            ExperimentValueFrequency = experimentValues.GroupBy(x => x.Value).ToDictionary(x => x.Key, x => x.SelectMany(x => x.Sessions).Count()) //Frequency for each value
                        }
                    );
            }

            return report;
        }
    }
}
