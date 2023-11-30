## Creating Menus, Categories, and Modules in LethalOS.API

## Introduction

LethalOS.API provides a flexible system for managing modules within Lethal Company's terminal environment. This guide demonstrates how to create menus, categories, and modules using the API.

Creating a Menu
-
```csharp
// Creating a menu instance
Menu mainMenu = new Menu("Main Menu", "Description of the main menu", "main", "John Doe");

// Finalizing and adding the menu to the terminal, do this after adding categories and their respective modules
mainMenu.Finished();
```

Adding Categories to the Menu
-
```csharp
// Creating a category instance
Category moonCategory = new Category("Moon", "Moon-related modules", "moon");

// Adding the category to the main menu, do this after adding the modules to the category
mainMenu.AddCategory(moonCategory);
```

Adding Modules to Categories
-
Creating a Custom Module Class

```csharp
using LethalOS.API;

public class CustomModule : ModuleBase
{
    public CustomModule(string displayName, string displayDescription, string keyword, bool requiresHost, bool toggled)
        : base(displayName, displayDescription, keyword, requiresHost, toggled)
    {
        // Custom initialization code
    }

    public override void OnAdded()
    {
        // Custom Logic when the module is first added to the terminal
    }

    public override void OnEnabled()
    {
        // Custom logic when the module is enabled

        //Change the current text on the terminal, does not effect the existing terminal node
        ChangeScreenText($"There are currently {enemyCount} enemies alive! {outside} of which are outside!", true);

        //Updates the current node with a new one, this will effect the existing terminal node
        UpdateNode(newNode: );
    }

    // Other overrideable lifecycle methods can be implemented here
}
```

Adding Custom Modules to Categories
```csharp
// Creating a custom module instance
CustomModule moonInfo = new CustomModule("MoonInfo", "Display moon information", "MoonInfo", false, false);

// Adding the module to the 'moon' category
moonCategory.AddModule(moonInfo);
```

Retrieving Modules from Categories
-
```csharp
// Retrieving a module by its name from the 'moon' category
ModuleBase retrievedModule = moonCategory.GetModuleByName("MoonInfo");
// Retrieving a module by its class from the 'moon' category
CustomModule moonModule = moonCategory.GetModule<CustomModule>();
```

Example Code
-
```csharp
// Creating a menu instance
Menu mainMenu = new Menu("Main Menu", "Description of the main menu", "main", "John Doe");

// Creating a category instance
Category moonCategory = new Category("Moon", "Moon-related modules", "moon");

// Creating a custom module instance
CustomModule moonInfo = new CustomModule("MoonInfo", "Display moon information", "MoonInfo", false, false);

// Adding the module to the 'moon' category
moonCategory.AddModule(moonInfo);

// Adding the category to the main menu
mainMenu.AddCategory(moonCategory);

// Finalizing and adding the menu to the terminal
mainMenu.Finished();

// Retrieving a module by its name from the 'moon' category
ModuleBase retrievedModule = moonCategory.GetModuleByName("MoonInfo");

if (retrievedModule != null)
{
    Console.WriteLine("Module Found: " + retrievedModule.DisplayName);
}
else
{
    Console.WriteLine("Module Not Found.");
}

// Retrieving a module by its class from the 'moon' category
CustomModule moonModule = moonCategory.GetModule<CustomModule>();

if (moonModule != null)
{
    Console.WriteLine("Custom Module Found: " + moonModule.DisplayName);
}
else
{
    Console.WriteLine("Custom Module Not Found.");
}
```
