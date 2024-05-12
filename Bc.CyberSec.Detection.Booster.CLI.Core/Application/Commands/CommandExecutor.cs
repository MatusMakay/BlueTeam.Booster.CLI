using Bc.CyberSec.Detection.Booster.CLI.Application.Application.Commands.Base;

namespace Bc.CyberSec.Detection.Booster.CLI.Application.Application.Commands;

public class CommandExecutor
{
    private List<IExecuteCommand> Commands { get; set; }

    public CommandExecutor(List<IExecuteCommand> commands)
    {
        Commands = commands;
    }

    public void Add(IExecuteCommand command) => Commands.Add(command);

    public void Execute()
    {
        Commands.Sort((c1, c2) => c1.GetPriority().CompareTo(c2.GetPriority()));

        foreach (var command in Commands)
        {
            command.Execute();
            Console.WriteLine(command.GetOutcome());
        }
    }

    public void Sort()
    {
        throw new System.NotImplementedException();
    }
}