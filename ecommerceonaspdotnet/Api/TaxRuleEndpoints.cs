
using ecommerceonaspdotnet.Service;
using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Api;

public static class TaxRuleEndpoints
{
    public static IEndpointRouteBuilder MapTaxRuleEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/taxRule").WithTags("TaxRules");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignMerchant", AssignMerchant);
        group.MapPut("/unassignMerchant", UnassignMerchant);

    group.MapPut("/addToChannels", AddToChannels);
    group.MapPut("/removeFromChannels", RemoveFromChannels);


        return app;
    }

    private static async Task<IResult> Create(
        TaxRuleRequest request,
        ITaxRuleService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToTaxRule( request );

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
        TaxRuleRequest request,
        ITaxRuleService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToTaxRule( request );

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
        ITaxRuleService service,
        CancellationToken cancellationToken) {

        var taxRule = await service.Get(identifier, cancellationToken);
        return taxRule is null ? Results.NotFound() : Results.Ok( taxRule );
    }


    private static async Task<IResult> GetAll(
        ITaxRuleService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( TaxRuleResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ITaxRuleService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignMerchant(
        AssociationRequest request,
        ITaxRuleService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignMerchant(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignMerchant(
    AssociationRequest request,
    ITaxRuleService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignMerchant(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToChannels(
        MultipleAssociationRequest request,
        ITaxRuleService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToChannels(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromChannels(
        MultipleAssociationRequest request,
        ITaxRuleService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromChannels(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static TaxRule mapRequestToTaxRule( TaxRuleRequest request ) {
        var model = new TaxRule
        {
            Id = request.Id,
            Name = request.Name,
            Country = request.Country,
            Region = request.Region,
            Rate = request.Rate,
            TaxInclusive = request.TaxInclusive,
            TaxClass = request.TaxClass,
        };
        return model;
    }

}
