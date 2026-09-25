
using ecommerceonaspdotnet.Service;
using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Api;

public static class CustomerEndpoints
{
    public static IEndpointRouteBuilder MapCustomerEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/customer").WithTags("Customers");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);


    group.MapPut("/addToAddresses", AddToAddresses);
    group.MapPut("/removeFromAddresses", RemoveFromAddresses);

    group.MapPut("/addToCarts", AddToCarts);
    group.MapPut("/removeFromCarts", RemoveFromCarts);

    group.MapPut("/addToOrders", AddToOrders);
    group.MapPut("/removeFromOrders", RemoveFromOrders);

    group.MapPut("/addToPayments", AddToPayments);
    group.MapPut("/removeFromPayments", RemoveFromPayments);

    group.MapPut("/addToReviews", AddToReviews);
    group.MapPut("/removeFromReviews", RemoveFromReviews);

    group.MapPut("/addToWishlists", AddToWishlists);
    group.MapPut("/removeFromWishlists", RemoveFromWishlists);

    group.MapPut("/addToSubscriptions", AddToSubscriptions);
    group.MapPut("/removeFromSubscriptions", RemoveFromSubscriptions);

    group.MapPut("/addToCouponRedemptions", AddToCouponRedemptions);
    group.MapPut("/removeFromCouponRedemptions", RemoveFromCouponRedemptions);

    group.MapPut("/addToGiftCards", AddToGiftCards);
    group.MapPut("/removeFromGiftCards", RemoveFromGiftCards);


        return app;
    }

    private static async Task<IResult> Create(
        CustomerRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCustomer( request );

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
        CustomerRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCustomer( request );

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
        ICustomerService service,
        CancellationToken cancellationToken) {

        var customer = await service.Get(identifier, cancellationToken);
        return customer is null ? Results.NotFound() : Results.Ok( customer );
    }


    private static async Task<IResult> GetAll(
        ICustomerService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( CustomerResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToAddresses(
        MultipleAssociationRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToAddresses(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromAddresses(
        MultipleAssociationRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromAddresses(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToCarts(
        MultipleAssociationRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToCarts(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromCarts(
        MultipleAssociationRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromCarts(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToOrders(
        MultipleAssociationRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToOrders(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromOrders(
        MultipleAssociationRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromOrders(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToPayments(
        MultipleAssociationRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToPayments(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromPayments(
        MultipleAssociationRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromPayments(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToReviews(
        MultipleAssociationRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToReviews(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromReviews(
        MultipleAssociationRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromReviews(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToWishlists(
        MultipleAssociationRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToWishlists(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromWishlists(
        MultipleAssociationRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromWishlists(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToSubscriptions(
        MultipleAssociationRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToSubscriptions(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromSubscriptions(
        MultipleAssociationRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromSubscriptions(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToCouponRedemptions(
        MultipleAssociationRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToCouponRedemptions(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromCouponRedemptions(
        MultipleAssociationRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromCouponRedemptions(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToGiftCards(
        MultipleAssociationRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToGiftCards(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromGiftCards(
        MultipleAssociationRequest request,
        ICustomerService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromGiftCards(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Customer mapRequestToCustomer( CustomerRequest request ) {
        var model = new Customer
        {
            Id = request.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Phone = request.Phone,
            MarketingOptIn = request.MarketingOptIn,
            CustomerGroup = request.CustomerGroup,
        };
        return model;
    }

}
