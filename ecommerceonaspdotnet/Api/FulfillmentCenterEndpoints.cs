
using ecommerceonaspdotnet.Service;
using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Api;

public static class FulfillmentCenterEndpoints
{
    public static IEndpointRouteBuilder MapFulfillmentCenterEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/fulfillmentCenter").WithTags("FulfillmentCenters");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignMerchant", AssignMerchant);
        group.MapPut("/unassignMerchant", UnassignMerchant);

    group.MapPut("/addToInventoryItems", AddToInventoryItems);
    group.MapPut("/removeFromInventoryItems", RemoveFromInventoryItems);

    group.MapPut("/addToShipments", AddToShipments);
    group.MapPut("/removeFromShipments", RemoveFromShipments);


        return app;
    }

    private static async Task<IResult> Create(
        FulfillmentCenterRequest request,
        IFulfillmentCenterService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToFulfillmentCenter( request );

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
        FulfillmentCenterRequest request,
        IFulfillmentCenterService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToFulfillmentCenter( request );

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
        IFulfillmentCenterService service,
        CancellationToken cancellationToken) {

        var fulfillmentCenter = await service.Get(identifier, cancellationToken);
        return fulfillmentCenter is null ? Results.NotFound() : Results.Ok( fulfillmentCenter );
    }


    private static async Task<IResult> GetAll(
        IFulfillmentCenterService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( FulfillmentCenterResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IFulfillmentCenterService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignMerchant(
        AssociationRequest request,
        IFulfillmentCenterService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignMerchant(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignMerchant(
    AssociationRequest request,
    IFulfillmentCenterService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignMerchant(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToInventoryItems(
        MultipleAssociationRequest request,
        IFulfillmentCenterService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToInventoryItems(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromInventoryItems(
        MultipleAssociationRequest request,
        IFulfillmentCenterService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromInventoryItems(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToShipments(
        MultipleAssociationRequest request,
        IFulfillmentCenterService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToShipments(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromShipments(
        MultipleAssociationRequest request,
        IFulfillmentCenterService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromShipments(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static FulfillmentCenter mapRequestToFulfillmentCenter( FulfillmentCenterRequest request ) {
        var model = new FulfillmentCenter
        {
            Id = request.Id,
            Name = request.Name,
            CenterCode = request.CenterCode,
            Address = request.Address,
            Timezone = request.Timezone,
            AsActive = request.AsActive,
        };
        return model;
    }

}
