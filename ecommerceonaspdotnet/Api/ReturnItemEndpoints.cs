
using ecommerceonaspdotnet.Service;
using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Api;

public static class ReturnItemEndpoints
{
    public static IEndpointRouteBuilder MapReturnItemEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/returnItem").WithTags("ReturnItems");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignReturnRequest", AssignReturnRequest);
        group.MapPut("/unassignReturnRequest", UnassignReturnRequest);
        group.MapPut("/assignOrderLine", AssignOrderLine);
        group.MapPut("/unassignOrderLine", UnassignOrderLine);


        return app;
    }

    private static async Task<IResult> Create(
        ReturnItemRequest request,
        IReturnItemService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToReturnItem( request );

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
        ReturnItemRequest request,
        IReturnItemService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToReturnItem( request );

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
        IReturnItemService service,
        CancellationToken cancellationToken) {

        var returnItem = await service.Get(identifier, cancellationToken);
        return returnItem is null ? Results.NotFound() : Results.Ok( returnItem );
    }


    private static async Task<IResult> GetAll(
        IReturnItemService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ReturnItemResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IReturnItemService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignReturnRequest(
        AssociationRequest request,
        IReturnItemService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignReturnRequest(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignReturnRequest(
    AssociationRequest request,
    IReturnItemService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignReturnRequest(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignOrderLine(
        AssociationRequest request,
        IReturnItemService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignOrderLine(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignOrderLine(
    AssociationRequest request,
    IReturnItemService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignOrderLine(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static ReturnItem mapRequestToReturnItem( ReturnItemRequest request ) {
        var model = new ReturnItem
        {
            Id = request.Id,
            Quantity = request.Quantity,
            Reason = request.Reason,
            Condition = request.Condition,
        };
        return model;
    }

}
