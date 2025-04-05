using AutoMapper;
using jwm_photography_api.Data.UnitOfWork.Interfaces;
using MediatR;

namespace jwm_photography_api.MediatR.Favourite.AddFavouritePhoto;

public class AddFavouritePhotoCommandHandler(IUnitOfWork unitOfWork,
                                             IMapper mapper) : IRequestHandler<AddFavouritePhotoRequest, AddFavouritePhotoResponse>
{
    public async Task<AddFavouritePhotoResponse> Handle(AddFavouritePhotoRequest addFavouritePhotoRequest, CancellationToken cancellationToken)
    {
        var favouritePhoto = mapper.Map<Domain.Favourite>(addFavouritePhotoRequest);
        await unitOfWork.Favourites.AddAsync(favouritePhoto);
        await unitOfWork.Complete();

        return new AddFavouritePhotoResponse();
    }
}