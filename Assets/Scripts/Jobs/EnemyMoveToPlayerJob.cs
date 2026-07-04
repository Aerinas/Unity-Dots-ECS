using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace TMG.Survivors
{
    [BurstCompile]
    [WithAll(typeof(EnemyTag))]
    public partial struct EnemyMoveToPlayerJob : IJobEntity
    {
        public float2 PlayerPosition;
        
        private void Execute(ref CharacterMoveDirection direction, in LocalTransform transform)
        {
            var vectorToPlayer = PlayerPosition - transform.Position.xy;
            direction.Value = math.normalize(vectorToPlayer);
        }
    }
}