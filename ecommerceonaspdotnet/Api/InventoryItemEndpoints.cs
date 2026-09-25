
using ecommerceonaspdotnet.Service;
using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Api;

public static class InventoryItemEndpoints
{
    public static IEndpointRouteBuilder MapInventoryItemEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/inventoryItem").WithTags("InventoryItems");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignVariant", AssignVariant);
        group.MapPut("/unassignVariant", UnassignVariant);
        group.MapPut("/assignFulfillmentCenter", AssignFulfillmentCenter);
        group.MapPut("/unassignFulfillmentCenter", UnassignFulfillmentCenter);


        return app;
    }

    private static async Task<IResult> Create(
        InventoryItemRequest request,
        IInventoryItemService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToInventoryItem( request );

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
        InventoryItemRequest request,
        IInventoryItemService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToInventoryItem( request );

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
        IInventoryItemService service,
        CancellationToken cancellationToken) {

        var inventoryItem = await service.Get(identifier, cancellationToken);
        return inventoryItem is null ? Results.NotFound() : Results.Ok( inventoryItem );
    }


    private static async Task<IResult> GetAll(
        IInventoryItemService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( InventoryItemResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IInventoryItemService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignVariant(
        AssociationRequest request,
        IInventoryItemService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignVariant(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignVariant(
    AssociationRequest request,
    IInventoryItemService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignVariant(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignFulfillmentCenter(
        AssociationRequest request,
        IInventoryItemService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignFulfillmentCenter(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignFulfillmentCenter(
    AssociationRequest request,
    IInventoryItemService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignFulfillmentCenter(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static InventoryItem mapRequestToInventoryItem( InventoryItemRequest request ) {
        var model = new InventoryItem
        {
            Id = request.Id,
            QuantityOnHand = request.QuantityOnHand,
            QuantityReserved = request.QuantityReserved,
            SafetyStock = request.SafetyStock,
            Status = request.Status,
        };
        return model;
    }

}
