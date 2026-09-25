
using ecommerceonaspdotnet.Service;
using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Api;

public static class CarrierServiceEndpoints
{
    public static IEndpointRouteBuilder MapCarrierServiceEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/carrierService").WithTags("CarrierServices");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);


    group.MapPut("/addToShippingMethods", AddToShippingMethods);
    group.MapPut("/removeFromShippingMethods", RemoveFromShippingMethods);


        return app;
    }

    private static async Task<IResult> Create(
        CarrierServiceRequest request,
        ICarrierServiceService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCarrierService( request );

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
        CarrierServiceRequest request,
        ICarrierServiceService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCarrierService( request );

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
        ICarrierServiceService service,
        CancellationToken cancellationToken) {

        var carrierService = await service.Get(identifier, cancellationToken);
        return carrierService is null ? Results.NotFound() : Results.Ok( carrierService );
    }


    private static async Task<IResult> GetAll(
        ICarrierServiceService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( CarrierServiceResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ICarrierServiceService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToShippingMethods(
        MultipleAssociationRequest request,
        ICarrierServiceService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToShippingMethods(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromShippingMethods(
        MultipleAssociationRequest request,
        ICarrierServiceService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromShippingMethods(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static CarrierService mapRequestToCarrierService( CarrierServiceRequest request ) {
        var model = new CarrierService
        {
            Id = request.Id,
            Name = request.Name,
            Code = request.Code,
            Carrier = request.Carrier,
            ServiceLevel = request.ServiceLevel,
        };
        return model;
    }

}
