namespace LethalOS.TerminalSystem.Bases;

public abstract class Module
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Keyword { get; set; }
    public bool Enabled { get; set; }

    protected Module(string name, string description, string keyword)
    {
        Name = name;
        Description = description;
        Keyword = keyword;
        Enabled = false;
    }

    public void Toggle()
    {
        Enabled = !Enabled;
        if (Enabled)
        {
            HUDManager.Instance.DisplayTip($"{Name} Enabled!", $"{Description}");
            OnEnable();
        }
        else
        {
            HUDManager.Instance.DisplayTip($"{Name} Disabled!", $"{Description}", true);
            OnDisable();
        }
    }
    
    protected virtual void OnEnable(){}
    protected virtual void OnDisable(){}
    
    public virtual void OnUpdate(){}
    public virtual void OnFixedUpdate(){}
    public virtual void OnGui(){}
}