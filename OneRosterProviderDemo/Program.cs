using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace OneRosterProviderDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            host.Services.EnsureDatabasesMigratedAndSeeded();
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseSerilogAndSeq()
                .UseStartup<Startup>()
                .Build();
    }
}
