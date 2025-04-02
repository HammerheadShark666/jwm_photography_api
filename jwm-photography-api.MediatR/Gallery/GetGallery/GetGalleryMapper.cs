using AutoMapper;
using jwm_photography_api.Domain;

namespace jwm_photography_api.MediatR.Gallery.GetGallery;

public class GetGalleryMapper : Profile
{
    public GetGalleryMapper()
    {
        base.CreateMap<Domain.Gallery, GetGalleryResponse>();

        base.CreateMap<GalleryPhoto, GalleryPhotoResponse>();

        base.CreateMap<Domain.Photo, PhotoResponse>();
    }
}