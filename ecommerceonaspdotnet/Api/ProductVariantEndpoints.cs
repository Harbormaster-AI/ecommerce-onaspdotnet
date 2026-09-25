
using ecommerceonaspdotnet.Service;
using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Api;

public static class ProductVariantEndpoints
{
    public static IEndpointRouteBuilder MapProductVariantEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/productVariant").WithTags("ProductVariants");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignProduct", AssignProduct);
        group.MapPut("/unassignProduct", UnassignProduct);

    group.MapPut("/addToPricing", AddToPricing);
    group.MapPut("/removeFromPricing", RemoveFromPricing);

    group.MapPut("/addToInventoryItems", AddToInventoryItems);
    group.MapPut("/removeFromInventoryItems", RemoveFromInventoryItems);

    group.MapPut("/addToMediaAssets", AddToMediaAssets);
    group.MapPut("/removeFromMediaAssets", RemoveFromMediaAssets);

    group.MapPut("/addToSubscriptions", AddToSubscriptions);
    group.MapPut("/removeFromSubscriptions", RemoveFromSubscriptions);

    group.MapPut("/addToCartItems", AddToCartItems);
    group.MapPut("/removeFromCartItems", RemoveFromCartItems);

    group.MapPut("/addToOrderLines", AddToOrderLines);
    group.MapPut("/removeFromOrderLines", RemoveFromOrderLines);

    group.MapPut("/addToWishlistItems", AddToWishlistItems);
    group.MapPut("/removeFromWishlistItems", RemoveFromWishlistItems);


        return app;
    }

    private static async Task<IResult> Create(
        ProductVariantRequest request,
        IProductVariantService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToProductVariant( request );

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
        ProductVariantRequest request,
        IProductVariantService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToProductVariant( request );

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
        IProductVariantService service,
        CancellationToken cancellationToken) {

        var productVariant = await service.Get(identifier, cancellationToken);
        return productVariant is null ? Results.NotFound() : Results.Ok( productVariant );
    }


    private static async Task<IResult> GetAll(
        IProductVariantService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ProductVariantResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IProductVariantService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignProduct(
        AssociationRequest request,
        IProductVariantService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignProduct(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignProduct(
    AssociationRequest request,
    IProductVariantService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignProduct(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToPricing(
        MultipleAssociationRequest request,
        IProductVariantService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPricing(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPricing(
        MultipleAssociationRequest request,
        IProductVariantService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPricing(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToInventoryItems(
        MultipleAssociationRequest request,
        IProductVariantService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToInventoryItems(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromInventoryItems(
        MultipleAssociationRequest request,
        IProductVariantService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromInventoryItems(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToMediaAssets(
        MultipleAssociationRequest request,
        IProductVariantService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToMediaAssets(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromMediaAssets(
        MultipleAssociationRequest request,
        IProductVariantService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromMediaAssets(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToSubscriptions(
        MultipleAssociationRequest request,
        IProductVariantService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToSubscriptions(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromSubscriptions(
        MultipleAssociationRequest request,
        IProductVariantService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromSubscriptions(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToCartItems(
        MultipleAssociationRequest request,
        IProductVariantService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToCartItems(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromCartItems(
        MultipleAssociationRequest request,
        IProductVariantService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromCartItems(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToOrderLines(
        MultipleAssociationRequest request,
        IProductVariantService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToOrderLines(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromOrderLines(
        MultipleAssociationRequest request,
        IProductVariantService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromOrderLines(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToWishlistItems(
        MultipleAssociationRequest request,
        IProductVariantService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToWishlistItems(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromWishlistItems(
        MultipleAssociationRequest request,
        IProductVariantService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromWishlistItems(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static ProductVariant mapRequestToProductVariant( ProductVariantRequest request ) {
        var model = new ProductVariant
        {
            Id = request.Id,
            Sku = request.Sku,
            Barcode = request.Barcode,
            Title = request.Title,
            Weight = request.Weight,
            RequiresShipping = request.RequiresShipping,
            WeightUnit = request.WeightUnit,
        };
        return model;
    }

}
