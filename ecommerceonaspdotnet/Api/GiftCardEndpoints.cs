
using ecommerceonaspdotnet.Service;
using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Api;

public static class GiftCardEndpoints
{
    public static IEndpointRouteBuilder MapGiftCardEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/giftCard").WithTags("GiftCards");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignCustomer", AssignCustomer);
        group.MapPut("/unassignCustomer", UnassignCustomer);
        group.MapPut("/assignIssuedOrder", AssignIssuedOrder);
        group.MapPut("/unassignIssuedOrder", UnassignIssuedOrder);

    group.MapPut("/addToRedemptions", AddToRedemptions);
    group.MapPut("/removeFromRedemptions", RemoveFromRedemptions);


        return app;
    }

    private static async Task<IResult> Create(
        GiftCardRequest request,
        IGiftCardService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToGiftCard( request );

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
        GiftCardRequest request,
        IGiftCardService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToGiftCard( request );

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
        IGiftCardService service,
        CancellationToken cancellationToken) {

        var giftCard = await service.Get(identifier, cancellationToken);
        return giftCard is null ? Results.NotFound() : Results.Ok( giftCard );
    }


    private static async Task<IResult> GetAll(
        IGiftCardService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( GiftCardResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IGiftCardService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCustomer(
        AssociationRequest request,
        IGiftCardService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCustomer(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCustomer(
    AssociationRequest request,
    IGiftCardService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCustomer(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignIssuedOrder(
        AssociationRequest request,
        IGiftCardService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignIssuedOrder(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignIssuedOrder(
    AssociationRequest request,
    IGiftCardService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignIssuedOrder(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToRedemptions(
        MultipleAssociationRequest request,
        IGiftCardService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToRedemptions(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromRedemptions(
        MultipleAssociationRequest request,
        IGiftCardService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromRedemptions(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static GiftCard mapRequestToGiftCard( GiftCardRequest request ) {
        var model = new GiftCard
        {
            Id = request.Id,
            Code = request.Code,
            Balance = request.Balance,
            ExpirationDate = request.ExpirationDate,
            Status = request.Status,
        };
        return model;
    }

}
