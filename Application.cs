using System;
using System.Threading.Tasks;
using McMaster.Extensions.CommandLineUtils;

namespace cli_injection_error_demo
{
    [Command]
    [Subcommand(typeof(MainGuidCommand), typeof(MultiGuidCommand))]
    internal sealed class Application
    {
        public Task<int> OnExecuteAsync()
        {
            throw new NotImplementedException(@"This command only has subcommands. Refer to help for details.");
        }
    }
}