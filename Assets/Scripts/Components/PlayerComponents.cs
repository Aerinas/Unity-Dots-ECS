using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Rendering;
using UnityEngine;
using UnityEngine.UI;

namespace TMG.Survivors
{
    public struct PlayerTag : IComponentData {}

    public struct CameraTarget : IComponentData
    {
        public UnityObjectRef<Transform> CameraTransform;
    }
    
    public struct InitializeCameraTargetTag : IComponentData {}

    [MaterialProperty("_AnimationIndex")]
    public struct AnimationIndexOverride : IComponentData
    {
        public float Value;
    }

    public enum PlayerAnimationIndex : byte
    {
        Movement = 0,
        Idle = 1,
        
        None = byte.MaxValue
    }

    public struct PlayerAttackData : IComponentData
    {
        public Entity AttackPrefab;
        public float CooldownTime;
        public float3 DetectionSize;
        public CollisionFilter CollisionFilter;
    }

    public struct PlayerCooldownExpirationTimestamp : IComponentData
    {
        public double Value;
    }

    public struct GemsCollectedCount : IComponentData
    {
        public int Value;
    }
    
    public struct UpdateGemUIFlag : IComponentData, IEnableableComponent {}

    public struct PlayerWorldUI : ICleanupComponentData
    {
        public UnityObjectRef<Transform> CanvasTransform;
        public UnityObjectRef<Slider> HealthBarSlider;
    }

    public struct PlayerWorldUIPrefab : IComponentData
    {
        public UnityObjectRef<GameObject> Value;
    }

}