﻿using AbTest.Data.Db;
using AbTest.Helpers;
using AbTest.Model;
using AbTest.Services;

namespace AbTest.RequestHandlers
{
    public class PriceHandler : HandlerBase<RequestBase, KeyValuePair<string, string>?>
    {
        private readonly ApplicationRepository _repository;

        public PriceHandler(ApplicationRepository repository, ILogger logger) : base(logger)
        {
            _repository = repository;
        }

        public override async Task<KeyValuePair<string, string>?> RequestLogic(RequestBase requestDto)
        {
            var experimentKey = "price";
            var experimentService = new ExperimentsService(experimentKey, _repository);

            var result = await experimentService.GetExperimentValue(requestDto.DeviceToken);

            return result;
        }
    }
}