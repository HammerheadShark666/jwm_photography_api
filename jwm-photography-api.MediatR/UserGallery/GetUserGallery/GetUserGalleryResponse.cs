namespace jwm_photography_api.MediatR.UserGallery.GetUserGallery;

public record GetUserGalleryResponse(int Id, string Name, string Description, List<UserGalleryResponse> Photos);

public record UserGalleryResponse(int Order, UserGalleryPhotoResponse Photo);

public record UserGalleryPhotoResponse(int Id, string FileName, string? Title, string? CountryName, int? Orientation);