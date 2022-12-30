using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using NorthwindClient;
using NorthwindClientService;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("Client", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Client"));

builder.Services.AddScoped<IEntityCreateService, EntityCreateService>();
builder.Services.AddScoped<IEntityEditService, EntityEditService>();
builder.Services.AddScoped<IEntityUpsertService, EntityUpsertService>();
builder.Services.AddScoped<IEntityRemoveService, EntityRemoveService>();
builder.Services.AddScoped<IEntityRemoveAllService, EntityRemoveAllService>();


builder.Services.AddOptions();

await builder.Build().RunAsync();