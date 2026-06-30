using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Utils;
using cs2_wasd.Commands;

namespace cs2_wasd;

public class cs2_wasd : BasePlugin
{
    // Plugin Configuration (Needed for CounterStrikeSharp)
    public override string ModuleName => "WASD Overlay Plugin";
    public override string ModuleVersion => "1.0.3";
    public override string ModuleAuthor => "BenjaminWall";
    public override string ModuleDescription => "Displays a real-time WASD overlay with additional settings.";

    // Plugin Initialisation
    public CCSGameRules? GameRules;
    public bool GameRulesInitialized;

    public Dictionary<ulong, PlayerSettings> PlayerSettings = new();
    public readonly List<BaseSubCommand> SubCommands = new();
    public string SaveFilePath = "";
    public readonly Dictionary<string, string> AllowedColors = new(StringComparer.OrdinalIgnoreCase)
    {
        { "rainbow",    "RAINBOW_MODE" },
        { "white",      "#FFFFFF" },
        { "lightblue",  "#ADD8E6" },
        { "blue",       "#0000FF" },
        { "purple",     "#800080" },
        { "magenta",    "#FF00FF" },
        { "red",        "#FF0000" },
        { "orange",     "#FFA500" },
        { "yellow",     "#FFFF00" },
        { "green",      "#00FF00" },
        { "aqua",       "#00FFFF" },
        { "pink",       "#FF69B4" }
    };

    public override void Load(bool hotReload)
    {
        // Get the save file path that we will need to load/save the player settings
        SaveFilePath = FileHelper.GetSaveFilePath(ModuleDirectory);

        // Load the player settings
        PlayerSettings = FileHelper.LoadDataFromFile(SaveFilePath);

        // Register each of the sub commands (E.G !wasd [help], !wasd [speed])
        SubCommands.Add(new HelpCommand());
        SubCommands.Add(new SpeedCommand());
        SubCommands.Add(new MouseCommand());
        SubCommands.Add(new ShiftCommand());
        SubCommands.Add(new ResetCommand());
        SubCommands.Add(new ColorCommand());

        RegisterListener<Listeners.OnTick>(OnTick);
        RegisterListener<Listeners.OnMapStart>(OnMapStartHandler);

        if (hotReload)
        {
            InitializeGameRules();
        }
    }

    private void OnMapStartHandler(string mapName)
    {
        GameRules = null;
       GameRulesInitialized = false;
    }

    private void InitializeGameRules()
    {
        if (GameRulesInitialized) return;

        var gameRulesProxy = Utilities.FindAllEntitiesByDesignerName<CCSGameRulesProxy>("cs_gamerules").FirstOrDefault();
        GameRules = gameRulesProxy?.GameRules;
        GameRulesInitialized = GameRules != null;
    }


    [ConsoleCommand("css_wasd", "Toggles the WASD key overlay or its sub-features.")]
    public void OnWasdCommand(CCSPlayerController? player, CommandInfo command)
    {
        // If the player is null, not valid or is a bot then we want to ignore and not continue any further
        if (player == null || !player.IsValid || player.IsBot) return;

        // If the player doesnt have any settings already then we want to create a new default set of settings
        if (!PlayerSettings.TryGetValue(player.SteamID, out var settings))
        {
            settings = new PlayerSettings();
            PlayerSettings[player.SteamID] = settings;
        }

        // Get any additional arguments (!wasd = 0, color = 1, green = 2)
        string subCommand = command.ArgCount > 1 ? command.GetArg(1).ToLower() : "";

        // If nothing comes after !wasd then the player is trying to toggle the overlay
        if (subCommand == "")
        {
            settings.OverlayEnabled = !settings.OverlayEnabled;
            UtilitiesHelper.SendToggleMessage(player, "Overlay", settings.OverlayEnabled);
            FileHelper.SaveDataToFile(SaveFilePath, PlayerSettings);
            return;
        }

        // If we do have additional args then we want to find the command and execute
        var matchingModule = SubCommands.Find(c => c.Argument == subCommand);
        if (matchingModule != null)
        {
            matchingModule.Execute(settings, player, command, this);
        }
        else
        {
            // Command was not found so give feedback in the chat
            player.PrintToChat($" {ChatColors.Green}[WASD]{ChatColors.White} Command argument not recognized!");
            player.PrintToChat($" {ChatColors.Green}[WASD]{ChatColors.White} Type {ChatColors.Olive}!wasd help{ChatColors.White} to see all available options.");
        }
    }

