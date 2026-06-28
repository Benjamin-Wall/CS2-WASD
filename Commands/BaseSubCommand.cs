using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;

namespace cs2_wasd;

public abstract class BaseSubCommand
{
    public abstract string Argument { get; }
    public abstract string UsageExample { get; }
    public abstract string Description { get; }

    // Every command will implement its own isolated execution logic here
    public abstract void Execute(PlayerSettings settings, CCSPlayerController player, CommandInfo command, cs2_wasd plugin);
}