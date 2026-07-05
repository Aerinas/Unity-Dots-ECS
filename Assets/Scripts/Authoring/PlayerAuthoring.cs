
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;

namespace TMG.Survivors
{

    public class PlayerAuthoring : MonoBehaviour
    {
        public GameObject AttackPrefab;
        public float CooldownTime;
        public float DetectionSize;
        public GameObject WorldUIPrefab;
        
        private class Baker : Baker<PlayerAuthoring>
        {
            public override void Bake(PlayerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<PlayerTag>(entity);
                AddComponent<InitializeCameraTargetTag>(entity);
                AddComponent<CameraTarget>(entity);
                AddComponent<AnimationIndexOverride>(entity);

                var enemyLayer = LayerMask.NameToLayer("Enemy");
                var enemyLayerMask = (uint)math.pow(2, enemyLayer);

                var attackCollisionFilter = new CollisionFilter
                {
                    BelongsTo = uint.MaxValue,
                    CollidesWith = enemyLayerMask
                };
                
                AddComponent(entity, new PlayerAttackData
                {
                    AttackPrefab = GetEntity(authoring.AttackPrefab, TransformUsageFlags.Dynamic),
                    CooldownTime = authoring.CooldownTime,
                    DetectionSize = new float3(authoring.DetectionSize),
                    CollisionFilter = attackCollisionFilter
                });
                AddComponent<PlayerCooldownExpirationTimestamp>(entity);
                AddComponent<GemsCollectedCount>(entity);
                AddComponent<UpdateGemUIFlag>(entity);
                AddComponent(entity, new PlayerWorldUIPrefab
                {
                    Value = authoring.WorldUIPrefab
                });
            }
        }
    }

}
