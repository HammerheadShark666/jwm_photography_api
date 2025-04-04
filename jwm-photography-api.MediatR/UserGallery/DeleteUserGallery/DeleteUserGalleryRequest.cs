using MediatR;

namespace jwm_photography_api.MediatR.UserGallery.DeleteUserGallery;

public record DeleteUserGalleryRequest(Guid AccountId, long GalleryId) : IRequest<DeleteUserGalleryResponse>;