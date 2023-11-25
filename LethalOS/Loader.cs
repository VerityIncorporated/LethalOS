using BepInEx;
using BepInEx.Unity.Mono;
using HarmonyLib;
using LethalOS.TerminalSystem;
using LethalOS.TerminalSystem.Bases;
using LethalOS.TerminalSystem.Modules.Example;
using LethalOS.Utils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace LethalOS;

[RequireComponent(typeof(Loader))]
[BepInPlugin("lethal.os", "LethalOS", "1.0.0")]
public class Loader : BaseUnityPlugin
{
    private void Awake()
    {
        Log.LogSource = Logger;
        Log.LogSource.LogInfo("LethalOS Loaded!");
        
        var plugin = gameObject.AddComponent<Plugin>();
        plugin.hideFlags = HideFlags.HideAndDontSave;
        DontDestroyOnLoad(plugin);

        var lethalTerminal = gameObject.AddComponent<LethalTerminal>();
        lethalTerminal.hideFlags = HideFlags.HideAndDontSave;
        DontDestroyOnLoad(lethalTerminal);
    }
}

public class Plugin : MonoBehaviour
{
    public static readonly Harmony Harmony = new("lethalOS");

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
}