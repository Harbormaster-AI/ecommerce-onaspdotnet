
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Domain;

public class OrderLine
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? OrderlineId { get; set; } 
 public virtual int? Quantity { get; set; } 
 public virtual Money? UnitPrice { get; set; } 
 public virtual Money? TotalPrice { get; set; } 
 public virtual Percentage? TaxRate { get; set; } 
public virtual Order? Order { get; set; } 
public virtual ProductVariant? Variant { get; set; } 
public virtual ICollection<Promotion> AppliedPromotions { get; set; } = new List<Promotion>();
 public virtual OrderLineStatus? LineStatus { get; set; } 

    public static OrderLine FromRequest(OrderLineRequest request) {
        return new OrderLine {
            Id = request.Id,
            Quantity = request.Quantity,
            UnitPrice = request.UnitPrice,
            TotalPrice = request.TotalPrice,
            TaxRate = request.TaxRate,
            LineStatus = request.LineStatus,
        };
    }
}
