using AbTest.Data.Db;
using AbTest.Helpers;
using AbTest.Model;

namespace AbTest.RequestHandlers
{
    public class ButtonColorHandler : HandlerBase<RequestBase, KeyValuePair<string, string>>
    {
        private readonly ApplicationRepository _repository;

        public ButtonColorHandler(ApplicationRepository repository)
        {
            _repository = repository;
        }

        public override async Task<KeyValuePair<string, string>> RequestLogic(RequestBase requestDto)
        {
            var isExist = await _repository.IsSessionExistAsync(requestDto.DeviceId);

            if (!isExist) 
            {
                await _repository.AddSession(requestDto.DeviceId);
            }
            var randomButtonColor = Randomizer.GetRandomCase<string>(new Dictionary<string, double>
                {
                    { "#FF0000",  0.4 },
                    { "#00FF00", 1.2 },
                    { "#0000FF", 0.4 }
            });

            return new KeyValuePair<string, string>("button_color", randomButtonColor);
        }
    }
}
