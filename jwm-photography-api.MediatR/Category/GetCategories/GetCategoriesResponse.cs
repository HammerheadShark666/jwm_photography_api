namespace jwm_photography_api.MediatR.Category.GetCategories;

public record GetCategoriesResponse(List<CategoryResponse> Categories);

public record CategoryResponse(int Id, string Name);