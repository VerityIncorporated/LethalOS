# LethalOS API Example

This example demonstrates how to use the LethalOS API to create menus, categories, and modules.

## Installation

Make sure you have BepInEx and LethalOS API installed in your game.
Add the LethalOS.API.dll as a dependency in your project, and make sure to add this line to your project so that BepInEx loads it correctly. LethalOS.API.dll needs to be in the plugins folder alongside your plugin.

PS: Pressing Insert will allow you to use the terminal from anywhere in the game.

```
[BepInDependency("verity.lethalos.api")]
```

## Usage Example

### Plugin Setup
All menus should be created on start or awake, before you load into a game. Once you load into a game the LethalOS API will add all of the created menus to the terminal, which is only done once.

```csharp
using BepInEx;
using BepInEx.Logging;
using LethalOS.API.Terminal;

namespace LethalOS.Example
{
    [BepInPlugin("verity.lethalos.example", "LethalOS API Example", "1.0.0")]
    [BepInDependency("verity.lethalos.api")]
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
            var exampleMenu = new Menu("LethalOS", "Example Menu using LethalOS", "lethalos", "Verity"); // Create Menu, Name, Description, Keyword, Author
            var exampleCategory = new Category("ExampleCategory", "This is an example category.", "examplecategory"); // Create Category
            exampleCategory.AddModule(new Modules.Example()); // Add module to Category

            exampleMenu.AddCategory(exampleCategory); // Add Category to Menu

            exampleMenu.Finished(); // Must do this once the menu categories & modules have been added!
        }
    }
}
```

### Example Module

```csharp
using LethalOS.API;

namespace LethalOS.Example.Modules;

public class Example : ModuleBase //Inherits off of LethalOS.API.ModuleBase
{
    public Example() : base("ExampleModule", "This is an example module!", "examplemodule") {}

    protected override void OnEnabled()
    {
        Plugin.LogSource.LogInfo("Enabled");
    }

    protected override void OnDisabled()
    {
        Plugin.LogSource.LogInfo("Disabled");
    }

    public override void OnUpdate()
    {
        //Do action on update while enabled
    }

    public override void OnFixedUpdate()
    {
        //Do action on fixed update while enabled
    }
    
    public override void OnGui()
    {
        //Do action on gui while enabled
    }
}
```
