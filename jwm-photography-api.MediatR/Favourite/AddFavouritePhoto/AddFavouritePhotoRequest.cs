using MediatR;

namespace jwm_photography_api.MediatR.Favourite.AddFavouritePhoto;

public record AddFavouritePhotoRequest(Guid UserId, int PhotoId) : IRequest<AddFavouritePhotoResponse>;