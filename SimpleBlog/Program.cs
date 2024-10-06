using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SimpleBlog.Data;
using SimpleBlog.Interfaces;
using SimpleBlog.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

// Add DbContext to the container
builder.Services.AddDbContext<BlogDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
    .AddEntityFrameworkStores<BlogDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromHours(2);
    options.SlidingExpiration = true;
});

builder.Services.AddAuthorization();

// Register IBlogPostRepository
builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();

var app = builder.Build();

// Seed the database
await app.SeedDatabase();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=BlogPost}/{action=Index}/{id?}");

app.Run();