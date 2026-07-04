using Unity.Burst;
using Unity.Entities;

namespace TMG.Survivors
{
    public partial struct ProcessDamageThisFrameSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (hitPoints, damageThisFrame, entity) in SystemAPI.Query<RefRW<CharacterCurrentHitPoints>, DynamicBuffer<DamageThisFrame>>().WithPresent<DestroyEntityFlag>().WithEntityAccess())
            {
                if (damageThisFrame.IsEmpty) continue;
                foreach (var damage in damageThisFrame)
                {
                    hitPoints.ValueRW.Value -= damage.Value;
                }

                damageThisFrame.Clear();

                if (hitPoints.ValueRO.Value <= 0)
                {
                    SystemAPI.SetComponentEnabled<DestroyEntityFlag>(entity, true);
                }
            }
        }
    }
}