using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Utils;
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
                player.PrintToChat($" {ChatColors.Green}[WASD]{ChatColors.White} Usage: {ChatColors.Olive}!wasd color <color_name>");
                return;
            }

            string chosenColor = command.GetArg(2).ToLower();


            if (plugin.AllowedColors.TryGetValue(chosenColor, out string? hexCode))
            {
                settings.OverlayColor = hexCode;
                player.PrintToChat($" {ChatColors.Green}[WASD]{ChatColors.White} Your overlay color has been updated to {ChatColors.Olive}{chosenColor}{ChatColors.White}!");
                FileHelper.SaveDataToFile(plugin.SaveFilePath, plugin.PlayerSettings);
            }
            else
            {
                string validList = string.Join(", ", plugin.AllowedColors.Keys);
                player.PrintToChat($" {ChatColors.DarkRed}[WASD]{ChatColors.White} '{ChatColors.Olive}{chosenColor}{ChatColors.White}' is not a valid color name.");
                player.PrintToChat($" {ChatColors.Green}[WASD]{ChatColors.White} Valid options: {ChatColors.Olive}{validList}");
            }
        }
    }
}
