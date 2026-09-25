
using ecommerceonaspdotnet.Service;
using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Api;

public static class ShippingMethodEndpoints
{
    public static IEndpointRouteBuilder MapShippingMethodEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/shippingMethod").WithTags("ShippingMethods");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignCarrierService", AssignCarrierService);
        group.MapPut("/unassignCarrierService", UnassignCarrierService);

    group.MapPut("/addToChannels", AddToChannels);
    group.MapPut("/removeFromChannels", RemoveFromChannels);


        return app;
    }

    private static async Task<IResult> Create(
        ShippingMethodRequest request,
        IShippingMethodService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToShippingMethod( request );

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
        ShippingMethodRequest request,
        IShippingMethodService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToShippingMethod( request );

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
        IShippingMethodService service,
        CancellationToken cancellationToken) {

        var shippingMethod = await service.Get(identifier, cancellationToken);
        return shippingMethod is null ? Results.NotFound() : Results.Ok( shippingMethod );
    }


    private static async Task<IResult> GetAll(
        IShippingMethodService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ShippingMethodResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IShippingMethodService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCarrierService(
        AssociationRequest request,
        IShippingMethodService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCarrierService(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCarrierService(
    AssociationRequest request,
    IShippingMethodService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCarrierService(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToChannels(
        MultipleAssociationRequest request,
        IShippingMethodService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToChannels(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromChannels(
        MultipleAssociationRequest request,
        IShippingMethodService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromChannels(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static ShippingMethod mapRequestToShippingMethod( ShippingMethodRequest request ) {
        var model = new ShippingMethod
        {
            Id = request.Id,
            Name = request.Name,
            FlatRate = request.FlatRate,
            EstimatedDays = request.EstimatedDays,
            AsActive = request.AsActive,
            MethodType = request.MethodType,
        };
        return model;
    }

}
