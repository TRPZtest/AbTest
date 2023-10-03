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
        [ResponseCache(Duration = 5, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new string[] { "device-token" })]
        public async Task<IActionResult> ButtonColor([FromQuery]DeviceTokenRequestDto request, [FromServices]ButtonColorHandler requestHandler)
        {
            var result = await requestHandler.RequestLogicHttpResponse(request);
            return result;
        }

        [HttpGet]
        [ResponseCache(Duration = 5, NoStore = false, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new string[] { "device-token" })]
        public async Task<IActionResult> Price([FromQuery] DeviceTokenRequestDto request, [FromServices]PriceHandler requestHandler)
        {
            var result = await requestHandler.RequestLogicHttpResponse(request);
            return result;
        }
            
        public async Task<IActionResult> ExperimentReport([FromServices] ExperimentsReportHandler requestHandler)
        {
            var result = await requestHandler.RequestLogicHttpResponse(new RequestBase());
            return result;
        }
    }
}
