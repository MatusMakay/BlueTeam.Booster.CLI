namespace Bc.CyberSec.Detection.Booster.CLI.Options;

public static class OptionsHandler
{

    public static bool InitOption(Options options)
    {
        return !string.IsNullOrWhiteSpace(options.InputFilePath);
    }

    public static bool ActivateOption(string[] args)
    {
        return args.Contains("-a") || args.Contains("--activate-uc");
    }

    public static bool ActivateOption(Options options)
    {
        return !string.IsNullOrWhiteSpace(options.ActivateUseCases);
    }
    public static bool DeactivateOption(Options options)
    {
        return !string.IsNullOrWhiteSpace(options.DeactivateUseCases);
    }


    public static bool DeactivateOption(string[] args)
    {
        return args.Contains("-d") || args.Contains("--deactivate-uc");
    }

    public static bool RangeOption(Options options)
    {
        return !string.IsNullOrWhiteSpace(options.RangeOfUseCases);
    }

    public static bool GetActiveOption(string[] args)
    {
        return args.Contains("-A") || args.Contains("--get-active");
    }

    public static bool GetInactiveOption(string[] args)
    {
        return args.Contains("-D") || args.Contains("--get-inactive");
    }

    public static bool DetailedOutput(string[] args)   {
        return args.Contains("-t") || args.Contains("--detailed");
    }

    public static bool GetAllUseCases(string[] args)
    {
        return args.Contains("-g") || args.Contains("--get-use-cases");
    }
}