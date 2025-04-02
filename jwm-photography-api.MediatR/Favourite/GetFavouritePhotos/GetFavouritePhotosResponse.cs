namespace jwm_photography_api.MediatR.Favourite.GetFavouritePhotos;

public record GetFavouritePhotosResponse(List<FavouritePhotoResponse> FavouritePhotos);

public record FavouritePhotoResponse(long PhotoId, string Title, string FileName, string Country, int Orientation);
