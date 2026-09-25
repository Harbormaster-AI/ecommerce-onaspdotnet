
using ecommerceonaspdotnet.Service;
using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Api;

public static class ProductEndpoints
{
    public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/product").WithTags("Products");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignBrand", AssignBrand);
        group.MapPut("/unassignBrand", UnassignBrand);
        group.MapPut("/assignSeller", AssignSeller);
        group.MapPut("/unassignSeller", UnassignSeller);

    group.MapPut("/addToCategories", AddToCategories);
    group.MapPut("/removeFromCategories", RemoveFromCategories);

    group.MapPut("/addToVariants", AddToVariants);
    group.MapPut("/removeFromVariants", RemoveFromVariants);

    group.MapPut("/addToMediaAssets", AddToMediaAssets);
    group.MapPut("/removeFromMediaAssets", RemoveFromMediaAssets);

    group.MapPut("/addToReviews", AddToReviews);
    group.MapPut("/removeFromReviews", RemoveFromReviews);


        return app;
    }

    private static async Task<IResult> Create(
        ProductRequest request,
        IProductService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToProduct( request );

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
        ProductRequest request,
        IProductService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToProduct( request );

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
        IProductService service,
        CancellationToken cancellationToken) {

        var product = await service.Get(identifier, cancellationToken);
        return product is null ? Results.NotFound() : Results.Ok( product );
    }


    private static async Task<IResult> GetAll(
        IProductService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ProductResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IProductService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignBrand(
        AssociationRequest request,
        IProductService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignBrand(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignBrand(
    AssociationRequest request,
    IProductService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignBrand(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignSeller(
        AssociationRequest request,
        IProductService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignSeller(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignSeller(
    AssociationRequest request,
    IProductService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignSeller(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToCategories(
        MultipleAssociationRequest request,
        IProductService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToCategories(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromCategories(
        MultipleAssociationRequest request,
        IProductService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromCategories(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToVariants(
        MultipleAssociationRequest request,
        IProductService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToVariants(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromVariants(
        MultipleAssociationRequest request,
        IProductService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromVariants(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToMediaAssets(
        MultipleAssociationRequest request,
        IProductService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToMediaAssets(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromMediaAssets(
        MultipleAssociationRequest request,
        IProductService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromMediaAssets(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToReviews(
        MultipleAssociationRequest request,
        IProductService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToReviews(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromReviews(
        MultipleAssociationRequest request,
        IProductService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromReviews(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Product mapRequestToProduct( ProductRequest request ) {
        var model = new Product
        {
            Id = request.Id,
            Name = request.Name,
            Slug = request.Slug,
            AsActive = request.AsActive,
            ProductType = request.ProductType,
            DefaultTaxClass = request.DefaultTaxClass,
        };
        return model;
    }

}
