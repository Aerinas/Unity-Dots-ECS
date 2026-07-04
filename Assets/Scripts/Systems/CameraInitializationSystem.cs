using Unity.Entities;

namespace TMG.Survivors
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial struct CameraInitializationSystem : ISystem
    {
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<InitializeCameraTargetTag>();
        }

        public void OnUpdate(ref SystemState state)
        {
            if (CameraTargetSingleton.Instance == null) return;
            var cameraTargetTransform = CameraTargetSingleton.Instance.transform;

            var ecb = new EntityCommandBuffer(state.WorldUpdateAllocator);
            foreach (var (cameraTarget, entity) in SystemAPI.Query<RefRW<CameraTarget>>().WithAll<InitializeCameraTargetTag, PlayerTag>().WithEntityAccess())
            {
                cameraTarget.ValueRW.CameraTransform = cameraTargetTransform;
                ecb.RemoveComponent<InitializeCameraTargetTag>(entity);
            }

            ecb.Playback(state.EntityManager);
        }
    }
}