using global_web_api.Context;
using global_web_api.Dtos;
using global_web_api.Interfaces;
using global_web_api.Middleware;
using global_web_api.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Add Database dependency Injection
builder.Services.AddDbContext<MyDbContext>(options =>
{
    var conString = builder.Configuration.GetConnectionString("globalAPI_con");
    options.UseSqlServer(conString);
});

builder.Services.AddControllers(options =>
{
    ///Exception Filter
    ///Uncomment the next line to use Filter for Global exception handling

    // options.Filters.Add<GlobalExceptionFilterAttribute>();
});

//Add Dependency Service
builder.Services.AddScoped<IGeneric<UserDto>, UserService>();
builder.Services.AddScoped<IGeneric<ProductDto>, ProductService>();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();


//Add Exception Middleware
app.UseExceptionMiddleware();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
