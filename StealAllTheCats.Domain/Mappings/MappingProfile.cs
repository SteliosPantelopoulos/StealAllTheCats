using AutoMapper;
using StealAllTheCats.Domain.DTOs.CatsApi;
using StealAllTheCats.Domain.DTOs.Entities;
using StealAllTheCats.Infrastructure.Context.Entities;
using StealAllTheCats.Infrastructure.Shared.Paging;

namespace StealAllTheCats.Domain.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CatAPI, Cat>()
            .ForMember(dest => dest.ID, opt => opt.Ignore())
            .ForMember(
                    dest => dest.CatID,
                    opt => opt.MapFrom(src => src.ID))
            .ForMember(
                    dest => dest.Created,
                    opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(
                    dest => dest.ImageUrl,
                    opt => opt.MapFrom(src => src.Url));

        CreateMap<string, Tag>()
            .ForMember(dest => dest.ID, opt => opt.Ignore())
            .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => src))
            .ForMember(
                    dest => dest.Created,
                    opt => opt.MapFrom(src => DateTime.UtcNow));


        CreateMap<Cat, CatDTO>();
        CreateMap<PagedList<Cat>, PagedList<CatDTO>>();
        CreateMap<Tag, TagDTO>();
    }
}
