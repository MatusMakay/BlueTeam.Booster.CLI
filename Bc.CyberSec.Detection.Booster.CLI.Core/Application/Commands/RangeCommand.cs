using Bc.CyberSec.Detection.Booster.Api.Client.Api;
using Bc.CyberSec.Detection.Booster.CLI.Application.Application.Commands.Base;

namespace Bc.CyberSec.Detection.Booster.CLI.Application.Application.Commands;

public class RangeCommand : Command, IExecuteCommand<List<int>>
{

    public RangeCommand(string input, IDetectionBoosterApi api) : base(input, api)
    {
    }

    public override void Execute()
    {
        throw new NotImplementedException();
    }

    public override List<int> GetOutcome()
    {
        return InputParser.Range(Input).Select(x => x).ToList();
    }
}