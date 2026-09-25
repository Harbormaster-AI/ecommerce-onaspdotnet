
using ecommerceonaspdotnet.Contracts;

namespace ecommerceonaspdotnet.Domain;

public class GiftCardRedemption
{
    public Guid Id { get; set; } = Guid.NewGuid();

 public virtual long? GiftcardredemptionId { get; set; } 
 public virtual DateOnly? RedeemedAt { get; set; } 
 public virtual Money? Amount { get; set; } 
public virtual GiftCard? GiftCard { get; set; } 
public virtual Order? Order { get; set; } 

    public static GiftCardRedemption FromRequest(GiftCardRedemptionRequest request) {
        return new GiftCardRedemption {
            Id = request.Id,
            RedeemedAt = request.RedeemedAt,
            Amount = request.Amount,
        };
    }
}
