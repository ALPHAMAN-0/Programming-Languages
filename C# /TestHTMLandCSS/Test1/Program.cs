using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.FileProviders;
using Test1.Services;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<CartService>();

// Configure for large file uploads
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = long.MaxValue;
    options.ValueLengthLimit = int.MaxValue;
    options.MemoryBufferThreshold = int.MaxValue;
});

// Configure Kestrel server options (preferred over IIS for .NET 9.0)
builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxRequestBodySize = long.MaxValue;
});

// Configure HTTPS port
builder.WebHost.UseUrls("http://localhost:5000", "https://localhost:5001");

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
