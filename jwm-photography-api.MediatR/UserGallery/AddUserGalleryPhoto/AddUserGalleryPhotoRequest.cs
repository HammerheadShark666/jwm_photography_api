using MediatR;

namespace jwm_photography_api.MediatR.UserGallery.AddUserGalleryPhoto;

public record AddUserGalleryPhotoRequest(Guid AccountId, long UserGalleryId, long PhotoId, int Order) : IRequest<AddUserGalleryPhotoResponse>;