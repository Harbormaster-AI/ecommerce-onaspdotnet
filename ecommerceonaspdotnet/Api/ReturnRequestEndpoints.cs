
using ecommerceonaspdotnet.Service;
using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Api;

public static class ReturnRequestEndpoints
{
    public static IEndpointRouteBuilder MapReturnRequestEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/returnRequest").WithTags("ReturnRequests");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignOrder", AssignOrder);
        group.MapPut("/unassignOrder", UnassignOrder);
        group.MapPut("/assignRefund", AssignRefund);
        group.MapPut("/unassignRefund", UnassignRefund);
        group.MapPut("/assignShipment", AssignShipment);
        group.MapPut("/unassignShipment", UnassignShipment);

    group.MapPut("/addToItems", AddToItems);
    group.MapPut("/removeFromItems", RemoveFromItems);


        return app;
    }

    private static async Task<IResult> Create(
        ReturnRequestRequest request,
        IReturnRequestService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToReturnRequest( request );

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
        ReturnRequestRequest request,
        IReturnRequestService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToReturnRequest( request );

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
        IReturnRequestService service,
        CancellationToken cancellationToken) {

        var returnRequest = await service.Get(identifier, cancellationToken);
        return returnRequest is null ? Results.NotFound() : Results.Ok( returnRequest );
    }


    private static async Task<IResult> GetAll(
        IReturnRequestService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ReturnRequestResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IReturnRequestService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignOrder(
        AssociationRequest request,
        IReturnRequestService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignOrder(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignOrder(
    AssociationRequest request,
    IReturnRequestService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignOrder(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignRefund(
        AssociationRequest request,
        IReturnRequestService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignRefund(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignRefund(
    AssociationRequest request,
    IReturnRequestService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignRefund(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignShipment(
        AssociationRequest request,
        IReturnRequestService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignShipment(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignShipment(
    AssociationRequest request,
    IReturnRequestService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignShipment(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToItems(
        MultipleAssociationRequest request,
        IReturnRequestService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToItems(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromItems(
        MultipleAssociationRequest request,
        IReturnRequestService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromItems(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static ReturnRequest mapRequestToReturnRequest( ReturnRequestRequest request ) {
        var model = new ReturnRequest
        {
            Id = request.Id,
            ReturnNumber = request.ReturnNumber,
            CreatedAt = request.CreatedAt,
            RefundAmount = request.RefundAmount,
            Status = request.Status,
        };
        return model;
    }

}
