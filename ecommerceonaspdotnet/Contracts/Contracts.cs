using ecommerceonaspdotnet.Domain;

namespace ecommerceonaspdotnet.Contracts;

public class IdentifierRequest
{
    public Guid Id { get; set; }
}

public class AssociationRequest
{
    public Guid ParentId { get; set; }
    public Guid ChildId { get; set; }
}

public class MultipleAssociationRequest
{
    public Guid ParentId { get; set; }
    public List<Guid> ChildIds { get; set; } = new();
}

public class MerchantRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? LegalName { get; set; } 
 public virtual string? Website { get; set; } 
 public virtual string? DefaultCurrency { get; set; } 
 public virtual string? DefaultLocale { get; set; } 
 public virtual string? SupportEmail { get; set; } 
}

public class MerchantResponse : MerchantRequest {
    public static MerchantResponse FromModel(Merchant model) {
        return new MerchantResponse {
            Id = model.Id,
            Name = model.Name,
            LegalName = model.LegalName,
            Website = model.Website,
            DefaultCurrency = model.DefaultCurrency,
            DefaultLocale = model.DefaultLocale,
            SupportEmail = model.SupportEmail,
        };
    }
}

public class ChannelRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? ChannelCode { get; set; } 
 public virtual string? Locale { get; set; } 
 public virtual string? Domain { get; set; } 
 public virtual bool? AsActive { get; set; } 
 public virtual string? DefaultCurrency { get; set; } 
 public virtual ChannelType? ChannelType { get; set; } 
}

public class ChannelResponse : ChannelRequest {
    public static ChannelResponse FromModel(Channel model) {
        return new ChannelResponse {
            Id = model.Id,
            Name = model.Name,
            ChannelCode = model.ChannelCode,
            Locale = model.Locale,
            Domain = model.Domain,
            AsActive = model.AsActive,
            DefaultCurrency = model.DefaultCurrency,
            ChannelType = model.ChannelType,
        };
    }
}

public class BrandRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? Description { get; set; } 
 public virtual string? Website { get; set; } 
}

public class BrandResponse : BrandRequest {
    public static BrandResponse FromModel(Brand model) {
        return new BrandResponse {
            Id = model.Id,
            Name = model.Name,
            Description = model.Description,
            Website = model.Website,
        };
    }
}

public class CatalogRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? CatalogCode { get; set; } 
 public virtual bool? AsActive { get; set; } 
}

public class CatalogResponse : CatalogRequest {
    public static CatalogResponse FromModel(Catalog model) {
        return new CatalogResponse {
            Id = model.Id,
            Name = model.Name,
            CatalogCode = model.CatalogCode,
            AsActive = model.AsActive,
        };
    }
}

public class CategoryRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? Slug { get; set; } 
 public virtual int? Position { get; set; } 
 public virtual bool? AsActive { get; set; } 
}

public class CategoryResponse : CategoryRequest {
    public static CategoryResponse FromModel(Category model) {
        return new CategoryResponse {
            Id = model.Id,
            Name = model.Name,
            Slug = model.Slug,
            Position = model.Position,
            AsActive = model.AsActive,
        };
    }
}

public class ProductRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? Slug { get; set; } 
 public virtual bool? AsActive { get; set; } 
 public virtual ProductType? ProductType { get; set; } 
 public virtual TaxClass? DefaultTaxClass { get; set; } 
}

public class ProductResponse : ProductRequest {
    public static ProductResponse FromModel(Product model) {
        return new ProductResponse {
            Id = model.Id,
            Name = model.Name,
            Slug = model.Slug,
            AsActive = model.AsActive,
            ProductType = model.ProductType,
            DefaultTaxClass = model.DefaultTaxClass,
        };
    }
}

public class ProductVariantRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual SKU? Sku { get; set; } 
 public virtual string? Barcode { get; set; } 
 public virtual string? Title { get; set; } 
 public virtual decimal? Weight { get; set; } 
 public virtual bool? RequiresShipping { get; set; } 
 public virtual WeightUnit? WeightUnit { get; set; } 
}

