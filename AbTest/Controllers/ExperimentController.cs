using AbTest.Helpers;
using AbTest.Model;
using AbTest.RequestHandlers;
using Microsoft.AspNetCore.Mvc;

namespace AbTest.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ExperimentController : ControllerBase
    {      
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(KeyValuePair<string, string>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ResponseCache(Duration = 10, NoStore = false, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new string[] { "device-token" })] //caching response for the same device-token //Duration = 10 is good for testing
        public async Task<IActionResult> ButtonColor([FromQuery]DeviceTokenRequestDto request, [FromServices]ButtonColorHandler requestHandler)
        {
            var result = await requestHandler.RequestLogicHttpResponse(request);
            return result;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(KeyValuePair<string, string>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ResponseCache(Duration = 10, NoStore = false, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new string[] { "device-token" })]
        public async Task<IActionResult> Price([FromQuery] DeviceTokenRequestDto request, [FromServices]PriceHandler requestHandler)
        {
            var result = await requestHandler.RequestLogicHttpResponse(request);
            return result;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ExperimentsReportDto))]
        public async Task<IActionResult> ExperimentReport([FromServices] ExperimentsReportHandler requestHandler)
        {
            var result = await requestHandler.RequestLogicHttpResponse(new RequestBase());
            return result;
        }
    }
}
