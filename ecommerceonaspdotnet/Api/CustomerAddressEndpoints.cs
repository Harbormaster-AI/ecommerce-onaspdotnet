
using ecommerceonaspdotnet.Service;
using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Api;

public static class CustomerAddressEndpoints
{
    public static IEndpointRouteBuilder MapCustomerAddressEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/customerAddress").WithTags("CustomerAddresss");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignCustomer", AssignCustomer);
        group.MapPut("/unassignCustomer", UnassignCustomer);


        return app;
    }

    private static async Task<IResult> Create(
        CustomerAddressRequest request,
        ICustomerAddressService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCustomerAddress( request );

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
        CustomerAddressRequest request,
        ICustomerAddressService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCustomerAddress( request );

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
        ICustomerAddressService service,
        CancellationToken cancellationToken) {

        var customerAddress = await service.Get(identifier, cancellationToken);
        return customerAddress is null ? Results.NotFound() : Results.Ok( customerAddress );
    }


    private static async Task<IResult> GetAll(
        ICustomerAddressService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( CustomerAddressResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ICustomerAddressService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCustomer(
        AssociationRequest request,
        ICustomerAddressService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCustomer(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCustomer(
    AssociationRequest request,
    ICustomerAddressService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCustomer(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static CustomerAddress mapRequestToCustomerAddress( CustomerAddressRequest request ) {
        var model = new CustomerAddress
        {
            Id = request.Id,
            Label = request.Label,
            Address = request.Address,
            AsDefaultShipping = request.AsDefaultShipping,
            AsDefaultBilling = request.AsDefaultBilling,
        };
        return model;
    }

}
