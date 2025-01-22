using System.Text;
using Azure.Core;
using JWT.Extensions.AspNetCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using sf.Server.Middlewares;
using sf.Server.Models.SF;
using SportsFestApi.Net.Generated;
using ILogger = Serilog.ILogger;
using User = sf.Server.Models.Auth.User;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File(".logs/latest.log", rollingInterval: RollingInterval.Day)
            .WriteTo.File($".logs/{DateTime.Now:dd-MM-yyyy}.log")
            .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddDbContext<SfContext>(
    options =>
    {
        options.UseMySql(
            builder.Configuration.GetConnectionString("DefaultConnection"),
            new MySqlServerVersion(new Version(8, 0, 21)),
            o =>
            {
                o.EnableRetryOnFailure();
            });
    },
    ServiceLifetime.Transient
);


builder.Services.AddControllers()
       .AddControllersAsServices()
       .AddNewtonsoftJson(
            options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
                options.SerializerSettings.Converters.Add(new IsoDateTimeConverter());
                options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                options.SerializerSettings.Formatting = Formatting.Indented;
                options.SerializerSettings.TypeNameHandling = TypeNameHandling.None;
                options.SerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
                options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
            });

builder.Services.AddApiVersioning(
    options =>
    {
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.DefaultApiVersion = new(1, 0);
        options.ReportApiVersions = true;
        options.ApiVersionReader = new UrlSegmentApiVersionReader();
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(
    options =>
    {
        options.SwaggerDoc(
            "v1",
            new()
                { Title = "SportsFestApi", Version = "v1" });
    });

builder.Services.AddSwaggerGenNewtonsoftSupport();
builder.Services.AddOpenApiDocument();

builder.Services.AddSerilog();
builder.Services.AddScoped<ILogger>(
    provider =>
        new LoggerConfiguration()
           .WriteTo.Console()
           .WriteTo.File(".logs/latest.log", rollingInterval: RollingInterval.Day)
           .WriteTo.File($".logs/{DateTime.Now:dd-MM-yyyy}.log")
           .CreateLogger());

builder.Services.AddScoped<ResultService>();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

var readmePath = Path.Combine(app.Environment.ContentRootPath, "README.md");
var changelogPath = Path.Combine(app.Environment.ContentRootPath, "CHANGELOG.md");
app.UseMiddleware<ReadmeMiddleware>(readmePath, "/swagger/readme.md");
app.UseMiddleware<ReadmeMiddleware>(changelogPath, "/swagger/changelog.md");

// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI();
app.UseOpenApi();
app.UseDeveloperExceptionPage();
// }

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapFallbackToFile("/swagger/index.html");
app.Run();