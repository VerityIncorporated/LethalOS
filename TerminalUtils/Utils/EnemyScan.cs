using LethalOS.API;
using LethalOS.API.Terminal;
using Object = UnityEngine.Object;

namespace Utils.Utils;

public class EnemyScan : ModuleBase
{
    public EnemyScan() : base("EnemyScan", "Scan for alive enemies.", "enemyscan", true){}

    protected override void OnEnabled()
    {
        var enemies = Object.FindObjectsOfType<EnemyAI>();
        var enemyCount = enemies.Length;
        var outside = enemies.Count(ai => ai.enemyType.isOutsideEnemy);
        
        ChangeScreenText($"There are currently {enemyCount} enemies alive! {outside} of which are outside!", true);
    }
}