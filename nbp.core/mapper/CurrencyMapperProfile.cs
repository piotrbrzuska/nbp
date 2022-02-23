using AutoMapper;
using nbp.core.dto;
using nbp.core.models;

namespace nbp.core.mapper
{
    public class CurrencyMapperProfile : Profile
    {
        public CurrencyMapperProfile()
        {
            this.CreateMap<string, CurrencyInfo>().ConstructUsing(x => new CurrencyInfo() { Code = x });
            this.CreateMap<CurrencyInfo, CurrencyInfoDto>().ReverseMap();
            this.CreateMap<api.client.models.CurrencyInfo, CurrencyInfo>().ReverseMap();
        }
    }
}