public class ProductVariantResponse : ProductVariantRequest {
    public static ProductVariantResponse FromModel(ProductVariant model) {
        return new ProductVariantResponse {
            Id = model.Id,
            Sku = model.Sku,
            Barcode = model.Barcode,
            Title = model.Title,
            Weight = model.Weight,
            RequiresShipping = model.RequiresShipping,
            WeightUnit = model.WeightUnit,
        };
    }
}

public class ProductPricingRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual Money? ListPrice { get; set; } 
 public virtual Money? SalePrice { get; set; } 
 public virtual DateOnly? ValidFrom { get; set; } 
 public virtual DateOnly? ValidTo { get; set; } 
}

public class ProductPricingResponse : ProductPricingRequest {
    public static ProductPricingResponse FromModel(ProductPricing model) {
        return new ProductPricingResponse {
            Id = model.Id,
            ListPrice = model.ListPrice,
            SalePrice = model.SalePrice,
            ValidFrom = model.ValidFrom,
            ValidTo = model.ValidTo,
        };
    }
}

public class MediaAssetRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Url { get; set; } 
 public virtual string? AltText { get; set; } 
 public virtual int? Position { get; set; } 
 public virtual MediaType? MediaType { get; set; } 
}

public class MediaAssetResponse : MediaAssetRequest {
    public static MediaAssetResponse FromModel(MediaAsset model) {
        return new MediaAssetResponse {
            Id = model.Id,
            Url = model.Url,
            AltText = model.AltText,
            Position = model.Position,
            MediaType = model.MediaType,
        };
    }
}

public class FulfillmentCenterRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? CenterCode { get; set; } 
 public virtual Address? Address { get; set; } 
 public virtual string? Timezone { get; set; } 
 public virtual bool? AsActive { get; set; } 
}

public class FulfillmentCenterResponse : FulfillmentCenterRequest {
    public static FulfillmentCenterResponse FromModel(FulfillmentCenter model) {
        return new FulfillmentCenterResponse {
            Id = model.Id,
            Name = model.Name,
            CenterCode = model.CenterCode,
            Address = model.Address,
            Timezone = model.Timezone,
            AsActive = model.AsActive,
        };
    }
}

public class InventoryItemRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual int? QuantityOnHand { get; set; } 
 public virtual int? QuantityReserved { get; set; } 
 public virtual int? SafetyStock { get; set; } 
 public virtual InventoryStatus? Status { get; set; } 
}

public class InventoryItemResponse : InventoryItemRequest {
    public static InventoryItemResponse FromModel(InventoryItem model) {
        return new InventoryItemResponse {
            Id = model.Id,
            QuantityOnHand = model.QuantityOnHand,
            QuantityReserved = model.QuantityReserved,
            SafetyStock = model.SafetyStock,
            Status = model.Status,
        };
    }
}

public class SupplierRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? ContactEmail { get; set; } 
 public virtual string? Website { get; set; } 
 public virtual SupplierStatus? Status { get; set; } 
}

public class SupplierResponse : SupplierRequest {
    public static SupplierResponse FromModel(Supplier model) {
        return new SupplierResponse {
            Id = model.Id,
            Name = model.Name,
            ContactEmail = model.ContactEmail,
            Website = model.Website,
            Status = model.Status,
        };
    }
}

public class SellerRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? SellerCode { get; set; } 
 public virtual string? ContactEmail { get; set; } 
 public virtual SellerStatus? Status { get; set; } 
}

public class SellerResponse : SellerRequest {
    public static SellerResponse FromModel(Seller model) {
        return new SellerResponse {
            Id = model.Id,
            Name = model.Name,
            SellerCode = model.SellerCode,
            ContactEmail = model.ContactEmail,
            Status = model.Status,
        };
    }
}

public class CustomerRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? FirstName { get; set; } 
 public virtual string? LastName { get; set; } 
 public virtual string? Email { get; set; } 
 public virtual string? Phone { get; set; } 
 public virtual bool? MarketingOptIn { get; set; } 
 public virtual CustomerGroup? CustomerGroup { get; set; } 
}

public class CustomerResponse : CustomerRequest {
    public static CustomerResponse FromModel(Customer model) {
        return new CustomerResponse {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            Phone = model.Phone,
            MarketingOptIn = model.MarketingOptIn,
            CustomerGroup = model.CustomerGroup,
        };
    }
}

