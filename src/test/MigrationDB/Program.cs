using db_cp.Models;
using Microsoft.EntityFrameworkCore;


var builder = new DbContextOptionsBuilder<AppDBContext>();
// Deploy
builder.UseNpgsql("Server=postgres;User ID=postgres;Password=17009839;Port=5432;Database=tractor_plant;");
// Local
// builder.UseNpgsql("Server=localhost;User ID=postgres;Password=17009839;Port=5433;Database=tractor_plant;");

using var context = new AppDBContext(builder.Options);
await context.Database.MigrateAsync();
