
using ecommerceonaspdotnet.Service;
using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Api;

public static class SellerEndpoints
{
    public static IEndpointRouteBuilder MapSellerEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/seller").WithTags("Sellers");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignMerchant", AssignMerchant);
        group.MapPut("/unassignMerchant", UnassignMerchant);

    group.MapPut("/addToProducts", AddToProducts);
    group.MapPut("/removeFromProducts", RemoveFromProducts);

    group.MapPut("/addToPayouts", AddToPayouts);
    group.MapPut("/removeFromPayouts", RemoveFromPayouts);

    group.MapPut("/addToOrders", AddToOrders);
    group.MapPut("/removeFromOrders", RemoveFromOrders);


        return app;
    }

    private static async Task<IResult> Create(
        SellerRequest request,
        ISellerService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToSeller( request );

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
        SellerRequest request,
        ISellerService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToSeller( request );

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
        ISellerService service,
        CancellationToken cancellationToken) {

        var seller = await service.Get(identifier, cancellationToken);
        return seller is null ? Results.NotFound() : Results.Ok( seller );
    }


    private static async Task<IResult> GetAll(
        ISellerService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( SellerResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ISellerService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignMerchant(
        AssociationRequest request,
        ISellerService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignMerchant(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignMerchant(
    AssociationRequest request,
    ISellerService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignMerchant(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToProducts(
        MultipleAssociationRequest request,
        ISellerService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToProducts(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromProducts(
        MultipleAssociationRequest request,
        ISellerService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromProducts(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToPayouts(
        MultipleAssociationRequest request,
        ISellerService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPayouts(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPayouts(
        MultipleAssociationRequest request,
        ISellerService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPayouts(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToOrders(
        MultipleAssociationRequest request,
        ISellerService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToOrders(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromOrders(
        MultipleAssociationRequest request,
        ISellerService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromOrders(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Seller mapRequestToSeller( SellerRequest request ) {
        var model = new Seller
        {
            Id = request.Id,
            Name = request.Name,
            SellerCode = request.SellerCode,
            ContactEmail = request.ContactEmail,
            Status = request.Status,
        };
        return model;
    }

}
