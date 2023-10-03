using AbTest.Data.Db;
using AbTest.Data.Db.Entites;
using AbTest.Helpers;
using AbTest.Model;
using AbTest.Services;

namespace AbTest.RequestHandlers
{
    public class ButtonColorHandler : HandlerBase<DeviceTokenRequestDto, KeyValuePair<string, string>?>
    {
        private readonly ExperimentService _experimentService;
        const string EXPERIMENT_KEY = "button_color";

        public ButtonColorHandler(ExperimentService experimentService)
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
