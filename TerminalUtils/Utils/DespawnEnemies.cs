using LethalOS.API;
using Object = UnityEngine.Object;

namespace Utils.Utils;

public class DespawnEnemies : ModuleBase
{
    public DespawnEnemies() : base("DespawnEnemies", "Despawns all enemies!", "despawnenemies", true) {}

    protected override void OnEnabled()
    {
        foreach (var enemy in Object.FindObjectsOfType<EnemyAI>())
        {
            RoundManager.Instance.DespawnEnemyServerRpc(enemy.thisNetworkObject);
        }
    }
}