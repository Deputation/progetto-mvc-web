using CoreWCF;
using CoreWCF.Configuration;
using CoreWCF.Description;
using Microsoft.OpenApi.Models;
using PaymentPointFinder.Core.Services;
using PaymentPointFinder.Core.Services.Interfaces;
using PaymentPointFinder.Services.Interfaces;
using System.Globalization;

namespace PaymentPointFinder.Web;

public class Program
{
    private static void ConfigureSoapEndpoints(WebApplication app)
    {
        app.UseServiceModel(serviceBuilder =>
        {
            serviceBuilder.AddService<PaymentPointSoapService>(serviceOptions =>
            {
                serviceOptions.DebugBehavior.IncludeExceptionDetailInFaults = true;
            });
        
            serviceBuilder.AddServiceEndpoint<PaymentPointSoapService, IPaymentPointSoapService>(
                new BasicHttpBinding(), 
                "/PaymentPointService.svc"
            );

            var serviceMetadataBehavior = app.Services.GetRequiredService<ServiceMetadataBehavior>();
            serviceMetadataBehavior.HttpGetEnabled = true;
        });
    }
    public static void Main(string[] args)
    {
        var cultureInfo = CultureInfo.InvariantCulture;
        CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
        CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        
        builder.Services.AddMemoryCache();
        builder.Services.AddSingleton<IPaymentPointCacheService, PaymentPointCacheService>();
        
        builder.Services.AddHttpClient<IPaymentPointRestService, PaymentPointRestService>();
        builder.Services.AddScoped<ILocationService, LocationService>();
        builder.Services.AddScoped<IPaymentPointSoapService, PaymentPointSoapService>();
        
        // Allow CoreWCF to instance it
        builder.Services.AddTransient<PaymentPointSoapService>();

        // CoreWCF services
        builder.Services.AddServiceModelServices();
        builder.Services.AddServiceModelMetadata();
        builder.Services.AddSingleton<IServiceBehavior, UseRequestHeadersForMetadataAddressBehavior>();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "PaymentPointFinder API",
                Version = "v1"
            });
        });
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        
        ConfigureSoapEndpoints(app);

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "PaymentPointFinder API");
        });
        
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}