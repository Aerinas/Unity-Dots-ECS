using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;

namespace TMG.Survivors
{
    public partial struct CharacterMoveSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (velocity, facingDirection, direction, speed, entity) in SystemAPI.Query<RefRW<PhysicsVelocity>, RefRW<FacingDirectionOverride>, CharacterMoveDirection, CharacterMoveSpeed>().WithEntityAccess())
            {
                var moveStep2d = direction.Value * speed.Value;
                velocity.ValueRW.Linear = new float3(moveStep2d, 0f);

                if (math.abs(moveStep2d.x) > 0.15f)
                {
                    facingDirection.ValueRW.Value = math.sign(moveStep2d.x);
                }

                if (SystemAPI.HasComponent<PlayerTag>(entity))
                {
                    var animationOverride = SystemAPI.GetComponentRW<AnimationIndexOverride>(entity);
                    var animationType = math.lengthsq(moveStep2d) > float.Epsilon ? PlayerAnimationIndex.Movement : PlayerAnimationIndex.Idle;
                    animationOverride.ValueRW.Value = (float)animationType;
                }
            }
        }
    }
}