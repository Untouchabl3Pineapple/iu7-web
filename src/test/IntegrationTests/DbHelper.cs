using db_cp.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using db_cp.Services;
using db_cp.Interfaces;
using db_cp.Repository;

namespace IntegrationTests
{
    public static class DbHelper
    {
        // Deploy
         private readonly static string connString = "Server=postgres;User ID=postgres;Password=17009839;Port=5432;Database=tractor_plant;";
        // Local
        //private readonly static string connString = "Server=localhost;User ID=postgres;Password=17009839;Port=5433;Database=tractor_plant;";


        private readonly static AppDBContext context = new AppDBContext(new DbContextOptionsBuilder<AppDBContext>()
                                                                             .UseNpgsql(connString)
                                                                             .Options);

        private readonly static IHost host = new HostBuilder().ConfigureServices((hostContext, services) =>
        {
            services
                .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
                .AddScoped<ButtonsEventsService>()
                .AddScoped<ButtonsPostsService>()
                .AddScoped<EventsService>()
                .AddScoped<EventsTypesService>()
                .AddScoped<UsersService>()
                .AddScoped<IButtonsEventsRepository, ButtonsEventsRepository>()
                .AddScoped<IButtonsPostsRepository, ButtonsPostsRepository>()
                .AddScoped<IEventsRepository, EventsRepository>()
                .AddScoped<IEventsTypesRepository, EventsTypesRepository>()
                .AddScoped<IUsersRepository, UsersRepository>()
                .AddSingleton(context);
        }).Build();

        public static void ClearDb()
        {
            context.EventsTypes.RemoveRange(context.EventsTypes);
            context.Users.RemoveRange(context.Users);
            context.Events.RemoveRange(context.Events);
            context.ButtonsEvents.RemoveRange(context.ButtonsEvents);
            context.ButtonsPosts.RemoveRange(context.ButtonsPosts);
            context.SaveChanges();
        }

        public static T GetRequiredService<T>() where T : notnull
        {
            using var serviceScope = host.Services.CreateScope();
            var services = serviceScope.ServiceProvider;
            return services.GetRequiredService<T>();
        }

        public static AppDBContext GetContext()
        {
            return context;
        }
    }
}
