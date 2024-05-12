using Bc.CyberSec.Detection.Booster.CLI.Application.Application.Commands;
using Bc.CyberSec.Detection.Booster.CLI.Application.Application.Commands.Base;
using Bc.CyberSec.Detection.Booster.CLI.Application.Infrastructure;
using Bc.CyberSec.Detection.Booster.CLI.Options;
using CommandLine;
using Microsoft.Extensions.Configuration;

namespace Bc.CyberSec.Detection.Booster.CLI;

public  class Program
{
    public static int Main(string[] args)
    {
        var builder = new ConfigurationBuilder()
            .AddUserSecrets<Configuration>();

        var api = Configuration.Configure(builder.Build());
        
        if (args == null || args.Length == 0)
        {
            args = new string[] { "--help" };
        }

        args = new[] { "-a", "2" };

        Parser.Default.ParseArguments<Options.Options>(args)
            .WithParsed(options =>
            {
                var commandExec = new CommandExecutor(new List<IExecuteCommand>());
                bool detailed = OptionsHandler.DetailedOutput(args);

                if (OptionsHandler.InitOption(options))
                {
                    commandExec.Add(new UploadUseCasesCommand(options.InputFilePath, api, detailed));
                }

                if (OptionsHandler.GetActiveOption(args))
                {
                    commandExec.Add(new GetActiveCommand(api));
                }

                if (OptionsHandler.GetInactiveOption(args))
                {
                    commandExec.Add(new GetInactiveCommand(api));
                }

                if (OptionsHandler.GetAllUseCases(args))
                {
                    commandExec.Add(new GetAllUseCasesCommand(api, detailed));
                }

                if (!string.IsNullOrWhiteSpace(options.OutputFilePath))
                {
                    commandExec.Add(new GenerateStateCommand(options.OutputFilePath, api));
                }

                if (OptionsHandler.RangeOption(options))
                {
                    var range = new RangeCommand(options.RangeOfUseCases, api);
                    
                    if (OptionsHandler.ActivateOption(args))
                    {
                        commandExec.Add(new ActivateCommand(api, range));
                    }
                    else if (OptionsHandler.DeactivateOption(args))
                    {
                        commandExec.Add(new DeactivateCommand(api, range));
                    }
                }

                if (!OptionsHandler.RangeOption(options) && OptionsHandler.ActivateOption(options))
                {
                    commandExec.Add(new ActivateCommand(options.ActivateUseCases, api));
                }
                if (!OptionsHandler.RangeOption(options) && OptionsHandler.DeactivateOption(options))
                {
                    commandExec.Add(new DeactivateCommand(options.DeactivateUseCases, api));
                }
                commandExec.Execute();
            });
        
        return 0;
    }
}