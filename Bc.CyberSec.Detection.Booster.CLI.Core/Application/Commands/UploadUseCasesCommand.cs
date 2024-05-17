using System.Net;
using Bc.CyberSec.Detection.Booster.Api.Client.Api;
using Bc.CyberSec.Detection.Booster.CLI.Application.Application;
using Bc.CyberSec.Detection.Booster.CLI.Application.Application.Commands.Base;
using Bc.CyberSec.Detection.Booster.CLI.Application.Application.UseCase;

namespace Bc.CyberSec.Detection.Booster.CLI.Application.Application.Commands;

public class UploadUseCasesCommand : Command, IExecuteCommand
{
    private bool DetailedOutput { get; set; }

    public UploadUseCasesCommand(string input, IDetectionBoosterApi api, bool detailed) : base(input, api)
    {
        DetailedOutput = detailed;
        Priority = 1;
    }

    public override void Execute()
    {
        var filePath = Input;
        var useCasesCreateDto = UseCaseIoHandler.Initialize(filePath);

        var result = Api.CreateUseCases(useCasesCreateDto);
        Outcome = DetailedOutput ? OutcomeBuilder.UseCaseCreatedDetail(useCasesCreateDto) : OutcomeBuilder.UseCaseCreated(useCasesCreateDto);
    }

    public override string GetOutcome()
    {
        return Outcome;
    }
}