using AutoMapper;

namespace jwm_photography_api.MediatR.Photo.GetRandomPhotos;

public class GetRandomPhotosMapper : Profile
{
    public GetRandomPhotosMapper()
    {
        base.CreateMap<List<Domain.Photo>, GetRandomPhotosResponse>()
             .ForCtorParam(nameof(GetRandomPhotosResponse.Photos),
                    opt => opt.MapFrom(src => src));

        base.CreateMap<Domain.Photo, PhotoResponse>()
            .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country != null ? src.Country.Name : null!));
    }
}