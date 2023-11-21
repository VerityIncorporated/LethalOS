namespace LethalOS.TerminalSystem.Bases;

public class Category
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Keyword { get; set; }
    public List<Module> Modules { get; set; }

    public Category(string title, string description, string keyword)
    {
        Title = title;
        Description = description;
        Keyword = keyword;
        Modules = new List<Module>();
    }
    
    public void AddModule(Module module)
    {
        Manager.Modules.Add(module);
        Modules.Add(module);
    }
}