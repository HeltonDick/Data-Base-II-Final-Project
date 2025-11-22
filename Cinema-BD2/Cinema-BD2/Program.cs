using Cinema_BD2.Data;
using Cinema_BD2.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<CinemaContext>(options =>
    options.UseSqlServer (
        builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'CinemaContext' not found.")
    )
);

// Repository Dependency Injection //
builder.Services.AddScoped<IGenderRepository, GenderRepository>(); // CERTO
builder.Services.AddScoped<IDistrictRepository, DistrictRepository>(); // CERTO
builder.Services.AddScoped<ILanguageRepository, LanguageRepository>(); // CERTO
builder.Services.AddScoped<IGenreRepository, GenreRepository>(); // CERTO
builder.Services.AddScoped<IStudioRepository, StudioRepository>(); // CERTO
builder.Services.AddScoped<IFilmRepository, FilmRepository>(); // CERTO
builder.Services.AddScoped<IRoomRepository, RoomRepository>(); // CERTO
builder.Services.AddScoped<IAddressRepository, AddressRepository>(); // CERTO
builder.Services.AddScoped<IClassificationRepository, ClassificationRepository>(); // CERTO
builder.Services.AddScoped<IPerosonRepository, PersonRepository>(); // CERTO
builder.Services.AddScoped<IRoleRepository, RoleRepository>(); // CERTO
builder.Services.AddScoped<ITypeOfRoomRepository, TypeOfRoomRepository>(); // CERTO
builder.Services.AddScoped<ITicketRepository, TicketRepository>(); // CERTO
builder.Services.AddScoped<IStreetRepository, StreetRepository>(); // CERTO
builder.Services.AddScoped<ISessionRepository, SessionRepository>(); // CERTO
builder.Services.AddScoped<IDimensionRepository, DimensionRepository>(); // CERTO
// End Repository Dependency Injection //

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

CreateDbIfNotExists(app);

app.Run();

static void CreateDbIfNotExists(IHost host)
{
    using (var scope = host.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<CinemaContext>();
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred creating the DB.");
        }
    }
}