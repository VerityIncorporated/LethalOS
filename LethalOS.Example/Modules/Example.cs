using LethalOS.API;

namespace LethalOS.Example.Modules;

public class Example : ModuleBase
{
    public Example() : base("ExampleModule", "This is an example module!", "examplemodule") {}

    protected override void OnEnabled()
    {
        Plugin.LogSource.LogInfo("Enabled");
    }

    protected override void OnDisabled()
    {
        Plugin.LogSource.LogInfo("Disabled");
    }

    public override void OnUpdate()
    {
        //Do action on update while enabled
    }

    public override void OnFixedUpdate()
    {
        //Do action on fixed update while enabled
    }
    
    public override void OnGui()
    {
        //Do action on gui while enabled
    }
}