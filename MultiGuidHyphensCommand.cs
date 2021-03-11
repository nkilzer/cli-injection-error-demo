using System;
using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.Logging;

namespace cli_injection_error_demo
{
    [Command(@"hyphens", Description = @"Create guids with hyphens representation", ResponseFileHandling = ResponseFileHandling.ParseArgsAsLineSeparated)]
    public class MultiGuidHyphensCommand
    {
        private readonly ILogger<MultiGuidHyphensCommand> _logger;
        private readonly MultiGuidCommand _parent;

        public MultiGuidHyphensCommand(MultiGuidCommand parent, ILoggerFactory loggerFactory)
        {
            _parent = parent;
            _logger = loggerFactory.CreateLogger<MultiGuidHyphensCommand>();
        }

        private void OnExecute()
        {
            for (var i = 0; i < _parent.Count; i++)
            {
                _logger.LogInformation($@"[{i:00}/{_parent.Count:00}]: {Guid.NewGuid():D}");
            }
        }
    }
}