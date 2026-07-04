using Unity.Entities;

namespace TMG.Survivors
{
    public struct PlasmaBlastData : IComponentData
    {
        public float MoveSpeed;
        public int AttackDamage;
    }

    public struct PlasmaBlastExpirationTimer : IComponentData
    {
        public float Value;
    }
}