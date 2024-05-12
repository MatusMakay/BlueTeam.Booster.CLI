using CommandLine;

namespace Bc.CyberSec.Detection.Booster.CLI.Options;

public class Options
{
  
    [Option('i', "init", Required = false, HelpText = "Input file path.")]
    public string InputFilePath { get; set; }

    [Option('t', "detailed", Required = false, HelpText = "Detailed output.")]
    public string DetailOutput { get; set; }

    [Option('a', "activate-uc", Required = false, HelpText = "Activate defined use cases")]
    public string ActivateUseCases { get; set; }

    [Option('d', "deactivate-uc", Required = false, HelpText = "Deactivate defined use cases")]
    public string DeactivateUseCases { get; set; }

    [Option('r', "range-uc", Required = false, HelpText = "Generate range of use cases [1..5,6..7] to activate/deactivate")]
    public string RangeOfUseCases { get; set; }

    [Option('A', "get-active", Required = false, HelpText = "Get active use cases")]
    public string GetActive { get; set; }

    [Option('D', "get-inactive", Required = false, HelpText = "Get inactive use cases")]
    public string GetInactive { get; set; }
    
    [Option('g', "get-use-cases", Required = false, HelpText = "Get all use cases")]
    public string GetUseCases { get; set; }

    [Option('v', "validate-init", Required = false, HelpText = "Validate init file")]
    public string ValidateInitFile { get; set; }

    [Option('o', "output", Required = false, HelpText = "Generate state to file. Provide path or default %temp% will be used. ")]
    public string OutputFilePath { get; set; }
}