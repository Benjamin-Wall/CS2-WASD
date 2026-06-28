using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;

namespace cs2_wasd;

public class HelpCommand : BaseSubCommand
{
    public override string Argument => "help";
    public override string UsageExample => "help";
    public override string Description => "Shows the help menu.";

    public override void Execute(PlayerSettings settings, CCSPlayerController player, CommandInfo command, cs2_wasd plugin)
    {
        player.PrintToChat(" \x04[WASD Overlay - Help Menu]\x01");
        player.PrintToChat(" \x05!wasd\x01 - Toggles the core WASD key overlay on/off.");

        foreach (var cmd in plugin.SubCommands)
        {
            player.PrintToChat($" \x05!wasd {cmd.UsageExample}\x01 - {cmd.Description}");
        }
    }
}