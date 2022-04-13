using System.Diagnostics;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using MicroSvc01Core.Context;

// Builder
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddControllersWithViews().AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddDbContext<DogContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("localdb"));
    //options.UseSqlServer(builder.Configuration.GetConnectionString("SQL"));
});
builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
    options.AppendTrailingSlash = true;
});

// Application
var app = builder.Build();

// Add response time to headers
app.Use(async (context, next) =>
{
    context.Response.Headers["X-Service"] = "MicroSvc01Core";
    context.Response.Headers["X-Company"] = "MCH Development";
    context.Response.Headers["X-Copyright"] = "2015-" + DateTime.Now.Year.ToString();

    Stopwatch sw = Stopwatch.StartNew();
    context.Response.OnStarting(() =>
    {
        sw.Stop();
        context.Response.Headers["X-Response-Time-ms"] = sw.ElapsedMilliseconds.ToString();
        return Task.CompletedTask;
    });
    await next.Invoke();
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    // --------------------------------------------
    // Do this if you want to "Seed" the database, otherwise comment/delete
    // --------------------------------------------
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        //SeedData.Initialize(services);
    }
}

app.UseAuthorization();
app.MapControllers();
app.Run();
