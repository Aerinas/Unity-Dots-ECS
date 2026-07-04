using Unity.Collections;
using Unity.Entities;
using Unity.Physics;

namespace TMG.Survivors
{
    public struct PlasmaBlastAttackJob : ITriggerEventsJob
    {
        [ReadOnly] public ComponentLookup<PlasmaBlastData> PlasmaBlastLookup;
        [ReadOnly] public ComponentLookup<EnemyTag> EnemyLookup;
        public BufferLookup<DamageThisFrame> DamageBufferLookup;
        public ComponentLookup<DestroyEntityFlag> DestroyEntityLookup;
        
        public void Execute(TriggerEvent triggerEvent)
        {
            Entity plasmaBlastEntity;
            Entity enemyEntity;

            if (PlasmaBlastLookup.HasComponent(triggerEvent.EntityA) && EnemyLookup.HasComponent(triggerEvent.EntityB))
            {
                plasmaBlastEntity = triggerEvent.EntityA;
                enemyEntity = triggerEvent.EntityB;
            }
            else if (PlasmaBlastLookup.HasComponent(triggerEvent.EntityB) && EnemyLookup.HasComponent(triggerEvent.EntityA))
            {
                plasmaBlastEntity = triggerEvent.EntityB;
                enemyEntity = triggerEvent.EntityA;
            }
            else
            {
                return;
            }

            var attackDamage = PlasmaBlastLookup[plasmaBlastEntity].AttackDamage;
            var enemyDamageBuffer = DamageBufferLookup[enemyEntity];
            enemyDamageBuffer.Add(new DamageThisFrame { Value = attackDamage });

            DestroyEntityLookup.SetComponentEnabled(plasmaBlastEntity, true);
        }
    }
}