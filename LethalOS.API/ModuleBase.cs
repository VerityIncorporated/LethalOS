namespace LethalOS.API;

/// <summary>
/// Base class representing a module in the LethalOS system.
/// </summary>
public class ModuleBase
{
    /// <summary>
    /// Gets the display name of the module that shows up in the terminal.
    /// </summary>
    public string DisplayName { get; private set; }

    /// <summary>
    /// Gets the display description of the module that shows up in the terminal.
    /// </summary>
    public string DisplayDescription { get; private set; }

    /// <summary>
    /// Gets the keyword typed into the terminal to activate the module.
    /// </summary>
    public string Keyword { get; private set; }

    /// <summary>
    /// Gets or sets a value indicating whether the module is currently enabled.
    /// </summary>
    public bool Enabled { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ModuleBase"/> class.
    /// </summary>
    /// <param name="displayName">The display name of the module.</param>
    /// <param name="displayDescription">The display description of the module.</param>
    /// <param name="keyword">The keyword to activate the module.</param>
    protected ModuleBase(string displayName, string displayDescription, string keyword)
    {
        DisplayName = displayName;
        DisplayDescription = displayDescription;
        Keyword = keyword;
    }

    /// <summary>
    /// Toggles the module on/off depending on its current state.
    /// </summary>
    public void ToggleModule()
    {
        Enabled = !Enabled;

        if (Enabled)
        {
            HUDManager.Instance.DisplayTip($"{DisplayName} Enabled!", $"{DisplayDescription}");
            OnEnabled();
        }
        else
        {
            HUDManager.Instance.DisplayTip($"{DisplayName} Disabled!", $"{DisplayDescription}", true);
            OnDisabled();
        }
    }

    /// <summary>
    /// Called when the module is enabled.
    /// Override this method to perform actions when the module is enabled.
    /// </summary>
    protected virtual void OnEnabled(){}

    /// <summary>
    /// Called when the module is disabled.
    /// Override this method to perform actions when the module is disabled.
    /// </summary>
    protected virtual void OnDisabled(){}

    /// <summary>
    /// Called every frame if the module is enabled (equivalent to Unity's Update method).
    /// Override this method to implement per-frame update logic.
    /// </summary>
    public virtual void OnUpdate(){}

    /// <summary>
    /// Called every fixed frame-rate frame if the module is enabled (equivalent to Unity's FixedUpdate method).
    /// Override this method to implement fixed frame-rate update logic.
    /// </summary>
    public virtual void OnFixedUpdate(){}

    /// <summary>
    /// Called when the module is enabled and GUI rendering is taking place (equivalent to Unity's OnGUI method).
    /// Override this method to implement GUI rendering logic.
    /// </summary>
    public virtual void OnGui(){}
}