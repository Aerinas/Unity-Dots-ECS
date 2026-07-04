using Unity.Entities;

namespace TMG.Survivors
{
    public struct EnemyTag : IComponentData
    {
    }

    public struct EnemyAttackData : IComponentData
    {
        public int HitPoints;
        public float CooldownTime;
    }

    public struct EnemyCooldownExpirationTimestamp : IComponentData, IEnableableComponent
    {
        public double Value;
    }

    public struct GemPrefab : IComponentData
    {
        public Entity Value;
    }
}