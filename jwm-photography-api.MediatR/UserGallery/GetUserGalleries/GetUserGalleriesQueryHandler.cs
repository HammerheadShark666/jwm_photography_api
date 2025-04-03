using AutoMapper;
using jwm_photography_api.Data.UnitOfWork.Interfaces;
using jwm_photography_api.Helpers.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace jwm_photography_api.MediatR.UserGallery.GetUserGalleries;

public class GetUserGalleriesQueryHandler(IUnitOfWork unitOfWork,
                                          ILogger<GetUserGalleriesQueryHandler> logger,
                                          IMapper mapper) : IRequestHandler<GetUserGalleriesRequest, GetUserGalleriesResponse>
{
    public async Task<GetUserGalleriesResponse> Handle(GetUserGalleriesRequest getUserGalleriesRequest, CancellationToken cancellationToken)
    {
        var userGalleries = await unitOfWork.UserGalleries.AllSortedAsync(getUserGalleriesRequest.AccountId);
        if (userGalleries.Count == 0)
        {
            logger.LogError("User Galleries not found.");
            throw new NotFoundException("User Galleries not found.");
        }

        return mapper.Map<GetUserGalleriesResponse>(userGalleries);
    }
}