using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace OneRosterProviderDemo
{
    public static class LoggingSetup
    {
        public static IWebHostBuilder UseSerilogAndSeq(this IWebHostBuilder webHostBuilder)
        {
            return webHostBuilder.UseSerilog((context, config) =>
            {
                var levelSwitch = new LoggingLevelSwitch(LogEventLevel.Debug);
                var seqConfig = context.Configuration.GetSection("Seq");

                config
                    .ReadFrom.Configuration(context.Configuration)
                    .MinimumLevel.ControlledBy(levelSwitch)
                    .Enrich.FromLogContext()
                    .WriteTo.Seq(
                        seqConfig["ServerUrl"],
                        apiKey: seqConfig["ApiKey"],
                        messageHandler: new SeqLoggingMessageHandler(),
                        controlLevelSwitch: levelSwitch)
                    //.WriteTo.File(@"log.txt")
                    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Literate);
            });
        }
    }

    public class SeqLoggingMessageHandler : HttpClientHandler
    {
        public SeqLoggingMessageHandler()
        {
            ServerCertificateCustomValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => true;
        }
    }
}
