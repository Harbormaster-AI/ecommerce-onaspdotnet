
using ecommerceonaspdotnet.Service;
using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Api;

public static class RefundEndpoints
{
    public static IEndpointRouteBuilder MapRefundEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/refund").WithTags("Refunds");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignPayment", AssignPayment);
        group.MapPut("/unassignPayment", UnassignPayment);
        group.MapPut("/assignOrder", AssignOrder);
        group.MapPut("/unassignOrder", UnassignOrder);


        return app;
    }

    private static async Task<IResult> Create(
        RefundRequest request,
        IRefundService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToRefund( request );

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
        RefundRequest request,
        IRefundService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToRefund( request );

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
        IRefundService service,
        CancellationToken cancellationToken) {

        var refund = await service.Get(identifier, cancellationToken);
        return refund is null ? Results.NotFound() : Results.Ok( refund );
    }


    private static async Task<IResult> GetAll(
        IRefundService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( RefundResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IRefundService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPayment(
        AssociationRequest request,
        IRefundService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPayment(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPayment(
    AssociationRequest request,
    IRefundService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPayment(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignOrder(
        AssociationRequest request,
        IRefundService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignOrder(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignOrder(
    AssociationRequest request,
    IRefundService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignOrder(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static Refund mapRequestToRefund( RefundRequest request ) {
        var model = new Refund
        {
            Id = request.Id,
            RefundNumber = request.RefundNumber,
            Amount = request.Amount,
            Reason = request.Reason,
            CreatedAt = request.CreatedAt,
            Status = request.Status,
        };
        return model;
    }

}
