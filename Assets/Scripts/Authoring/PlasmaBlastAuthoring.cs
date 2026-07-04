using Unity.Collections;
using UnityEngine;
using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;

namespace TMG.Survivors
{
    
    public class PlasmaBlastAuthoring : MonoBehaviour
    {
        public float MoveSpeed;
        public int AttackDamage;

        public float DestroyAfterTime;
        
        private class Baker : Baker<PlasmaBlastAuthoring>
        {
            public override void Bake(PlasmaBlastAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new PlasmaBlastData
                {
                    MoveSpeed = authoring.MoveSpeed,
                    AttackDamage = authoring.AttackDamage
                });
                
                AddComponent(entity, new PlasmaBlastExpirationTimer
                {
                    Value = authoring.DestroyAfterTime
                });
                
                AddComponent<DestroyEntityFlag>(entity);
                SetComponentEnabled<DestroyEntityFlag>(entity, false);
            }
        }
    }


}