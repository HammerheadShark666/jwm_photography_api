using MediatR;

namespace jwm_photography_api.MediatR.UserGallery.GetUserGallery;

public record GetUserGalleryRequest(Guid AccountId, long UserGalleryId) : IRequest<GetUserGalleryResponse>;