    private void OnTick()
    {
        //Fix: stops the overlay "flickering" every game second
        if (!GameRulesInitialized)
        {
            InitializeGameRules();
            return;
        }

        if (GameRules != null)
        {
            GameRules.GameRestart = GameRules.RestartRoundTime < Server.CurrentTime;
        }

        DrawOverlay(this);
    }

    private void DrawOverlay(cs2_wasd plugin)
    {
        foreach (var player in Utilities.GetPlayers())
        {
            // If the player is null, not valid or is a bot then we want to skip and not continue any further
            if (player == null || !player.IsValid || player.IsBot || !player.PlayerPawn.IsValid)
                continue;

            // If the player doesnt have any settings or the overlay is disabled then skip and not continue any further
            if (!plugin.PlayerSettings.TryGetValue(player.SteamID, out var settings) || !settings.OverlayEnabled)
                continue;

            // If the player is not physically in the world then skip and not continue any further
            var movementServices = player.PlayerPawn.Value!.MovementServices;
            if (movementServices == null || movementServices.Buttons == null)
                continue;

            // Get the button states of the current player
            ulong buttons = movementServices.Buttons.ButtonStates[0];

            // If the overlay color is "RAINBOW_MODE" (a special color mode) then we want to handle that with utilities helper otherwise just use the overlay color the player defined
            string activeColor = settings.OverlayColor == "RAINBOW_MODE" ? UtilitiesHelper.GetRainbowHexColor(settings) : settings.OverlayColor;

            // Sets the colors of each button, button will either be "activeColor" if they are pressing that button or it will be greyed out "#55555"
            string colorC = (buttons & (ulong)PlayerButtons.Duck) != 0 ? activeColor : "#555555";
            string colorW = (buttons & (ulong)PlayerButtons.Forward) != 0 ? activeColor : "#555555";
            string colorJ = (buttons & (ulong)PlayerButtons.Jump) != 0 ? activeColor : "#555555";
            string colorA = (buttons & (ulong)PlayerButtons.Moveleft) != 0 ? activeColor : "#555555";
            string colorS = (buttons & (ulong)PlayerButtons.Back) != 0 ? activeColor : "#555555";
            string colorD = (buttons & (ulong)PlayerButtons.Moveright) != 0 ? activeColor : "#555555";
            string colorShift = (buttons & (ulong)PlayerButtons.Speed) != 0 ? activeColor : "#555555";
            string colorLClick = (buttons & (ulong)PlayerButtons.Attack) != 0 ? activeColor : "#555555";
            string colorRClick = (buttons & (ulong)PlayerButtons.Attack2) != 0 ? activeColor : "#555555";

            // Build up the wasd overlay, we have multiple if statements that change the overlay based on the different settings the player has enabled/disabled
            string htmlDisplay = $"</pre>" +
                                 $"<font color='{colorC}'>C</font> <font color='{colorW}'>W</font> <font color='{colorJ}'>J</font><br>" +
                                 $"<font color='{colorA}'>A</font> <font color='{colorS}'>S</font> <font color='{colorD}'>D</font>";

            if (settings.ShiftEnabled) htmlDisplay += $"<br><font color='{colorShift}'>SHIFT</font>";
            if (settings.MouseEnabled) htmlDisplay += $"<br><font color='{colorLClick}'>M1</font> <font color='{colorRClick}'>M2</font>";
            if (settings.SpeedEnabled)
            {
                double currentSpeed = Math.Round(player.PlayerPawn.Value.AbsVelocity.Length2D());
                string dynamicSpeedColor = UtilitiesHelper.GetSpeedColor(currentSpeed);
                htmlDisplay += $"<br>Speed: <font color='{dynamicSpeedColor}'>{currentSpeed}</font>";
            }

            htmlDisplay += "</pre>";

            // Send the overlay we built up to the current player
            player.PrintToCenterHtml(htmlDisplay);
        }
    }
}