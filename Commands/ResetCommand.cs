using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace cs2_wasd.Commands
{
    public class ResetCommand : BaseSubCommand
    {
        public override string Argument => "reset";
        public override string UsageExample => "reset";
        public override string Description => "Resets the overlay to the default settings.";

        public override void Execute(PlayerSettings settings, CCSPlayerController player, CommandInfo command, cs2_wasd plugin)
        {
            settings.OverlayEnabled = true;
            settings.ShiftEnabled = false;
            settings.SpeedEnabled = false;
            settings.MouseEnabled = false;
            settings.OverlayColor = "#FFFF00";
            FileHelper.SaveDataToFile(plugin.SaveFilePath, plugin.PlayerSettings);
        }
    }
}
