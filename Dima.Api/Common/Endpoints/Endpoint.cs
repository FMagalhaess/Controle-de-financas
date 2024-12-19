using Dima.Api.Common.Api;
using Dima.Api.Common.Endpoints.Categories;

namespace Dima.Api.Common.Endpoints;

public static class Endpoint
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpoints = app
            .MapGroup("");
        endpoints.MapGroup("v1/categories")
            .WithTags("categories")
            //.RequireAuthorization()
            .MapEndpoint<CreateCategoryEndpoint>();
    }

    private static IEndpointRouteBuilder MapEndpoint<TEndepoint>(this IEndpointRouteBuilder app)
    where TEndepoint : IEndpoint
    {
        TEndepoint.Map(app);
        return app;
    }
}