using MediatR;

namespace jwm_photography_api.MediatR.UserGallery.GetUserGalleries;

public record GetUserGalleriesRequest(Guid AccountId) : IRequest<GetUserGalleriesResponse>;