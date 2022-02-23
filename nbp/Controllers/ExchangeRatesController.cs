using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using nbp.core.commands;
using nbp.core.models;

namespace nbp.Controllers
{
    [ApiController]
    [Route("rates")]
    public class ExchangeRatesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ExchangeRatesController> _logger;

        public ExchangeRatesController(IMediator mediator, ILogger<ExchangeRatesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        
        [HttpGet]
        public async Task<IEnumerable<ExchangeRateTable>> Get(CancellationToken ct)
        {
            var requestCommand = new ExchangeRateTableRequestCommand();
            var exchangeRates = await _mediator.Send(requestCommand, ct);

            return exchangeRates;
        }
        
        [HttpGet]
        [Route("today")]
        public async Task<IEnumerable<ExchangeRateTable>> GetToday(CancellationToken ct)
        {
            var requestCommand = new ExchangeRateTableRequestCommand(){Date = DateTime.Now};
            var exchangeRates = await _mediator.Send(requestCommand, ct);

            return exchangeRates;
        }
        
        [HttpGet]
        [Route("last")]
        public async Task<IEnumerable<ExchangeRateTable>> GetLast(CancellationToken ct)
        {
            var requestCommand = new ExchangeRateTableRequestCommand(){Last = true};
            var exchangeRates = await _mediator.Send(requestCommand, ct);

            return exchangeRates;
        }
        
        [HttpGet]
        [Route("date/{date}")]
        public async Task<IEnumerable<ExchangeRateTable>> GetDate(DateTime date, CancellationToken ct)
        {
            var requestCommand = new ExchangeRateTableRequestCommand(){Date = date};
            var exchangeRates = await _mediator.Send(requestCommand, ct);

            return exchangeRates;
        }       
        [HttpGet]
        [Route("date/{startDate}/{endDate}")]
        public async Task<IEnumerable<ExchangeRateTable>> GetDateRange(DateTime startDate, DateTime endDate, CancellationToken ct)
        {
            var requestCommand = new ExchangeRateTableRequestCommand(){StartDate = startDate, EndDate = endDate};
            var exchangeRates = await _mediator.Send(requestCommand, ct);

            return exchangeRates;
        }
        
        
        [HttpGet]
        [Route("today/{currency}")]
        public async Task<IEnumerable<ExchangeRateTable>> GetTodayCurrency(string currency, CancellationToken ct)
        {
            var requestCommand = new ExchangeRateTableRequestCommand(){Date = DateTime.Now, Currency = currency};
            var exchangeRates = await _mediator.Send(requestCommand, ct);

            return exchangeRates;
        }
        [HttpGet]
        [Route("date/{date}/{currency}")]
        public async Task<IEnumerable<ExchangeRateTable>> GetDate(DateTime date, string currency, CancellationToken ct)
        {
            var requestCommand = new ExchangeRateTableRequestCommand(){Date = date, Currency = currency};
            var exchangeRates = await _mediator.Send(requestCommand, ct);

            return exchangeRates;
        }       
        [HttpGet]
        [Route("date/{startDate}/{endDate}/{currency}")]
        public async Task<IEnumerable<ExchangeRateTable>> GetDateRange(DateTime startDate, DateTime endDate, string currency, CancellationToken ct)
        {
            var requestCommand = new ExchangeRateTableRequestCommand(){StartDate = startDate, EndDate = endDate, Currency = currency};
            var exchangeRates = await _mediator.Send(requestCommand, ct);

            return exchangeRates;
        }
    }
}
