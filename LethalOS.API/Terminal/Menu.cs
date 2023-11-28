namespace LethalOS.API.Terminal;

/// <summary>
/// Manages modules within the terminal.
/// </summary>
public class Menu
{
    /// <summary>
    /// Gets or sets the name of the menu.
    /// </summary>
    public string MenuName { get; set; }

    /// <summary>
    /// Gets or sets the description of the menu.
    /// </summary>
    public string MenuDescription { get; set; }

    /// <summary>
    /// Gets or sets the terminal keyword of the menu.
    /// </summary>
    public string MenuKeyword { get; set; }
    
    /// <summary>
    /// Gets or sets the author of this mod.
    /// </summary>
    public string ModAuthor { get; set; }

    private List<Category> MenuCategories { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Menu"/> class.
    /// </summary>
    /// <param name="menuName">The name of the menu.</param>
    /// <param name="menuDescription">The description of the menu.</param>
    /// <param name="menuKeyword">The terminal keyword for this menu.</param>
    /// <param name="modAuthor">The person who created the mod.</param>
    public Menu(string menuName, string menuDescription, string menuKeyword, string modAuthor)
    {
        MenuName = menuName;
        MenuDescription = menuDescription;
        MenuKeyword = menuKeyword;
        ModAuthor = modAuthor;
        MenuCategories = new List<Category>();
    }

    /// <summary>
    /// Adds a category to the menu.
    /// </summary>
    /// <param name="category">The category to be added.</param>
    public void AddCategory(Category category)
    {
        MenuCategories.Add(category);
    }

    public void Finished()
    {
        Manager.AddMenu(this);
    }

    /// <summary>
    /// Retrieves the list of categories in the menu.
    /// </summary>
    /// <returns>The list of categories in the menu.</returns>
    public List<Category> GetCategories => MenuCategories;
}