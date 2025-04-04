using AutoMapper;
using jwm_photography_api.Domain;
using jwm_photography_api.Helper;

namespace jwm_photography_api.MediatR.UserGallery.GetUserGallery;

public class GetUserGalleryMapper : Profile
{
    public GetUserGalleryMapper()
    {

        base.CreateMap<Domain.UserGallery, GetUserGalleryResponse>();

        base.CreateMap<UserGalleryPhoto, UserGalleryResponse>();

        base.CreateMap<Domain.Photo, UserGalleryPhotoResponse>()
            .ForCtorParam(nameof(UserGalleryPhotoResponse.Id),
                    opt => opt.MapFrom(src => src.Id))
            .ForCtorParam(nameof(UserGalleryPhotoResponse.FileName),
                    opt => opt.MapFrom(src => EnvironmentVariables.AzureStorageUrl + "/photos/" + src.FileName))
            .ForCtorParam(nameof(UserGalleryPhotoResponse.Title),
                    opt => opt.MapFrom(src => src.Title))
            .ForCtorParam(nameof(UserGalleryPhotoResponse.Orientation),
                    opt => opt.MapFrom(src => src.Orientation))
            .ForCtorParam(nameof(UserGalleryPhotoResponse.CountryName),
                    opt => opt.MapFrom(src => src.Country!.Name));
    }
}