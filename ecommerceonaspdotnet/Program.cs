using ecommerceonaspdotnet.Api;
using ecommerceonaspdotnet.Persistence;
using ecommerceonaspdotnet.Service;
using ecommerceonaspdotnet.Telemetry;
using OpenTelemetry.Metrics;
using Microsoft.EntityFrameworkCore;
using Oracle.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddOpenApi();
builder.Services.AddHealthChecks()
    .AddDbContextCheck<ApplicationDbContext>();

var dbEngineEnvironment = builder.Configuration["DB_TYPE"];

string dbEngine;
string dbUserName;
string dbPassword;
string dbName;
string dbHost;
string dbPort;

if (string.IsNullOrWhiteSpace(dbEngineEnvironment))
{
    // Generation-time configuration
    dbEngine = "mysql";
    dbUserName = "postgres";
    dbPassword = "no_password";
    dbName = "testDb";
    dbHost = "localhost";
    dbPort = "5432";
}
else
{
    // Runtime environment configuration
    dbEngine = dbEngineEnvironment;
    dbUserName = builder.Configuration["DB_USER_NAME"]
        ?? throw new InvalidOperationException("DB_USER_NAME is required when DB_ENGINE is provided.");

    dbPassword = builder.Configuration["DB_PASSWORD"]
        ?? throw new InvalidOperationException("DB_PASSWORD is required when DB_ENGINE is provided.");

    dbName = builder.Configuration["DB_NAME"]
        ?? throw new InvalidOperationException("DB_NAME is required when DB_ENGINE is provided.");

    dbHost = builder.Configuration["DB_HOST"]
        ?? throw new InvalidOperationException("DB_HOST is required when DB_ENGINE is provided.");

    dbPort = builder.Configuration["DB_PORT"]
        ?? throw new InvalidOperationException("DB_PORT is required when DB_ENGINE is provided.");
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString =
        $"Host={dbHost};Port={dbPort};Database={dbName};Username={dbUserName};Password={dbPassword}";

    if (dbEngine.Equals("mysql", StringComparison.OrdinalIgnoreCase))
    {
        options.UseMySQL(connectionString);
    }
    else if (dbEngine.Equals("mariadb", StringComparison.OrdinalIgnoreCase))
    {
        options.UseMySQL(connectionString);
    }
    else if (dbEngine.Equals("sqlite", StringComparison.OrdinalIgnoreCase))
    {
        options.UseSqlite(connectionString);
    }
    else if (dbEngine.Equals("sqlserver", StringComparison.OrdinalIgnoreCase) ||
             dbEngine.Equals("azuresql", StringComparison.OrdinalIgnoreCase))
    {
        options.UseSqlServer(connectionString);
    }
    else if (dbEngine.Equals("oracle", StringComparison.OrdinalIgnoreCase))
    {
        options.UseOracle(connectionString);
    }
    else if (dbEngine.Equals("mongodb", StringComparison.OrdinalIgnoreCase))
    {
        options.UseMongoDB(connectionString);
    }
    else if (dbEngine.Equals("inmemory", StringComparison.OrdinalIgnoreCase))
    {
        options.UseInMemoryDatabase("ecommerceonaspdotnet");
    }
    else if (dbEngine.Equals("cosmosdb", StringComparison.OrdinalIgnoreCase))
    {
        var cosmosEndpoint = builder.Configuration["DB_ENDPOINT"]
            ?? throw new InvalidOperationException(
                "DB_ENDPOINT is required for Cosmos DB.");

        var cosmosKey = builder.Configuration["DB_KEY"]
            ?? throw new InvalidOperationException(
                "DB_KEY is required for Cosmos DB.");

        options.UseCosmos(
            cosmosEndpoint,
            cosmosKey,
            dbName);
    }
    else if (dbEngine.Equals("postgres", StringComparison.OrdinalIgnoreCase))
    {
        options.UseNpgsql(connectionString);
    }
    else
    {
        throw new InvalidOperationException(
            "Unsupported or missing database engine configuration.");
    }
    // etc.
});

    builder.Services.AddScoped<IMerchantRepository, MerchantRepository>();
    builder.Services.AddScoped<IChannelRepository, ChannelRepository>();
    builder.Services.AddScoped<IBrandRepository, BrandRepository>();
    builder.Services.AddScoped<ICatalogRepository, CatalogRepository>();
    builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
    builder.Services.AddScoped<IProductRepository, ProductRepository>();
    builder.Services.AddScoped<IProductVariantRepository, ProductVariantRepository>();
    builder.Services.AddScoped<IProductPricingRepository, ProductPricingRepository>();
    builder.Services.AddScoped<IMediaAssetRepository, MediaAssetRepository>();
    builder.Services.AddScoped<IFulfillmentCenterRepository, FulfillmentCenterRepository>();
    builder.Services.AddScoped<IInventoryItemRepository, InventoryItemRepository>();
    builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
    builder.Services.AddScoped<ISellerRepository, SellerRepository>();
    builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
    builder.Services.AddScoped<ICustomerAddressRepository, CustomerAddressRepository>();
    builder.Services.AddScoped<IWishlistRepository, WishlistRepository>();
    builder.Services.AddScoped<IWishlistItemRepository, WishlistItemRepository>();
    builder.Services.AddScoped<ICartRepository, CartRepository>();
    builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();
    builder.Services.AddScoped<IOrderRepository, OrderRepository>();
    builder.Services.AddScoped<IOrderLineRepository, OrderLineRepository>();
    builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
    builder.Services.AddScoped<IRefundRepository, RefundRepository>();
    builder.Services.AddScoped<IShipmentRepository, ShipmentRepository>();
    builder.Services.AddScoped<IShipmentItemRepository, ShipmentItemRepository>();
    builder.Services.AddScoped<IReturnRequestRepository, ReturnRequestRepository>();
    builder.Services.AddScoped<IReturnItemRepository, ReturnItemRepository>();
    builder.Services.AddScoped<IPromotionRepository, PromotionRepository>();
    builder.Services.AddScoped<ICouponRepository, CouponRepository>();
    builder.Services.AddScoped<ICouponRedemptionRepository, CouponRedemptionRepository>();
    builder.Services.AddScoped<ITaxRuleRepository, TaxRuleRepository>();
    builder.Services.AddScoped<IShippingMethodRepository, ShippingMethodRepository>();
    builder.Services.AddScoped<ICarrierServiceRepository, CarrierServiceRepository>();
    builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
    builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
    builder.Services.AddScoped<IPaymentProviderRepository, PaymentProviderRepository>();
    builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
    builder.Services.AddScoped<IGiftCardRepository, GiftCardRepository>();
    builder.Services.AddScoped<IGiftCardRedemptionRepository, GiftCardRedemptionRepository>();
    builder.Services.AddScoped<IPayoutRepository, PayoutRepository>();

    builder.Services.AddScoped<IMerchantService, MerchantService>();
    builder.Services.AddScoped<IChannelService, ChannelService>();
    builder.Services.AddScoped<IBrandService, BrandService>();
    builder.Services.AddScoped<ICatalogService, CatalogService>();
    builder.Services.AddScoped<ICategoryService, CategoryService>();
    builder.Services.AddScoped<IProductService, ProductService>();
    builder.Services.AddScoped<IProductVariantService, ProductVariantService>();
    builder.Services.AddScoped<IProductPricingService, ProductPricingService>();
    builder.Services.AddScoped<IMediaAssetService, MediaAssetService>();
    builder.Services.AddScoped<IFulfillmentCenterService, FulfillmentCenterService>();
    builder.Services.AddScoped<IInventoryItemService, InventoryItemService>();
    builder.Services.AddScoped<ISupplierService, SupplierService>();
    builder.Services.AddScoped<ISellerService, SellerService>();
    builder.Services.AddScoped<ICustomerService, CustomerService>();
    builder.Services.AddScoped<ICustomerAddressService, CustomerAddressService>();
    builder.Services.AddScoped<IWishlistService, WishlistService>();
    builder.Services.AddScoped<IWishlistItemService, WishlistItemService>();
    builder.Services.AddScoped<ICartService, CartService>();
    builder.Services.AddScoped<ICartItemService, CartItemService>();
    builder.Services.AddScoped<IOrderService, OrderService>();
    builder.Services.AddScoped<IOrderLineService, OrderLineService>();
    builder.Services.AddScoped<IPaymentService, PaymentService>();
    builder.Services.AddScoped<IRefundService, RefundService>();
    builder.Services.AddScoped<IShipmentService, ShipmentService>();
    builder.Services.AddScoped<IShipmentItemService, ShipmentItemService>();
    builder.Services.AddScoped<IReturnRequestService, ReturnRequestService>();
    builder.Services.AddScoped<IReturnItemService, ReturnItemService>();
    builder.Services.AddScoped<IPromotionService, PromotionService>();
    builder.Services.AddScoped<ICouponService, CouponService>();
    builder.Services.AddScoped<ICouponRedemptionService, CouponRedemptionService>();
    builder.Services.AddScoped<ITaxRuleService, TaxRuleService>();
    builder.Services.AddScoped<IShippingMethodService, ShippingMethodService>();
    builder.Services.AddScoped<ICarrierServiceService, CarrierServiceService>();
    builder.Services.AddScoped<IReviewService, ReviewService>();
    builder.Services.AddScoped<ISubscriptionService, SubscriptionService>();
    builder.Services.AddScoped<IPaymentProviderService, PaymentProviderService>();
    builder.Services.AddScoped<IInvoiceService, InvoiceService>();
    builder.Services.AddScoped<IGiftCardService, GiftCardService>();
    builder.Services.AddScoped<IGiftCardRedemptionService, GiftCardRedemptionService>();
    builder.Services.AddScoped<IPayoutService, PayoutService>();

