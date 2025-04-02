using AutoMapper;

namespace jwm_photography_api.MediatR.Category.GetCategories;

public class GetCategoriesMapper : Profile
{
    public GetCategoriesMapper()
    {
        base.CreateMap<List<Domain.Category>, GetCategoriesResponse>()
            .ForCtorParam(nameof(GetCategoriesResponse.Categories),
                    opt => opt.MapFrom(src => src));

        base.CreateMap<Domain.Category, CategoryResponse>();
    }
}