public class CustomerAddressRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Label { get; set; } 
 public virtual Address? Address { get; set; } 
 public virtual bool? AsDefaultShipping { get; set; } 
 public virtual bool? AsDefaultBilling { get; set; } 
}

public class CustomerAddressResponse : CustomerAddressRequest {
    public static CustomerAddressResponse FromModel(CustomerAddress model) {
        return new CustomerAddressResponse {
            Id = model.Id,
            Label = model.Label,
            Address = model.Address,
            AsDefaultShipping = model.AsDefaultShipping,
            AsDefaultBilling = model.AsDefaultBilling,
        };
    }
}

public class WishlistRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual bool? AsPublic { get; set; } 
 public virtual DateOnly? CreatedAt { get; set; } 
}

public class WishlistResponse : WishlistRequest {
    public static WishlistResponse FromModel(Wishlist model) {
        return new WishlistResponse {
            Id = model.Id,
            Name = model.Name,
            AsPublic = model.AsPublic,
            CreatedAt = model.CreatedAt,
        };
    }
}

public class WishlistItemRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual DateOnly? AddedDate { get; set; } 
}

public class WishlistItemResponse : WishlistItemRequest {
    public static WishlistItemResponse FromModel(WishlistItem model) {
        return new WishlistItemResponse {
            Id = model.Id,
            AddedDate = model.AddedDate,
        };
    }
}

public class CartRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? CartNumber { get; set; } 
 public virtual DateOnly? CreatedAt { get; set; } 
 public virtual string? Currency { get; set; } 
 public virtual Address? ShippingAddress { get; set; } 
 public virtual Address? BillingAddress { get; set; } 
 public virtual CartStatus? Status { get; set; } 
}

public class CartResponse : CartRequest {
    public static CartResponse FromModel(Cart model) {
        return new CartResponse {
            Id = model.Id,
            CartNumber = model.CartNumber,
            CreatedAt = model.CreatedAt,
            Currency = model.Currency,
            ShippingAddress = model.ShippingAddress,
            BillingAddress = model.BillingAddress,
            Status = model.Status,
        };
    }
}

public class CartItemRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual int? Quantity { get; set; } 
 public virtual Money? UnitPrice { get; set; } 
 public virtual Money? TotalPrice { get; set; } 
}

public class CartItemResponse : CartItemRequest {
    public static CartItemResponse FromModel(CartItem model) {
        return new CartItemResponse {
            Id = model.Id,
            Quantity = model.Quantity,
            UnitPrice = model.UnitPrice,
            TotalPrice = model.TotalPrice,
        };
    }
}

public class OrderRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? OrderNumber { get; set; } 
 public virtual DateOnly? PlacedDate { get; set; } 
 public virtual Money? Subtotal { get; set; } 
 public virtual Money? DiscountTotal { get; set; } 
 public virtual Money? ShippingTotal { get; set; } 
 public virtual Money? TaxTotal { get; set; } 
 public virtual Money? GrandTotal { get; set; } 
 public virtual Address? ShippingAddress { get; set; } 
 public virtual Address? BillingAddress { get; set; } 
 public virtual OrderStatus? Status { get; set; } 
}

public class OrderResponse : OrderRequest {
    public static OrderResponse FromModel(Order model) {
        return new OrderResponse {
            Id = model.Id,
            OrderNumber = model.OrderNumber,
            PlacedDate = model.PlacedDate,
            Subtotal = model.Subtotal,
            DiscountTotal = model.DiscountTotal,
            ShippingTotal = model.ShippingTotal,
            TaxTotal = model.TaxTotal,
            GrandTotal = model.GrandTotal,
            ShippingAddress = model.ShippingAddress,
            BillingAddress = model.BillingAddress,
            Status = model.Status,
        };
    }
}

public class OrderLineRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual int? Quantity { get; set; } 
 public virtual Money? UnitPrice { get; set; } 
 public virtual Money? TotalPrice { get; set; } 
 public virtual Percentage? TaxRate { get; set; } 
 public virtual OrderLineStatus? LineStatus { get; set; } 
}

