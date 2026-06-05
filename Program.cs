using CineScope.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CineScopeDbContext>(options => options.UseSqlServer(connectionString));

var appConfig = builder.Configuration.GetSection("App");

builder.Services
    .AddScoped<MovieRepository, MovieRepository>()
    .AddScoped<GenreRepository, GenreRepository>()
    .AddScoped<UserRepository, UserRepository>()
    .AddSingleton(new TmdbApiService(appConfig["TmdbApiKey"]!))
    ;

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();
