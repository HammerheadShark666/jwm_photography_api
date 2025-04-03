using MediatR;

namespace jwm_photography_api.MediatR.Favourite.AddFavouritePhoto;

public record AddFavouritePhotoRequest(Guid AccountId, int PhotoId) : IRequest<AddFavouritePhotoResponse>;