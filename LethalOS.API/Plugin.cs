using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using LethalOS.API.Terminal;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace LethalOS.API;

[BepInPlugin("verity.lethalos.api", "LethalOS API", "1.0.4")]
internal class Plugin : BaseUnityPlugin
{
    private static readonly Harmony Harmony = new("lethalos");
    private static ManualLogSource LogSource { get; set; } = null!;
    private bool _terminalSetup;
    
    private void Awake()
    {
        LogSource = Logger;
        LogSource.LogInfo("LethalOS API Loaded!");
        
        SceneManager.sceneLoaded += SceneManagerOnsceneLoaded;
        
        var insertKeyAction = new InputAction(binding: "<Keyboard>/insert");
        insertKeyAction.performed += OnInsertKeyPressed;
        insertKeyAction.Enable();
    }

    private void OnInsertKeyPressed(InputAction.CallbackContext obj)
    {
        var terminal = FindObjectOfType<global::Terminal>();
        if (terminal is null) return;
            
        if (!terminal.terminalInUse)
        {
            terminal.BeginUsingTerminal();
            terminal.playerActions.Movement.Disable();
            HUDManager.Instance.ChangeControlTip(0, string.Empty, true);
        }
        else
        {
            terminal.QuitTerminal();
            terminal.playerActions.Movement.Enable();
        }
    }

    private void SceneManagerOnsceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name.ToLower() == "samplescenerelay" && !_terminalSetup)
        { 
            _terminalSetup = true;
            
            foreach (var menu in Manager.Menus)
            {
                LogSource.LogInfo($"Menu Create: {menu.MenuName}");
                Node.CreateMenuNode(menu);
            }
            
            var lethalOSMenu = new Menu("Menus", "Showing Menus", "menus", "Verity");

            foreach (var showMenusCategory in Manager.Menus.Select(menu => new Category($"{menu.MenuName} - [Made by {menu.ModAuthor}]", $"{menu.MenuDescription}", "placeholder")))
            {
                lethalOSMenu.AddCategory(showMenusCategory);
            }
            
            Node.CreateMenuNode(lethalOSMenu);
            
            var original = typeof(global::Terminal).GetMethod(nameof(global::Terminal.LoadNewNode));
            var postfix = typeof(TerminalPatch).GetMethod(nameof(TerminalPatch.CatchNodes));
            Harmony.Patch(original, postfix: new HarmonyMethod(postfix));
        }
        
        var terminal = FindObjectOfType<global::Terminal>();
        if (terminal is null) return;
        
        terminal.terminalNodes.specialNodes[13].displayText = ">MOONS\nTo see the list of moons the autopilot can route to.\n\n>STORE\nTo see the company store's selection of useful items.\n\n>BESTIARY\nTo see the list of wildlife on record.\n\n>STORAGE\nTo access objects placed into storage.\n\n>OTHER\nTo see the list of other commands.\n\n>MENUS\nTo see the list of menus created using LethalOS.\n\n";;
        
        terminal.terminalUIScreen.renderMode = RenderMode.ScreenSpaceOverlay;
        terminal.terminalUIScreen.scaleFactor = 2.35f;
    }

    private void Update()
    {
        foreach (var module in Manager.Modules.Where(@base => @base.Enabled))
        {
            module.OnUpdate();
        }
    }

    private void FixedUpdate()
    {
        foreach (var module in Manager.Modules.Where(@base => @base.Enabled))
        {
            module.OnFixedUpdate();
        }
    }
    
    private void OnGUI()
    {
        foreach (var module in Manager.Modules.Where(@base => @base.Enabled))
        {
            module.OnGui();
        }
    }
    
    [HarmonyPatch(typeof(global::Terminal), nameof(global::Terminal.LoadNewNode))]
    public class TerminalPatch
    {
        public static void CatchNodes(TerminalNode node)
        {
            if (node.name == "placeholder") return;
            
            var module = Manager.Modules.FirstOrDefault(module => string.Equals(module.Keyword, node.name, StringComparison.CurrentCultureIgnoreCase));
            module?.ToggleModule();
        }
    }
}