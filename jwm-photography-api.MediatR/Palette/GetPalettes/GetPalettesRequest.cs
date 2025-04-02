using MediatR;

namespace jwm_photography_api.MediatR.Palette.GetPalettes;

public record GetPalettesRequest() : IRequest<GetPalettesResponse>;