
using ecommerceonaspdotnet.Service;
using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Api;

public static class PayoutEndpoints
{
    public static IEndpointRouteBuilder MapPayoutEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/payout").WithTags("Payouts");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignSeller", AssignSeller);
        group.MapPut("/unassignSeller", UnassignSeller);

    group.MapPut("/addToOrders", AddToOrders);
    group.MapPut("/removeFromOrders", RemoveFromOrders);


        return app;
    }

    private static async Task<IResult> Create(
        PayoutRequest request,
        IPayoutService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPayout( request );

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
        PayoutRequest request,
        IPayoutService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPayout( request );

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
        IPayoutService service,
        CancellationToken cancellationToken) {

        var payout = await service.Get(identifier, cancellationToken);
        return payout is null ? Results.NotFound() : Results.Ok( payout );
    }


    private static async Task<IResult> GetAll(
        IPayoutService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( PayoutResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IPayoutService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignSeller(
        AssociationRequest request,
        IPayoutService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignSeller(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignSeller(
    AssociationRequest request,
    IPayoutService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignSeller(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToOrders(
        MultipleAssociationRequest request,
        IPayoutService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToOrders(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromOrders(
        MultipleAssociationRequest request,
        IPayoutService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromOrders(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Payout mapRequestToPayout( PayoutRequest request ) {
        var model = new Payout
        {
            Id = request.Id,
            PayoutNumber = request.PayoutNumber,
            Amount = request.Amount,
            ScheduledDate = request.ScheduledDate,
            PaidDate = request.PaidDate,
            Status = request.Status,
        };
        return model;
    }

}
