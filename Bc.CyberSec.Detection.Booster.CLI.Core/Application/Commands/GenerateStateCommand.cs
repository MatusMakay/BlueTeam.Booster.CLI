using Bc.CyberSec.Detection.Booster.Api.Client.Api;
using Bc.CyberSec.Detection.Booster.CLI.Application.Application.Commands.Base;
using Bc.CyberSec.Detection.Booster.CLI.Application.Application.UseCase;

namespace Bc.CyberSec.Detection.Booster.CLI.Application.Application.Commands;

public class GenerateStateCommand: Command, IExecuteCommand
{
    public GenerateStateCommand(string input, IDetectionBoosterApi api) : base(input, api)
    {
        Priority = 3;
    }

    public GenerateStateCommand(IDetectionBoosterApi api) : base(api)
    {
    }

    public override void Execute()
    {
        var all = Api.GetAllUseCases();
        var outputFile = UseCaseIOHandler.GenerateStateToFile(all, Input);
        Outcome = "State generated to " + outputFile;
    }

    public override object GetOutcome()
    {
        return Outcome;
    }
}