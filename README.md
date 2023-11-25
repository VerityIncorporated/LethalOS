# LethalOS

Lethal Company terminal wrapper which allows you to create and manage your own menus/modules for the in game terminal.

This requires BepInEx: https://builds.bepinex.dev/projects/bepinex_be/674/BepInEx-Unity.Mono-win-x64-6.0.0-be.674%2B82077ec.zip

Example Menu:

```cs
if (!Manager.Initialized()) return;

var mainMenu = new Menu("LethalOS", "Made by Verity", "lethalos"); //Create menus
var exampleCategory = new Category("Example", "Hello, this is an example category.", "example"); //Create Categories
exampleCategory.AddModule(new ExampleModule()); //Add modules to the category
exampleCategory.AddModule(new ExampleModuleTwo()); //Add modules to the category
mainMenu.AddCategory(exampleCategory); //Add Category to the menu
      
Manager.AddMenu(mainMenu); //Add everything in the menu to the terminal
```

![Menu Image](https://r2.e-z.host/73b83a6e-5101-4059-9426-8abb720d5508/wjoat9oa.png)

![Menu Image](https://r2.e-z.host/73b83a6e-5101-4059-9426-8abb720d5508/ov4417d0.png)
