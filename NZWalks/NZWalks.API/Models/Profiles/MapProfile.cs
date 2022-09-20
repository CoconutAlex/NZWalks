using AutoMapper;

namespace NZWalks.API.Models.Profiles
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Domain.Region, DTO.Region>()
                .ReverseMap();

            CreateMap<Domain.Walk, DTO.Walk>()
                .ReverseMap();

            CreateMap<Domain.WalkDifficulty, DTO.WalkDifficulty>()
                .ReverseMap();

            //If Domain model is different by DTO model
            //CreateMap<Models.Domain.Region, Models.DTO.Region>()
            //    .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id));
        }
    }
}
