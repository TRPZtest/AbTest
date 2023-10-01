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
        public ActionResult Button() 
        {
            var results = new List<string>();
            for (int i = 0; i < 100000; i++)
            {
                var randomButtonColor = Randomizer.GetRandomCase<string>(new Dictionary<string, double>
                {
                    { "#FF0000",  0.4 },
                    { "#00FF00", 1.2 },
                    { "#0000FF", 0.4 }
                });
                results.Add(randomButtonColor);
            }

            var counts = results.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());
       
            return Ok(counts);
        }

        [HttpGet]
        public async Task<IActionResult> ButtonColor([FromQuery]RequestBase request, [FromServices]ButtonColorHandler requestHandler)
        {
            var result = await requestHandler.RequestLogicHttpResponse(request);
            return result;
        }
    }
}
