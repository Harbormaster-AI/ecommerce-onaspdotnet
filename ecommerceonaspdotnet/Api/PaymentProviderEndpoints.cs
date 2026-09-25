
using ecommerceonaspdotnet.Service;
using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Api;

public static class PaymentProviderEndpoints
{
    public static IEndpointRouteBuilder MapPaymentProviderEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/paymentProvider").WithTags("PaymentProviders");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignMerchant", AssignMerchant);
        group.MapPut("/unassignMerchant", UnassignMerchant);

    group.MapPut("/addToChannels", AddToChannels);
    group.MapPut("/removeFromChannels", RemoveFromChannels);

    group.MapPut("/addToPayments", AddToPayments);
    group.MapPut("/removeFromPayments", RemoveFromPayments);

    group.MapPut("/addToSubscriptions", AddToSubscriptions);
    group.MapPut("/removeFromSubscriptions", RemoveFromSubscriptions);


        return app;
    }

    private static async Task<IResult> Create(
        PaymentProviderRequest request,
        IPaymentProviderService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPaymentProvider( request );

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
        PaymentProviderRequest request,
        IPaymentProviderService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPaymentProvider( request );

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
        IPaymentProviderService service,
        CancellationToken cancellationToken) {

        var paymentProvider = await service.Get(identifier, cancellationToken);
        return paymentProvider is null ? Results.NotFound() : Results.Ok( paymentProvider );
    }


    private static async Task<IResult> GetAll(
        IPaymentProviderService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( PaymentProviderResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IPaymentProviderService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignMerchant(
        AssociationRequest request,
        IPaymentProviderService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignMerchant(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignMerchant(
    AssociationRequest request,
    IPaymentProviderService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignMerchant(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToChannels(
        MultipleAssociationRequest request,
        IPaymentProviderService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToChannels(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromChannels(
        MultipleAssociationRequest request,
        IPaymentProviderService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromChannels(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToPayments(
        MultipleAssociationRequest request,
        IPaymentProviderService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPayments(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPayments(
        MultipleAssociationRequest request,
        IPaymentProviderService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPayments(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToSubscriptions(
        MultipleAssociationRequest request,
        IPaymentProviderService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToSubscriptions(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromSubscriptions(
        MultipleAssociationRequest request,
        IPaymentProviderService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromSubscriptions(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static PaymentProvider mapRequestToPaymentProvider( PaymentProviderRequest request ) {
        var model = new PaymentProvider
        {
            Id = request.Id,
            Name = request.Name,
            Enabled = request.Enabled,
            MerchantAccountId = request.MerchantAccountId,
            ProviderType = request.ProviderType,
        };
        return model;
    }

}
