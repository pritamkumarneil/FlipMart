using FlipCommerce.Repository;
using FlipCommerce.Service;
using FlipCommerce.Service.ServiceImpl;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
    builder.Services.AddDbContext<FlipCommerceDbContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("flipcommerceConnection")));
    // now registering the Services into the IOC container
    builder.Services.AddScoped<ISellerService,SellerService>(serviceProvider=>new SellerService(serviceProvider.GetRequiredService<FlipCommerceDbContext>()));
    builder.Services.AddScoped<IItemService, ItemService>(serviceProvider => new ItemService(serviceProvider.GetRequiredService<FlipCommerceDbContext>()));
    builder.Services.AddScoped<ICartService, CartService>(serviceProvider => new CartService(serviceProvider.GetRequiredService<FlipCommerceDbContext>(), serviceProvider.GetRequiredService<IItemService>()));
    builder.Services.AddScoped<ICustomerService, CustomerService>(serviceProvider => new CustomerService(serviceProvider.GetRequiredService<FlipCommerceDbContext>(),serviceProvider.GetRequiredService<ICartService>()));
    builder.Services.AddScoped<IProductService, ProductService>(serviceProvider => new ProductService(serviceProvider.GetRequiredService<FlipCommerceDbContext>()));
    builder.Services.AddScoped<IOrderService, OrderService>(serviceProvider => new OrderService(serviceProvider.GetRequiredService<FlipCommerceDbContext>()));
    builder.Services.AddScoped<ICardService, CardService>(serviceProvider => new CardService(serviceProvider.GetRequiredService<FlipCommerceDbContext>()));
    builder.Services.AddCors(option=>option.AddPolicy("FlipCommerce",j=>j.AllowAnyHeader().AllowAnyOrigin().AllowAnyOrigin()));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("FlipCommerce");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
