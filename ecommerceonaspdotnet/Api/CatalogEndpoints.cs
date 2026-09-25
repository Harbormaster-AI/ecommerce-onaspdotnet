
using ecommerceonaspdotnet.Service;
using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Api;

public static class CatalogEndpoints
{
    public static IEndpointRouteBuilder MapCatalogEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/catalog").WithTags("Catalogs");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignChannel", AssignChannel);
        group.MapPut("/unassignChannel", UnassignChannel);

    group.MapPut("/addToCategories", AddToCategories);
    group.MapPut("/removeFromCategories", RemoveFromCategories);


        return app;
    }

    private static async Task<IResult> Create(
        CatalogRequest request,
        ICatalogService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCatalog( request );

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
        CatalogRequest request,
        ICatalogService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCatalog( request );

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
        ICatalogService service,
        CancellationToken cancellationToken) {

        var catalog = await service.Get(identifier, cancellationToken);
        return catalog is null ? Results.NotFound() : Results.Ok( catalog );
    }


    private static async Task<IResult> GetAll(
        ICatalogService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( CatalogResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ICatalogService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignChannel(
        AssociationRequest request,
        ICatalogService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignChannel(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignChannel(
    AssociationRequest request,
    ICatalogService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignChannel(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToCategories(
        MultipleAssociationRequest request,
        ICatalogService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToCategories(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromCategories(
        MultipleAssociationRequest request,
        ICatalogService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromCategories(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Catalog mapRequestToCatalog( CatalogRequest request ) {
        var model = new Catalog
        {
            Id = request.Id,
            Name = request.Name,
            CatalogCode = request.CatalogCode,
            AsActive = request.AsActive,
        };
        return model;
    }

}
