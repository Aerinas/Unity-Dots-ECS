
using Unity.Entities;
using Unity.Transforms;

namespace TMG.Survivors
{
    public partial struct EnemyMoveToPlayerSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PlayerTag>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
            var playerPosition = SystemAPI.GetComponent<LocalTransform>(playerEntity).Position.xy;

            var moveToPlayerJob = new EnemyMoveToPlayerJob
            {
                PlayerPosition = playerPosition
            };

            state.Dependency = moveToPlayerJob.ScheduleParallel(state.Dependency);
        }
    }
}