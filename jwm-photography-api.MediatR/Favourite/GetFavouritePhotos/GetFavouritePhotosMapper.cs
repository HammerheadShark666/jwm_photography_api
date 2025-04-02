using AutoMapper;
using jwm_photography_api.Helper;

namespace jwm_photography_api.MediatR.Favourite.GetFavouritePhotos;

public class GetFavouritePhotosMapper : Profile
{
    public GetFavouritePhotosMapper()
    {
        base.CreateMap<List<Domain.Photo>, GetFavouritePhotosResponse>()
            .ForCtorParam(nameof(GetFavouritePhotosResponse.FavouritePhotos),
                    opt => opt.MapFrom(src => src));

        base.CreateMap<Domain.Photo, FavouritePhotoResponse>()
            .ForCtorParam(nameof(FavouritePhotoResponse.PhotoId),
                    opt => opt.MapFrom(src => src.Id))
            .ForCtorParam(nameof(FavouritePhotoResponse.FileName),
                    opt => opt.MapFrom(src => EnvironmentVariables.AzureStorageUrl + "/photos/" + src.FileName))
            .ForCtorParam(nameof(FavouritePhotoResponse.Title),
                    opt => opt.MapFrom(src => src.Title))
            .ForCtorParam(nameof(FavouritePhotoResponse.Orientation),
                    opt => opt.MapFrom(src => src.Orientation))
            .ForCtorParam(nameof(FavouritePhotoResponse.Country),
                    opt => opt.MapFrom(src => src.Country!.Name));
    }
}