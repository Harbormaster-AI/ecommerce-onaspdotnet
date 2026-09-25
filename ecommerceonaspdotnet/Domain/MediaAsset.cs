
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Domain;

public class MediaAsset
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? MediaassetId { get; set; } 
 public virtual string? Url { get; set; } 
 public virtual string? AltText { get; set; } 
 public virtual int? Position { get; set; } 
public virtual Product? Product { get; set; } 
public virtual ProductVariant? Variant { get; set; } 
 public virtual MediaType? MediaType { get; set; } 

    public static MediaAsset FromRequest(MediaAssetRequest request) {
        return new MediaAsset {
            Id = request.Id,
            Url = request.Url,
            AltText = request.AltText,
            Position = request.Position,
            MediaType = request.MediaType,
        };
    }
}
