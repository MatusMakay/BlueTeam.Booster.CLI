using Bc.CyberSec.Detection.Booster.Api.Client.Api;
using Bc.CyberSec.Detection.Booster.Api.Client.Dto.SyslogNgConfigurator;
using Bc.CyberSec.Detection.Booster.CLI.Application.Application;
using Bc.CyberSec.Detection.Booster.CLI.Application.Application.Commands.Base;

namespace Bc.CyberSec.Detection.Booster.CLI.Application.Application.Commands;

public class DeactivateCommand : Command, IExecuteCommand
{
    private readonly IExecuteCommand<List<string>>? _range;

    public DeactivateCommand(string input, IDetectionBoosterApi api) : base(input, api)
    {
        Priority = 2;
        _range = null;
    }

    public DeactivateCommand(IDetectionBoosterApi api, IExecuteCommand<List<string>>? range) : base(api)
    {
        Priority = 2;
        _range = range;
    }

   
    public override string GetOutcome()
    {
        return Outcome;
    }

    public override void Execute()
    {
        try
        {
            var identifiers = _range == null ? InputParser.Parse(Input).Select(x => $"UC{x}") : _range.GetOutcome();
            Api.DeactivateUseCase(identifiers.ToList());
            Outcome = OutcomeBuilder.UseCasesDeactivated(identifiers.ToList());
        }
        catch (ApplicationException e)
        {
            Outcome = OutcomeBuilder.Error(e.Message);
        }
    }
}