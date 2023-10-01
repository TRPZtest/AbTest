using Microsoft.AspNetCore.Mvc;

namespace AbTest.RequestHandlers
{
    public abstract class HandlerBase <TReq, TResp>
    {
        private readonly ILogger? _logger;

        public abstract Task<TResp> RequestLogic(TReq requestDto);


        protected HandlerBase(ILogger? logger = null) 
        {
            _logger = logger;
        }

        public async Task<IActionResult> RequestLogicHttpResponse(TReq requestDto)
        {
            try
            {
                var result = await RequestLogic(requestDto);

                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.Message);

                return new ObjectResult(new { message = ex.Message }) { StatusCode = 500 };
            }
        }
    }
}
