using AutoMapper;
using nbp.core.dto;
using nbp.core.models;

namespace nbp.core.mapper
{
    public class ExchangeRateProfile : Profile
    {
        public ExchangeRateProfile()
        {
            this.CreateMap<ExchangeRate, ExchangeRateDto>()
                .ForMember( d => d.Currency, o => o.Ignore())
                .ForMember( d => d.CurrencyId, o => o.MapFrom( s => s.Currency.Id))
                .ForMember( d => d.Table, o => o.Ignore())
                .ReverseMap();
            this.CreateMap<api.client.models.ExchangeRatesTableValue, ExchangeRate>()
                .ForMember( d => d.Currency, o => o.MapFrom(s => s.Code))
                .ReverseMap();
        }
    }
}