namespace LethalOS.TerminalSystem.Bases;

public class Menu
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Keyword { get; set; }
    public List<Category> Categories { get; set; }

    public Menu(string name, string description, string keyword)
    {
        Title = name;
        Description = description;
        Keyword = keyword;
        Categories = new List<Category>();
    }

    public void AddCategory(Category category)
    {
        Categories.Add(category);
    }
}