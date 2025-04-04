using AutoMapper;

namespace jwm_photography_api.MediatR.UserGallery.AddUserGallery;

public class AddUserGalleryMapper : Profile
{
    public AddUserGalleryMapper()
    {
        base.CreateMap<AddUserGalleryRequest, Domain.UserGallery>();

        base.CreateMap<Domain.UserGallery, AddUserGalleryResponse>();
    }
}