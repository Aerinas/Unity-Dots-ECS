using Unity.Burst;
using Unity.Collections;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;

namespace TMG.Survivors
{
   
    
    [RequireComponent(typeof(CharacterAuthoring))]
    public class EnemyAuthoring : MonoBehaviour
    {
        public int AttackDamage;
        public float CooldownTime;
        public GameObject GemPrefab;
        
        private class Baker : Baker<EnemyAuthoring>
        {
            public override void Bake(EnemyAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<EnemyTag>(entity);
                AddComponent(entity, new EnemyAttackData
                {
                    HitPoints = authoring.AttackDamage,
                    CooldownTime = authoring.CooldownTime
                });
                AddComponent<EnemyCooldownExpirationTimestamp>(entity);
                SetComponentEnabled<EnemyCooldownExpirationTimestamp>(entity, false);
                AddComponent(entity, new GemPrefab
                {
                    Value = GetEntity(authoring.GemPrefab, TransformUsageFlags.Dynamic)
                });
            }
        }
    }







 
}