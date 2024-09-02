using backend.AuthrModule.Model;
using backend.Context;
using backend.Helpers;
using backend.ProductCategoryModule.Repository;
using backend.ProductModule.Repository;
using backend.ProductVariantModule.Repository;
using backend.UserModule.Model;
using backend.UserModule.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Config DB
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("local");
    options.UseSqlServer(connectionString, sqlServerOptionsAction: sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
    });
});


builder.Services.AddControllers();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
builder.Services.AddScoped<IProductVariantRepository, ProductVariantRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();


builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<AesEncryptionHelper>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Add autorize
builder.Services.AddAuthorization();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options => {
    options
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod();
});
// builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowSpecificOrigin",
//        builder =>
//        {
//            builder.AllowAnyOrigin()
//                   .AllowAnyHeader()
//                   .AllowAnyMethod();
//        });
//});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
