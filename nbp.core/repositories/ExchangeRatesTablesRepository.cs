using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using nbp.core.dto;
using nbp.core.models;

namespace nbp.core.repositories
{
    public class ExchangeRatesTablesRepository : IExchangeRatesTablesRepository
    {
        private readonly DatabaseContext _dbContext;
        private readonly IMapper _mapper;

        public ExchangeRatesTablesRepository(DatabaseContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ExchangeRateTable> Get(int id, CancellationToken ct)
        {
            var dto = await _dbContext.ExchangeTables
                .Include( x => x.Rates)
                .ThenInclude( x => x.Currency)
                .FirstOrDefaultAsync(x => x.Id == id, ct);
            return _mapper.Map<ExchangeRateTable>(dto);
        }

        public async Task<IEnumerable<ExchangeRateTable>> Get(CancellationToken ct)
        {
            var dto = await _dbContext.ExchangeTables
                .OrderBy( x=> x.EffectiveDate)
                .ToArrayAsync(ct);
            return _mapper.Map<IEnumerable<ExchangeRateTable>>(dto);
        }
        public async Task<IEnumerable<ExchangeRateTable>> Get(DateTime startDate, DateTime endDate, CancellationToken ct)
        {
            var dto = await _dbContext.ExchangeTables
                .Where( x=> x.EffectiveDate >= startDate.Date && x.EffectiveDate <= endDate.Date)
                .OrderBy( x=> x.EffectiveDate)
                .ToArrayAsync(ct);
            return _mapper.Map<IEnumerable<ExchangeRateTable>>(dto);
        }
        public async Task<bool> Register(ExchangeRateTable model, CancellationToken ct)
        {
            var dto = _mapper.Map<ExchangeRateTableDto>(model);
            await _dbContext.ExchangeTables.AddAsync(dto, ct);
            return true;
        }

        public async Task<bool> Register(IEnumerable<ExchangeRateTable> models, CancellationToken ct)
        {
            var dtos = _mapper.Map<IEnumerable<ExchangeRateTableDto>>(models);
            await _dbContext.ExchangeTables.AddRangeAsync(dtos, ct);
            return true;
        }

        public async Task<bool> Clear(CancellationToken ct)
        {
            var dtos = await _dbContext.ExchangeTables.ToArrayAsync(ct);
            _dbContext.ExchangeTables.RemoveRange(dtos);
            return true;
        }
        
    }
}