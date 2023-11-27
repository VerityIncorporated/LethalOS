using BepInEx;
using BepInEx.Logging;
using LethalOS.API.Terminal;

namespace LethalOS.Example;

[BepInPlugin("verity.lethalos.example", "LethalOS API Example", "1.0.0")]
[BepInDependency("verity.lethalos.api", "1.0.0")]
public class Plugin : BaseUnityPlugin
{
    public static ManualLogSource LogSource { get; set; } = null!;
    
    private void Awake()
    {
        LogSource = Logger;
        LogSource.LogInfo("LethalOS API Example Loaded!");
    }

    private void Start()
    {
        var exampleMenu = new Menu("LethalOS", "Made by Verity <3", "lethalos"); //Create Menu
        var exampleCategory = new Category("ExampleCategory", "This is an example category.", "examplecategory"); //Create Category
        exampleCategory.AddModule(new Modules.Example()); //Add module to Category
        
        exampleMenu.AddCategory(exampleCategory); //Add Category to Menu
        
        Manager.AddMenu(exampleMenu);
    }
}