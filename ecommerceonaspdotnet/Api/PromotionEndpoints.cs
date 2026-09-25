
using ecommerceonaspdotnet.Service;
using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Api;

public static class PromotionEndpoints
{
    public static IEndpointRouteBuilder MapPromotionEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/promotion").WithTags("Promotions");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignMerchant", AssignMerchant);
        group.MapPut("/unassignMerchant", UnassignMerchant);

    group.MapPut("/addToChannels", AddToChannels);
    group.MapPut("/removeFromChannels", RemoveFromChannels);

    group.MapPut("/addToApplicableProducts", AddToApplicableProducts);
    group.MapPut("/removeFromApplicableProducts", RemoveFromApplicableProducts);

    group.MapPut("/addToApplicableCategories", AddToApplicableCategories);
    group.MapPut("/removeFromApplicableCategories", RemoveFromApplicableCategories);

    group.MapPut("/addToCoupons", AddToCoupons);
    group.MapPut("/removeFromCoupons", RemoveFromCoupons);


        return app;
    }

    private static async Task<IResult> Create(
        PromotionRequest request,
        IPromotionService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPromotion( request );

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
        PromotionRequest request,
        IPromotionService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToPromotion( request );

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
        IPromotionService service,
        CancellationToken cancellationToken) {

        var promotion = await service.Get(identifier, cancellationToken);
        return promotion is null ? Results.NotFound() : Results.Ok( promotion );
    }


    private static async Task<IResult> GetAll(
        IPromotionService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( PromotionResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IPromotionService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignMerchant(
        AssociationRequest request,
        IPromotionService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignMerchant(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignMerchant(
    AssociationRequest request,
    IPromotionService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignMerchant(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToChannels(
        MultipleAssociationRequest request,
        IPromotionService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToChannels(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromChannels(
        MultipleAssociationRequest request,
        IPromotionService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromChannels(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToApplicableProducts(
        MultipleAssociationRequest request,
        IPromotionService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToApplicableProducts(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromApplicableProducts(
        MultipleAssociationRequest request,
        IPromotionService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromApplicableProducts(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToApplicableCategories(
        MultipleAssociationRequest request,
        IPromotionService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToApplicableCategories(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromApplicableCategories(
        MultipleAssociationRequest request,
        IPromotionService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromApplicableCategories(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToCoupons(
        MultipleAssociationRequest request,
        IPromotionService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToCoupons(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromCoupons(
        MultipleAssociationRequest request,
        IPromotionService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromCoupons(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Promotion mapRequestToPromotion( PromotionRequest request ) {
        var model = new Promotion
        {
            Id = request.Id,
            Name = request.Name,
            Code = request.Code,
            Value = request.Value,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            AsStackable = request.AsStackable,
            MaxRedemptions = request.MaxRedemptions,
            PromotionType = request.PromotionType,
            DiscountType = request.DiscountType,
        };
        return model;
    }

}
