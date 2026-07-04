using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

namespace TMG.Survivors
{
public partial struct PlayerAttackSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<PhysicsWorldSingleton>();
            state.RequireForUpdate<BeginInitializationEntityCommandBufferSystem.Singleton>();
        }
        
        public void OnUpdate(ref SystemState state)
        {
            var elapsedTime = SystemAPI.Time.ElapsedTime;

            var ecbSystem = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
            var ecb = ecbSystem.CreateCommandBuffer(state.WorldUnmanaged);
            
            var physicsWorldSingleton = SystemAPI.GetSingleton<PhysicsWorldSingleton>();
                
            foreach (var (expirationTimestamp, attackData, transform) in SystemAPI.Query<RefRW<PlayerCooldownExpirationTimestamp>, PlayerAttackData, LocalTransform>())
            {
                if (expirationTimestamp.ValueRO.Value > elapsedTime) continue;

                var spawnPosition = transform.Position;
                var minDetectPosition = spawnPosition - attackData.DetectionSize;
                var maxDetectPosition = spawnPosition + attackData.DetectionSize;

                var aabbInput = new OverlapAabbInput
                {
                    Aabb = new Aabb
                    {
                        Min = minDetectPosition,
                        Max = maxDetectPosition
                    },
                    Filter = attackData.CollisionFilter
                };

                var overlapHits = new NativeList<int>(state.WorldUpdateAllocator);
                if (!physicsWorldSingleton.OverlapAabb(aabbInput, ref overlapHits))
                {
                    continue;
                }

                var maxDistanceSq = float.MaxValue;
                var closestEnemyPosition = float3.zero;
                foreach (var overlapHit in overlapHits)
                {
                    var curEnemyPosition = physicsWorldSingleton.Bodies[overlapHit].WorldFromBody.pos;
                    var distanceToPlayerSq = math.distancesq(spawnPosition.xy, curEnemyPosition.xy);
                    if (distanceToPlayerSq < maxDistanceSq)
                    {
                        maxDistanceSq = distanceToPlayerSq;
                        closestEnemyPosition = curEnemyPosition;
                    }
                }

                var vectorToClosestEnemy = closestEnemyPosition - spawnPosition;
                var angleToClosestEnemy = math.atan2(vectorToClosestEnemy.y, vectorToClosestEnemy.x);
                var spawnOrientation = quaternion.Euler(0f, 0f, angleToClosestEnemy);
                    
                var newAttack = ecb.Instantiate(attackData.AttackPrefab);
                ecb.SetComponent(newAttack, LocalTransform.FromPositionRotation(spawnPosition, spawnOrientation));

                expirationTimestamp.ValueRW.Value = elapsedTime + attackData.CooldownTime;
            }
        }
    }
}