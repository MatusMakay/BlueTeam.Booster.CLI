using System.Text;
using Bc.CyberSec.Detection.Booster.Api.Client.Dto.SyslogNgConfigurator;

namespace Bc.CyberSec.Detection.Booster.CLI.Application.Application;

public static class OutcomeBuilder
{
    public static string GetActiveUseCases(List<UseCaseGetDto> useCaseGetDtos)
    {
        if (useCaseGetDtos.Count == 0)
        {
            return "No active use cases found";
        }

        var outcome = "";
        foreach (var useCaseGetDto in useCaseGetDtos)
        {
            outcome += @$"UC{useCaseGetDto.UseCaseIdentifier} is active {Environment.NewLine}";
        }

        return outcome;
    }

    public static string GetInactiveUseCases(List<UseCaseGetDto> useCaseGetDtos)
    {
        if (useCaseGetDtos.Count == 0)
        {
            return "No inactive use cases found";
        }

        var outcome = "";
        foreach (var useCaseGetDto in useCaseGetDtos)
        {
            outcome += @$"UC{useCaseGetDto.UseCaseIdentifier} is inactive {Environment.NewLine}";
        }
        return outcome;
    }

    public static string Error(string errorMessage)
    {
        return $@"
            Error occurred while processing the request. 
            Message: {errorMessage}  
        ";
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

            outcome.AppendLine($"UC{useCaseCreateDto.UseCaseIdentifier} was created. Details:");
            outcome.AppendLine($"   - Name: {useCaseCreateDto.Name}");
            outcome.AppendLine($"   - Mitre Attack Id: {useCaseCreateDto.MitreAttackId}");
            outcome.AppendLine($"   - Kibana Rule Id: {useCaseCreateDto.KibanaRuleId}");
            outcome.AppendLine($"{outcomeMnemonic}");
        }

        return outcome.ToString();
    }

    public static string GetAllUseCases(List<UseCaseGetDto> useCases)
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

            var isActive = useCaseCreateDto.IsActive ? "active" : "inactive";

            outcome.AppendLine($"UC{useCaseCreateDto.UseCaseIdentifier} is {isActive}. Details:");
            outcome.AppendLine($"   - Name: {useCaseCreateDto.Name}");
            outcome.AppendLine($"   - Mitre Attack Id: {useCaseCreateDto.MitreAttackId}");
            outcome.AppendLine($"   - Kibana Rule Id: {useCaseCreateDto.KibanaRuleId}");
            outcome.AppendLine($"{outcomeMnemonic}");
        }

        return outcome.ToString();
    }

    public static string UseCasesActivated(List<string> identifiers)
    {
        var outcome = "";
        foreach (var identifier in identifiers)
        {
            outcome += @$"UC{identifier} was activated. {Environment.NewLine}";
        }

        return outcome;
    }

    public static string UseCasesDeactivated(List<string> identifiers)
    {
        var outcome = "";
        foreach (var identifier in identifiers)
        {
            outcome += @$"UC{identifier} was deactivated. {Environment.NewLine}";
        }
        return outcome;
    }
}