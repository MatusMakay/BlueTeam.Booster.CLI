using Bc.CyberSec.Detection.Booster.Api.Client.Api;

namespace Bc.CyberSec.Detection.Booster.CLI.Application.Application.Commands.Base;

public interface IExecuteCommand
{
    void Execute();
    object GetOutcome();
    int GetPriority();
}

public interface IExecuteCommand<out T> : IExecuteCommand
{
    new T GetOutcome();
}

public abstract class Command : IExecuteCommand
{
    protected readonly IDetectionBoosterApi Api;
    protected string Input = null!;
    protected int Priority;
    protected string Outcome = null!;

    protected Command(string input, IDetectionBoosterApi api)
    {
        Api = api;
        Input = input;
    }

    protected Command(IDetectionBoosterApi api)
    {
        Api = api;
    }

    public abstract void Execute();

    public abstract object GetOutcome();

    public int GetPriority()
    {
        return Priority;
    }
}