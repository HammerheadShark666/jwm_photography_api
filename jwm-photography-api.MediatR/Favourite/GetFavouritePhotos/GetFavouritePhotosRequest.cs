using MediatR;

namespace jwm_photography_api.MediatR.Favourite.GetFavouritePhotos;

public record GetFavouritePhotosRequest(Guid UserId) : IRequest<GetFavouritePhotosResponse>;