using AutoMapper;
using nbp.core.dto;

namespace nbp.core.mapper
{
    public class CurrencyMapperProfile:Profile
    {
        public CurrencyMapperProfile()
        {
            this.CreateMap<CurrencyInfo, CurrencyInfoDto>().ReverseMap();
            this.CreateMap<api.client.models.CurrencyInfo, CurrencyInfo>().ReverseMap();
        }
    }
}