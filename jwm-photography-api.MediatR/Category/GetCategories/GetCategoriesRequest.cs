using MediatR;

namespace jwm_photography_api.MediatR.Category.GetCategories;

public record GetCategoriesRequest() : IRequest<GetCategoriesResponse>;