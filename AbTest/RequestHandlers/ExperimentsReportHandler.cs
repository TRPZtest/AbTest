using AbTest.Data.Db;
using AbTest.Model;
using Microsoft.AspNetCore.Authentication;

namespace AbTest.RequestHandlers
{
    public class ExperimentsReportHandler : HandlerBase<RequestBase?, ExperimentsReport>
    {
        private readonly ApplicationRepository _repository;

        public ExperimentsReportHandler(ApplicationRepository repository)
        {
            _repository = repository;
        }

        public override Task<ExperimentsReport> RequestLogic(RequestBase? requestDto = null)
        {
            //var 
            throw new NotImplementedException();
        }
    }
}
