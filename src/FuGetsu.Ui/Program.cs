using Edrington.CaskTracking.Web.Client.Application;
using FuGetsu.Ui;
using FuGetsu.Ui.Application;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddUiServices();
builder.Services.AddAppAuthentication(builder.Configuration);
builder.Services.AddHttpClients();

await builder.Build().RunAsync();