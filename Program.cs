using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace cli_injection_error_demo
{
    internal class Program
    {
        private static IHostBuilder CreateBuilder(string[] args)
        {
            return new HostBuilder()
                .ConfigureHostConfiguration(
                    builder =>
                    {
                        const string prefix = @"DOTNET_";
                        builder.AddEnvironmentVariables(prefix);

                        builder.AddInMemoryCollection(new Dictionary<string, string>());

                        if (args.Length <= 0) return;
                        builder.AddCommandLine(args);
                    })
                .ConfigureAppConfiguration(
                    (_, builder) =>
                    {
                        builder.AddEnvironmentVariables();
                        builder.AddCommandLine(args);
                    })
                .ConfigureLogging(l => l.AddConsole())
                .UseDefaultServiceProvider(
                    (context, options) =>
                    {
                        var isDevelopment = context.HostingEnvironment.IsDevelopment();
                        options.ValidateScopes = isDevelopment;
                        options.ValidateOnBuild = isDevelopment;
                    });
        }

        private static async Task<int> Main(string[] args)
        {
            var builder = CreateBuilder(args);
            //builder.ConfigureServices(
            //    s =>
            //    {
            //        s.AddSingleton<Application>();
            //        s.AddSingleton<MainGuidCommand>();
            //        s.AddSingleton<MultiGuidCommand>();
            //        s.AddSingleton<MultiGuidParensCommand>();
            //        s.AddSingleton<MultiGuidHyphensCommand>();
            //    });
            builder = builder.UseCommandLineApplication<Application>(args);

            var cancellationToken = CancellationToken.None;
            using var host = builder.Build();
            var logger = host.Services.GetRequiredService<ILoggerFactory>().CreateLogger(@"Injection-Error-Demo");

            try
            {
                return await host.RunCommandLineApplicationAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, @"Execution failed");
                return int.MinValue;
            }
        }
    }
}