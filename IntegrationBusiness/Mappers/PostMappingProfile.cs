using AutoMapper;
using IntegrationDtos.Request;
using IntegrationDtos.Responses;
using IntegrationModels;

namespace IntegrationBusiness.Mappers;

public class PostMappingProfile : Profile
{
    public PostMappingProfile()
    {
        CreateMap<PostRequest, PostEntity>()
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images ?? new List<string>()));

        CreateMap<PostEntity, PostResponse>();
    }
}