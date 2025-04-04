using MediatR;

namespace jwm_photography_api.MediatR.UserGallery.UpdateUserGallery;

public record UpdateUserGalleryRequest(Guid AccountId, long GalleryId, string Name, string Description) : IRequest<UpdateUserGalleryResponse>;