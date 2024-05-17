using Bc.CyberSec.Detection.Booster.Api.Client.Dto.SyslogNgConfigurator;
using ClosedXML.Excel;

namespace Bc.CyberSec.Detection.Booster.CLI.Application.Application.UseCase;

public static class FileWriter
{
    private const int NameColumn = 1;
    private const int MitreAttackIdColumn = 2;
    private const int MnemonicsColumn = 3;
    private const int RuleIdColumn = 4;
    public static readonly string DefaultFilePath = Path.Combine(Path.GetTempPath(), "UseCase.xlsx");

    public static string GenerateStateToCsv(List<UseCaseGetDto> data, string? filePath = null)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            filePath = DefaultFilePath.Replace(".xlsx", ".csv");
        }

        using var writer = new StreamWriter(filePath);
        writer.WriteLine("Meno;MitreAttackId;Mnemoniky;Rule Id");

        foreach (var useCase in data)
        {
            if (useCase.Mnemonics != null)
            {
                writer.WriteLine($"{useCase.Name};;{useCase.MitreAttackId};{string.Join(" ", useCase.Mnemonics)};{useCase.KibanaRuleId}");
            }
            else
            {
                writer.WriteLine($"{useCase.Name};;{useCase.MitreAttackId};;{useCase.KibanaRuleId}");
            }
        }

        return filePath;
    }

    public static string GenerateStateToXlsx(List<UseCaseGetDto> data, string? filePath = null)
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
            if (useCase.Mnemonics != null)
                worksheet.Cell(i + 2, MnemonicsColumn).Value = string.Join(",", useCase.Mnemonics);
            worksheet.Cell(i + 2, RuleIdColumn).Value = useCase.KibanaRuleId.ToString();
            worksheet.Row(i + 2).Style.Fill.BackgroundColor = useCase.IsActive ? XLColor.Green : XLColor.Red;
        }

        workbook.SaveAs(filePath);
        return filePath;
    }

    public static List<UseCaseCreateDto> InitializeFromCsv(string? filePath)
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

    public static List<UseCaseCreateDto> InitializeFromXlsx(string? filePath)
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