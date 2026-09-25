
using ecommerceonaspdotnet.Service;
using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Api;

public static class OrderEndpoints
{
    public static IEndpointRouteBuilder MapOrderEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/order").WithTags("Orders");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignCustomer", AssignCustomer);
        group.MapPut("/unassignCustomer", UnassignCustomer);
        group.MapPut("/assignChannel", AssignChannel);
        group.MapPut("/unassignChannel", UnassignChannel);
        group.MapPut("/assignSeller", AssignSeller);
        group.MapPut("/unassignSeller", UnassignSeller);
        group.MapPut("/assignInvoice", AssignInvoice);
        group.MapPut("/unassignInvoice", UnassignInvoice);

    group.MapPut("/addToOrderLines", AddToOrderLines);
    group.MapPut("/removeFromOrderLines", RemoveFromOrderLines);

    group.MapPut("/addToPayments", AddToPayments);
    group.MapPut("/removeFromPayments", RemoveFromPayments);

    group.MapPut("/addToShipments", AddToShipments);
    group.MapPut("/removeFromShipments", RemoveFromShipments);

    group.MapPut("/addToRefunds", AddToRefunds);
    group.MapPut("/removeFromRefunds", RemoveFromRefunds);

    group.MapPut("/addToAppliedPromotions", AddToAppliedPromotions);
    group.MapPut("/removeFromAppliedPromotions", RemoveFromAppliedPromotions);

    group.MapPut("/addToGiftCardRedemptions", AddToGiftCardRedemptions);
    group.MapPut("/removeFromGiftCardRedemptions", RemoveFromGiftCardRedemptions);

    group.MapPut("/addToCouponRedemptions", AddToCouponRedemptions);
    group.MapPut("/removeFromCouponRedemptions", RemoveFromCouponRedemptions);

    group.MapPut("/addToReturnRequests", AddToReturnRequests);
    group.MapPut("/removeFromReturnRequests", RemoveFromReturnRequests);


        return app;
    }

    private static async Task<IResult> Create(
        OrderRequest request,
        IOrderService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToOrder( request );

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
        OrderRequest request,
        IOrderService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToOrder( request );

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
        IOrderService service,
        CancellationToken cancellationToken) {

        var order = await service.Get(identifier, cancellationToken);
        return order is null ? Results.NotFound() : Results.Ok( order );
    }


    private static async Task<IResult> GetAll(
        IOrderService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( OrderResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IOrderService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCustomer(
        AssociationRequest request,
        IOrderService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCustomer(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCustomer(
    AssociationRequest request,
    IOrderService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCustomer(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignChannel(
        AssociationRequest request,
        IOrderService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignChannel(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignChannel(
    AssociationRequest request,
    IOrderService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignChannel(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignSeller(
        AssociationRequest request,
        IOrderService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignSeller(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignSeller(
    AssociationRequest request,
    IOrderService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignSeller(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignInvoice(
        AssociationRequest request,
        IOrderService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignInvoice(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignInvoice(
    AssociationRequest request,
    IOrderService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignInvoice(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToOrderLines(
        MultipleAssociationRequest request,
        IOrderService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToOrderLines(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromOrderLines(
        MultipleAssociationRequest request,
        IOrderService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromOrderLines(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToPayments(
        MultipleAssociationRequest request,
        IOrderService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPayments(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPayments(
        MultipleAssociationRequest request,
        IOrderService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPayments(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToShipments(
        MultipleAssociationRequest request,
        IOrderService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToShipments(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromShipments(
        MultipleAssociationRequest request,
        IOrderService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromShipments(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToRefunds(
        MultipleAssociationRequest request,
        IOrderService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToRefunds(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromRefunds(
        MultipleAssociationRequest request,
        IOrderService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromRefunds(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToAppliedPromotions(
        MultipleAssociationRequest request,
        IOrderService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToAppliedPromotions(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromAppliedPromotions(
        MultipleAssociationRequest request,
        IOrderService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromAppliedPromotions(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToGiftCardRedemptions(
        MultipleAssociationRequest request,
        IOrderService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToGiftCardRedemptions(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromGiftCardRedemptions(
        MultipleAssociationRequest request,
        IOrderService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromGiftCardRedemptions(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToCouponRedemptions(
        MultipleAssociationRequest request,
        IOrderService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToCouponRedemptions(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromCouponRedemptions(
        MultipleAssociationRequest request,
        IOrderService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromCouponRedemptions(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToReturnRequests(
        MultipleAssociationRequest request,
        IOrderService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToReturnRequests(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromReturnRequests(
        MultipleAssociationRequest request,
        IOrderService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromReturnRequests(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Order mapRequestToOrder( OrderRequest request ) {
        var model = new Order
        {
            Id = request.Id,
            OrderNumber = request.OrderNumber,
            PlacedDate = request.PlacedDate,
            Subtotal = request.Subtotal,
            DiscountTotal = request.DiscountTotal,
            ShippingTotal = request.ShippingTotal,
            TaxTotal = request.TaxTotal,
            GrandTotal = request.GrandTotal,
            ShippingAddress = request.ShippingAddress,
            BillingAddress = request.BillingAddress,
            Status = request.Status,
        };
        return model;
    }

}
