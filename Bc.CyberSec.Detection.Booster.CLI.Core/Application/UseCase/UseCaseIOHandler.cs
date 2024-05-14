using System;
using System.Globalization;
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
        if (filePath == null || string.IsNullOrEmpty(filePath) || string.IsNullOrWhiteSpace(filePath))
        {
            throw new ApplicationException("File path is null");
        }
        if (filePath.EndsWith(".csv"))
        {
            return InitializeFromCsv(filePath);
        }
        else if (filePath.EndsWith(".xlsx"))
        {
            return InitializeFromXlsx(filePath);
        }
        else
        {
            throw new ApplicationException("Unsupported file format");
        }
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

    private static List<UseCaseCreateDto> InitializeFromCsv(string? filePath)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            filePath = DefaultFilePath.Replace(".xlsx", ".csv");
        }

        var useCases = new List<UseCaseCreateDto>();
        var lines = File.ReadAllLines(filePath).Skip(1);
        var id = 1;

        foreach (var line in lines)
        {
            var cells = line.Split(';');

            if (cells.Length < 4)
            {
                continue;
            }

            var mnemonicsValue = cells[2];
            var mnemonicsList = string.IsNullOrWhiteSpace(mnemonicsValue) ? new List<string>() : mnemonicsValue.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            var uc = new UseCaseCreateDto
            {
                UseCaseIdentifier = $"UC{id++}",
                Name = cells[0],
                MitreAttackId = cells[1],
                Mnemonics = mnemonicsList,
                KibanaRuleId = cells.Length > 3 ? cells[3] : null
            };

            useCases.Add(uc);
        }

        return useCases;
    }

    private static List<UseCaseCreateDto> InitializeFromXlsx(string? filePath)
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
}