using MediatR;

namespace jwm_photography_api.MediatR.UserGallery.AddUserGallery;

public record AddUserGalleryRequest(Guid AccountId, string Name, string Description) : IRequest<AddUserGalleryResponse>;