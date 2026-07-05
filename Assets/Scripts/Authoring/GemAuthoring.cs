
using UnityEngine;
using Unity.Entities;



namespace TMG.Survivors
{
    public class GemAuthoring : MonoBehaviour
    {
        private class Baker : Baker<GemAuthoring>
        {
            public override void Bake(GemAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<GemTag>(entity);
                AddComponent<DestroyEntityFlag>(entity);
                SetComponentEnabled<DestroyEntityFlag>(entity, false);
            }
        }
   }
}
