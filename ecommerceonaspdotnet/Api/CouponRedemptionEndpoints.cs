
using ecommerceonaspdotnet.Service;
using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Api;

public static class CouponRedemptionEndpoints
{
    public static IEndpointRouteBuilder MapCouponRedemptionEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/couponRedemption").WithTags("CouponRedemptions");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignCoupon", AssignCoupon);
        group.MapPut("/unassignCoupon", UnassignCoupon);
        group.MapPut("/assignOrder", AssignOrder);
        group.MapPut("/unassignOrder", UnassignOrder);
        group.MapPut("/assignCustomer", AssignCustomer);
        group.MapPut("/unassignCustomer", UnassignCustomer);


        return app;
    }

    private static async Task<IResult> Create(
        CouponRedemptionRequest request,
        ICouponRedemptionService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCouponRedemption( request );

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
        CouponRedemptionRequest request,
        ICouponRedemptionService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCouponRedemption( request );

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
        ICouponRedemptionService service,
        CancellationToken cancellationToken) {

        var couponRedemption = await service.Get(identifier, cancellationToken);
        return couponRedemption is null ? Results.NotFound() : Results.Ok( couponRedemption );
    }


    private static async Task<IResult> GetAll(
        ICouponRedemptionService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( CouponRedemptionResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ICouponRedemptionService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCoupon(
        AssociationRequest request,
        ICouponRedemptionService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCoupon(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCoupon(
    AssociationRequest request,
    ICouponRedemptionService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCoupon(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignOrder(
        AssociationRequest request,
        ICouponRedemptionService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignOrder(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignOrder(
    AssociationRequest request,
    ICouponRedemptionService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignOrder(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCustomer(
        AssociationRequest request,
        ICouponRedemptionService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCustomer(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCustomer(
    AssociationRequest request,
    ICouponRedemptionService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCustomer(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static CouponRedemption mapRequestToCouponRedemption( CouponRedemptionRequest request ) {
        var model = new CouponRedemption
        {
            Id = request.Id,
            RedeemedAt = request.RedeemedAt,
        };
        return model;
    }

}
