using Unity.Entities;
using Unity.Transforms;

namespace TMG.Survivors
{
    public partial struct MovePlasmaBlastSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;
            foreach (var (transform, data) in SystemAPI.Query<RefRW<LocalTransform>, PlasmaBlastData>())
            {
                transform.ValueRW.Position += transform.ValueRO.Right() * data.MoveSpeed * deltaTime;
            }
            
            // Destroy Plasma Blast After Time
            foreach (var (timer, entity) in SystemAPI.Query<RefRW<PlasmaBlastExpirationTimer>>().WithPresent<DestroyEntityFlag>().WithEntityAccess())
            {
                timer.ValueRW.Value -= deltaTime;
                if (timer.ValueRO.Value > 0) continue;
                SystemAPI.SetComponentEnabled<DestroyEntityFlag>(entity, true);
            }
        }
    }
}