using MediatR;

namespace jwm_photography_api.MediatR.Gallery.GetGalleries;

public record GetGalleriesRequest() : IRequest<GetGalleriesResponse>;