// apply the service resolver
builder.Services.AddScoped<IServiceResolver, ServiceResolver>();

builder.Services.AddSingleton<ApplicationTelemetry>();

builder.Services
    .AddOpenTelemetry()
    .WithMetrics(metrics =>
    {
        metrics.AddMeter("Harbormaster.Application");

        metrics.AddPrometheusExporter();
    });

var app = builder.Build();

app.MapPrometheusScrapingEndpoint();


// Health endpoint
app.MapHealthChecks("/health");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    // Testing uses a shared SQLite file; wipe it so unique indexes (e.g. email) don't fail on re-runs.
    if (app.Environment.IsEnvironment("Testing"))
    {
        db.Database.EnsureDeleted();
    }

    // EnsureCreated does not alter an existing schema. If you changed relationships locally,
    // recreate the MySQL database (docker compose down -v && docker compose up -d).
    db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


    app.MapMerchantEndpoints();
    app.MapChannelEndpoints();
    app.MapBrandEndpoints();
    app.MapCatalogEndpoints();
    app.MapCategoryEndpoints();
    app.MapProductEndpoints();
    app.MapProductVariantEndpoints();
    app.MapProductPricingEndpoints();
    app.MapMediaAssetEndpoints();
    app.MapFulfillmentCenterEndpoints();
    app.MapInventoryItemEndpoints();
    app.MapSupplierEndpoints();
    app.MapSellerEndpoints();
    app.MapCustomerEndpoints();
    app.MapCustomerAddressEndpoints();
    app.MapWishlistEndpoints();
    app.MapWishlistItemEndpoints();
    app.MapCartEndpoints();
    app.MapCartItemEndpoints();
    app.MapOrderEndpoints();
    app.MapOrderLineEndpoints();
    app.MapPaymentEndpoints();
    app.MapRefundEndpoints();
    app.MapShipmentEndpoints();
    app.MapShipmentItemEndpoints();
    app.MapReturnRequestEndpoints();
    app.MapReturnItemEndpoints();
    app.MapPromotionEndpoints();
    app.MapCouponEndpoints();
    app.MapCouponRedemptionEndpoints();
    app.MapTaxRuleEndpoints();
    app.MapShippingMethodEndpoints();
    app.MapCarrierServiceEndpoints();
    app.MapReviewEndpoints();
    app.MapSubscriptionEndpoints();
    app.MapPaymentProviderEndpoints();
    app.MapInvoiceEndpoints();
    app.MapGiftCardEndpoints();
    app.MapGiftCardRedemptionEndpoints();
    app.MapPayoutEndpoints();

app.Run();

public partial class Program { }

