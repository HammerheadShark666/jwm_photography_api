using AutoMapper;

namespace jwm_photography_api.MediatR.Favourite.AddFavouritePhoto;

public class AddFavouritePhotoMapper : Profile
{
    public AddFavouritePhotoMapper()
    {
        base.CreateMap<AddFavouritePhotoRequest, Domain.Favourite>();
    }
}