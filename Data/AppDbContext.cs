using Microsoft.EntityFrameworkCore;

namespace CampusEats.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<MenuItem> MenuItems => Set<MenuItem>();

    protected override void OnModelCreating(ModelBuilder b) =>
        b.Entity<MenuItem>().HasData(
            new MenuItem { Id = 1, Name = "Chicken Rice", Price = 450 },
            new MenuItem { Id = 2, Name = "Veggie Wrap",  Price = 350 },
            new MenuItem { Id = 3, Name = "Iced Coffee",  Price = 200 });
}
