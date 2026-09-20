using Microsoft.EntityFrameworkCore;

using ecommerceonaspdotnet.Domain;

namespace ecommerceonaspdotnet.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

public DbSet<Merchant> Merchants => Set<Merchant>();
public DbSet<Channel> Channels => Set<Channel>();
public DbSet<Brand> Brands => Set<Brand>();
public DbSet<Catalog> Catalogs => Set<Catalog>();
public DbSet<Category> Categorys => Set<Category>();
public DbSet<Product> Products => Set<Product>();
public DbSet<ProductVariant> ProductVariants => Set<ProductVariant>();
public DbSet<ProductPricing> ProductPricings => Set<ProductPricing>();
public DbSet<MediaAsset> MediaAssets => Set<MediaAsset>();
public DbSet<FulfillmentCenter> FulfillmentCenters => Set<FulfillmentCenter>();
public DbSet<InventoryItem> InventoryItems => Set<InventoryItem>();
public DbSet<Supplier> Suppliers => Set<Supplier>();
public DbSet<Seller> Sellers => Set<Seller>();
public DbSet<Customer> Customers => Set<Customer>();
public DbSet<CustomerAddress> CustomerAddresss => Set<CustomerAddress>();
public DbSet<Wishlist> Wishlists => Set<Wishlist>();
public DbSet<WishlistItem> WishlistItems => Set<WishlistItem>();
public DbSet<Cart> Carts => Set<Cart>();
public DbSet<CartItem> CartItems => Set<CartItem>();
public DbSet<Order> Orders => Set<Order>();
public DbSet<OrderLine> OrderLines => Set<OrderLine>();
public DbSet<Payment> Payments => Set<Payment>();
public DbSet<Refund> Refunds => Set<Refund>();
public DbSet<Shipment> Shipments => Set<Shipment>();
public DbSet<ShipmentItem> ShipmentItems => Set<ShipmentItem>();
public DbSet<ReturnRequest> ReturnRequests => Set<ReturnRequest>();
public DbSet<ReturnItem> ReturnItems => Set<ReturnItem>();
public DbSet<Promotion> Promotions => Set<Promotion>();
public DbSet<Coupon> Coupons => Set<Coupon>();
public DbSet<CouponRedemption> CouponRedemptions => Set<CouponRedemption>();
public DbSet<TaxRule> TaxRules => Set<TaxRule>();
public DbSet<ShippingMethod> ShippingMethods => Set<ShippingMethod>();
public DbSet<CarrierService> CarrierServices => Set<CarrierService>();
public DbSet<Review> Reviews => Set<Review>();
public DbSet<Subscription> Subscriptions => Set<Subscription>();
public DbSet<PaymentProvider> PaymentProviders => Set<PaymentProvider>();
public DbSet<Invoice> Invoices => Set<Invoice>();
public DbSet<GiftCard> GiftCards => Set<GiftCard>();
public DbSet<GiftCardRedemption> GiftCardRedemptions => Set<GiftCardRedemption>();
public DbSet<Payout> Payouts => Set<Payout>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        // Merchant has one or more Channels of type Channel
        modelBuilder.Entity<Channel>()
            .HasOne<Merchant>()
            .WithMany(parent => parent.Channels)
            .HasForeignKey("ChannelsId");

        // Merchant has one or more Brands of type Brand
        modelBuilder.Entity<Brand>()
            .HasOne<Merchant>()
            .WithMany(parent => parent.Brands)
            .HasForeignKey("BrandsId");

        // Merchant has one or more FulfillmentCenters of type FulfillmentCenter
        modelBuilder.Entity<FulfillmentCenter>()
            .HasOne<Merchant>()
            .WithMany(parent => parent.FulfillmentCenters)
            .HasForeignKey("FulfillmentCentersId");

        // Merchant has one or more TaxRules of type TaxRule
        modelBuilder.Entity<TaxRule>()
            .HasOne<Merchant>()
            .WithMany(parent => parent.TaxRules)
            .HasForeignKey("TaxRulesId");

        // Merchant has one or more PaymentProviders of type PaymentProvider
        modelBuilder.Entity<PaymentProvider>()
            .HasOne<Merchant>()
            .WithMany(parent => parent.PaymentProviders)
            .HasForeignKey("PaymentProvidersId");

        // Merchant has one or more Sellers of type Seller
        modelBuilder.Entity<Seller>()
            .HasOne<Merchant>()
            .WithMany(parent => parent.Sellers)
            .HasForeignKey("SellersId");

        // Merchant has one or more Promotions of type Promotion
        modelBuilder.Entity<Promotion>()
            .HasOne<Merchant>()
            .WithMany(parent => parent.Promotions)
            .HasForeignKey("PromotionsId");

        // Channel has one Merchant of type Merchant
        modelBuilder.Entity<Channel>()
            .HasOne(x => x.Merchant)
            .WithMany()
            .HasForeignKey("MerchantId");


        // Channel has one or more Catalogs of type Catalog
        modelBuilder.Entity<Catalog>()
            .HasOne<Channel>()
            .WithMany(parent => parent.Catalogs)
            .HasForeignKey("CatalogsId");

        // Channel has one or more Promotions of type Promotion
        modelBuilder.Entity<Promotion>()
            .HasOne<Channel>()
            .WithMany(parent => parent.Promotions)
            .HasForeignKey("PromotionsId");

        // Channel has one or more ShippingMethods of type ShippingMethod
        modelBuilder.Entity<ShippingMethod>()
            .HasOne<Channel>()
            .WithMany(parent => parent.ShippingMethods)
            .HasForeignKey("ShippingMethodsId");

        // Channel has one or more PaymentProviders of type PaymentProvider
        modelBuilder.Entity<PaymentProvider>()
            .HasOne<Channel>()
            .WithMany(parent => parent.PaymentProviders)
            .HasForeignKey("PaymentProvidersId");

        // Brand has one Merchant of type Merchant
        modelBuilder.Entity<Brand>()
            .HasOne(x => x.Merchant)
            .WithMany()
            .HasForeignKey("MerchantId");


        // Brand has one or more Products of type Product
        modelBuilder.Entity<Product>()
            .HasOne<Brand>()
            .WithMany(parent => parent.Products)
            .HasForeignKey("ProductsId");

        // Catalog has one Channel of type Channel
        modelBuilder.Entity<Catalog>()
            .HasOne(x => x.Channel)
            .WithMany()
            .HasForeignKey("ChannelId");


        // Catalog has one or more Categories of type Category
        modelBuilder.Entity<Category>()
            .HasOne<Catalog>()
            .WithMany(parent => parent.Categories)
            .HasForeignKey("CategoriesId");

        // Category has one Catalog of type Catalog
        modelBuilder.Entity<Category>()
            .HasOne(x => x.Catalog)
            .WithMany()
            .HasForeignKey("CatalogId");

        // Category has one ParentCategory of type Category
        modelBuilder.Entity<Category>()
            .HasOne(x => x.ParentCategory)
            .WithMany()
            .HasForeignKey("ParentCategoryId");


        // Category has one or more Subcategories of type Category
        modelBuilder.Entity<Category>()
            .HasOne<Category>()
            .WithMany(parent => parent.Subcategories)
            .HasForeignKey("SubcategoriesId");

        // Category has one or more Products of type Product
        modelBuilder.Entity<Product>()
            .HasOne<Category>()
            .WithMany(parent => parent.Products)
            .HasForeignKey("ProductsId");

        // Product has one Brand of type Brand
        modelBuilder.Entity<Product>()
            .HasOne(x => x.Brand)
            .WithMany()
            .HasForeignKey("BrandId");

        // Product has one Seller of type Seller
        modelBuilder.Entity<Product>()
            .HasOne(x => x.Seller)
            .WithMany()
            .HasForeignKey("SellerId");


        // Product has one or more Categories of type Category
        modelBuilder.Entity<Category>()
            .HasOne<Product>()
            .WithMany(parent => parent.Categories)
            .HasForeignKey("CategoriesId");

        // Product has one or more Variants of type ProductVariant
        modelBuilder.Entity<ProductVariant>()
            .HasOne<Product>()
            .WithMany(parent => parent.Variants)
            .HasForeignKey("VariantsId");

        // Product has one or more MediaAssets of type MediaAsset
        modelBuilder.Entity<MediaAsset>()
            .HasOne<Product>()
            .WithMany(parent => parent.MediaAssets)
            .HasForeignKey("MediaAssetsId");

        // Product has one or more Reviews of type Review
        modelBuilder.Entity<Review>()
            .HasOne<Product>()
            .WithMany(parent => parent.Reviews)
            .HasForeignKey("ReviewsId");

        // ProductVariant has one Product of type Product
        modelBuilder.Entity<ProductVariant>()
            .HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey("ProductId");


        // ProductVariant has one or more Pricing of type ProductPricing
        modelBuilder.Entity<ProductPricing>()
            .HasOne<ProductVariant>()
            .WithMany(parent => parent.Pricing)
            .HasForeignKey("PricingId");

        // ProductVariant has one or more InventoryItems of type InventoryItem
        modelBuilder.Entity<InventoryItem>()
            .HasOne<ProductVariant>()
            .WithMany(parent => parent.InventoryItems)
            .HasForeignKey("InventoryItemsId");

        // ProductVariant has one or more MediaAssets of type MediaAsset
        modelBuilder.Entity<MediaAsset>()
            .HasOne<ProductVariant>()
            .WithMany(parent => parent.MediaAssets)
            .HasForeignKey("MediaAssetsId");

        // ProductVariant has one or more Subscriptions of type Subscription
        modelBuilder.Entity<Subscription>()
            .HasOne<ProductVariant>()
            .WithMany(parent => parent.Subscriptions)
            .HasForeignKey("SubscriptionsId");

        // ProductVariant has one or more CartItems of type CartItem
        modelBuilder.Entity<CartItem>()
            .HasOne<ProductVariant>()
            .WithMany(parent => parent.CartItems)
            .HasForeignKey("CartItemsId");

        // ProductVariant has one or more OrderLines of type OrderLine
        modelBuilder.Entity<OrderLine>()
            .HasOne<ProductVariant>()
            .WithMany(parent => parent.OrderLines)
            .HasForeignKey("OrderLinesId");

        // ProductVariant has one or more WishlistItems of type WishlistItem
        modelBuilder.Entity<WishlistItem>()
            .HasOne<ProductVariant>()
            .WithMany(parent => parent.WishlistItems)
            .HasForeignKey("WishlistItemsId");

        // ProductPricing has one Variant of type ProductVariant
        modelBuilder.Entity<ProductPricing>()
            .HasOne(x => x.Variant)
            .WithMany()
            .HasForeignKey("VariantId");

        // ProductPricing has one Channel of type Channel
        modelBuilder.Entity<ProductPricing>()
            .HasOne(x => x.Channel)
            .WithMany()
            .HasForeignKey("ChannelId");


        // MediaAsset has one Product of type Product
        modelBuilder.Entity<MediaAsset>()
            .HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey("ProductId");

        // MediaAsset has one Variant of type ProductVariant
        modelBuilder.Entity<MediaAsset>()
            .HasOne(x => x.Variant)
            .WithMany()
            .HasForeignKey("VariantId");


        // FulfillmentCenter has one Merchant of type Merchant
        modelBuilder.Entity<FulfillmentCenter>()
            .HasOne(x => x.Merchant)
            .WithMany()
            .HasForeignKey("MerchantId");


        // FulfillmentCenter has one or more InventoryItems of type InventoryItem
        modelBuilder.Entity<InventoryItem>()
            .HasOne<FulfillmentCenter>()
            .WithMany(parent => parent.InventoryItems)
            .HasForeignKey("InventoryItemsId");

        // FulfillmentCenter has one or more Shipments of type Shipment
        modelBuilder.Entity<Shipment>()
            .HasOne<FulfillmentCenter>()
            .WithMany(parent => parent.Shipments)
            .HasForeignKey("ShipmentsId");

        // InventoryItem has one Variant of type ProductVariant
        modelBuilder.Entity<InventoryItem>()
            .HasOne(x => x.Variant)
            .WithMany()
            .HasForeignKey("VariantId");

        // InventoryItem has one FulfillmentCenter of type FulfillmentCenter
        modelBuilder.Entity<InventoryItem>()
            .HasOne(x => x.FulfillmentCenter)
            .WithMany()
            .HasForeignKey("FulfillmentCenterId");


        // Supplier has one Merchant of type Merchant
        modelBuilder.Entity<Supplier>()
            .HasOne(x => x.Merchant)
            .WithMany()
            .HasForeignKey("MerchantId");


        // Supplier has one or more Products of type Product
        modelBuilder.Entity<Product>()
            .HasOne<Supplier>()
            .WithMany(parent => parent.Products)
            .HasForeignKey("ProductsId");

        // Supplier has one or more FulfillmentCenters of type FulfillmentCenter
        modelBuilder.Entity<FulfillmentCenter>()
            .HasOne<Supplier>()
            .WithMany(parent => parent.FulfillmentCenters)
            .HasForeignKey("FulfillmentCentersId");

        // Seller has one Merchant of type Merchant
        modelBuilder.Entity<Seller>()
            .HasOne(x => x.Merchant)
            .WithMany()
            .HasForeignKey("MerchantId");


        // Seller has one or more Products of type Product
        modelBuilder.Entity<Product>()
            .HasOne<Seller>()
            .WithMany(parent => parent.Products)
            .HasForeignKey("ProductsId");

        // Seller has one or more Payouts of type Payout
        modelBuilder.Entity<Payout>()
            .HasOne<Seller>()
            .WithMany(parent => parent.Payouts)
            .HasForeignKey("PayoutsId");

        // Seller has one or more Orders of type Order
        modelBuilder.Entity<Order>()
            .HasOne<Seller>()
            .WithMany(parent => parent.Orders)
            .HasForeignKey("OrdersId");


        // Customer has one or more Addresses of type CustomerAddress
        modelBuilder.Entity<CustomerAddress>()
            .HasOne<Customer>()
            .WithMany(parent => parent.Addresses)
            .HasForeignKey("AddressesId");

        // Customer has one or more Carts of type Cart
        modelBuilder.Entity<Cart>()
            .HasOne<Customer>()
            .WithMany(parent => parent.Carts)
            .HasForeignKey("CartsId");

        // Customer has one or more Orders of type Order
        modelBuilder.Entity<Order>()
            .HasOne<Customer>()
            .WithMany(parent => parent.Orders)
            .HasForeignKey("OrdersId");

        // Customer has one or more Payments of type Payment
        modelBuilder.Entity<Payment>()
            .HasOne<Customer>()
            .WithMany(parent => parent.Payments)
            .HasForeignKey("PaymentsId");

        // Customer has one or more Reviews of type Review
        modelBuilder.Entity<Review>()
            .HasOne<Customer>()
            .WithMany(parent => parent.Reviews)
            .HasForeignKey("ReviewsId");

        // Customer has one or more Wishlists of type Wishlist
        modelBuilder.Entity<Wishlist>()
            .HasOne<Customer>()
            .WithMany(parent => parent.Wishlists)
            .HasForeignKey("WishlistsId");

        // Customer has one or more Subscriptions of type Subscription
        modelBuilder.Entity<Subscription>()
            .HasOne<Customer>()
            .WithMany(parent => parent.Subscriptions)
            .HasForeignKey("SubscriptionsId");

        // Customer has one or more CouponRedemptions of type CouponRedemption
        modelBuilder.Entity<CouponRedemption>()
            .HasOne<Customer>()
            .WithMany(parent => parent.CouponRedemptions)
            .HasForeignKey("CouponRedemptionsId");

        // Customer has one or more GiftCards of type GiftCard
        modelBuilder.Entity<GiftCard>()
            .HasOne<Customer>()
            .WithMany(parent => parent.GiftCards)
            .HasForeignKey("GiftCardsId");

        // CustomerAddress has one Customer of type Customer
        modelBuilder.Entity<CustomerAddress>()
            .HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey("CustomerId");


        // Wishlist has one Customer of type Customer
        modelBuilder.Entity<Wishlist>()
            .HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey("CustomerId");


        // Wishlist has one or more Items of type WishlistItem
        modelBuilder.Entity<WishlistItem>()
            .HasOne<Wishlist>()
            .WithMany(parent => parent.Items)
            .HasForeignKey("ItemsId");

        // WishlistItem has one Wishlist of type Wishlist
        modelBuilder.Entity<WishlistItem>()
            .HasOne(x => x.Wishlist)
            .WithMany()
            .HasForeignKey("WishlistId");

        // WishlistItem has one Variant of type ProductVariant
        modelBuilder.Entity<WishlistItem>()
            .HasOne(x => x.Variant)
            .WithMany()
            .HasForeignKey("VariantId");


        // Cart has one Customer of type Customer
        modelBuilder.Entity<Cart>()
            .HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey("CustomerId");

        // Cart has one Channel of type Channel
        modelBuilder.Entity<Cart>()
            .HasOne(x => x.Channel)
            .WithMany()
            .HasForeignKey("ChannelId");


        // Cart has one or more Items of type CartItem
        modelBuilder.Entity<CartItem>()
            .HasOne<Cart>()
            .WithMany(parent => parent.Items)
            .HasForeignKey("ItemsId");

        // Cart has one or more AppliedPromotions of type Promotion
        modelBuilder.Entity<Promotion>()
            .HasOne<Cart>()
            .WithMany(parent => parent.AppliedPromotions)
            .HasForeignKey("AppliedPromotionsId");

        // CartItem has one Cart of type Cart
        modelBuilder.Entity<CartItem>()
            .HasOne(x => x.Cart)
            .WithMany()
            .HasForeignKey("CartId");

        // CartItem has one Variant of type ProductVariant
        modelBuilder.Entity<CartItem>()
            .HasOne(x => x.Variant)
            .WithMany()
            .HasForeignKey("VariantId");


        // CartItem has one or more AppliedPromotions of type Promotion
        modelBuilder.Entity<Promotion>()
            .HasOne<CartItem>()
            .WithMany(parent => parent.AppliedPromotions)
            .HasForeignKey("AppliedPromotionsId");

        // Order has one Customer of type Customer
        modelBuilder.Entity<Order>()
            .HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey("CustomerId");

        // Order has one Channel of type Channel
        modelBuilder.Entity<Order>()
            .HasOne(x => x.Channel)
            .WithMany()
            .HasForeignKey("ChannelId");

        // Order has one Seller of type Seller
        modelBuilder.Entity<Order>()
            .HasOne(x => x.Seller)
            .WithMany()
            .HasForeignKey("SellerId");

        // Order has one Invoice of type Invoice
        modelBuilder.Entity<Order>()
            .HasOne(x => x.Invoice)
            .WithMany()
            .HasForeignKey("InvoiceId");


        // Order has one or more OrderLines of type OrderLine
        modelBuilder.Entity<OrderLine>()
            .HasOne<Order>()
            .WithMany(parent => parent.OrderLines)
            .HasForeignKey("OrderLinesId");

        // Order has one or more Payments of type Payment
        modelBuilder.Entity<Payment>()
            .HasOne<Order>()
            .WithMany(parent => parent.Payments)
            .HasForeignKey("PaymentsId");

        // Order has one or more Shipments of type Shipment
        modelBuilder.Entity<Shipment>()
            .HasOne<Order>()
            .WithMany(parent => parent.Shipments)
            .HasForeignKey("ShipmentsId");

        // Order has one or more Refunds of type Refund
        modelBuilder.Entity<Refund>()
            .HasOne<Order>()
            .WithMany(parent => parent.Refunds)
            .HasForeignKey("RefundsId");

        // Order has one or more AppliedPromotions of type Promotion
        modelBuilder.Entity<Promotion>()
            .HasOne<Order>()
            .WithMany(parent => parent.AppliedPromotions)
            .HasForeignKey("AppliedPromotionsId");

        // Order has one or more GiftCardRedemptions of type GiftCardRedemption
        modelBuilder.Entity<GiftCardRedemption>()
            .HasOne<Order>()
            .WithMany(parent => parent.GiftCardRedemptions)
            .HasForeignKey("GiftCardRedemptionsId");

        // Order has one or more CouponRedemptions of type CouponRedemption
        modelBuilder.Entity<CouponRedemption>()
            .HasOne<Order>()
            .WithMany(parent => parent.CouponRedemptions)
            .HasForeignKey("CouponRedemptionsId");

        // Order has one or more ReturnRequests of type ReturnRequest
        modelBuilder.Entity<ReturnRequest>()
            .HasOne<Order>()
            .WithMany(parent => parent.ReturnRequests)
            .HasForeignKey("ReturnRequestsId");

        // OrderLine has one Order of type Order
        modelBuilder.Entity<OrderLine>()
            .HasOne(x => x.Order)
            .WithMany()
            .HasForeignKey("OrderId");

        // OrderLine has one Variant of type ProductVariant
        modelBuilder.Entity<OrderLine>()
            .HasOne(x => x.Variant)
            .WithMany()
            .HasForeignKey("VariantId");


        // OrderLine has one or more AppliedPromotions of type Promotion
        modelBuilder.Entity<Promotion>()
            .HasOne<OrderLine>()
            .WithMany(parent => parent.AppliedPromotions)
            .HasForeignKey("AppliedPromotionsId");

        // Payment has one Order of type Order
        modelBuilder.Entity<Payment>()
            .HasOne(x => x.Order)
            .WithMany()
            .HasForeignKey("OrderId");

        // Payment has one Customer of type Customer
        modelBuilder.Entity<Payment>()
            .HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey("CustomerId");

        // Payment has one PaymentProvider of type PaymentProvider
        modelBuilder.Entity<Payment>()
            .HasOne(x => x.PaymentProvider)
            .WithMany()
            .HasForeignKey("PaymentProviderId");


        // Payment has one or more Refunds of type Refund
        modelBuilder.Entity<Refund>()
            .HasOne<Payment>()
            .WithMany(parent => parent.Refunds)
            .HasForeignKey("RefundsId");

        // Refund has one Payment of type Payment
        modelBuilder.Entity<Refund>()
            .HasOne(x => x.Payment)
            .WithMany()
            .HasForeignKey("PaymentId");

        // Refund has one Order of type Order
        modelBuilder.Entity<Refund>()
            .HasOne(x => x.Order)
            .WithMany()
            .HasForeignKey("OrderId");


        // Shipment has one Order of type Order
        modelBuilder.Entity<Shipment>()
            .HasOne(x => x.Order)
            .WithMany()
            .HasForeignKey("OrderId");

        // Shipment has one FulfillmentCenter of type FulfillmentCenter
        modelBuilder.Entity<Shipment>()
            .HasOne(x => x.FulfillmentCenter)
            .WithMany()
            .HasForeignKey("FulfillmentCenterId");


        // Shipment has one or more ShipmentItems of type ShipmentItem
        modelBuilder.Entity<ShipmentItem>()
            .HasOne<Shipment>()
            .WithMany(parent => parent.ShipmentItems)
            .HasForeignKey("ShipmentItemsId");

        // ShipmentItem has one Shipment of type Shipment
        modelBuilder.Entity<ShipmentItem>()
            .HasOne(x => x.Shipment)
            .WithMany()
            .HasForeignKey("ShipmentId");

        // ShipmentItem has one OrderLine of type OrderLine
        modelBuilder.Entity<ShipmentItem>()
            .HasOne(x => x.OrderLine)
            .WithMany()
            .HasForeignKey("OrderLineId");


        // ReturnRequest has one Order of type Order
        modelBuilder.Entity<ReturnRequest>()
            .HasOne(x => x.Order)
            .WithMany()
            .HasForeignKey("OrderId");

        // ReturnRequest has one Refund of type Refund
        modelBuilder.Entity<ReturnRequest>()
            .HasOne(x => x.Refund)
            .WithMany()
            .HasForeignKey("RefundId");

        // ReturnRequest has one Shipment of type Shipment
        modelBuilder.Entity<ReturnRequest>()
            .HasOne(x => x.Shipment)
            .WithMany()
            .HasForeignKey("ShipmentId");


        // ReturnRequest has one or more Items of type ReturnItem
        modelBuilder.Entity<ReturnItem>()
            .HasOne<ReturnRequest>()
            .WithMany(parent => parent.Items)
            .HasForeignKey("ItemsId");

        // ReturnItem has one ReturnRequest of type ReturnRequest
        modelBuilder.Entity<ReturnItem>()
            .HasOne(x => x.ReturnRequest)
            .WithMany()
            .HasForeignKey("ReturnRequestId");

        // ReturnItem has one OrderLine of type OrderLine
        modelBuilder.Entity<ReturnItem>()
            .HasOne(x => x.OrderLine)
            .WithMany()
            .HasForeignKey("OrderLineId");


        // Promotion has one Merchant of type Merchant
        modelBuilder.Entity<Promotion>()
            .HasOne(x => x.Merchant)
            .WithMany()
            .HasForeignKey("MerchantId");


        // Promotion has one or more Channels of type Channel
        modelBuilder.Entity<Channel>()
            .HasOne<Promotion>()
            .WithMany(parent => parent.Channels)
            .HasForeignKey("ChannelsId");

        // Promotion has one or more ApplicableProducts of type Product
        modelBuilder.Entity<Product>()
            .HasOne<Promotion>()
            .WithMany(parent => parent.ApplicableProducts)
            .HasForeignKey("ApplicableProductsId");

        // Promotion has one or more ApplicableCategories of type Category
        modelBuilder.Entity<Category>()
            .HasOne<Promotion>()
            .WithMany(parent => parent.ApplicableCategories)
            .HasForeignKey("ApplicableCategoriesId");

        // Promotion has one or more Coupons of type Coupon
        modelBuilder.Entity<Coupon>()
            .HasOne<Promotion>()
            .WithMany(parent => parent.Coupons)
            .HasForeignKey("CouponsId");

        // Coupon has one Promotion of type Promotion
        modelBuilder.Entity<Coupon>()
            .HasOne(x => x.Promotion)
            .WithMany()
            .HasForeignKey("PromotionId");


        // Coupon has one or more Redemptions of type CouponRedemption
        modelBuilder.Entity<CouponRedemption>()
            .HasOne<Coupon>()
            .WithMany(parent => parent.Redemptions)
            .HasForeignKey("RedemptionsId");

        // CouponRedemption has one Coupon of type Coupon
        modelBuilder.Entity<CouponRedemption>()
            .HasOne(x => x.Coupon)
            .WithMany()
            .HasForeignKey("CouponId");

        // CouponRedemption has one Order of type Order
        modelBuilder.Entity<CouponRedemption>()
            .HasOne(x => x.Order)
            .WithMany()
            .HasForeignKey("OrderId");

        // CouponRedemption has one Customer of type Customer
        modelBuilder.Entity<CouponRedemption>()
            .HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey("CustomerId");


        // TaxRule has one Merchant of type Merchant
        modelBuilder.Entity<TaxRule>()
            .HasOne(x => x.Merchant)
            .WithMany()
            .HasForeignKey("MerchantId");


        // TaxRule has one or more Channels of type Channel
        modelBuilder.Entity<Channel>()
            .HasOne<TaxRule>()
            .WithMany(parent => parent.Channels)
            .HasForeignKey("ChannelsId");

        // ShippingMethod has one CarrierService of type CarrierService
        modelBuilder.Entity<ShippingMethod>()
            .HasOne(x => x.CarrierService)
            .WithMany()
            .HasForeignKey("CarrierServiceId");


        // ShippingMethod has one or more Channels of type Channel
        modelBuilder.Entity<Channel>()
            .HasOne<ShippingMethod>()
            .WithMany(parent => parent.Channels)
            .HasForeignKey("ChannelsId");


        // CarrierService has one or more ShippingMethods of type ShippingMethod
        modelBuilder.Entity<ShippingMethod>()
            .HasOne<CarrierService>()
            .WithMany(parent => parent.ShippingMethods)
            .HasForeignKey("ShippingMethodsId");

        // Review has one Product of type Product
        modelBuilder.Entity<Review>()
            .HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey("ProductId");

        // Review has one Customer of type Customer
        modelBuilder.Entity<Review>()
            .HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey("CustomerId");

        // Review has one Order of type Order
        modelBuilder.Entity<Review>()
            .HasOne(x => x.Order)
            .WithMany()
            .HasForeignKey("OrderId");


        // Subscription has one Customer of type Customer
        modelBuilder.Entity<Subscription>()
            .HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey("CustomerId");

        // Subscription has one Variant of type ProductVariant
        modelBuilder.Entity<Subscription>()
            .HasOne(x => x.Variant)
            .WithMany()
            .HasForeignKey("VariantId");

        // Subscription has one PaymentProvider of type PaymentProvider
        modelBuilder.Entity<Subscription>()
            .HasOne(x => x.PaymentProvider)
            .WithMany()
            .HasForeignKey("PaymentProviderId");

        // Subscription has one Channel of type Channel
        modelBuilder.Entity<Subscription>()
            .HasOne(x => x.Channel)
            .WithMany()
            .HasForeignKey("ChannelId");


        // PaymentProvider has one Merchant of type Merchant
        modelBuilder.Entity<PaymentProvider>()
            .HasOne(x => x.Merchant)
            .WithMany()
            .HasForeignKey("MerchantId");


        // PaymentProvider has one or more Channels of type Channel
        modelBuilder.Entity<Channel>()
            .HasOne<PaymentProvider>()
            .WithMany(parent => parent.Channels)
            .HasForeignKey("ChannelsId");

        // PaymentProvider has one or more Payments of type Payment
        modelBuilder.Entity<Payment>()
            .HasOne<PaymentProvider>()
            .WithMany(parent => parent.Payments)
            .HasForeignKey("PaymentsId");

        // PaymentProvider has one or more Subscriptions of type Subscription
        modelBuilder.Entity<Subscription>()
            .HasOne<PaymentProvider>()
            .WithMany(parent => parent.Subscriptions)
            .HasForeignKey("SubscriptionsId");

        // Invoice has one Order of type Order
        modelBuilder.Entity<Invoice>()
            .HasOne(x => x.Order)
            .WithMany()
            .HasForeignKey("OrderId");


        // GiftCard has one Customer of type Customer
        modelBuilder.Entity<GiftCard>()
            .HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey("CustomerId");

        // GiftCard has one IssuedOrder of type Order
        modelBuilder.Entity<GiftCard>()
            .HasOne(x => x.IssuedOrder)
            .WithMany()
            .HasForeignKey("IssuedOrderId");


        // GiftCard has one or more Redemptions of type GiftCardRedemption
        modelBuilder.Entity<GiftCardRedemption>()
            .HasOne<GiftCard>()
            .WithMany(parent => parent.Redemptions)
            .HasForeignKey("RedemptionsId");

        // GiftCardRedemption has one GiftCard of type GiftCard
        modelBuilder.Entity<GiftCardRedemption>()
            .HasOne(x => x.GiftCard)
            .WithMany()
            .HasForeignKey("GiftCardId");

        // GiftCardRedemption has one Order of type Order
        modelBuilder.Entity<GiftCardRedemption>()
            .HasOne(x => x.Order)
            .WithMany()
            .HasForeignKey("OrderId");


        // Payout has one Seller of type Seller
        modelBuilder.Entity<Payout>()
            .HasOne(x => x.Seller)
            .WithMany()
            .HasForeignKey("SellerId");


        // Payout has one or more Orders of type Order
        modelBuilder.Entity<Order>()
            .HasOne<Payout>()
            .WithMany(parent => parent.Orders)
            .HasForeignKey("OrdersId");

    }
}
