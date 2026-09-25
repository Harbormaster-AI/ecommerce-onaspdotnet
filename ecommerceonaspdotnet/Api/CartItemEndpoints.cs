
using ecommerceonaspdotnet.Service;
using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Api;

public static class CartItemEndpoints
{
    public static IEndpointRouteBuilder MapCartItemEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/cartItem").WithTags("CartItems");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignCart", AssignCart);
        group.MapPut("/unassignCart", UnassignCart);
        group.MapPut("/assignVariant", AssignVariant);
        group.MapPut("/unassignVariant", UnassignVariant);

    group.MapPut("/addToAppliedPromotions", AddToAppliedPromotions);
    group.MapPut("/removeFromAppliedPromotions", RemoveFromAppliedPromotions);


        return app;
    }

    private static async Task<IResult> Create(
        CartItemRequest request,
        ICartItemService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCartItem( request );

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
        CartItemRequest request,
        ICartItemService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCartItem( request );

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
        ICartItemService service,
        CancellationToken cancellationToken) {

        var cartItem = await service.Get(identifier, cancellationToken);
        return cartItem is null ? Results.NotFound() : Results.Ok( cartItem );
    }


    private static async Task<IResult> GetAll(
        ICartItemService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( CartItemResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ICartItemService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCart(
        AssociationRequest request,
        ICartItemService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCart(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCart(
    AssociationRequest request,
    ICartItemService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCart(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignVariant(
        AssociationRequest request,
        ICartItemService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignVariant(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignVariant(
    AssociationRequest request,
    ICartItemService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignVariant(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToAppliedPromotions(
        MultipleAssociationRequest request,
        ICartItemService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToAppliedPromotions(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromAppliedPromotions(
        MultipleAssociationRequest request,
        ICartItemService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromAppliedPromotions(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static CartItem mapRequestToCartItem( CartItemRequest request ) {
        var model = new CartItem
        {
            Id = request.Id,
            Quantity = request.Quantity,
            UnitPrice = request.UnitPrice,
            TotalPrice = request.TotalPrice,
        };
        return model;
    }

}
