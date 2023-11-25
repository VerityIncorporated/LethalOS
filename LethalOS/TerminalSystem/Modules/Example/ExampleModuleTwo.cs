using LethalOS.TerminalSystem.Bases;
using LethalOS.Utils;

namespace LethalOS.TerminalSystem.Modules.Example;

public class ExampleModuleTwo : Module
{
    public ExampleModuleTwo() : base("Example Module", "Hello, this is an example module.", "example module") {}

    protected override void OnEnable()
    {
        Log.LogSource.LogInfo("I was Enabled!");
    }

    protected override void OnDisable()
    {
        Log.LogSource.LogInfo("I was disabled!");
    }

    public override void OnUpdate()
    {
        Log.LogSource.LogInfo("I am OnUpdate!");
    }

    public override void OnFixedUpdate()
    {
        Log.LogSource.LogInfo("I am OnFixedUpdate!");
    }
}