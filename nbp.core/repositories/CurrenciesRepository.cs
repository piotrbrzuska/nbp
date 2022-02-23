using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using nbp.core.dto;

namespace nbp.core.repositories
{
    public class CurrenciesRepository : ICurrenciesRepository
    {
        private readonly DatabaseContext _dbContext;
        private readonly IMapper _mapper;

        public CurrenciesRepository(DatabaseContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<CurrencyInfo> Get(int id, CancellationToken ct)
        {
             var dto = await _dbContext.Currencies.FirstOrDefaultAsync(x => x.Id == id, ct);
             return _mapper.Map<CurrencyInfo>(dto);
        }
        public async Task<IEnumerable<CurrencyInfo>> Get(CancellationToken ct)
        {
            var dto = await _dbContext.Currencies.ToArrayAsync(ct);
            return _mapper.Map<IEnumerable<CurrencyInfo>>(dto);
        }
        public async Task<CurrencyInfo> GetByCode(string code, CancellationToken ct)
        {
            var dto = await _dbContext.Currencies.FirstOrDefaultAsync(x => x.Code == code, ct);
            return _mapper.Map<CurrencyInfo>(dto);
        }
        public async Task<bool> Register(CurrencyInfo model, CancellationToken ct)
        {
            var dto = _mapper.Map<CurrencyInfoDto>(model);
            await _dbContext.Currencies.AddAsync(dto, ct);
            return true;
        }

        public async Task<bool> Register(IEnumerable<CurrencyInfo> models, CancellationToken ct)
        {
            var dtos = _mapper.Map<IEnumerable<CurrencyInfoDto>>(models);
            await _dbContext.Currencies.AddRangeAsync(dtos, ct);
            return true;
        }
        public async Task<bool> Clear(CancellationToken ct)
        {
            var dtos = await _dbContext.Currencies.ToArrayAsync(ct);
            _dbContext.Currencies.RemoveRange(dtos);
            return true;
        }
    }
}