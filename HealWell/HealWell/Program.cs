using HealWell;
using HealWell.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazored.LocalStorage;
using HealWell.Models;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<PatientService>();
builder.Services.AddScoped<DoctorService>();
builder.Services.AddScoped<AppointmentService>();
builder.Services.AddScoped<CheckoutService>();
builder.Services.AddScoped<ChatService>();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<CustomAuthorizationMessageHandler>();

// Register named HttpClient
builder.Services.AddHttpClient("SecureAPI", client =>
{
    // You can leave BaseAddress empty since you'll set it manually in each call
}).AddHttpMessageHandler<CustomAuthorizationMessageHandler>();

// Instead of IHttpClientFactory, just register HttpClient directly
builder.Services.AddScoped(sp =>
{
    var clientFactory = sp.GetRequiredService<IHttpClientFactory>();
    return clientFactory.CreateClient("SecureAPI");
});
await builder.Build().RunAsync();
