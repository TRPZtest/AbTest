using Microsoft.AspNetCore.Mvc;

namespace AbTest.RequestHandlers
{
    public abstract class HandlerBase <TReq, TResp>
    {
        public abstract Task<TResp> RequestLogic(TReq requestDto);
      
        public async Task<IActionResult> RequestLogicHttpResponse(TReq requestDto)
        {
            try
            {
                var result = await RequestLogic(requestDto);

                if (result == null)
                    return new NoContentResult();

                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {               
                return new ObjectResult(new { message = ex.Message }) { StatusCode = 500 };
            }
        }
    }
}
