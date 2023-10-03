using AbTest.Data.Db;
using AbTest.Data.Db.Entites;
using AbTest.Helpers;
using AbTest.Model;
using AbTest.Services;

namespace AbTest.RequestHandlers
{
    public class ButtonColorHandler : HandlerBase<DeviceTokenRequestDto, KeyValuePair<string, string>?>
    {
        private readonly ApplicationRepository _repository;

        public ButtonColorHandler(ApplicationRepository repository)
        {
            _repository = repository;
        }

        public override async Task<KeyValuePair<string, string>?> RequestLogic(DeviceTokenRequestDto requestDto)
        {
            var experimentKey = "button_color";
            var experimentService = new ExperimentsService(experimentKey, _repository);

            var result = await experimentService.GetExperimentValue(requestDto.DeviceToken);

            return result;
        }
    }
}
