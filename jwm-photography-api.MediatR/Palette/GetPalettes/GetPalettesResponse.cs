namespace jwm_photography_api.MediatR.Palette.GetPalettes;

public record GetPalettesResponse(List<PaletteResponse> Palettes);

public record PaletteResponse(int Id, string Name);