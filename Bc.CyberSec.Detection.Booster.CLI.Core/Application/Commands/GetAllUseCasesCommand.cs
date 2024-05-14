using Bc.CyberSec.Detection.Booster.Api.Client.Api;
using Bc.CyberSec.Detection.Booster.CLI.Application.Application;
using Bc.CyberSec.Detection.Booster.CLI.Application.Application.Commands.Base;

namespace Bc.CyberSec.Detection.Booster.CLI.Application.Application.Commands;

public class GetAllUseCasesCommand : Command, IExecuteCommand<string>
{

    private readonly IDetectionBoosterApi _api;
    private bool DetailedOutput { get; set; }

    public GetAllUseCasesCommand(string input, IDetectionBoosterApi api, bool detailedOutput) : base(input, api)
    {
        DetailedOutput = detailedOutput;
        _api = api;
        Priority = 3;
    }

    public GetAllUseCasesCommand(IDetectionBoosterApi api, bool detailed) : base(api)
    {
        DetailedOutput = detailed;
        _api = api;
        Priority = 3;
    }

    public override void Execute()
    {
        var useCases = _api.GetAllUseCases();
        Outcome = DetailedOutput ? OutcomeBuilder.PrintUseCasesDetailed(useCases) : OutcomeBuilder.PrintUseCases(useCases);
    }

    public override string GetOutcome()
    {
        return Outcome;
    }
}