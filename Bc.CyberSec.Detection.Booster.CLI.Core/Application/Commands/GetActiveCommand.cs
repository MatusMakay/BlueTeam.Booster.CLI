using Bc.CyberSec.Detection.Booster.Api.Client.Api;
using Bc.CyberSec.Detection.Booster.Api.Client.Dto.SyslogNgConfigurator;
using Bc.CyberSec.Detection.Booster.CLI.Application.Application;
using Bc.CyberSec.Detection.Booster.CLI.Application.Application.Commands.Base;

namespace Bc.CyberSec.Detection.Booster.CLI.Application.Application.Commands;

public class GetActiveCommand : Command, IExecuteCommand
{
    private List<UseCaseGetDto> _useCaseGetDtos;
    public GetActiveCommand(IDetectionBoosterApi api) : base(api)
    {
        Priority = 4;
    }

    public override string GetOutcome()
    {
        return Outcome;
    }

    public override void Execute()
    {
        try
        {
            _useCaseGetDtos = Api.GetActiveUseCases();
            Outcome = OutcomeBuilder.GetActiveUseCases(_useCaseGetDtos);
        }
        catch (ApplicationException e)
        {
            Outcome = OutcomeBuilder.Error(e.Message);
        }
    }
}

