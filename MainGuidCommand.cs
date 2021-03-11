using System;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;

namespace cli_injection_error_demo
{
    [Command(@"guid", Description = @"Create a list of GUIDs", ResponseFileHandling = ResponseFileHandling.ParseArgsAsLineSeparated)]
    public class MainGuidCommand
    {
        private readonly ILogger<MainGuidCommand> _logger;

        public MainGuidCommand(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<MainGuidCommand>();
        }

        [Option(@"-c|--count")]
        public int Count { get; set; } = 5;

        private void OnExecute()
        {
            for (var i = 0; i < Count; i++)
            {
                _logger.LogInformation($@"[{i:00}/{Count:00}]: {Guid.NewGuid():D}");
            }
        }
    }
}