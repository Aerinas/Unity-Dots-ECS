using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;

namespace TMG.Survivors
{
    [BurstCompile]
    public struct CollectGemJob : ITriggerEventsJob
    {
        [ReadOnly] public ComponentLookup<GemTag> GemLookup;
        public ComponentLookup<GemsCollectedCount> GemsCollectedLookup;
        public ComponentLookup<DestroyEntityFlag> DestroyEntityLookup;
        public ComponentLookup<UpdateGemUIFlag> UpdateGemUILookup;
        
        public void Execute(TriggerEvent triggerEvent)
        {
            Entity gemEntity;
            Entity playerEntity;

            if (GemLookup.HasComponent(triggerEvent.EntityA) && GemsCollectedLookup.HasComponent(triggerEvent.EntityB))
            {
                gemEntity = triggerEvent.EntityA;
                playerEntity = triggerEvent.EntityB;
            }
            else if (GemLookup.HasComponent(triggerEvent.EntityB) && GemsCollectedLookup.HasComponent(triggerEvent.EntityA))
            {
                gemEntity = triggerEvent.EntityB;
                playerEntity = triggerEvent.EntityA;
            }
            else
            {
                return;
            }

            var gemsCollected = GemsCollectedLookup[playerEntity];
            gemsCollected.Value += 1;
            GemsCollectedLookup[playerEntity] = gemsCollected;

            UpdateGemUILookup.SetComponentEnabled(playerEntity, true);

            DestroyEntityLookup.SetComponentEnabled(gemEntity, true);
        }
    }
}