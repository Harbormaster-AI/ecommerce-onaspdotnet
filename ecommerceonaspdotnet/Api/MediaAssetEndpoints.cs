
using ecommerceonaspdotnet.Service;
using ecommerceonaspdotnet.Domain;
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Api;

public static class MediaAssetEndpoints
{
    public static IEndpointRouteBuilder MapMediaAssetEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/mediaAsset").WithTags("MediaAssets");

        group.MapPost("/create", Create);
        group.MapPost("/get", Get);
        group.MapGet("/getAll", GetAll);
        group.MapPost("/update", Update);
        group.MapPost("/delete", Delete);

        group.MapPut("/assignProduct", AssignProduct);
        group.MapPut("/unassignProduct", UnassignProduct);
        group.MapPut("/assignVariant", AssignVariant);
        group.MapPut("/unassignVariant", UnassignVariant);


        return app;
    }

    private static async Task<IResult> Create(
        MediaAssetRequest request,
        IMediaAssetService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToMediaAsset( request );

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
        MediaAssetRequest request,
        IMediaAssetService service,
        CancellationToken cancellationToken) {

        var model = mapRequestToMediaAsset( request );

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
        IMediaAssetService service,
        CancellationToken cancellationToken) {

        var mediaAsset = await service.Get(identifier, cancellationToken);
        return mediaAsset is null ? Results.NotFound() : Results.Ok( mediaAsset );
    }


    private static async Task<IResult> GetAll(
        IMediaAssetService service,
        CancellationToken cancellationToken) {

        var all = await service.GetAll(cancellationToken);
        return Results.Ok( all.Select( MediaAssetResponse.FromModel ) );
        }

    private static async Task<IResult> Delete(
        IdentifierRequest identifier,
        IMediaAssetService service,
        CancellationToken cancellationToken) {
        var deleted = await service.Delete(identifier, cancellationToken);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignProduct(
        AssociationRequest request,
        IMediaAssetService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignProduct(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignProduct(
    AssociationRequest request,
    IMediaAssetService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignProduct(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> AssignVariant(
        AssociationRequest request,
        IMediaAssetService service,
        CancellationToken cancellationToken) {
        var assigned = await service.AssignVariant(request, cancellationToken);
        return assigned ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> UnassignVariant(
    AssociationRequest request,
    IMediaAssetService service,
    CancellationToken cancellationToken) {
        var unassigned = await service.UnassignVariant(request, cancellationToken);
        return unassigned ? Results.NoContent() : Results.NotFound();
    }


    private static MediaAsset mapRequestToMediaAsset( MediaAssetRequest request ) {
        var model = new MediaAsset
        {
            Id = request.Id,
            Url = request.Url,
            AltText = request.AltText,
            Position = request.Position,
            MediaType = request.MediaType,
        };
        return model;
    }

}
