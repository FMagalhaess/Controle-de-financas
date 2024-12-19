using Dima.Api.Common.Api;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Categories;
using Dima.Core.Responses;

namespace Dima.Api.Common.Endpoints.Categories;

public class CreateCategoryEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app.MapPost("/", HandleAsync)
            .WithName("Categories: Create")
            .WithSummary("Creates a new category")
            .WithDescription("Creates a new category")
            .WithOrder(1)
            .Produces<Response<Category?>>();

    public static async Task<IResult> HandleAsync(ICategoryHandler handler, CreateCategoryRequest request)
    {
        var result = await handler.CreateAsync(request);
        return result.IsSuccess ? TypedResults.Created($"/{result.Data?.Id}", result.Data) : TypedResults.BadRequest(result.Data);
    }
}