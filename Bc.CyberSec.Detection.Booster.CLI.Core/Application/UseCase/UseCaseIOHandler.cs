using System;
using Bc.CyberSec.Detection.Booster.Api.Client.Dto.SyslogNgConfigurator;
using ClosedXML.Excel;

namespace Bc.CyberSec.Detection.Booster.CLI.Application.Application.UseCase;

public static class UseCaseIOHandler
{
    private const int NameColumn = 1;
    private const int MitreAttackIdColumn = 2;
    private const int MnemonicsColumn = 3;
    private const int RuleIdColumn = 4;

    public static readonly string DefaultFilePath = Path.Combine(Path.GetTempPath(), "UseCase.xlsx");
    public static List<UseCaseCreateDto> Initialize(string? filePath)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            filePath = DefaultFilePath;
        }

        var useCases = new List<UseCaseCreateDto>();

        using (var workbook = new XLWorkbook(filePath))
        {
            var worksheet = workbook.Worksheet(1);
            var rows = worksheet.RangeUsed().RowsUsed().Skip(1);
            var id = 1;

            foreach (var row in rows)
            {
                var uc = new UseCaseCreateDto
                {
                    UseCaseIdentifier = $"UC{id}",
                    Name = row.Cell(NameColumn).GetValue<string>(),
                    MitreAttackId = row.Cell(MitreAttackIdColumn).GetValue<string>(),
                    Mnemonics = row.Cell(MnemonicsColumn).GetValue<string>().Split(new[] { ' ', ',', '\t' }, StringSplitOptions.RemoveEmptyEntries).ToList(),
                    KibanaRuleId = row.Cell(RuleIdColumn).GetValue<string>(),
                };

                id++;
                useCases.Add(uc);
            }
        }

        return useCases;
    }

    public static string GenerateStateToFile(List<UseCaseGetDto> data, string? filePath = null)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            filePath = DefaultFilePath;
        }

        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Sheet1");
        worksheet.Cell(1, NameColumn).Value = "Meno";
        worksheet.Cell(1, MitreAttackIdColumn).Value = "MitreAttackId";
        worksheet.Cell(1, MnemonicsColumn).Value = "Mnemoniky";
        worksheet.Cell(1, RuleIdColumn).Value = "Rule Id";

        for (int i = 0; i < data.Count; i++)
        {
            var useCase = data[i];

            worksheet.Cell(i + 2, NameColumn).Value = useCase.Name;
            worksheet.Cell(i + 2, MitreAttackIdColumn).Value = useCase.MitreAttackId;
            if(useCase.Mnemonics != null)
                worksheet.Cell(i + 2, MnemonicsColumn).Value = string.Join(",", useCase.Mnemonics); 
            worksheet.Cell(i + 2, RuleIdColumn).Value = useCase.KibanaRuleId.ToString();
            worksheet.Row(i + 2).Style.Fill.BackgroundColor = useCase.IsActive ? XLColor.Green : XLColor.Red;
        }

        workbook.SaveAs(filePath);

        return filePath;
    }
}