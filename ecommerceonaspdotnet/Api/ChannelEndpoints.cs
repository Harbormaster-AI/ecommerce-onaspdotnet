
using ecommerceonaspdotnet.Service;
using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Api;

public static class ChannelEndpoints
{
    public static IEndpointRouteBuilder MapChannelEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/channel").WithTags("Channels");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignMerchant", AssignMerchant);
        group.MapPut("/unassignMerchant", UnassignMerchant);

    group.MapPut("/addToCatalogs", AddToCatalogs);
    group.MapPut("/removeFromCatalogs", RemoveFromCatalogs);

    group.MapPut("/addToPromotions", AddToPromotions);
    group.MapPut("/removeFromPromotions", RemoveFromPromotions);

    group.MapPut("/addToShippingMethods", AddToShippingMethods);
    group.MapPut("/removeFromShippingMethods", RemoveFromShippingMethods);

    group.MapPut("/addToPaymentProviders", AddToPaymentProviders);
    group.MapPut("/removeFromPaymentProviders", RemoveFromPaymentProviders);


        return app;
    }

    private static async Task<IResult> Create(
        ChannelRequest request,
        IChannelService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToChannel( request );

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
        ChannelRequest request,
        IChannelService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToChannel( request );

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
        IChannelService service,
        CancellationToken cancellationToken) {

        var channel = await service.Get(identifier, cancellationToken);
        return channel is null ? Results.NotFound() : Results.Ok( channel );
    }


    private static async Task<IResult> GetAll(
        IChannelService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( ChannelResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IChannelService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignMerchant(
        AssociationRequest request,
        IChannelService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignMerchant(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignMerchant(
    AssociationRequest request,
    IChannelService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignMerchant(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToCatalogs(
        MultipleAssociationRequest request,
        IChannelService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToCatalogs(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromCatalogs(
        MultipleAssociationRequest request,
        IChannelService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromCatalogs(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToPromotions(
        MultipleAssociationRequest request,
        IChannelService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPromotions(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPromotions(
        MultipleAssociationRequest request,
        IChannelService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPromotions(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToShippingMethods(
        MultipleAssociationRequest request,
        IChannelService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToShippingMethods(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromShippingMethods(
        MultipleAssociationRequest request,
        IChannelService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromShippingMethods(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToPaymentProviders(
        MultipleAssociationRequest request,
        IChannelService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPaymentProviders(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPaymentProviders(
        MultipleAssociationRequest request,
        IChannelService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPaymentProviders(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Channel mapRequestToChannel( ChannelRequest request ) {
        var model = new Channel
        {
            Id = request.Id,
            Name = request.Name,
            ChannelCode = request.ChannelCode,
            Locale = request.Locale,
            Domain = request.Domain,
            AsActive = request.AsActive,
            DefaultCurrency = request.DefaultCurrency,
            ChannelType = request.ChannelType,
        };
        return model;
    }

}
