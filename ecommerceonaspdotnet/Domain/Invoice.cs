
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Domain;

public class Invoice
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? InvoiceId { get; set; } 
 public virtual string? InvoiceNumber { get; set; } 
 public virtual DateOnly? IssuedDate { get; set; } 
 public virtual DateOnly? DueDate { get; set; } 
 public virtual Money? Total { get; set; } 
public virtual Order? Order { get; set; } 
 public virtual InvoiceStatus? Status { get; set; } 

    public static Invoice FromRequest(InvoiceRequest request) {
        return new Invoice {
            Id = request.Id,
            InvoiceNumber = request.InvoiceNumber,
            IssuedDate = request.IssuedDate,
            DueDate = request.DueDate,
            Total = request.Total,
            Status = request.Status,
        };
    }
}
