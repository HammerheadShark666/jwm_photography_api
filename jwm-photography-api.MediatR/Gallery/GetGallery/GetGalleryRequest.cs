using MediatR;

namespace jwm_photography_api.MediatR.Gallery.GetGallery;

public record GetGalleryRequest(int Id) : IRequest<GetGalleryResponse>;