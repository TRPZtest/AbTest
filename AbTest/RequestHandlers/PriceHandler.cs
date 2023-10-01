using AbTest.Helpers;
using AbTest.Model;

namespace AbTest.RequestHandlers
{
    public class PriceHandler : HandlerBase<RequestBase, KeyValuePair<string, string>>
    {
        public override async Task<KeyValuePair<string, string>> RequestLogic(RequestBase requestDto)
        {
            var randomButtonColor = Randomizer.GetRandomCase<string>(new Dictionary<string, double>
                {
                    { "10",  0.75 },
                    { "20", 0.1 },
                    { "50", 0.5 },
                    { "5", 0.1 }
            });

            return new KeyValuePair<string, string>("price", randomButtonColor);
        }
    }
}
