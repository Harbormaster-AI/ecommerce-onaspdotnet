
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Domain;

public class Catalog
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? CatalogId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? CatalogCode { get; set; } 
 public virtual bool? AsActive { get; set; } 
public virtual Channel? Channel { get; set; } 
public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public static Catalog FromRequest(CatalogRequest request) {
        return new Catalog {
            Id = request.Id,
            Name = request.Name,
            CatalogCode = request.CatalogCode,
            AsActive = request.AsActive,
        };
    }
}
