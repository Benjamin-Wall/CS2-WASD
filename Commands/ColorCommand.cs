using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace cs2_wasd.Commands
{
    public class ColorCommand : BaseSubCommand
    {
        public override string Argument => "color";
        public override string UsageExample => "color <name>";
        public override string Description => "Changes key highlight colors.";

        public override void Execute(PlayerSettings settings, CCSPlayerController player, CommandInfo command, cs2_wasd plugin)
        {
            if (command.ArgCount < 3)
            {
                player.PrintToChat(" \u0004[WASD]\x01 Usage: \x05!wasd color <color_name>\x01");
                return;
            }

            string chosenColor = command.GetArg(2).ToLower();


            if (plugin.AllowedColors.TryGetValue(chosenColor, out string? hexCode))
            {
                settings.OverlayColor = hexCode;
                player.PrintToChat($" \u0004[WASD]\x01 Your overlay color has been updated to \x05{chosenColor}\x01!");
                FileHelper.SaveDataToFile(plugin.SaveFilePath, plugin.PlayerSettings);
            }
            else
            {
                string validList = string.Join(", ", plugin.AllowedColors.Keys);
                player.PrintToChat($" \u0002[WASD]\x01 '\x05{chosenColor}\x01' is not a valid color name.");
                player.PrintToChat($" \u0004[WASD]\x01 Valid options: \x05{validList}\x01");
            }
        }
    }
}