public class OrderLineResponse : OrderLineRequest {
    public static OrderLineResponse FromModel(OrderLine model) {
        return new OrderLineResponse {
            Id = model.Id,
            Quantity = model.Quantity,
            UnitPrice = model.UnitPrice,
            TotalPrice = model.TotalPrice,
            TaxRate = model.TaxRate,
            LineStatus = model.LineStatus,
        };
    }
}

public class PaymentRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? PaymentNumber { get; set; } 
 public virtual Money? Amount { get; set; } 
 public virtual string? TransactionId { get; set; } 
 public virtual DateOnly? AuthorizedAt { get; set; } 
 public virtual DateOnly? CapturedAt { get; set; } 
 public virtual PaymentStatus? Status { get; set; } 
 public virtual PaymentMethodType? PaymentMethod { get; set; } 
}

public class PaymentResponse : PaymentRequest {
    public static PaymentResponse FromModel(Payment model) {
        return new PaymentResponse {
            Id = model.Id,
            PaymentNumber = model.PaymentNumber,
            Amount = model.Amount,
            TransactionId = model.TransactionId,
            AuthorizedAt = model.AuthorizedAt,
            CapturedAt = model.CapturedAt,
            Status = model.Status,
            PaymentMethod = model.PaymentMethod,
        };
    }
}

public class RefundRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? RefundNumber { get; set; } 
 public virtual Money? Amount { get; set; } 
 public virtual string? Reason { get; set; } 
 public virtual DateOnly? CreatedAt { get; set; } 
 public virtual RefundStatus? Status { get; set; } 
}

public class RefundResponse : RefundRequest {
    public static RefundResponse FromModel(Refund model) {
        return new RefundResponse {
            Id = model.Id,
            RefundNumber = model.RefundNumber,
            Amount = model.Amount,
            Reason = model.Reason,
            CreatedAt = model.CreatedAt,
            Status = model.Status,
        };
    }
}

public class ShipmentRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? ShipmentNumber { get; set; } 
 public virtual DateOnly? ShippedDate { get; set; } 
 public virtual DateOnly? DeliveredDate { get; set; } 
 public virtual string? TrackingNumber { get; set; } 
 public virtual Address? ShippingAddress { get; set; } 
 public virtual ShipmentStatus? Status { get; set; } 
 public virtual Carrier? Carrier { get; set; } 
}

public class ShipmentResponse : ShipmentRequest {
    public static ShipmentResponse FromModel(Shipment model) {
        return new ShipmentResponse {
            Id = model.Id,
            ShipmentNumber = model.ShipmentNumber,
            ShippedDate = model.ShippedDate,
            DeliveredDate = model.DeliveredDate,
            TrackingNumber = model.TrackingNumber,
            ShippingAddress = model.ShippingAddress,
            Status = model.Status,
            Carrier = model.Carrier,
        };
    }
}

public class ShipmentItemRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual int? Quantity { get; set; } 
}

public class ShipmentItemResponse : ShipmentItemRequest {
    public static ShipmentItemResponse FromModel(ShipmentItem model) {
        return new ShipmentItemResponse {
            Id = model.Id,
            Quantity = model.Quantity,
        };
    }
}

public class ReturnRequestRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? ReturnNumber { get; set; } 
 public virtual DateOnly? CreatedAt { get; set; } 
 public virtual Money? RefundAmount { get; set; } 
 public virtual ReturnStatus? Status { get; set; } 
}

public class ReturnRequestResponse : ReturnRequestRequest {
    public static ReturnRequestResponse FromModel(ReturnRequest model) {
        return new ReturnRequestResponse {
            Id = model.Id,
            ReturnNumber = model.ReturnNumber,
            CreatedAt = model.CreatedAt,
            RefundAmount = model.RefundAmount,
            Status = model.Status,
        };
    }
}

public class ReturnItemRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual int? Quantity { get; set; } 
 public virtual ReturnReason? Reason { get; set; } 
 public virtual ReturnItemCondition? Condition { get; set; } 
}

public class ReturnItemResponse : ReturnItemRequest {
    public static ReturnItemResponse FromModel(ReturnItem model) {
        return new ReturnItemResponse {
            Id = model.Id,
            Quantity = model.Quantity,
            Reason = model.Reason,
            Condition = model.Condition,
        };
    }
}

