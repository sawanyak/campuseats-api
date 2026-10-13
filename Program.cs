var builder = WebApplication.CreateBuilder(args);

// Allowed frontend origin comes from configuration (env var Cors__Frontend on the host)
var frontendOrigin = builder.Configuration["Cors:Frontend"] ?? "https://campuseats-web.vercel.app";

builder.Services.AddCors(o => o.AddPolicy("Frontend", p =>
    p.WithOrigins(frontendOrigin, "http://localhost:5173")
     .AllowAnyHeader().AllowAnyMethod()));

var app = builder.Build();

app.UseCors("Frontend");   // before the endpoints are mapped

app.MapGet("/", () => "CampusEats API is running");
app.MapGet("/health", () => Results.Ok(new { status = "healthy" }));

app.MapGet("/api/menu", () => new[]
{
    new { id = 1, name = "Chicken Rice", price = 450 },
    new { id = 2, name = "Veggie Wrap",  price = 350 },
    new { id = 3, name = "Iced Coffee",  price = 200 },
});

app.Run();
