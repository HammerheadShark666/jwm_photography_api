using AutoMapper;

namespace jwm_photography_api.MediatR.Gallery.GetGalleries;

public class GetGalleriesMapper : Profile
{
    public GetGalleriesMapper()
    {
        base.CreateMap<List<Domain.Gallery>, GetGalleriesResponse>()
            .ForCtorParam(nameof(GetGalleriesResponse.Galleries),
                    opt => opt.MapFrom(src => src));

        base.CreateMap<Domain.Gallery, GalleriesResponse>();
    }
}