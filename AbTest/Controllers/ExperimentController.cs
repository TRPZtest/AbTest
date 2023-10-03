﻿using AbTest.Helpers;
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
        public async Task<IActionResult> ButtonColor([FromQuery]ExperimentRequestDto request, [FromServices]ButtonColorHandler requestHandler)
        {
            var result = await requestHandler.RequestLogicHttpResponse(request);
            return result;
        }

        [HttpGet]
        public async Task<IActionResult> Price([FromQuery] ExperimentRequestDto request, [FromServices] PriceHandler requestHandler)
        {
            var result = await requestHandler.RequestLogicHttpResponse(request);
            return result;
        }
    }
}
