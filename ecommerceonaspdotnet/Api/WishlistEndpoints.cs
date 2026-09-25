
using ecommerceonaspdotnet.Service;
using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Api;

public static class WishlistEndpoints
{
    public static IEndpointRouteBuilder MapWishlistEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/wishlist").WithTags("Wishlists");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignCustomer", AssignCustomer);
        group.MapPut("/unassignCustomer", UnassignCustomer);

    group.MapPut("/addToItems", AddToItems);
    group.MapPut("/removeFromItems", RemoveFromItems);


        return app;
    }

    private static async Task<IResult> Create(
        WishlistRequest request,
        IWishlistService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToWishlist( request );

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
        WishlistRequest request,
        IWishlistService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToWishlist( request );

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
        IWishlistService service,
        CancellationToken cancellationToken) {

        var wishlist = await service.Get(identifier, cancellationToken);
        return wishlist is null ? Results.NotFound() : Results.Ok( wishlist );
    }


    private static async Task<IResult> GetAll(
        IWishlistService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( WishlistResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IWishlistService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCustomer(
        AssociationRequest request,
        IWishlistService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCustomer(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCustomer(
    AssociationRequest request,
    IWishlistService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCustomer(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToItems(
        MultipleAssociationRequest request,
        IWishlistService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToItems(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromItems(
        MultipleAssociationRequest request,
        IWishlistService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromItems(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Wishlist mapRequestToWishlist( WishlistRequest request ) {
        var model = new Wishlist
        {
            Id = request.Id,
            Name = request.Name,
            AsPublic = request.AsPublic,
            CreatedAt = request.CreatedAt,
        };
        return model;
    }

}
