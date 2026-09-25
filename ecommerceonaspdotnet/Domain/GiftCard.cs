
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Domain;

public class GiftCard
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? GiftcardId { get; set; } 
 public virtual string? Code { get; set; } 
 public virtual Money? Balance { get; set; } 
 public virtual DateOnly? ExpirationDate { get; set; } 
public virtual Customer? Customer { get; set; } 
public virtual Order? IssuedOrder { get; set; } 
public virtual ICollection<GiftCardRedemption> Redemptions { get; set; } = new List<GiftCardRedemption>();
 public virtual GiftCardStatus? Status { get; set; } 

    public static GiftCard FromRequest(GiftCardRequest request) {
        return new GiftCard {
            Id = request.Id,
            Code = request.Code,
            Balance = request.Balance,
            ExpirationDate = request.ExpirationDate,
            Status = request.Status,
        };
    }
}
