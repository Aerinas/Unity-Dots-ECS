using Unity.Entities;
using Unity.Transforms;

namespace TMG.Survivors
{
    [UpdateAfter(typeof(TransformSystemGroup))]
    public partial struct MoveCameraSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (transform, cameraTarget) in SystemAPI.Query<LocalToWorld, CameraTarget>().WithAll<PlayerTag>().WithNone<InitializeCameraTargetTag>())
            {
                cameraTarget.CameraTransform.Value.position = transform.Position;
            }
        }
    }
}