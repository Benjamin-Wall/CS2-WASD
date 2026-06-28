using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace cs2_wasd.Commands
{
    public class SpeedCommand : BaseSubCommand
    {
        public override string Argument => "speed";
        public override string UsageExample => "speed";
        public override string Description => "Toggles the 2D speedometer.";

        public override void Execute(PlayerSettings settings, CCSPlayerController player, CommandInfo command, cs2_wasd plugin)
        {
            settings.SpeedEnabled = !settings.SpeedEnabled;
            UtilitiesHelper.SendToggleMessage(player, "Speed Details", settings.SpeedEnabled);
            FileHelper.SaveDataToFile(plugin.SaveFilePath, plugin.PlayerSettings);
        }
    }
}
