
using ecommerceonaspdotnet.Service;
using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Api;

public static class MerchantEndpoints
{
    public static IEndpointRouteBuilder MapMerchantEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/merchant").WithTags("Merchants");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);


    group.MapPut("/addToChannels", AddToChannels);
    group.MapPut("/removeFromChannels", RemoveFromChannels);

    group.MapPut("/addToBrands", AddToBrands);
    group.MapPut("/removeFromBrands", RemoveFromBrands);

    group.MapPut("/addToFulfillmentCenters", AddToFulfillmentCenters);
    group.MapPut("/removeFromFulfillmentCenters", RemoveFromFulfillmentCenters);

    group.MapPut("/addToTaxRules", AddToTaxRules);
    group.MapPut("/removeFromTaxRules", RemoveFromTaxRules);

    group.MapPut("/addToPaymentProviders", AddToPaymentProviders);
    group.MapPut("/removeFromPaymentProviders", RemoveFromPaymentProviders);

    group.MapPut("/addToSellers", AddToSellers);
    group.MapPut("/removeFromSellers", RemoveFromSellers);

    group.MapPut("/addToPromotions", AddToPromotions);
    group.MapPut("/removeFromPromotions", RemoveFromPromotions);


        return app;
    }

    private static async Task<IResult> Create(
        MerchantRequest request,
        IMerchantService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToMerchant( request );

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
        MerchantRequest request,
        IMerchantService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToMerchant( request );

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
        IMerchantService service,
        CancellationToken cancellationToken) {

        var merchant = await service.Get(identifier, cancellationToken);
        return merchant is null ? Results.NotFound() : Results.Ok( merchant );
    }


    private static async Task<IResult> GetAll(
        IMerchantService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( MerchantResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IMerchantService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToChannels(
        MultipleAssociationRequest request,
        IMerchantService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToChannels(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromChannels(
        MultipleAssociationRequest request,
        IMerchantService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromChannels(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToBrands(
        MultipleAssociationRequest request,
        IMerchantService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToBrands(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromBrands(
        MultipleAssociationRequest request,
        IMerchantService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromBrands(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToFulfillmentCenters(
        MultipleAssociationRequest request,
        IMerchantService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToFulfillmentCenters(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromFulfillmentCenters(
        MultipleAssociationRequest request,
        IMerchantService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromFulfillmentCenters(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToTaxRules(
        MultipleAssociationRequest request,
        IMerchantService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToTaxRules(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromTaxRules(
        MultipleAssociationRequest request,
        IMerchantService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromTaxRules(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToPaymentProviders(
        MultipleAssociationRequest request,
        IMerchantService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPaymentProviders(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPaymentProviders(
        MultipleAssociationRequest request,
        IMerchantService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPaymentProviders(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToSellers(
        MultipleAssociationRequest request,
        IMerchantService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToSellers(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromSellers(
        MultipleAssociationRequest request,
        IMerchantService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromSellers(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToPromotions(
        MultipleAssociationRequest request,
        IMerchantService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPromotions(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPromotions(
        MultipleAssociationRequest request,
        IMerchantService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPromotions(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Merchant mapRequestToMerchant( MerchantRequest request ) {
        var model = new Merchant
        {
            Id = request.Id,
            Name = request.Name,
            LegalName = request.LegalName,
            Website = request.Website,
            DefaultCurrency = request.DefaultCurrency,
            DefaultLocale = request.DefaultLocale,
            SupportEmail = request.SupportEmail,
        };
        return model;
    }

}
