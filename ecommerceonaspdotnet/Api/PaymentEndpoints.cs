
using ecommerceonaspdotnet.Service;
using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Api;

public static class PaymentEndpoints
{
    public static IEndpointRouteBuilder MapPaymentEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/payment").WithTags("Payments");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignOrder", AssignOrder);
        group.MapPut("/unassignOrder", UnassignOrder);
        group.MapPut("/assignCustomer", AssignCustomer);
        group.MapPut("/unassignCustomer", UnassignCustomer);
        group.MapPut("/assignPaymentProvider", AssignPaymentProvider);
        group.MapPut("/unassignPaymentProvider", UnassignPaymentProvider);

    group.MapPut("/addToRefunds", AddToRefunds);
    group.MapPut("/removeFromRefunds", RemoveFromRefunds);


        return app;
    }

    private static async Task<IResult> Create(
        PaymentRequest request,
        IPaymentService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPayment( request );

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
        PaymentRequest request,
        IPaymentService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPayment( request );

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
        IPaymentService service,
        CancellationToken cancellationToken) {

        var payment = await service.Get(identifier, cancellationToken);
        return payment is null ? Results.NotFound() : Results.Ok( payment );
    }


    private static async Task<IResult> GetAll(
        IPaymentService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( PaymentResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IPaymentService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignOrder(
        AssociationRequest request,
        IPaymentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignOrder(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignOrder(
    AssociationRequest request,
    IPaymentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignOrder(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCustomer(
        AssociationRequest request,
        IPaymentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCustomer(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCustomer(
    AssociationRequest request,
    IPaymentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCustomer(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPaymentProvider(
        AssociationRequest request,
        IPaymentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPaymentProvider(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPaymentProvider(
    AssociationRequest request,
    IPaymentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPaymentProvider(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToRefunds(
        MultipleAssociationRequest request,
        IPaymentService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToRefunds(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromRefunds(
        MultipleAssociationRequest request,
        IPaymentService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromRefunds(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Payment mapRequestToPayment( PaymentRequest request ) {
        var model = new Payment
        {
            Id = request.Id,
            PaymentNumber = request.PaymentNumber,
            Amount = request.Amount,
            TransactionId = request.TransactionId,
            AuthorizedAt = request.AuthorizedAt,
            CapturedAt = request.CapturedAt,
            Status = request.Status,
            PaymentMethod = request.PaymentMethod,
        };
        return model;
    }

}
