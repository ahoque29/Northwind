using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using NorthwindClient;
using NorthwindClientService;
using Radzen;
using Radzen.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("Client", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Client"));

builder.Services.AddScoped<DialogService>();
builder.Services.AddScoped<NotificationService>();
builder.Services.AddScoped<TooltipService>();
builder.Services.AddScoped<ContextMenuService>();

builder.Services.AddScoped<IReadOnlyService, ReadOnlyService>();
builder.Services.AddScoped<IEntityCreateService, EntityCreateService>();
builder.Services.AddScoped<IEntityEditService, EntityEditService>();
builder.Services.AddScoped<IEntityUpsertService, EntityUpsertService>();
builder.Services.AddScoped<IEntityRemoveService, EntityRemoveService>();
builder.Services.AddScoped<IEntityRemoveAllService, EntityRemoveAllService>();

builder.Services.AddOptions();

await builder.Build().RunAsync();