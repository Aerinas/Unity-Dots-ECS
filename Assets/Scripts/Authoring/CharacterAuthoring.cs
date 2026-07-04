using Unity.Entities;
using UnityEngine;

namespace TMG.Survivors
{
    public class CharacterAuthoring : MonoBehaviour
    {
        public float MoveSpeed;
        public int HitPoints;

        private class Baker : Baker<CharacterAuthoring>
        {
            public override void Bake(CharacterAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent<InitializeCharacterFlag>(entity);
                AddComponent<CharacterMoveDirection>(entity);

                AddComponent(entity, new CharacterMoveSpeed
                {
                    Value = authoring.MoveSpeed
                });

                AddComponent(entity, new FacingDirectionOverride
                {
                    Value = 1
                });

                AddComponent(entity, new CharacterMaxHitPoints
                {
                    Value = authoring.HitPoints
                });

                AddComponent(entity, new CharacterCurrentHitPoints
                {
                    Value = authoring.HitPoints
                });

                AddBuffer<DamageThisFrame>(entity);

                AddComponent<DestroyEntityFlag>(entity);
                SetComponentEnabled<DestroyEntityFlag>(entity, false);
            }
        }
    }
}