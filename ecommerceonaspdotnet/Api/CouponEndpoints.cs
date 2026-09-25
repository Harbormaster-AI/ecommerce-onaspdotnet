
using ecommerceonaspdotnet.Service;
using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Api;

public static class CouponEndpoints
{
    public static IEndpointRouteBuilder MapCouponEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/coupon").WithTags("Coupons");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignPromotion", AssignPromotion);
        group.MapPut("/unassignPromotion", UnassignPromotion);

    group.MapPut("/addToRedemptions", AddToRedemptions);
    group.MapPut("/removeFromRedemptions", RemoveFromRedemptions);


        return app;
    }

    private static async Task<IResult> Create(
        CouponRequest request,
        ICouponService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCoupon( request );

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
        CouponRequest request,
        ICouponService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCoupon( request );

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
        ICouponService service,
        CancellationToken cancellationToken) {

        var coupon = await service.Get(identifier, cancellationToken);
        return coupon is null ? Results.NotFound() : Results.Ok( coupon );
    }


    private static async Task<IResult> GetAll(
        ICouponService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( CouponResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ICouponService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignPromotion(
        AssociationRequest request,
        ICouponService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignPromotion(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignPromotion(
    AssociationRequest request,
    ICouponService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignPromotion(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToRedemptions(
        MultipleAssociationRequest request,
        ICouponService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToRedemptions(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromRedemptions(
        MultipleAssociationRequest request,
        ICouponService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromRedemptions(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Coupon mapRequestToCoupon( CouponRequest request ) {
        var model = new Coupon
        {
            Id = request.Id,
            Code = request.Code,
            UsageLimit = request.UsageLimit,
            PerCustomerLimit = request.PerCustomerLimit,
            ExpirationDate = request.ExpirationDate,
            Status = request.Status,
        };
        return model;
    }

}
