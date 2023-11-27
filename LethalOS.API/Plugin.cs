using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using LethalOS.API.Terminal;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace LethalOS.API;

[BepInPlugin("verity.lethalos.api", "LethalOS API", "1.0.0.0")]
internal class Plugin : BaseUnityPlugin
{
    private static readonly Harmony Harmony = new("lethalos");
    public static ManualLogSource LogSource { get; set; } = null!;
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
                LogSource.LogInfo($"Menu Created: {menu.MenuName}");
                Node.CreateMenuNode(menu);
            }
        
            var original = typeof(global::Terminal).GetMethod(nameof(global::Terminal.LoadNewNode));
            var prefix = typeof(TerminalPatch).GetMethod(nameof(TerminalPatch.CatchNodes));
            Harmony.Patch(original, new HarmonyMethod(prefix));
        }
        
        var terminal = FindObjectOfType<global::Terminal>();
        if (terminal is null) return;
            
        terminal.terminalUIScreen.renderMode = RenderMode.ScreenSpaceOverlay;
        terminal.terminalUIScreen.scaleFactor += 1.35f;
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
            var module = Manager.Modules.FirstOrDefault(module => module.Keyword.ToLower() == node.name);
            module?.ToggleModule();
        }
    }
}