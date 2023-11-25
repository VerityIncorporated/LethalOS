using LethalOS.TerminalSystem;
using LethalOS.TerminalSystem.Bases;
using LethalOS.TerminalSystem.Modules.Example;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace LethalOS.Utils;

public class LethalTerminal : MonoBehaviour
{
    private static readonly List<Menu> Menus = new();
    
    private void Start()
    {
        SceneManager.activeSceneChanged += SceneManagerOnactiveSceneChanged;
        var insertKeyAction = new InputAction(binding: "<Keyboard>/insert");
        insertKeyAction.performed += TerminalActivated;
        insertKeyAction.Enable();
        
        var mainMenu = new Menu("LethalOS", "Made by Verity", "lethalos");
        var exampleCategory = new Category("Example", "Hello, this is an example category.", "example");
        exampleCategory.AddModule(new ExampleModule());
        exampleCategory.AddModule(new ExampleModuleTwo());
        
        mainMenu.AddCategory(exampleCategory);
        Menus.Add(mainMenu);
    }

    private void Update()
    {
        if (!Manager.Initialized()) return;
        foreach (var menu in Menus)
        {
            Manager.AddMenu(menu);
        }
    }

    private void TerminalActivated(InputAction.CallbackContext obj)
    {
        var terminal = FindObjectOfType<Terminal>();
        if (terminal is null) return;
            
        if (!terminal.terminalInUse)
        {
            terminal.BeginUsingTerminal();
            terminal.playerActions.Movement.Disable();
            HUDManager.Instance.ChangeControlTip(0, string.Empty, true);
        }
        else
        {
            terminal.QuitTerminal();
            terminal.playerActions.Movement.Enable();
        }
    }
    
    private void SceneManagerOnactiveSceneChanged(Scene arg0, Scene arg1)
    {
        var terminal = FindObjectOfType<Terminal>();
        if (terminal is null) return;
            
        terminal.terminalUIScreen.renderMode = RenderMode.ScreenSpaceOverlay;
        terminal.terminalUIScreen.scaleFactor += 1.35f;
    }
}