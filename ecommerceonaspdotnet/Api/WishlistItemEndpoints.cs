
using ecommerceonaspdotnet.Service;
using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Api;

public static class WishlistItemEndpoints
{
    public static IEndpointRouteBuilder MapWishlistItemEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/wishlistItem").WithTags("WishlistItems");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignWishlist", AssignWishlist);
        group.MapPut("/unassignWishlist", UnassignWishlist);
        group.MapPut("/assignVariant", AssignVariant);
        group.MapPut("/unassignVariant", UnassignVariant);


        return app;
    }

    private static async Task<IResult> Create(
        WishlistItemRequest request,
        IWishlistItemService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToWishlistItem( request );

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
        WishlistItemRequest request,
        IWishlistItemService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToWishlistItem( request );

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
        IWishlistItemService service,
        CancellationToken cancellationToken) {

        var wishlistItem = await service.Get(identifier, cancellationToken);
        return wishlistItem is null ? Results.NotFound() : Results.Ok( wishlistItem );
    }


    private static async Task<IResult> GetAll(
        IWishlistItemService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( WishlistItemResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IWishlistItemService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignWishlist(
        AssociationRequest request,
        IWishlistItemService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignWishlist(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignWishlist(
    AssociationRequest request,
    IWishlistItemService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignWishlist(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignVariant(
        AssociationRequest request,
        IWishlistItemService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignVariant(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignVariant(
    AssociationRequest request,
    IWishlistItemService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignVariant(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static WishlistItem mapRequestToWishlistItem( WishlistItemRequest request ) {
        var model = new WishlistItem
        {
            Id = request.Id,
            AddedDate = request.AddedDate,
        };
        return model;
    }

}
