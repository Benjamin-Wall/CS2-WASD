using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Commands;

namespace cs2_wasd;

public class PlayerSettings
{
    public bool OverlayEnabled { get; set; } = false;
    public bool ShiftEnabled { get; set; } = false;
    public bool SpeedEnabled { get; set; } = false;
    public bool MouseEnabled { get; set; } = false;
    public string OverlayColor { get; set; } = "#FFFF00";

    [System.Text.Json.Serialization.JsonIgnore]
    public double CurrentRainbowHue { get; set; } = 0.0;
}