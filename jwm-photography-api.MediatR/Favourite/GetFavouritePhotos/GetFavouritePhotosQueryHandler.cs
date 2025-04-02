using AutoMapper;
using jwm_photography_api.Data.UnitOfWork.Interfaces;
using jwm_photography_api.Helpers.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace jwm_photography_api.MediatR.Favourite.GetFavouritePhotos;

public class GetGalleriesQueryHandler(IUnitOfWork unitOfWork,
                                      ILogger<GetGalleriesQueryHandler> logger,
                                      IMapper mapper) : IRequestHandler<GetFavouritePhotosRequest, GetFavouritePhotosResponse>
{
    public async Task<GetFavouritePhotosResponse> Handle(GetFavouritePhotosRequest getFavouritePhotosRequest, CancellationToken cancellationToken)
    {
        var favourites = await unitOfWork.Favourites.GetFavouritePhotosAsync(getFavouritePhotosRequest.UserId);
        if (favourites.Count == 0)
        {
            logger.LogError("Favourites not found.");
            throw new NotFoundException("Favourites not found.");
        }

        return mapper.Map<GetFavouritePhotosResponse>(favourites);
    }
}