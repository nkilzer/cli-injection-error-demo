using System;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;

namespace cli_injection_error_demo
{
    [Command(@"parens", Description = @"Create guids with parens representation", ResponseFileHandling = ResponseFileHandling.ParseArgsAsLineSeparated)]
    public class MultiGuidParensCommand
    {
        private readonly ILogger<MultiGuidParensCommand> _logger;
        private readonly MultiGuidCommand _parent;

        public MultiGuidParensCommand(MultiGuidCommand parent, ILoggerFactory loggerFactory)
        {
            _parent = parent;
            _logger = loggerFactory.CreateLogger<MultiGuidParensCommand>();
        }

        private void OnExecute()
        {
            for (var i = 0; i < _parent.Count; i++)
            {
                _logger.LogInformation($@"[{i:00}/{_parent.Count:00}]: {Guid.NewGuid():P}");
            }
        }
    }
}