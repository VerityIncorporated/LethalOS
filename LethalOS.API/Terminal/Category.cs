using LethalOS.API;

namespace LethalOS.API.Terminal;

/// <summary>
/// Represents a category in the terminal, grouping related modules.
/// </summary>
public class Category
{
    /// <summary>
    /// Gets or sets the name of the category.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the description of the category.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the keyword used to identify the category in the terminal.
    /// </summary>
    public string Keyword { get; set; }

    /// <summary>
    /// Gets the list of modules belonging to this category.
    /// </summary>
    private List<ModuleBase> Modules { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Category"/> class.
    /// </summary>
    /// <param name="name">The name of the category.</param>
    /// <param name="description">The description of the category.</param>
    /// <param name="keyword">The keyword used to identify the category.</param>
    public Category(string name, string description, string keyword)
    {
        Name = name;
        Description = description;
        Keyword = keyword;
        Modules = new List<ModuleBase>();
    }

    /// <summary>
    /// Adds a module to this category.
    /// </summary>
    /// <param name="module">The module to be added.</param>
    public void AddModule(ModuleBase module)
    {
        Modules.Add(module);
    }

    /// <summary>
    /// Retrieves the list of modules belonging to this category.
    /// </summary>
    /// <returns>The list of modules.</returns>
    public List<ModuleBase> GetModules() => Modules;
}