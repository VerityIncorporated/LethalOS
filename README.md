# LethalOS

Lethal Company terminal wrapper which allows you to create and manage your own menus/modules for the in game terminal.

Example Menu:

```cs
        if (!Manager.Initialized()) return;
        
        //Menu Items

        var mainMenu = new Menu("LethalOS", "Made by Verity", "lethalos"); //Create menus
        var exampleCategory = new Category("Example", "Hello, this is an example category.", "example"); //Create Categories
        exampleCategory.AddModule(new ExampleModule()); //Add modules to the category
        exampleCategory.AddModule(new ExampleModuleTwo()); //Add modules to the category
        mainMenu.AddCategory(exampleCategory); //Add Category to the menu
        
        Manager.AddMenu(mainMenu); //Add everything in the menu to the terminal
```

![Menu Image](https://big.cock.rentals/sale/wjoat9oa.png)
![Menu Image](https://big.cock.rentals/sale/ov4417d0.png)
