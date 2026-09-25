
using ecommerceonaspdotnet.Service;
using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Api;

public static class SupplierEndpoints
{
    public static IEndpointRouteBuilder MapSupplierEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/supplier").WithTags("Suppliers");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignMerchant", AssignMerchant);
        group.MapPut("/unassignMerchant", UnassignMerchant);

    group.MapPut("/addToProducts", AddToProducts);
    group.MapPut("/removeFromProducts", RemoveFromProducts);

    group.MapPut("/addToFulfillmentCenters", AddToFulfillmentCenters);
    group.MapPut("/removeFromFulfillmentCenters", RemoveFromFulfillmentCenters);


        return app;
    }

    private static async Task<IResult> Create(
        SupplierRequest request,
        ISupplierService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToSupplier( request );

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
        SupplierRequest request,
        ISupplierService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToSupplier( request );

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
        ISupplierService service,
        CancellationToken cancellationToken) {

        var supplier = await service.Get(identifier, cancellationToken);
        return supplier is null ? Results.NotFound() : Results.Ok( supplier );
    }


    private static async Task<IResult> GetAll(
        ISupplierService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( SupplierResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ISupplierService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignMerchant(
        AssociationRequest request,
        ISupplierService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignMerchant(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignMerchant(
    AssociationRequest request,
    ISupplierService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignMerchant(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToProducts(
        MultipleAssociationRequest request,
        ISupplierService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToProducts(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromProducts(
        MultipleAssociationRequest request,
        ISupplierService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromProducts(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToFulfillmentCenters(
        MultipleAssociationRequest request,
        ISupplierService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToFulfillmentCenters(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromFulfillmentCenters(
        MultipleAssociationRequest request,
        ISupplierService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromFulfillmentCenters(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Supplier mapRequestToSupplier( SupplierRequest request ) {
        var model = new Supplier
        {
            Id = request.Id,
            Name = request.Name,
            ContactEmail = request.ContactEmail,
            Website = request.Website,
            Status = request.Status,
        };
        return model;
    }

}
