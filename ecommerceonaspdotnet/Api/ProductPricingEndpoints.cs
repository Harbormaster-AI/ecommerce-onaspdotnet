
using ecommerceonaspdotnet.Service;
using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Api;

public static class ProductPricingEndpoints
{
    public static IEndpointRouteBuilder MapProductPricingEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/productPricing").WithTags("ProductPricings");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignVariant", AssignVariant);
        group.MapPut("/unassignVariant", UnassignVariant);
        group.MapPut("/assignChannel", AssignChannel);
        group.MapPut("/unassignChannel", UnassignChannel);


        return app;
    }

    private static async Task<IResult> Create(
        ProductPricingRequest request,
        IProductPricingService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToProductPricing( request );

        try
        {
            await service.Create(model, cancellationToken);
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }

        return Results.NoContent();
    }

    private static async Task<IResult> Update(
        ProductPricingRequest request,
        IProductPricingService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToProductPricing( request );

        try
        {
            var updated = await service.Update(model, cancellationToken);
            return updated ? Results.NoContent() : Results.NotFound();
        }
        catch (InvalidOperationException ex)
        {
            return Results.BadRequest(new { error = ex.Message });
        }
    }


    private static async Task<IResult> Get(
        IdentifierRequest identifier,
        IProductPricingService service,
        CancellationToken cancellationToken) {

        var productPricing = await service.Get(identifier, cancellationToken);
        return productPricing is null ? Results.NotFound() : Results.Ok( productPricing );
    }


    private static async Task<IResult> GetAll(
        IProductPricingService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ProductPricingResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IProductPricingService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignVariant(
        AssociationRequest request,
        IProductPricingService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignVariant(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignVariant(
    AssociationRequest request,
    IProductPricingService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignVariant(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignChannel(
        AssociationRequest request,
        IProductPricingService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignChannel(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignChannel(
    AssociationRequest request,
    IProductPricingService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignChannel(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static ProductPricing mapRequestToProductPricing( ProductPricingRequest request ) {
        var model = new ProductPricing
        {
            Id = request.Id,
            ListPrice = request.ListPrice,
            SalePrice = request.SalePrice,
            ValidFrom = request.ValidFrom,
            ValidTo = request.ValidTo,
        };
        return model;
    }

}
