using AutoMapper;
using jwm_photography_api.Data.UnitOfWork.Interfaces;
using jwm_photography_api.Helpers.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace jwm_photography_api.MediatR.Palette.GetPalettes;

public class GetPalettesQueryHandler(IUnitOfWork unitOfWork,
                                     ILogger<GetPalettesQueryHandler> logger,
                                     IMapper mapper) : IRequestHandler<GetPalettesRequest, GetPalettesResponse>
{
    public async Task<GetPalettesResponse> Handle(GetPalettesRequest getPaletteRequest, CancellationToken cancellationToken)
    {
        var palettes = await unitOfWork.Palettes.AllSortedAsync();
        if (palettes.Count == 0)
        {
            logger.LogError("Palettes not found.");
            throw new NotFoundException("Palettes not found.");
        }

        return mapper.Map<GetPalettesResponse>(palettes);
    }
}