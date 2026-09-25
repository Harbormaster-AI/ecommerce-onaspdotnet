
using ecommerceonaspdotnet.Service;
using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Api;

public static class CategoryEndpoints
{
    public static IEndpointRouteBuilder MapCategoryEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/category").WithTags("Categorys");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignCatalog", AssignCatalog);
        group.MapPut("/unassignCatalog", UnassignCatalog);
        group.MapPut("/assignParentCategory", AssignParentCategory);
        group.MapPut("/unassignParentCategory", UnassignParentCategory);

    group.MapPut("/addToSubcategories", AddToSubcategories);
    group.MapPut("/removeFromSubcategories", RemoveFromSubcategories);

    group.MapPut("/addToProducts", AddToProducts);
    group.MapPut("/removeFromProducts", RemoveFromProducts);


        return app;
    }

    private static async Task<IResult> Create(
        CategoryRequest request,
        ICategoryService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCategory( request );

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
        CategoryRequest request,
        ICategoryService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToCategory( request );

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
        ICategoryService service,
        CancellationToken cancellationToken) {

        var category = await service.Get(identifier, cancellationToken);
        return category is null ? Results.NotFound() : Results.Ok( category );
    }


    private static async Task<IResult> GetAll(
        ICategoryService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( CategoryResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        ICategoryService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignCatalog(
        AssociationRequest request,
        ICategoryService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignCatalog(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignCatalog(
    AssociationRequest request,
    ICategoryService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignCatalog(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignParentCategory(
        AssociationRequest request,
        ICategoryService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignParentCategory(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignParentCategory(
    AssociationRequest request,
    ICategoryService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignParentCategory(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static async Task<IResult> AddToSubcategories(
        MultipleAssociationRequest request,
        ICategoryService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToSubcategories(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromSubcategories(
        MultipleAssociationRequest request,
        ICategoryService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromSubcategories(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static async Task<IResult> AddToProducts(
        MultipleAssociationRequest request,
        ICategoryService service,
        CancellationToken cancellationToken) {
        var addTo = await service.AddToProducts(request, cancellationToken);
        return addTo ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveFromProducts(
        MultipleAssociationRequest request,
        ICategoryService service,
        CancellationToken cancellationToken) {
        var removeFrom = await service.RemoveFromProducts(request, cancellationToken);
        return removeFrom ? Results.NoContent() : Results.NotFound();
    }
    private static Category mapRequestToCategory( CategoryRequest request ) {
        var model = new Category
        {
            Id = request.Id,
            Name = request.Name,
            Slug = request.Slug,
            Position = request.Position,
            AsActive = request.AsActive,
        };
        return model;
    }

}
