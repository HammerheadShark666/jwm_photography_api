using MediatR;

namespace jwm_photography_api.MediatR.Favourite.DeleteFavouritePhoto;

public record DeleteFavouritePhotoRequest(Guid UserId, int PhotoId) : IRequest<DeleteFavouritePhotoResponse>;