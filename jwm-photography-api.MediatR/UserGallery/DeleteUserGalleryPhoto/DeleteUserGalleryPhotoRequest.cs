using MediatR;

namespace jwm_photography_api.MediatR.UserGallery.DeleteUserGalleryPhoto;

public record DeleteUserGalleryPhotoRequest(Guid AccountId, long UserGalleryId, long PhotoId) : IRequest<DeleteUserGalleryPhotoResponse>;