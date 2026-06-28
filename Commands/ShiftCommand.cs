using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace cs2_wasd.Commands
{
    public class ShiftCommand : BaseSubCommand
    {
        public override string Argument => "shift";
        public override string UsageExample => "shift";
        public override string Description => "Toggles the SHIFT key logging.";

        public override void Execute(PlayerSettings settings, CCSPlayerController player, CommandInfo command, cs2_wasd plugin)
        {
            settings.ShiftEnabled = !settings.ShiftEnabled;
            UtilitiesHelper.SendToggleMessage(player, "Shift Details", settings.ShiftEnabled);
            FileHelper.SaveDataToFile(plugin.SaveFilePath, plugin.PlayerSettings);
        }
    }
}