public class PromotionRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? Code { get; set; } 
 public virtual decimal? Value { get; set; } 
 public virtual DateOnly? StartDate { get; set; } 
 public virtual DateOnly? EndDate { get; set; } 
 public virtual bool? AsStackable { get; set; } 
 public virtual int? MaxRedemptions { get; set; } 
 public virtual PromotionType? PromotionType { get; set; } 
 public virtual DiscountType? DiscountType { get; set; } 
}

public class PromotionResponse : PromotionRequest {
    public static PromotionResponse FromModel(Promotion model) {
        return new PromotionResponse {
            Id = model.Id,
            Name = model.Name,
            Code = model.Code,
            Value = model.Value,
            StartDate = model.StartDate,
            EndDate = model.EndDate,
            AsStackable = model.AsStackable,
            MaxRedemptions = model.MaxRedemptions,
            PromotionType = model.PromotionType,
            DiscountType = model.DiscountType,
        };
    }
}

public class CouponRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Code { get; set; } 
 public virtual int? UsageLimit { get; set; } 
 public virtual int? PerCustomerLimit { get; set; } 
 public virtual DateOnly? ExpirationDate { get; set; } 
 public virtual CouponStatus? Status { get; set; } 
}

public class CouponResponse : CouponRequest {
    public static CouponResponse FromModel(Coupon model) {
        return new CouponResponse {
            Id = model.Id,
            Code = model.Code,
            UsageLimit = model.UsageLimit,
            PerCustomerLimit = model.PerCustomerLimit,
            ExpirationDate = model.ExpirationDate,
            Status = model.Status,
        };
    }
}

public class CouponRedemptionRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual DateOnly? RedeemedAt { get; set; } 
}

public class CouponRedemptionResponse : CouponRedemptionRequest {
    public static CouponRedemptionResponse FromModel(CouponRedemption model) {
        return new CouponRedemptionResponse {
            Id = model.Id,
            RedeemedAt = model.RedeemedAt,
        };
    }
}

public class TaxRuleRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? Country { get; set; } 
 public virtual string? Region { get; set; } 
 public virtual Percentage? Rate { get; set; } 
 public virtual bool? TaxInclusive { get; set; } 
 public virtual TaxClass? TaxClass { get; set; } 
}

public class TaxRuleResponse : TaxRuleRequest {
    public static TaxRuleResponse FromModel(TaxRule model) {
        return new TaxRuleResponse {
            Id = model.Id,
            Name = model.Name,
            Country = model.Country,
            Region = model.Region,
            Rate = model.Rate,
            TaxInclusive = model.TaxInclusive,
            TaxClass = model.TaxClass,
        };
    }
}

public class ShippingMethodRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual Money? FlatRate { get; set; } 
 public virtual int? EstimatedDays { get; set; } 
 public virtual bool? AsActive { get; set; } 
 public virtual ShippingMethodType? MethodType { get; set; } 
}

public class ShippingMethodResponse : ShippingMethodRequest {
    public static ShippingMethodResponse FromModel(ShippingMethod model) {
        return new ShippingMethodResponse {
            Id = model.Id,
            Name = model.Name,
            FlatRate = model.FlatRate,
            EstimatedDays = model.EstimatedDays,
            AsActive = model.AsActive,
            MethodType = model.MethodType,
        };
    }
}

public class CarrierServiceRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual string? Code { get; set; } 
 public virtual Carrier? Carrier { get; set; } 
 public virtual ServiceLevel? ServiceLevel { get; set; } 
}

public class CarrierServiceResponse : CarrierServiceRequest {
    public static CarrierServiceResponse FromModel(CarrierService model) {
        return new CarrierServiceResponse {
            Id = model.Id,
            Name = model.Name,
            Code = model.Code,
            Carrier = model.Carrier,
            ServiceLevel = model.ServiceLevel,
        };
    }
}

public class ReviewRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual int? Rating { get; set; } 
 public virtual string? Title { get; set; } 
 public virtual string? Content { get; set; } 
 public virtual DateOnly? CreatedAt { get; set; } 
 public virtual ReviewStatus? Status { get; set; } 
}

