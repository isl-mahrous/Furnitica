using API.Errors;
using API.Middleware;
using Microsoft.AspNetCore.Mvc;
using API.Helpers;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Database Service
builder.Services.AddDbContext<StoreContext>(options => options.
            UseSqlServer(builder.Configuration.
            GetConnectionString("Default")));


builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = actionContext =>
    {
        var errors = actionContext.ModelState
            .Where(e => e.Value.Errors.Count() > 0)
            .SelectMany(x => x.Value.Errors)
            .Select(x => x.ErrorMessage)
            .ToArray();

        var errorResponse = new ApiValidationErrorResponse()
        {
            Errors = errors
        };

        return new BadRequestObjectResult(errorResponse);
    };
});

//Auto Mapper
builder.Services.AddAutoMapper(typeof(MappingProfiles));

//Generic Repostiory Service
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
//How To Use? 
/// <summary>
/// In the constructor you just need to specify the types... for example
/// public ProductsController(IGenericRepository<Product> productRepo,IGenericRepository<ProductBrand> productBrandRepo)
/// </summary>

var app = builder.Build();


//Configure Database and Seed
using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
    var loggerFactory = service.GetRequiredService<ILoggerFactory>();
    try
    {
        //Ensure Database Creation and Update to Latest Migration
        var context = service.GetRequiredService<StoreContext>();
        await context.Database.MigrateAsync();
        await SeedData.SeedAsync(context, loggerFactory);
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "An Error Ocurred During Migration");
    }
}

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStatusCodePagesWithReExecute("/errors/{0}");


app.UseAuthorization();

app.MapControllers();

app.Run();
