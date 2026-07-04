using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;

namespace TMG.Survivors
{
    [UpdateInGroup(typeof(PhysicsSystemGroup))]
    [UpdateAfter(typeof(PhysicsSimulationGroup))]
    [UpdateBefore(typeof(AfterPhysicsSystemGroup))]
    public partial struct PlasmaBlastAttackSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<SimulationSingleton>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var attackJob = new PlasmaBlastAttackJob
            {
                PlasmaBlastLookup = SystemAPI.GetComponentLookup<PlasmaBlastData>(true),
                EnemyLookup = SystemAPI.GetComponentLookup<EnemyTag>(true),
                DamageBufferLookup = SystemAPI.GetBufferLookup<DamageThisFrame>(),
                DestroyEntityLookup = SystemAPI.GetComponentLookup<DestroyEntityFlag>()
            };

            var simulationSingleton = SystemAPI.GetSingleton<SimulationSingleton>();
            state.Dependency = attackJob.Schedule(simulationSingleton, state.Dependency);
        }
    }
}