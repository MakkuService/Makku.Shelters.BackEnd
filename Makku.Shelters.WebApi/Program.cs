using System.Reflection;
using Makku.Shelters.Application;
using Makku.Shelters.Application.Common.Mappings;
using Makku.Shelters.Application.Interfaces;
using Makku.Shelters.Persistence;
using Makku.Shelters.WebApi.Middleware;
using Makku.Shelters.WebApi.Services;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .WriteTo.File("Logs/SheltersWebAppLog-.txt", rollingInterval:RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(ISheltersDbContext).Assembly));
});
builder.Services.AddApplication();
builder.Services.AddPersistence(config);
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});
builder.Services.AddSingleton<ICurrentUserService, CurrentUserService>();
builder.Services.AddHttpContextAccessor();
builder.Host.UseSerilog();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var context = serviceProvider.GetRequiredService<SheltersDbContext>();
        DbInitializer.Initialize(context);
    }
    catch (Exception e)
    {
        Log.Fatal(e, "An error occured while app initialization");
    }
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.RoutePrefix = string.Empty;
    c.SwaggerEndpoint("swagger/v1/swagger.json", "Shelters API");
});


app.UseCustomExceptionHandler();
app.UseRouting();
app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.MapControllers();
app.Run();
