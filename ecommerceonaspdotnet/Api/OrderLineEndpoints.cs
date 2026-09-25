
using ecommerceonaspdotnet.Service;
using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Api;

public static class OrderLineEndpoints
{
    public static IEndpointRouteBuilder MapOrderLineEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/orderLine").WithTags("OrderLines");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignOrder", AssignOrder);
        group.MapPut("/unassignOrder", UnassignOrder);
        group.MapPut("/assignVariant", AssignVariant);
        group.MapPut("/unassignVariant", UnassignVariant);

    group.MapPut("/addToAppliedPromotions", AddToAppliedPromotions);
    group.MapPut("/removeFromAppliedPromotions", RemoveFromAppliedPromotions);


        return app;
    }

    private static async Task<IResult> Create(
        OrderLineRequest request,
        IOrderLineService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToOrderLine( request );

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
        OrderLineRequest request,
        IOrderLineService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToOrderLine( request );

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
        IOrderLineService service,
        CancellationToken cancellationToken) {

        var orderLine = await service.Get(identifier, cancellationToken);
        return orderLine is null ? Results.NotFound() : Results.Ok( orderLine );
    }


    private static async Task<IResult> GetAll(
        IOrderLineService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( OrderLineResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IOrderLineService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignOrder(
        AssociationRequest request,
        IOrderLineService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignOrder(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignOrder(
    AssociationRequest request,
    IOrderLineService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignOrder(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignVariant(
        AssociationRequest request,
        IOrderLineService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignVariant(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignVariant(
    AssociationRequest request,
    IOrderLineService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignVariant(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToAppliedPromotions(
        MultipleAssociationRequest request,
        IOrderLineService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToAppliedPromotions(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromAppliedPromotions(
        MultipleAssociationRequest request,
        IOrderLineService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromAppliedPromotions(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static OrderLine mapRequestToOrderLine( OrderLineRequest request ) {
        var model = new OrderLine
        {
            Id = request.Id,
            Quantity = request.Quantity,
            UnitPrice = request.UnitPrice,
            TotalPrice = request.TotalPrice,
            TaxRate = request.TaxRate,
            LineStatus = request.LineStatus,
        };
        return model;
    }

}
