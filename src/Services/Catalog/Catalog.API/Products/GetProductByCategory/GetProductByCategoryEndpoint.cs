﻿
namespace Catalog.API.Products.GetProductByCategory;

public record GetProductByCategoryResponse(List<Product> Products);

public class GetProductByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", async (IMediator sender, string category) =>
        { 
            var result = await sender.Send(new GetProductByCategoryQuery(category));

            var response = result.Adapt<GetProductByCategoryResponse>();

            return Results.Ok(response);
        })
        .WithName("GetProductByCategory")
        .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Product By Category")
        .WithDescription("Get Product By Category");
    }
}
