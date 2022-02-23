using AutoMapper;
using nbp.core.dto;
using nbp.core.models;

namespace nbp.core.mapper
{
    public class ExchangeRateTableProfile : Profile
    {
        public ExchangeRateTableProfile()
        {
            this.CreateMap<ExchangeRateTable, ExchangeRateTableDto>().ReverseMap();
            this.CreateMap<api.client.models.ExchangeRatesTable, ExchangeRateTable>()
                .ForMember(d => d.Rates, o => o.MapFrom( s=> s.Rates))
                .ReverseMap();
        }
    }
}