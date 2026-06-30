using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Utils;

namespace cs2_wasd;

public class HelpCommand : BaseSubCommand
{
    public override string Argument => "help";
    public override string UsageExample => "help";
    public override string Description => "Shows the help menu.";

    public override void Execute(PlayerSettings settings, CCSPlayerController player, CommandInfo command, cs2_wasd plugin)
    {
        player.PrintToChat($" {ChatColors.Green}[WASD Overlay - Help Menu]");
        player.PrintToChat($" {ChatColors.Olive}!wasd{ChatColors.White} - Toggles the core WASD key overlay on/off.");

        foreach (var cmd in plugin.SubCommands)
        {
            player.PrintToChat($" {ChatColors.Olive}!wasd {cmd.UsageExample}{ChatColors.White} - {cmd.Description}");
        }
    }
}