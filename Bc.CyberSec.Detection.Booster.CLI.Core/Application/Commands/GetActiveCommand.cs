using Bc.CyberSec.Detection.Booster.Api.Client.Api;
using Bc.CyberSec.Detection.Booster.Api.Client.Dto.SyslogNgConfigurator;
using Bc.CyberSec.Detection.Booster.CLI.Application.Application;
using Bc.CyberSec.Detection.Booster.CLI.Application.Application.Commands.Base;

namespace Bc.CyberSec.Detection.Booster.CLI.Application.Application.Commands;

public class GetActiveCommand : Command, IExecuteCommand
{
    private List<UseCaseGetDto> _useCaseGetDtos;
    private bool Detailed { get; set; }
    private bool IsActive => true;

    public GetActiveCommand(IDetectionBoosterApi api, bool detailedOutput) : base(api)
    {
        Priority = 4;
        Detailed = detailedOutput;
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
            Outcome = Detailed ? OutcomeBuilder.PrintUseCasesDetailed(_useCaseGetDtos) : OutcomeBuilder.PrintUseCases(_useCaseGetDtos);
        }
        catch (ApplicationException e)
        {
            Outcome = OutcomeBuilder.Error(e.Message);
        }
    }
}

