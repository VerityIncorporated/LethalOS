using BepInEx;
using BepInEx.Unity.Mono;
using HarmonyLib;
using LethalOS.TerminalSystem;
using LethalOS.TerminalSystem.Bases;
using LethalOS.TerminalSystem.Modules.Example;
using UnityEngine;

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
        
        if (!Manager.Initialized()) return;
        
        //Menu Items

        var mainMenu = new Menu("LethalOS", "Made by Verity", "lethalos");
        var exampleCategory = new Category("Example", "Hello, this is an example category.", "example");
        exampleCategory.AddModule(new ExampleModule());
        exampleCategory.AddModule(new ExampleModuleTwo());
        
        mainMenu.AddCategory(exampleCategory);
        Manager.AddMenu(mainMenu);
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