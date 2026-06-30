using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Entities;
using CounterStrikeSharp.API.Modules.Utils;

namespace cs2_wasd;

public static class UtilitiesHelper
{
    #region "Game Methods"
    public static void SendToggleMessage(CCSPlayerController player, string featureName, bool isEnabled)
    {
        char color = isEnabled ? ChatColors.Green : ChatColors.DarkRed;
        string status = isEnabled ? "enabled!" : "disabled.";
        player.PrintToChat($" {color}[WASD]{ChatColors.White} {featureName} {status}");
    }

    public static void PrintHelpMenu(CCSPlayerController player, List<BaseSubCommand> subCommands)
    {
        player.PrintToChat($" {ChatColors.Green}[WASD Overlay - Help Menu]");
        player.PrintToChat($" {ChatColors.Olive}!wasd{ChatColors.White} - Toggles the core WASD key overlay on/off.");

        foreach (var cmd in subCommands)
        {
            player.PrintToChat($" {ChatColors.Olive}!wasd {cmd.UsageExample}{ChatColors.White} - {cmd.Description}");
        }
    }

    #endregion

    #region "Mathematical Methods"
    public static string GetRainbowHexColor(PlayerSettings settings)
    {
        settings.CurrentRainbowHue = (settings.CurrentRainbowHue + 1.5) % 360.0;
        double h = settings.CurrentRainbowHue, s = 1.0, v = 1.0;
        double c = v * s;
        double x = c * (1.0 - Math.Abs((h / 60.0) % 2.0 - 1.0));
        double m = v - c;
        double r = 0, g = 0, b = 0;

        if (h >= 0 && h < 60) { r = c; g = x; b = 0; }
        else if (h >= 60 && h < 120) { r = x; g = c; b = 0; }
        else if (h >= 120 && h < 180) { r = 0; g = c; b = x; }
        else if (h >= 180 && h < 240) { r = 0; g = x; b = c; }
        else if (h >= 240 && h < 300) { r = x; g = 0; b = c; }
        else if (h >= 300 && h < 360) { r = c; g = 0; b = x; }

        return $"#{(int)((r + m) * 255):X2}{(int)((g + m) * 255):X2}{(int)((b + m) * 255):X2}";
    }

    public static string GetSpeedColor(double speed)
    {
        double clampedSpeed = Math.Clamp(speed, 0, 250);
        int r = (int)Math.Round((1.0 - (clampedSpeed / 250.0)) * 255.0);
        int g = (int)Math.Round((clampedSpeed / 250.0) * 255.0);
        return $"#{r:X2}{g:X2}00";
    }
    #endregion
}