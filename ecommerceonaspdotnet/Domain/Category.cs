
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Domain;

public class Category
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? CategoryId { get; set; } 
 public virtual string? Name { get; set; } 
 public virtual string? Slug { get; set; } 
 public virtual int? Position { get; set; } 
 public virtual bool? AsActive { get; set; } 
public virtual Catalog? Catalog { get; set; } 
public virtual Category? ParentCategory { get; set; } 
public virtual ICollection<Category> Subcategories { get; set; } = new List<Category>();
public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public static Category FromRequest(CategoryRequest request) {
        return new Category {
            Id = request.Id,
            Name = request.Name,
            Slug = request.Slug,
            Position = request.Position,
            AsActive = request.AsActive,
        };
    }
}
