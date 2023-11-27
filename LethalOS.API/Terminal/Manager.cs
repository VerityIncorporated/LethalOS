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
    public static void AddMenu(Menu menu)
    {
        Menus.Add(menu);

        // Add modules from the added menu to the global modules list
        foreach (var module in menu.GetCategories.SelectMany(category => category.GetModules()))
        {
            Modules.Add(module);
        }
    }
}