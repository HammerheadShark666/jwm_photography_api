using AutoMapper;

namespace jwm_photography_api.MediatR.UserGallery.UpdateUserGallery;

public class UpdateUserGalleryMapper : Profile
{
    public UpdateUserGalleryMapper()
    {
        base.CreateMap<UpdateUserGalleryRequest, Domain.UserGallery>();
    }
}