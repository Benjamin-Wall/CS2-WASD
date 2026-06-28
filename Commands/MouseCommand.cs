using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace cs2_wasd.Commands
{
    public class MouseCommand : BaseSubCommand
    {
        public override string Argument => "mouse";
        public override string UsageExample => "mouse";
        public override string Description => "Toggles M1/M2 click logging.";

        public override void Execute(PlayerSettings settings, CCSPlayerController player, CommandInfo command, cs2_wasd plugin)
        {
            settings.MouseEnabled = !settings.MouseEnabled;
            UtilitiesHelper.SendToggleMessage(player, "Mouse Details", settings.MouseEnabled);
            FileHelper.SaveDataToFile(plugin.SaveFilePath, plugin.PlayerSettings);
        }
    }
}
