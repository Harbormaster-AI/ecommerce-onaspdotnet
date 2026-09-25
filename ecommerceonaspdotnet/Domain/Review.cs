
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Domain;

public class Review
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? ReviewId { get; set; } 
 public virtual int? Rating { get; set; } 
 public virtual string? Title { get; set; } 
 public virtual string? Content { get; set; } 
 public virtual DateOnly? CreatedAt { get; set; } 
public virtual Product? Product { get; set; } 
public virtual Customer? Customer { get; set; } 
public virtual Order? Order { get; set; } 
 public virtual ReviewStatus? Status { get; set; } 

    public static Review FromRequest(ReviewRequest request) {
        return new Review {
            Id = request.Id,
            Rating = request.Rating,
            Title = request.Title,
            Content = request.Content,
            CreatedAt = request.CreatedAt,
            Status = request.Status,
        };
    }
}
