using AutoMapper;

namespace jwm_photography_api.MediatR.Palette.GetPalettes;

public class GetPalettesMapper : Profile
{
    public GetPalettesMapper()
    {
        base.CreateMap<List<Domain.Palette>, GetPalettesResponse>()
            .ForCtorParam(nameof(GetPalettesResponse.Palettes),
                    opt => opt.MapFrom(src => src));

        base.CreateMap<Domain.Palette, PaletteResponse>();
    }
}