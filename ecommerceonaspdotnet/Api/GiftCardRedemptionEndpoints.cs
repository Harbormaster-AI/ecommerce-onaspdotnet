
using ecommerceonaspdotnet.Service;
using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Api;

public static class GiftCardRedemptionEndpoints
{
    public static IEndpointRouteBuilder MapGiftCardRedemptionEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/giftCardRedemption").WithTags("GiftCardRedemptions");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignGiftCard", AssignGiftCard);
        group.MapPut("/unassignGiftCard", UnassignGiftCard);
        group.MapPut("/assignOrder", AssignOrder);
        group.MapPut("/unassignOrder", UnassignOrder);


        return app;
    }

    private static async Task<IResult> Create(
        GiftCardRedemptionRequest request,
        IGiftCardRedemptionService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToGiftCardRedemption( request );

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
        GiftCardRedemptionRequest request,
        IGiftCardRedemptionService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToGiftCardRedemption( request );

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
        IGiftCardRedemptionService service,
        CancellationToken cancellationToken) {

        var giftCardRedemption = await service.Get(identifier, cancellationToken);
        return giftCardRedemption is null ? Results.NotFound() : Results.Ok( giftCardRedemption );
    }


    private static async Task<IResult> GetAll(
        IGiftCardRedemptionService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( GiftCardRedemptionResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IGiftCardRedemptionService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignGiftCard(
        AssociationRequest request,
        IGiftCardRedemptionService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignGiftCard(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignGiftCard(
    AssociationRequest request,
    IGiftCardRedemptionService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignGiftCard(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignOrder(
        AssociationRequest request,
        IGiftCardRedemptionService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignOrder(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignOrder(
    AssociationRequest request,
    IGiftCardRedemptionService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignOrder(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static GiftCardRedemption mapRequestToGiftCardRedemption( GiftCardRedemptionRequest request ) {
        var model = new GiftCardRedemption
        {
            Id = request.Id,
            RedeemedAt = request.RedeemedAt,
            Amount = request.Amount,
        };
        return model;
    }

}
