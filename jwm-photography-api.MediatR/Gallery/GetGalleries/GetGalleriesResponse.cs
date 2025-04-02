namespace jwm_photography_api.MediatR.Gallery.GetGalleries;

public record GetGalleriesResponse(List<GalleriesResponse> Galleries);

public record GalleriesResponse(int Id, string Name, string Description);