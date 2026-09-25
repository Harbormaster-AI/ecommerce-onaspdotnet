
using ecommerceonaspdotnet.Service;
using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Api;

public static class ShipmentItemEndpoints
{
    public static IEndpointRouteBuilder MapShipmentItemEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/shipmentItem").WithTags("ShipmentItems");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignShipment", AssignShipment);
        group.MapPut("/unassignShipment", UnassignShipment);
        group.MapPut("/assignOrderLine", AssignOrderLine);
        group.MapPut("/unassignOrderLine", UnassignOrderLine);


        return app;
    }

    private static async Task<IResult> Create(
        ShipmentItemRequest request,
        IShipmentItemService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToShipmentItem( request );

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
        ShipmentItemRequest request,
        IShipmentItemService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToShipmentItem( request );

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
        IShipmentItemService service,
        CancellationToken cancellationToken) {

        var shipmentItem = await service.Get(identifier, cancellationToken);
        return shipmentItem is null ? Results.NotFound() : Results.Ok( shipmentItem );
    }


    private static async Task<IResult> GetAll(
        IShipmentItemService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ShipmentItemResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IShipmentItemService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignShipment(
        AssociationRequest request,
        IShipmentItemService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignShipment(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignShipment(
    AssociationRequest request,
    IShipmentItemService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignShipment(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignOrderLine(
        AssociationRequest request,
        IShipmentItemService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignOrderLine(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignOrderLine(
    AssociationRequest request,
    IShipmentItemService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignOrderLine(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static ShipmentItem mapRequestToShipmentItem( ShipmentItemRequest request ) {
        var model = new ShipmentItem
        {
            Id = request.Id,
            Quantity = request.Quantity,
        };
        return model;
    }

}
