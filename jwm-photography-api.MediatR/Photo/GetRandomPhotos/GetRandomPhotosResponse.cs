namespace jwm_photography_api.MediatR.Photo.GetRandomPhotos;

public record GetRandomPhotosResponse(List<PhotoResponse> Photos);

public record PhotoResponse(int Id, string FileName, string? Title, string? CountryName, int? Orientation);