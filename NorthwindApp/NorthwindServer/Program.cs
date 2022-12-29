using Microsoft.EntityFrameworkCore;
using NorthwindCommon;
using NorthwindCommon.DTOs;
using NorthwindDataAccess.DatabaseContext;
using NorthwindDataAccess.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddControllers();

var northwindConnectionString = builder.Configuration["NorthwindConnectionString"];
builder.Services.AddDbContext<NorthwindContext>(opts =>
	opts.UseSqlServer(northwindConnectionString));

builder.Services.AddAutoMapper(typeof(MapperProfiles));

builder.Services.AddSingleton<IReadService<CustomerDto>, CustomerService>();
builder.Services.AddSingleton<ICreateService<CustomerDto>, CustomerService>();
builder.Services.AddSingleton<IEditService<CustomerDto>, CustomerService>();
builder.Services.AddSingleton<IRemoveService<CustomerDto>, CustomerService>();
builder.Services.AddSingleton<IUpsertService<CustomerDto>, CustomerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
	app.UseWebAssemblyDebugging();
}
else
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseBlazorFrameworkFiles();
app.MapRazorPages();
app.MapControllers();
app.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
app.MapFallbackToFile("index.html");

app.Run();