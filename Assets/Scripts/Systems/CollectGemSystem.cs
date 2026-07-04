using Unity.Burst;
using Unity.Entities;
using Unity.Physics;

namespace TMG.Survivors
{
    public partial struct CollectGemSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<SimulationSingleton>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var newCollectJob = new CollectGemJob
            {
                GemLookup = SystemAPI.GetComponentLookup<GemTag>(true),
                GemsCollectedLookup = SystemAPI.GetComponentLookup<GemsCollectedCount>(),
                DestroyEntityLookup = SystemAPI.GetComponentLookup<DestroyEntityFlag>(),
                UpdateGemUILookup = SystemAPI.GetComponentLookup<UpdateGemUIFlag>()
            };

            var simulationSingleton = SystemAPI.GetSingleton<SimulationSingleton>();
            state.Dependency = newCollectJob.Schedule(simulationSingleton, state.Dependency);
        }
    }
}