using UnityEngine;
using Object = UnityEngine.Object;

namespace LethalOS.API.Terminal;

/// <summary>
/// Represents the manager for menus and modules in the terminal.
/// </summary>
public abstract class Manager
{
    internal static readonly List<Menu> Menus = new();
    
    internal static readonly List<ModuleBase> Modules = new();

    /// <summary>
    /// Adds a menu to the terminal manager.
    /// </summary>
    /// <param name="menu">The menu to be added.</param>
    internal static void AddMenu(Menu menu)
    {
        Menus.Add(menu);

        // Add modules from the added menu to the global modules list
        foreach (var module in menu.GetCategories.SelectMany(category => category.GetModules()))
        {
            Modules.Add(module);
        }
    }
    
    /// <summary>
    /// Changes the text in the terminal.
    /// </summary>
    /// <param name="newText">The text to be shown in the terminal.</param>
    /// <param name="clearText">Determines whether to keep the previous text in the terminal.</param>
    internal static void ChangeScreenText(string newText, bool clearText = false)
    {
        var terminal = Object.FindObjectOfType<global::Terminal>();
        if (terminal is null) return;
        
        var moduleNode = ScriptableObject.CreateInstance<TerminalNode>();
        moduleNode.displayText = newText + "\n\n";
        moduleNode.clearPreviousText = clearText;
        moduleNode.name = "placeholder";
        
        terminal.LoadNewNode(moduleNode);
    }
}