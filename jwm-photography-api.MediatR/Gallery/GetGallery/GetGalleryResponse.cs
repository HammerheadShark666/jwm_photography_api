using jwm_photography_api.MediatR.Category.GetCategories;
using jwm_photography_api.MediatR.Country.GetCountries;
using jwm_photography_api.MediatR.Palette.GetPalettes;

namespace jwm_photography_api.MediatR.Gallery.GetGallery;

public record GetGalleryResponse(int Id, string Name, string Description, List<GalleryPhotoResponse> Photos);

public record GalleryPhotoResponse(int Order, PhotoResponse Photo);

public record PhotoResponse(int Id, string FileName, string? Title, string? Camera, string? Lens,
                            string? ExposureTime, string? ExposureProgram, int? Iso, DateTime? DateTaken,
                            string? FocalLength, int? Orientation, int? Height, int? Width, CategoryResponse? Category,
                            PaletteResponse? Palette, CountryResponse? Country);



