
using ecommerceonaspdotnet.Service;
using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Api;

public static class ShipmentEndpoints
{
    public static IEndpointRouteBuilder MapShipmentEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/shipment").WithTags("Shipments");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignOrder", AssignOrder);
        group.MapPut("/unassignOrder", UnassignOrder);
        group.MapPut("/assignFulfillmentCenter", AssignFulfillmentCenter);
        group.MapPut("/unassignFulfillmentCenter", UnassignFulfillmentCenter);

    group.MapPut("/addToShipmentItems", AddToShipmentItems);
    group.MapPut("/removeFromShipmentItems", RemoveFromShipmentItems);


        return app;
    }

    private static async Task<IResult> Create(
        ShipmentRequest request,
        IShipmentService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToShipment( request );

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
        ShipmentRequest request,
        IShipmentService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToShipment( request );

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
        IShipmentService service,
        CancellationToken cancellationToken) {

        var shipment = await service.Get(identifier, cancellationToken);
        return shipment is null ? Results.NotFound() : Results.Ok( shipment );
    }


    private static async Task<IResult> GetAll(
        IShipmentService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ShipmentResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IShipmentService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignOrder(
        AssociationRequest request,
        IShipmentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignOrder(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignOrder(
    AssociationRequest request,
    IShipmentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignOrder(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignFulfillmentCenter(
        AssociationRequest request,
        IShipmentService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignFulfillmentCenter(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignFulfillmentCenter(
    AssociationRequest request,
    IShipmentService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignFulfillmentCenter(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToShipmentItems(
        MultipleAssociationRequest request,
        IShipmentService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToShipmentItems(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromShipmentItems(
        MultipleAssociationRequest request,
        IShipmentService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromShipmentItems(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Shipment mapRequestToShipment( ShipmentRequest request ) {
        var model = new Shipment
        {
            Id = request.Id,
            ShipmentNumber = request.ShipmentNumber,
            ShippedDate = request.ShippedDate,
            DeliveredDate = request.DeliveredDate,
            TrackingNumber = request.TrackingNumber,
            ShippingAddress = request.ShippingAddress,
            Status = request.Status,
            Carrier = request.Carrier,
        };
        return model;
    }

}
