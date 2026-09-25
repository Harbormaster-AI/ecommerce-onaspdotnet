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
            .HasForeignKey("Merchant_Id");

        // Merchant has one or more Brands of type Brand
        modelBuilder.Entity<Brand>()
            .HasOne<Merchant>()
            .WithMany(parent => parent.Brands)
            .HasForeignKey("Merchant_Id");

        // Merchant has one or more FulfillmentCenters of type FulfillmentCenter
        modelBuilder.Entity<FulfillmentCenter>()
            .HasOne<Merchant>()
            .WithMany(parent => parent.FulfillmentCenters)
            .HasForeignKey("Merchant_Id");

        // Merchant has one or more TaxRules of type TaxRule
        modelBuilder.Entity<TaxRule>()
            .HasOne<Merchant>()
            .WithMany(parent => parent.TaxRules)
            .HasForeignKey("Merchant_Id");

        // Merchant has one or more PaymentProviders of type PaymentProvider
        modelBuilder.Entity<PaymentProvider>()
            .HasOne<Merchant>()
            .WithMany(parent => parent.PaymentProviders)
            .HasForeignKey("Merchant_Id");

        // Merchant has one or more Sellers of type Seller
        modelBuilder.Entity<Seller>()
            .HasOne<Merchant>()
            .WithMany(parent => parent.Sellers)
            .HasForeignKey("Merchant_Id");

        // Merchant has one or more Promotions of type Promotion
        modelBuilder.Entity<Promotion>()
            .HasOne<Merchant>()
            .WithMany(parent => parent.Promotions)
            .HasForeignKey("Merchant_Id");

        // Channel has one Merchant of type Merchant
        modelBuilder.Entity<Channel>()
            .HasOne(x => x.Merchant)
            .WithMany()
            .HasForeignKey("Merchant_Id");


        // Channel has one or more Catalogs of type Catalog
        modelBuilder.Entity<Catalog>()
            .HasOne<Channel>()
            .WithMany(parent => parent.Catalogs)
            .HasForeignKey("Channel_Id");

        // Channel has one or more Promotions of type Promotion
        modelBuilder.Entity<Promotion>()
            .HasOne<Channel>()
            .WithMany(parent => parent.Promotions)
            .HasForeignKey("Channel_Id");

        // Channel has one or more ShippingMethods of type ShippingMethod
        modelBuilder.Entity<ShippingMethod>()
            .HasOne<Channel>()
            .WithMany(parent => parent.ShippingMethods)
            .HasForeignKey("Channel_Id");

        // Channel has one or more PaymentProviders of type PaymentProvider
        modelBuilder.Entity<PaymentProvider>()
            .HasOne<Channel>()
            .WithMany(parent => parent.PaymentProviders)
            .HasForeignKey("Channel_Id");

        // Brand has one Merchant of type Merchant
        modelBuilder.Entity<Brand>()
            .HasOne(x => x.Merchant)
            .WithMany()
            .HasForeignKey("Merchant_Id");


        // Brand has one or more Products of type Product
        modelBuilder.Entity<Product>()
            .HasOne<Brand>()
            .WithMany(parent => parent.Products)
            .HasForeignKey("Brand_Id");

        // Catalog has one Channel of type Channel
        modelBuilder.Entity<Catalog>()
            .HasOne(x => x.Channel)
            .WithMany()
            .HasForeignKey("Channel_Id");


        // Catalog has one or more Categories of type Category
        modelBuilder.Entity<Category>()
            .HasOne<Catalog>()
            .WithMany(parent => parent.Categories)
            .HasForeignKey("Catalog_Id");

        // Category has one Catalog of type Catalog
        modelBuilder.Entity<Category>()
            .HasOne(x => x.Catalog)
            .WithMany()
            .HasForeignKey("Catalog_Id");

        // Category has one ParentCategory of type Category
        modelBuilder.Entity<Category>()
            .HasOne(x => x.ParentCategory)
            .WithMany()
            .HasForeignKey("ParentCategory_Id");


        // Category has one or more Subcategories of type Category
        modelBuilder.Entity<Category>()
            .HasOne<Category>()
            .WithMany(parent => parent.Subcategories)
            .HasForeignKey("Category_Id");

        // Category has one or more Products of type Product
        modelBuilder.Entity<Product>()
            .HasOne<Category>()
            .WithMany(parent => parent.Products)
            .HasForeignKey("Category_Id");

        // Product has one Brand of type Brand
        modelBuilder.Entity<Product>()
            .HasOne(x => x.Brand)
            .WithMany()
            .HasForeignKey("Brand_Id");

        // Product has one Seller of type Seller
        modelBuilder.Entity<Product>()
            .HasOne(x => x.Seller)
            .WithMany()
            .HasForeignKey("Seller_Id");


        // Product has one or more Categories of type Category
        modelBuilder.Entity<Category>()
            .HasOne<Product>()
            .WithMany(parent => parent.Categories)
            .HasForeignKey("Product_Id");

        // Product has one or more Variants of type ProductVariant
        modelBuilder.Entity<ProductVariant>()
            .HasOne<Product>()
            .WithMany(parent => parent.Variants)
            .HasForeignKey("Product_Id");

        // Product has one or more MediaAssets of type MediaAsset
        modelBuilder.Entity<MediaAsset>()
            .HasOne<Product>()
            .WithMany(parent => parent.MediaAssets)
            .HasForeignKey("Product_Id");

        // Product has one or more Reviews of type Review
        modelBuilder.Entity<Review>()
            .HasOne<Product>()
            .WithMany(parent => parent.Reviews)
            .HasForeignKey("Product_Id");

        // ProductVariant has one Product of type Product
        modelBuilder.Entity<ProductVariant>()
            .HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey("Product_Id");


        // ProductVariant has one or more Pricing of type ProductPricing
        modelBuilder.Entity<ProductPricing>()
            .HasOne<ProductVariant>()
            .WithMany(parent => parent.Pricing)
            .HasForeignKey("ProductVariant_Id");

        // ProductVariant has one or more InventoryItems of type InventoryItem
        modelBuilder.Entity<InventoryItem>()
            .HasOne<ProductVariant>()
            .WithMany(parent => parent.InventoryItems)
            .HasForeignKey("ProductVariant_Id");

        // ProductVariant has one or more MediaAssets of type MediaAsset
        modelBuilder.Entity<MediaAsset>()
            .HasOne<ProductVariant>()
            .WithMany(parent => parent.MediaAssets)
            .HasForeignKey("ProductVariant_Id");

        // ProductVariant has one or more Subscriptions of type Subscription
        modelBuilder.Entity<Subscription>()
            .HasOne<ProductVariant>()
            .WithMany(parent => parent.Subscriptions)
            .HasForeignKey("ProductVariant_Id");

        // ProductVariant has one or more CartItems of type CartItem
        modelBuilder.Entity<CartItem>()
            .HasOne<ProductVariant>()
            .WithMany(parent => parent.CartItems)
            .HasForeignKey("ProductVariant_Id");

        // ProductVariant has one or more OrderLines of type OrderLine
        modelBuilder.Entity<OrderLine>()
            .HasOne<ProductVariant>()
            .WithMany(parent => parent.OrderLines)
            .HasForeignKey("ProductVariant_Id");

        // ProductVariant has one or more WishlistItems of type WishlistItem
        modelBuilder.Entity<WishlistItem>()
            .HasOne<ProductVariant>()
            .WithMany(parent => parent.WishlistItems)
            .HasForeignKey("ProductVariant_Id");

        // ProductPricing has one Variant of type ProductVariant
        modelBuilder.Entity<ProductPricing>()
            .HasOne(x => x.Variant)
            .WithMany()
            .HasForeignKey("Variant_Id");

        // ProductPricing has one Channel of type Channel
        modelBuilder.Entity<ProductPricing>()
            .HasOne(x => x.Channel)
            .WithMany()
            .HasForeignKey("Channel_Id");


        // MediaAsset has one Product of type Product
        modelBuilder.Entity<MediaAsset>()
            .HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey("Product_Id");

        // MediaAsset has one Variant of type ProductVariant
        modelBuilder.Entity<MediaAsset>()
            .HasOne(x => x.Variant)
            .WithMany()
            .HasForeignKey("Variant_Id");


        // FulfillmentCenter has one Merchant of type Merchant
        modelBuilder.Entity<FulfillmentCenter>()
            .HasOne(x => x.Merchant)
            .WithMany()
            .HasForeignKey("Merchant_Id");


        // FulfillmentCenter has one or more InventoryItems of type InventoryItem
        modelBuilder.Entity<InventoryItem>()
            .HasOne<FulfillmentCenter>()
            .WithMany(parent => parent.InventoryItems)
            .HasForeignKey("FulfillmentCenter_Id");

        // FulfillmentCenter has one or more Shipments of type Shipment
        modelBuilder.Entity<Shipment>()
            .HasOne<FulfillmentCenter>()
            .WithMany(parent => parent.Shipments)
            .HasForeignKey("FulfillmentCenter_Id");

        // InventoryItem has one Variant of type ProductVariant
        modelBuilder.Entity<InventoryItem>()
            .HasOne(x => x.Variant)
            .WithMany()
            .HasForeignKey("Variant_Id");

        // InventoryItem has one FulfillmentCenter of type FulfillmentCenter
        modelBuilder.Entity<InventoryItem>()
            .HasOne(x => x.FulfillmentCenter)
            .WithMany()
            .HasForeignKey("FulfillmentCenter_Id");


        // Supplier has one Merchant of type Merchant
        modelBuilder.Entity<Supplier>()
            .HasOne(x => x.Merchant)
            .WithMany()
            .HasForeignKey("Merchant_Id");


        // Supplier has one or more Products of type Product
        modelBuilder.Entity<Product>()
            .HasOne<Supplier>()
            .WithMany(parent => parent.Products)
            .HasForeignKey("Supplier_Id");

        // Supplier has one or more FulfillmentCenters of type FulfillmentCenter
        modelBuilder.Entity<FulfillmentCenter>()
            .HasOne<Supplier>()
            .WithMany(parent => parent.FulfillmentCenters)
            .HasForeignKey("Supplier_Id");

        // Seller has one Merchant of type Merchant
        modelBuilder.Entity<Seller>()
            .HasOne(x => x.Merchant)
            .WithMany()
            .HasForeignKey("Merchant_Id");


        // Seller has one or more Products of type Product
        modelBuilder.Entity<Product>()
            .HasOne<Seller>()
            .WithMany(parent => parent.Products)
            .HasForeignKey("Seller_Id");

        // Seller has one or more Payouts of type Payout
        modelBuilder.Entity<Payout>()
            .HasOne<Seller>()
            .WithMany(parent => parent.Payouts)
            .HasForeignKey("Seller_Id");

        // Seller has one or more Orders of type Order
        modelBuilder.Entity<Order>()
            .HasOne<Seller>()
            .WithMany(parent => parent.Orders)
            .HasForeignKey("Seller_Id");


        // Customer has one or more Addresses of type CustomerAddress
        modelBuilder.Entity<CustomerAddress>()
            .HasOne<Customer>()
            .WithMany(parent => parent.Addresses)
            .HasForeignKey("Customer_Id");

        // Customer has one or more Carts of type Cart
        modelBuilder.Entity<Cart>()
            .HasOne<Customer>()
            .WithMany(parent => parent.Carts)
            .HasForeignKey("Customer_Id");

        // Customer has one or more Orders of type Order
        modelBuilder.Entity<Order>()
            .HasOne<Customer>()
            .WithMany(parent => parent.Orders)
            .HasForeignKey("Customer_Id");

        // Customer has one or more Payments of type Payment
        modelBuilder.Entity<Payment>()
            .HasOne<Customer>()
            .WithMany(parent => parent.Payments)
            .HasForeignKey("Customer_Id");

        // Customer has one or more Reviews of type Review
        modelBuilder.Entity<Review>()
            .HasOne<Customer>()
            .WithMany(parent => parent.Reviews)
            .HasForeignKey("Customer_Id");

        // Customer has one or more Wishlists of type Wishlist
        modelBuilder.Entity<Wishlist>()
            .HasOne<Customer>()
            .WithMany(parent => parent.Wishlists)
            .HasForeignKey("Customer_Id");

        // Customer has one or more Subscriptions of type Subscription
        modelBuilder.Entity<Subscription>()
            .HasOne<Customer>()
            .WithMany(parent => parent.Subscriptions)
            .HasForeignKey("Customer_Id");

        // Customer has one or more CouponRedemptions of type CouponRedemption
        modelBuilder.Entity<CouponRedemption>()
            .HasOne<Customer>()
            .WithMany(parent => parent.CouponRedemptions)
            .HasForeignKey("Customer_Id");

        // Customer has one or more GiftCards of type GiftCard
        modelBuilder.Entity<GiftCard>()
            .HasOne<Customer>()
            .WithMany(parent => parent.GiftCards)
            .HasForeignKey("Customer_Id");

        // CustomerAddress has one Customer of type Customer
        modelBuilder.Entity<CustomerAddress>()
            .HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey("Customer_Id");


        // Wishlist has one Customer of type Customer
        modelBuilder.Entity<Wishlist>()
            .HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey("Customer_Id");


        // Wishlist has one or more Items of type WishlistItem
        modelBuilder.Entity<WishlistItem>()
            .HasOne<Wishlist>()
            .WithMany(parent => parent.Items)
            .HasForeignKey("Wishlist_Id");

        // WishlistItem has one Wishlist of type Wishlist
        modelBuilder.Entity<WishlistItem>()
            .HasOne(x => x.Wishlist)
            .WithMany()
            .HasForeignKey("Wishlist_Id");

        // WishlistItem has one Variant of type ProductVariant
        modelBuilder.Entity<WishlistItem>()
            .HasOne(x => x.Variant)
            .WithMany()
            .HasForeignKey("Variant_Id");


        // Cart has one Customer of type Customer
        modelBuilder.Entity<Cart>()
            .HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey("Customer_Id");

        // Cart has one Channel of type Channel
        modelBuilder.Entity<Cart>()
            .HasOne(x => x.Channel)
            .WithMany()
            .HasForeignKey("Channel_Id");


        // Cart has one or more Items of type CartItem
        modelBuilder.Entity<CartItem>()
            .HasOne<Cart>()
            .WithMany(parent => parent.Items)
            .HasForeignKey("Cart_Id");

        // Cart has one or more AppliedPromotions of type Promotion
        modelBuilder.Entity<Promotion>()
            .HasOne<Cart>()
            .WithMany(parent => parent.AppliedPromotions)
            .HasForeignKey("Cart_Id");

        // CartItem has one Cart of type Cart
        modelBuilder.Entity<CartItem>()
            .HasOne(x => x.Cart)
            .WithMany()
            .HasForeignKey("Cart_Id");

        // CartItem has one Variant of type ProductVariant
        modelBuilder.Entity<CartItem>()
            .HasOne(x => x.Variant)
            .WithMany()
            .HasForeignKey("Variant_Id");


        // CartItem has one or more AppliedPromotions of type Promotion
        modelBuilder.Entity<Promotion>()
            .HasOne<CartItem>()
            .WithMany(parent => parent.AppliedPromotions)
            .HasForeignKey("CartItem_Id");

        // Order has one Customer of type Customer
        modelBuilder.Entity<Order>()
            .HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey("Customer_Id");

        // Order has one Channel of type Channel
        modelBuilder.Entity<Order>()
            .HasOne(x => x.Channel)
            .WithMany()
            .HasForeignKey("Channel_Id");

        // Order has one Seller of type Seller
        modelBuilder.Entity<Order>()
            .HasOne(x => x.Seller)
            .WithMany()
            .HasForeignKey("Seller_Id");

        // Order has one Invoice of type Invoice
        modelBuilder.Entity<Order>()
            .HasOne(x => x.Invoice)
            .WithMany()
            .HasForeignKey("Invoice_Id");


        // Order has one or more OrderLines of type OrderLine
        modelBuilder.Entity<OrderLine>()
            .HasOne<Order>()
            .WithMany(parent => parent.OrderLines)
            .HasForeignKey("Order_Id");

        // Order has one or more Payments of type Payment
        modelBuilder.Entity<Payment>()
            .HasOne<Order>()
            .WithMany(parent => parent.Payments)
            .HasForeignKey("Order_Id");

        // Order has one or more Shipments of type Shipment
        modelBuilder.Entity<Shipment>()
            .HasOne<Order>()
            .WithMany(parent => parent.Shipments)
            .HasForeignKey("Order_Id");

        // Order has one or more Refunds of type Refund
        modelBuilder.Entity<Refund>()
            .HasOne<Order>()
            .WithMany(parent => parent.Refunds)
            .HasForeignKey("Order_Id");

        // Order has one or more AppliedPromotions of type Promotion
        modelBuilder.Entity<Promotion>()
            .HasOne<Order>()
            .WithMany(parent => parent.AppliedPromotions)
            .HasForeignKey("Order_Id");

        // Order has one or more GiftCardRedemptions of type GiftCardRedemption
        modelBuilder.Entity<GiftCardRedemption>()
            .HasOne<Order>()
            .WithMany(parent => parent.GiftCardRedemptions)
            .HasForeignKey("Order_Id");

        // Order has one or more CouponRedemptions of type CouponRedemption
        modelBuilder.Entity<CouponRedemption>()
            .HasOne<Order>()
            .WithMany(parent => parent.CouponRedemptions)
            .HasForeignKey("Order_Id");

        // Order has one or more ReturnRequests of type ReturnRequest
        modelBuilder.Entity<ReturnRequest>()
            .HasOne<Order>()
            .WithMany(parent => parent.ReturnRequests)
            .HasForeignKey("Order_Id");

        // OrderLine has one Order of type Order
        modelBuilder.Entity<OrderLine>()
            .HasOne(x => x.Order)
            .WithMany()
            .HasForeignKey("Order_Id");

        // OrderLine has one Variant of type ProductVariant
        modelBuilder.Entity<OrderLine>()
            .HasOne(x => x.Variant)
            .WithMany()
            .HasForeignKey("Variant_Id");


        // OrderLine has one or more AppliedPromotions of type Promotion
        modelBuilder.Entity<Promotion>()
            .HasOne<OrderLine>()
            .WithMany(parent => parent.AppliedPromotions)
            .HasForeignKey("OrderLine_Id");

        // Payment has one Order of type Order
        modelBuilder.Entity<Payment>()
            .HasOne(x => x.Order)
            .WithMany()
            .HasForeignKey("Order_Id");

        // Payment has one Customer of type Customer
        modelBuilder.Entity<Payment>()
            .HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey("Customer_Id");

        // Payment has one PaymentProvider of type PaymentProvider
        modelBuilder.Entity<Payment>()
            .HasOne(x => x.PaymentProvider)
            .WithMany()
            .HasForeignKey("PaymentProvider_Id");


        // Payment has one or more Refunds of type Refund
        modelBuilder.Entity<Refund>()
            .HasOne<Payment>()
            .WithMany(parent => parent.Refunds)
            .HasForeignKey("Payment_Id");

        // Refund has one Payment of type Payment
        modelBuilder.Entity<Refund>()
            .HasOne(x => x.Payment)
            .WithMany()
            .HasForeignKey("Payment_Id");

        // Refund has one Order of type Order
        modelBuilder.Entity<Refund>()
            .HasOne(x => x.Order)
            .WithMany()
            .HasForeignKey("Order_Id");


        // Shipment has one Order of type Order
        modelBuilder.Entity<Shipment>()
            .HasOne(x => x.Order)
            .WithMany()
            .HasForeignKey("Order_Id");

        // Shipment has one FulfillmentCenter of type FulfillmentCenter
        modelBuilder.Entity<Shipment>()
            .HasOne(x => x.FulfillmentCenter)
            .WithMany()
            .HasForeignKey("FulfillmentCenter_Id");


        // Shipment has one or more ShipmentItems of type ShipmentItem
        modelBuilder.Entity<ShipmentItem>()
            .HasOne<Shipment>()
            .WithMany(parent => parent.ShipmentItems)
            .HasForeignKey("Shipment_Id");

        // ShipmentItem has one Shipment of type Shipment
        modelBuilder.Entity<ShipmentItem>()
            .HasOne(x => x.Shipment)
            .WithMany()
            .HasForeignKey("Shipment_Id");

        // ShipmentItem has one OrderLine of type OrderLine
        modelBuilder.Entity<ShipmentItem>()
            .HasOne(x => x.OrderLine)
            .WithMany()
            .HasForeignKey("OrderLine_Id");


        // ReturnRequest has one Order of type Order
        modelBuilder.Entity<ReturnRequest>()
            .HasOne(x => x.Order)
            .WithMany()
            .HasForeignKey("Order_Id");

        // ReturnRequest has one Refund of type Refund
        modelBuilder.Entity<ReturnRequest>()
            .HasOne(x => x.Refund)
            .WithMany()
            .HasForeignKey("Refund_Id");

        // ReturnRequest has one Shipment of type Shipment
        modelBuilder.Entity<ReturnRequest>()
            .HasOne(x => x.Shipment)
            .WithMany()
            .HasForeignKey("Shipment_Id");


        // ReturnRequest has one or more Items of type ReturnItem
        modelBuilder.Entity<ReturnItem>()
            .HasOne<ReturnRequest>()
            .WithMany(parent => parent.Items)
            .HasForeignKey("ReturnRequest_Id");

        // ReturnItem has one ReturnRequest of type ReturnRequest
        modelBuilder.Entity<ReturnItem>()
            .HasOne(x => x.ReturnRequest)
            .WithMany()
            .HasForeignKey("ReturnRequest_Id");

        // ReturnItem has one OrderLine of type OrderLine
        modelBuilder.Entity<ReturnItem>()
            .HasOne(x => x.OrderLine)
            .WithMany()
            .HasForeignKey("OrderLine_Id");


        // Promotion has one Merchant of type Merchant
        modelBuilder.Entity<Promotion>()
            .HasOne(x => x.Merchant)
            .WithMany()
            .HasForeignKey("Merchant_Id");


        // Promotion has one or more Channels of type Channel
        modelBuilder.Entity<Channel>()
            .HasOne<Promotion>()
            .WithMany(parent => parent.Channels)
            .HasForeignKey("Promotion_Id");

        // Promotion has one or more ApplicableProducts of type Product
        modelBuilder.Entity<Product>()
            .HasOne<Promotion>()
            .WithMany(parent => parent.ApplicableProducts)
            .HasForeignKey("Promotion_Id");

        // Promotion has one or more ApplicableCategories of type Category
        modelBuilder.Entity<Category>()
            .HasOne<Promotion>()
            .WithMany(parent => parent.ApplicableCategories)
            .HasForeignKey("Promotion_Id");

        // Promotion has one or more Coupons of type Coupon
        modelBuilder.Entity<Coupon>()
            .HasOne<Promotion>()
            .WithMany(parent => parent.Coupons)
            .HasForeignKey("Promotion_Id");

        // Coupon has one Promotion of type Promotion
        modelBuilder.Entity<Coupon>()
            .HasOne(x => x.Promotion)
            .WithMany()
            .HasForeignKey("Promotion_Id");


        // Coupon has one or more Redemptions of type CouponRedemption
        modelBuilder.Entity<CouponRedemption>()
            .HasOne<Coupon>()
            .WithMany(parent => parent.Redemptions)
            .HasForeignKey("Coupon_Id");

        // CouponRedemption has one Coupon of type Coupon
        modelBuilder.Entity<CouponRedemption>()
            .HasOne(x => x.Coupon)
            .WithMany()
            .HasForeignKey("Coupon_Id");

        // CouponRedemption has one Order of type Order
        modelBuilder.Entity<CouponRedemption>()
            .HasOne(x => x.Order)
            .WithMany()
            .HasForeignKey("Order_Id");

        // CouponRedemption has one Customer of type Customer
        modelBuilder.Entity<CouponRedemption>()
            .HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey("Customer_Id");


        // TaxRule has one Merchant of type Merchant
        modelBuilder.Entity<TaxRule>()
            .HasOne(x => x.Merchant)
            .WithMany()
            .HasForeignKey("Merchant_Id");


        // TaxRule has one or more Channels of type Channel
        modelBuilder.Entity<Channel>()
            .HasOne<TaxRule>()
            .WithMany(parent => parent.Channels)
            .HasForeignKey("TaxRule_Id");

        // ShippingMethod has one CarrierService of type CarrierService
        modelBuilder.Entity<ShippingMethod>()
            .HasOne(x => x.CarrierService)
            .WithMany()
            .HasForeignKey("CarrierService_Id");


        // ShippingMethod has one or more Channels of type Channel
        modelBuilder.Entity<Channel>()
            .HasOne<ShippingMethod>()
            .WithMany(parent => parent.Channels)
            .HasForeignKey("ShippingMethod_Id");


        // CarrierService has one or more ShippingMethods of type ShippingMethod
        modelBuilder.Entity<ShippingMethod>()
            .HasOne<CarrierService>()
            .WithMany(parent => parent.ShippingMethods)
            .HasForeignKey("CarrierService_Id");

        // Review has one Product of type Product
        modelBuilder.Entity<Review>()
            .HasOne(x => x.Product)
            .WithMany()
            .HasForeignKey("Product_Id");

        // Review has one Customer of type Customer
        modelBuilder.Entity<Review>()
            .HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey("Customer_Id");

        // Review has one Order of type Order
        modelBuilder.Entity<Review>()
            .HasOne(x => x.Order)
            .WithMany()
            .HasForeignKey("Order_Id");


        // Subscription has one Customer of type Customer
        modelBuilder.Entity<Subscription>()
            .HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey("Customer_Id");

        // Subscription has one Variant of type ProductVariant
        modelBuilder.Entity<Subscription>()
            .HasOne(x => x.Variant)
            .WithMany()
            .HasForeignKey("Variant_Id");

        // Subscription has one PaymentProvider of type PaymentProvider
        modelBuilder.Entity<Subscription>()
            .HasOne(x => x.PaymentProvider)
            .WithMany()
            .HasForeignKey("PaymentProvider_Id");

        // Subscription has one Channel of type Channel
        modelBuilder.Entity<Subscription>()
            .HasOne(x => x.Channel)
            .WithMany()
            .HasForeignKey("Channel_Id");


        // PaymentProvider has one Merchant of type Merchant
        modelBuilder.Entity<PaymentProvider>()
            .HasOne(x => x.Merchant)
            .WithMany()
            .HasForeignKey("Merchant_Id");


        // PaymentProvider has one or more Channels of type Channel
        modelBuilder.Entity<Channel>()
            .HasOne<PaymentProvider>()
            .WithMany(parent => parent.Channels)
            .HasForeignKey("PaymentProvider_Id");

        // PaymentProvider has one or more Payments of type Payment
        modelBuilder.Entity<Payment>()
            .HasOne<PaymentProvider>()
            .WithMany(parent => parent.Payments)
            .HasForeignKey("PaymentProvider_Id");

        // PaymentProvider has one or more Subscriptions of type Subscription
        modelBuilder.Entity<Subscription>()
            .HasOne<PaymentProvider>()
            .WithMany(parent => parent.Subscriptions)
            .HasForeignKey("PaymentProvider_Id");

        // Invoice has one Order of type Order
        modelBuilder.Entity<Invoice>()
            .HasOne(x => x.Order)
            .WithMany()
            .HasForeignKey("Order_Id");


        // GiftCard has one Customer of type Customer
        modelBuilder.Entity<GiftCard>()
            .HasOne(x => x.Customer)
            .WithMany()
            .HasForeignKey("Customer_Id");

        // GiftCard has one IssuedOrder of type Order
        modelBuilder.Entity<GiftCard>()
            .HasOne(x => x.IssuedOrder)
            .WithMany()
            .HasForeignKey("IssuedOrder_Id");


        // GiftCard has one or more Redemptions of type GiftCardRedemption
        modelBuilder.Entity<GiftCardRedemption>()
            .HasOne<GiftCard>()
            .WithMany(parent => parent.Redemptions)
            .HasForeignKey("GiftCard_Id");

        // GiftCardRedemption has one GiftCard of type GiftCard
        modelBuilder.Entity<GiftCardRedemption>()
            .HasOne(x => x.GiftCard)
            .WithMany()
            .HasForeignKey("GiftCard_Id");

        // GiftCardRedemption has one Order of type Order
        modelBuilder.Entity<GiftCardRedemption>()
            .HasOne(x => x.Order)
            .WithMany()
            .HasForeignKey("Order_Id");


        // Payout has one Seller of type Seller
        modelBuilder.Entity<Payout>()
            .HasOne(x => x.Seller)
            .WithMany()
            .HasForeignKey("Seller_Id");


        // Payout has one or more Orders of type Order
        modelBuilder.Entity<Order>()
            .HasOne<Payout>()
            .WithMany(parent => parent.Orders)
            .HasForeignKey("Payout_Id");

    }
}
