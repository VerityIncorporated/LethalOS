namespace LethalOS.API.Terminal;

public class Category
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Keyword { get; set; }
    private List<ModuleBase> Modules { get; set; }

    public Category(string name, string description, string keyword)
    {
        Name = name;
        Description = description;
        Keyword = keyword;
        Modules = new List<ModuleBase>();
    }
    
    public void AddModule(ModuleBase module)
    {
        Modules.Add(module);
    }

    public List<ModuleBase> GetModules() => Modules;
}