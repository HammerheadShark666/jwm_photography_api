using AutoMapper;

namespace jwm_photography_api.MediatR.UserGallery.AddUserGalleryPhoto;

public class AddUserGalleryPhotoMapper : Profile
{
    public AddUserGalleryPhotoMapper()
    {
        base.CreateMap<AddUserGalleryPhotoRequest, Domain.UserGalleryPhoto>();


    }
}