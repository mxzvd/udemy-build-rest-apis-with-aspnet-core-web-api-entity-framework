using AutoMapper;
using NewZealand.Walks.Rest.Models.DataTransferObjects;

namespace NewZealand.Walks.Rest.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Region, GetRegionResponse>();
        CreateMap<AddRegionRequest, Region>();
        CreateMap<UpdateRegionRequest, Region>();

        CreateMap<Walk, GetWalkResponse>()
            .ForMember(
                dest => dest.Difficulty,
                opt => opt.MapFrom(src => src.DifficultyNavigation)
            )
            .ForMember(
                dest => dest.Region,
                opt => opt.MapFrom(src => src.RegionNavigation)
            );
        CreateMap<AddWalkRequest, Walk>();
        CreateMap<Difficulty, GetDifficultyResponse>();
        CreateMap<UpdateWalkRequest, Walk>();
    }
}
