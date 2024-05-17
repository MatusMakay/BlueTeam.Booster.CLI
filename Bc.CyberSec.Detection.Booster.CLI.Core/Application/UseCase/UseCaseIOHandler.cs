using Bc.CyberSec.Detection.Booster.Api.Client.Dto.SyslogNgConfigurator;

namespace Bc.CyberSec.Detection.Booster.CLI.Application.Application.UseCase;

public static class UseCaseIoHandler
{

    // TODO: When input for activation is 10 it activated uc 1 and uc 2 fix it
    public static List<UseCaseCreateDto> Initialize(string? filePath)
    {
        if (filePath == null || string.IsNullOrEmpty(filePath) || string.IsNullOrWhiteSpace(filePath))
        {
            throw new ApplicationException("File path is null");
        }
        if (filePath.EndsWith(".csv"))
        {
            return FileWriter.InitializeFromCsv(filePath);
        }
        if (filePath.EndsWith(".xlsx"))
        {
            return FileWriter.InitializeFromXlsx(filePath);
        }
        throw new ApplicationException("Unsupported file format");
    }

    public static string GenerateStateToFile(List<UseCaseGetDto> data, string? path)
    {
        if (path == null || string.IsNullOrEmpty(path) || string.IsNullOrWhiteSpace(path))
        {
            Console.WriteLine("Dou you want generate state to CSV or XLSX?");
            Console.WriteLine("1. CSV");
            Console.WriteLine("2. XLSX");
            var input = Console.ReadLine();
            
            if (input == "1")
            {
                return FileWriter.GenerateStateToCsv(data);
            }
            if (input == "2")
            {
                return FileWriter.GenerateStateToXlsx(data);
            }
            throw new ApplicationException("Unsupported file format");
        }
      
        if (path.EndsWith(".csv"))
        {
            return FileWriter.GenerateStateToCsv(data, path);
        }

        if (path.EndsWith(".xlsx"))
        {
            return FileWriter.GenerateStateToXlsx(data, path);
        }
     
        throw new ApplicationException("Unsupported file format");
    }
}