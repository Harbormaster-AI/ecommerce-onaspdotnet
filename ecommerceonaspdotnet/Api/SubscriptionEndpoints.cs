
using ecommerceonaspdotnet.Service;
using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Api;

public static class SubscriptionEndpoints
{
    public static IEndpointRouteBuilder MapSubscriptionEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/subscription").WithTags("Subscriptions");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignCustomer", AssignCustomer);
        group.MapPut("/unassignCustomer", UnassignCustomer);
        group.MapPut("/assignVariant", AssignVariant);
        group.MapPut("/unassignVariant", UnassignVariant);
        group.MapPut("/assignPaymentProvider", AssignPaymentProvider);
        group.MapPut("/unassignPaymentProvider", UnassignPaymentProvider);
        group.MapPut("/assignChannel", AssignChannel);
        group.MapPut("/unassignChannel", UnassignChannel);


        return app;
    }

    private static async Task<IResult> Create(
        SubscriptionRequest request,
        ISubscriptionService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToSubscription( request );

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
        SubscriptionRequest request,
        ISubscriptionService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToSubscription( request );

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
        ISubscriptionService service,
        CancellationToken cancellationToken) {

        var subscription = await service.Get(identifier, cancellationToken);
        return subscription is null ? Results.NotFound() : Results.Ok( subscription );
    }


    private static async Task<IResult> GetAll(
        ISubscriptionService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( SubscriptionResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ISubscriptionService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCustomer(
        AssociationRequest request,
        ISubscriptionService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCustomer(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCustomer(
    AssociationRequest request,
    ISubscriptionService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCustomer(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignVariant(
        AssociationRequest request,
        ISubscriptionService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignVariant(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignVariant(
    AssociationRequest request,
    ISubscriptionService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignVariant(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPaymentProvider(
        AssociationRequest request,
        ISubscriptionService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPaymentProvider(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPaymentProvider(
    AssociationRequest request,
    ISubscriptionService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPaymentProvider(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignChannel(
        AssociationRequest request,
        ISubscriptionService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignChannel(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignChannel(
    AssociationRequest request,
    ISubscriptionService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignChannel(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static Subscription mapRequestToSubscription( SubscriptionRequest request ) {
        var model = new Subscription
        {
            Id = request.Id,
            SubscriptionNumber = request.SubscriptionNumber,
            NextBillingDate = request.NextBillingDate,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            Status = request.Status,
            Interval = request.Interval,
        };
        return model;
    }

}