public class ReviewResponse : ReviewRequest {
    public static ReviewResponse FromModel(Review model) {
        return new ReviewResponse {
            Id = model.Id,
            Rating = model.Rating,
            Title = model.Title,
            Content = model.Content,
            CreatedAt = model.CreatedAt,
            Status = model.Status,
        };
    }
}

public class SubscriptionRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? SubscriptionNumber { get; set; } 
 public virtual DateOnly? NextBillingDate { get; set; } 
 public virtual DateOnly? StartDate { get; set; } 
 public virtual DateOnly? EndDate { get; set; } 
 public virtual SubscriptionStatus? Status { get; set; } 
 public virtual SubscriptionInterval? Interval { get; set; } 
}

public class SubscriptionResponse : SubscriptionRequest {
    public static SubscriptionResponse FromModel(Subscription model) {
        return new SubscriptionResponse {
            Id = model.Id,
            SubscriptionNumber = model.SubscriptionNumber,
            NextBillingDate = model.NextBillingDate,
            StartDate = model.StartDate,
            EndDate = model.EndDate,
            Status = model.Status,
            Interval = model.Interval,
        };
    }
}

public class PaymentProviderRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Name { get; set; } 
 public virtual bool? Enabled { get; set; } 
 public virtual string? MerchantAccountId { get; set; } 
 public virtual PaymentProviderType? ProviderType { get; set; } 
}

public class PaymentProviderResponse : PaymentProviderRequest {
    public static PaymentProviderResponse FromModel(PaymentProvider model) {
        return new PaymentProviderResponse {
            Id = model.Id,
            Name = model.Name,
            Enabled = model.Enabled,
            MerchantAccountId = model.MerchantAccountId,
            ProviderType = model.ProviderType,
        };
    }
}

public class InvoiceRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? InvoiceNumber { get; set; } 
 public virtual DateOnly? IssuedDate { get; set; } 
 public virtual DateOnly? DueDate { get; set; } 
 public virtual Money? Total { get; set; } 
 public virtual InvoiceStatus? Status { get; set; } 
}

public class InvoiceResponse : InvoiceRequest {
    public static InvoiceResponse FromModel(Invoice model) {
        return new InvoiceResponse {
            Id = model.Id,
            InvoiceNumber = model.InvoiceNumber,
            IssuedDate = model.IssuedDate,
            DueDate = model.DueDate,
            Total = model.Total,
            Status = model.Status,
        };
    }
}

public class GiftCardRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? Code { get; set; } 
 public virtual Money? Balance { get; set; } 
 public virtual DateOnly? ExpirationDate { get; set; } 
 public virtual GiftCardStatus? Status { get; set; } 
}

public class GiftCardResponse : GiftCardRequest {
    public static GiftCardResponse FromModel(GiftCard model) {
        return new GiftCardResponse {
            Id = model.Id,
            Code = model.Code,
            Balance = model.Balance,
            ExpirationDate = model.ExpirationDate,
            Status = model.Status,
        };
    }
}

public class GiftCardRedemptionRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual DateOnly? RedeemedAt { get; set; } 
 public virtual Money? Amount { get; set; } 
}

public class GiftCardRedemptionResponse : GiftCardRedemptionRequest {
    public static GiftCardRedemptionResponse FromModel(GiftCardRedemption model) {
        return new GiftCardRedemptionResponse {
            Id = model.Id,
            RedeemedAt = model.RedeemedAt,
            Amount = model.Amount,
        };
    }
}

public class PayoutRequest {
    public Guid Id { get; set; } = Guid.NewGuid();
 public virtual string? PayoutNumber { get; set; } 
 public virtual Money? Amount { get; set; } 
 public virtual DateOnly? ScheduledDate { get; set; } 
 public virtual DateOnly? PaidDate { get; set; } 
 public virtual PayoutStatus? Status { get; set; } 
}

public class PayoutResponse : PayoutRequest {
    public static PayoutResponse FromModel(Payout model) {
        return new PayoutResponse {
            Id = model.Id,
            PayoutNumber = model.PayoutNumber,
            Amount = model.Amount,
            ScheduledDate = model.ScheduledDate,
            PaidDate = model.PaidDate,
            Status = model.Status,
        };
    }
}

