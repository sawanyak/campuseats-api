using CampusEats.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Connection string comes from env var ConnectionStrings__Default (never from code)
builder.Services.AddDbContext<AppDbContext>(o =>
    o.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

// Allowed frontend origin comes from configuration (env var Cors__Frontend on the host)
var frontendOrigin = builder.Configuration["Cors:Frontend"] ?? "https://campuseats-web.vercel.app";

builder.Services.AddCors(o => o.AddPolicy("Frontend", p =>
    p.WithOrigins(frontendOrigin, "http://localhost:5173")
     .AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();

app.UseCors("Frontend");   // before the endpoints are mapped

app.MapGet("/", () => "CampusEats API is running");
app.MapGet("/health", () => Results.Ok(new { status = "healthy" }));
app.MapGet("/api/menu", async (AppDbContext db) => await db.MenuItems.OrderBy(m => m.Id).ToListAsync());

app.Run();
