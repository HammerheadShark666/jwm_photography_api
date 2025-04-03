using AutoMapper;

namespace jwm_photography_api.MediatR.UserGallery.GetUserGalleries;

public class GetUserGalleriesMapper : Profile
{
    public GetUserGalleriesMapper()
    {
        base.CreateMap<List<Domain.UserGallery>, GetUserGalleriesResponse>()
            .ForCtorParam(nameof(GetUserGalleriesResponse.UserGalleries),
                    opt => opt.MapFrom(src => src));

        base.CreateMap<Domain.UserGallery, UserGalleriesResponse>();
    }
}