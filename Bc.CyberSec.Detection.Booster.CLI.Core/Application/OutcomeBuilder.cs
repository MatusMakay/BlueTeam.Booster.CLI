using System;
using System.Text;
using Bc.CyberSec.Detection.Booster.Api.Client.Dto.SyslogNgConfigurator;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Bc.CyberSec.Detection.Booster.CLI.Application.Application;

public static class OutcomeBuilder
{
    public static string Error(string errorMessage)
    {
        return $@"
            Error occurred while processing the request. 
            Message: {errorMessage}  
        ";
    }

    public static string PrintUseCaseState(List<int> identifiers, bool activated)
    {
        var outcome = "";
        var activatedString = activated ? "activated" : "deactivated";
        foreach (var identifier in identifiers)
        {
            outcome += @$"UC{identifier} was {activatedString}. {Environment.NewLine}";
        }
        return outcome;
    }

    public static string UseCaseCreated(List<UseCaseCreateDto> useCase)
    {
        var outcome = "";
        outcome = $"{useCase.Count}UC was created.";
        return outcome;
    }

    public static string UseCaseCreatedDetail(List<UseCaseCreateDto> useCase)
    {
        var outcome = new StringBuilder();
        foreach (var useCaseCreateDto in useCase)
        {

            var mnemonicList = useCaseCreateDto.Mnemonics != null && useCaseCreateDto.Mnemonics.Any()
                ? useCaseCreateDto.Mnemonics
                : new List<string>();
            var mnemonicString = string.Join(", ", mnemonicList);

            var outcomeMnemonic = mnemonicList.Any()
                ? $"   - Mnemonics: {mnemonicString}{Environment.NewLine}"
                : $"{Environment.NewLine}";

            outcome.AppendLine($"{useCaseCreateDto.UseCaseIdentifier} was created. Details:");
            outcome.AppendLine($"   - Name: {useCaseCreateDto.Name}");
            outcome.AppendLine($"   - Mitre Attack Id: {useCaseCreateDto.MitreAttackId}");
            outcome.AppendLine($"   - Kibana Rule Id: {useCaseCreateDto.KibanaRuleId}");
            outcome.AppendLine($"{outcomeMnemonic}");
        }

        return outcome.ToString();
    }

    public static string PrintUseCasesDetailed(List<UseCaseGetDto> useCases)
    {
        var outcome = new StringBuilder();
        foreach (var useCaseCreateDto in useCases)
        {

            var mnemonicList = useCaseCreateDto.Mnemonics != null && useCaseCreateDto.Mnemonics.Any()
                ? useCaseCreateDto.Mnemonics
                : new List<string>();
            var mnemonicString = string.Join(", ", mnemonicList);

            var outcomeMnemonic = mnemonicList.Any()
                ? $"   - Mnemonics: {mnemonicString}{Environment.NewLine}"
                : $"{Environment.NewLine}";

            AddDetailedUseCaseDescription(useCaseCreateDto, outcome, outcomeMnemonic);
            
        }

        return outcome.ToString();
    }

    public static string PrintUseCases(List<UseCaseGetDto> useCases)
    {
        var outcome = new StringBuilder();
        foreach (var useCaseCreateDto in useCases)
        {

            AddUseCaseDescription(useCaseCreateDto, outcome);
        }

        return outcome.ToString();
    }

    private static void AddDetailedUseCaseDescription(UseCaseGetDto dto, StringBuilder builder, string outcomeMnemonic)
    {
        var isActive = dto.IsActive ? "active" : "inactive";

        builder.AppendLine($"{dto.UseCaseIdentifier} is {isActive}. Details:");
        builder.AppendLine($"   - Name: {dto.Name}");
        builder.AppendLine($"   - Mitre Attack Id: {dto.MitreAttackId}");
        builder.AppendLine($"   - Kibana Rule Id: {dto.KibanaRuleId}");
        builder.AppendLine($"{outcomeMnemonic}");
    }

    private static void AddUseCaseDescription(UseCaseGetDto dto, StringBuilder builder)
    {
        var isActive = dto.IsActive ? "active" : "inactive";
        builder.AppendLine($"{dto.UseCaseIdentifier} is {isActive}. Name:");
        builder.AppendLine($"   - {dto.Name}");
    }

}