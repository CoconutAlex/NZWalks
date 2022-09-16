using AutoMapper;

namespace NZWalks.API.Models.Profiles
{
    public class RegionsProfile : Profile
    {
        public RegionsProfile()
        {
            CreateMap<Models.Domain.Region, Models.DTO.Region>()
                .ReverseMap();

            //If Domain model is different by DTO model
            //CreateMap<Models.Domain.Region, Models.DTO.Region>()
            //    .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id));
        }
    }
}
