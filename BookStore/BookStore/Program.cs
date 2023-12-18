using BookStore.DbOperations;

namespace BookStore;

public class Program
{
    public static void Main(string[] args)
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); }).Build();
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            DataGenerator.Initialize(services);
        }
        host.Run();
    }
}