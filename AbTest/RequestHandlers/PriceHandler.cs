using AbTest.Data.Db;
using AbTest.Helpers;
using AbTest.Model;
using AbTest.Services;

namespace AbTest.RequestHandlers
{
    public class PriceHandler : HandlerBase<DeviceTokenRequestDto, KeyValuePair<string, string>?>
    {
        private readonly ExperimentService _experimentService;
        const string EXPERIMENT_KEY = "price";

        public PriceHandler(ExperimentService experimentService)
        {
            _experimentService = experimentService;
        }

        public override async Task<KeyValuePair<string, string>?> RequestLogic(DeviceTokenRequestDto requestDto)
        {            
            var result = await _experimentService.GetExperimentValue(requestDto.DeviceToken, EXPERIMENT_KEY);

            return result;
        }
    }
}
