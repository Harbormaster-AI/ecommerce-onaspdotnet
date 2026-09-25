
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Domain;

public class Order
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? OrderId { get; set; } 
 public virtual string? OrderNumber { get; set; } 
 public virtual DateOnly? PlacedDate { get; set; } 
 public virtual Money? Subtotal { get; set; } 
 public virtual Money? DiscountTotal { get; set; } 
 public virtual Money? ShippingTotal { get; set; } 
 public virtual Money? TaxTotal { get; set; } 
 public virtual Money? GrandTotal { get; set; } 
 public virtual Address? ShippingAddress { get; set; } 
 public virtual Address? BillingAddress { get; set; } 
public virtual Customer? Customer { get; set; } 
public virtual Channel? Channel { get; set; } 
public virtual ICollection<OrderLine> OrderLines { get; set; } = new List<OrderLine>();
public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
public virtual ICollection<Shipment> Shipments { get; set; } = new List<Shipment>();
public virtual ICollection<Refund> Refunds { get; set; } = new List<Refund>();
public virtual ICollection<Promotion> AppliedPromotions { get; set; } = new List<Promotion>();
public virtual Seller? Seller { get; set; } 
public virtual ICollection<GiftCardRedemption> GiftCardRedemptions { get; set; } = new List<GiftCardRedemption>();
public virtual ICollection<CouponRedemption> CouponRedemptions { get; set; } = new List<CouponRedemption>();
public virtual ICollection<ReturnRequest> ReturnRequests { get; set; } = new List<ReturnRequest>();
public virtual Invoice? Invoice { get; set; } 
 public virtual OrderStatus? Status { get; set; } 

    public static Order FromRequest(OrderRequest request) {
        return new Order {
            Id = request.Id,
            OrderNumber = request.OrderNumber,
            PlacedDate = request.PlacedDate,
            Subtotal = request.Subtotal,
            DiscountTotal = request.DiscountTotal,
            ShippingTotal = request.ShippingTotal,
            TaxTotal = request.TaxTotal,
            GrandTotal = request.GrandTotal,
            ShippingAddress = request.ShippingAddress,
            BillingAddress = request.BillingAddress,
            Status = request.Status,
        };
    }
}
