namespace jwm_photography_api.MediatR.UserGallery.GetUserGalleries;

public record GetUserGalleriesResponse(List<UserGalleriesResponse> UserGalleries);

public record UserGalleriesResponse(int Id, string Name, string Description);