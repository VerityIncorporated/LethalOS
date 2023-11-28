using BepInEx;
using LethalOS.API.Terminal;
using Utils.Utils;

namespace Utils;

[BepInPlugin("verity.terminalutils", "Terminal Utils", "1.0.0")]
[BepInDependency("verity.lethalos.api")]
internal class Plugin : BaseUnityPlugin
{
    private void Start()
    {
        var utilsMenu = new Menu("TerminalUtils", "Some terminal utilities.", "terminalutils", "Verity");
        
        //EnemyCategory
        var enemyCategory = new Category("EnemyUtils", "Enemy utils.", "enemyutils");
        enemyCategory.AddModule(new EnemyScan());
        enemyCategory.AddModule(new DespawnEnemies());
        //EnemyCategory
        
        utilsMenu.AddCategory(enemyCategory);
        utilsMenu.Finished();
    }
}