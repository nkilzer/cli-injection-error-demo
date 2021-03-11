using System;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;

namespace cli_injection_error_demo
{
    [Subcommand(typeof(MultiGuidHyphensCommand), typeof(MultiGuidParensCommand))]
    [Command(@"multiguid", Description = @"Create multi-format GUIDs", ResponseFileHandling = ResponseFileHandling.ParseArgsAsLineSeparated)]
    public class MultiGuidCommand
    {
        private readonly ILogger<MultiGuidCommand> _logger;

        public MultiGuidCommand(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<MultiGuidCommand>();
        }

        [Option(@"-c|--count")]
        public int Count { get; set; } = 5;

        private void OnExecute()
        {
            _logger.LogInformation($@"Executed multi-guid command: {Count}");
            throw new NotImplementedException(@"This command only has subcommands. Refer to help for details.");
        }
    }
}