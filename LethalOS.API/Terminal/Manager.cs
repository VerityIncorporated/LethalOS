namespace LethalOS.API.Terminal;

public abstract class Manager
{
    public static readonly List<Menu> Menus = new();
    public static readonly List<ModuleBase> Modules = new();
    
    public static void AddMenu(Menu menu)
    {
        Menus.Add(menu);
        
        foreach (var module in menu.GetCategories.SelectMany(category => category.GetModules()))
        {
            Modules.Add(module);
        }
    }
}