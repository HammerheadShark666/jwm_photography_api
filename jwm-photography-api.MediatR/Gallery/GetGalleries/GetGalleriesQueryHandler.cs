using AutoMapper;
using jwm_photography_api.Data.UnitOfWork.Interfaces;
using jwm_photography_api.Helpers.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace jwm_photography_api.MediatR.Gallery.GetGalleries;

public class GetGalleriesQueryHandler(IUnitOfWork unitOfWork,
                                      ILogger<GetGalleriesQueryHandler> logger,
                                      IMapper mapper) : IRequestHandler<GetGalleriesRequest, GetGalleriesResponse>
{
    public async Task<GetGalleriesResponse> Handle(GetGalleriesRequest getGalleriesRequest, CancellationToken cancellationToken)
    {
        var galleries = await unitOfWork.Galleries.GetAllGalleriesSortedAsync();
        if (galleries.Count == 0)
        {
            logger.LogError("Galleries not found.");
            throw new NotFoundException("Galleries not found.");
        }

        return mapper.Map<GetGalleriesResponse>(galleries);
    }
}