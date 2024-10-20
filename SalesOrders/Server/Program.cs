global using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.IdentityModel.Tokens;
using SalesOrders.Client.Service.AuthService;
using SalesOrders.DAL.Models;
using SalesOrders.Server.Middleware;
using SalesOrders.Shared.ExternalCalls;
using SalesOrders.Shared.Orders;
using SalesOrders.Shared.User;
using Serilog;


var builder = WebApplication.CreateBuilder(args);


#region Context
builder.Services.AddDbContext<SalesOrderDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
#endregion

builder.Host.UseSerilog();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

//Add swagger docs
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IExternalCallService, ExteernalCallService>();
builder.Services.AddScoped<IOrderService, OrderService>();
#endregion

#region Authentication 
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

#endregion


builder.Services.AddHttpContextAccessor();

#region Serilog
var homeDirectory = Environment.GetEnvironmentVariable("HOME") ?? ".";
var logDirectory = Path.Combine(homeDirectory, "LogFiles");

builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .Enrich.WithProperty("ApplicationName", builder.Environment.ApplicationName)
    .Enrich.WithProperty("EnvironmentName", builder.Environment.EnvironmentName)
    .Enrich.With(new SerilogMiddleware())
      .WriteTo.Console(outputTemplate: "{Timestamp:HH:mm:ss.fff} [{Level:u1}] {Message:lj}{NewLine}{Exception}")
    .WriteTo.File(
          path: $"{logDirectory}/log.txt",
          rollingInterval: RollingInterval.Day,
          shared: true,
          flushToDiskInterval: System.TimeSpan.FromSeconds(1),
          outputTemplate: "{Timestamp:HH:mm:ss.fff} [{Level:u1}] {Message:lj}{NewLine}{Exception}{NewLine}{Properties:j}{NewLine}{CustomProperties}{NewLine}",
          retainedFileCountLimit: 10
      )

);
#endregion

#region Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});
#endregion

var app = builder.Build();

app.UseSwaggerUI();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSwagger();

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapRazorPages();
app.MapControllers();
app.UseSerilogRequestLogging();
app.MapFallbackToFile("index.html");

app.Run();
