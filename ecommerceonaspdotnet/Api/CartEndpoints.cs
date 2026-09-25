
using ecommerceonaspdotnet.Service;
using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Api;

public static class CartEndpoints
{
    public static IEndpointRouteBuilder MapCartEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/cart").WithTags("Carts");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignCustomer", AssignCustomer);
        group.MapPut("/unassignCustomer", UnassignCustomer);
        group.MapPut("/assignChannel", AssignChannel);
        group.MapPut("/unassignChannel", UnassignChannel);

    group.MapPut("/addToItems", AddToItems);
    group.MapPut("/removeFromItems", RemoveFromItems);

    group.MapPut("/addToAppliedPromotions", AddToAppliedPromotions);
    group.MapPut("/removeFromAppliedPromotions", RemoveFromAppliedPromotions);


        return app;
    }

    private static async Task<IResult> Create(
        CartRequest request,
        ICartService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCart( request );

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
        CartRequest request,
        ICartService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCart( request );

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
        ICartService service,
        CancellationToken cancellationToken) {

        var cart = await service.Get(identifier, cancellationToken);
        return cart is null ? Results.NotFound() : Results.Ok( cart );
    }


    private static async Task<IResult> GetAll(
        ICartService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( CartResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ICartService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCustomer(
        AssociationRequest request,
        ICartService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCustomer(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCustomer(
    AssociationRequest request,
    ICartService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCustomer(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignChannel(
        AssociationRequest request,
        ICartService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignChannel(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignChannel(
    AssociationRequest request,
    ICartService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignChannel(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToItems(
        MultipleAssociationRequest request,
        ICartService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToItems(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromItems(
        MultipleAssociationRequest request,
        ICartService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromItems(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToAppliedPromotions(
        MultipleAssociationRequest request,
        ICartService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToAppliedPromotions(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromAppliedPromotions(
        MultipleAssociationRequest request,
        ICartService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromAppliedPromotions(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Cart mapRequestToCart( CartRequest request ) {
        var model = new Cart
        {
            Id = request.Id,
            CartNumber = request.CartNumber,
            CreatedAt = request.CreatedAt,
            Currency = request.Currency,
            ShippingAddress = request.ShippingAddress,
            BillingAddress = request.BillingAddress,
            Status = request.Status,
        };
        return model;
    }

}
