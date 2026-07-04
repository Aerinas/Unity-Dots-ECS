using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;

namespace TMG.Survivors
{
    public struct InitializeCharacterFlag : IComponentData, IEnableableComponent {}
    
    public struct CharacterMoveDirection : IComponentData
    {
        public float2 Value;
    }

    public struct CharacterMoveSpeed : IComponentData
    {
        public float Value;
    }

    [MaterialProperty("_FacingDirection")]
    public struct FacingDirectionOverride : IComponentData
    {
        public float Value;
    }

    public struct CharacterMaxHitPoints : IComponentData
    {
        public int Value;
    }

    public struct CharacterCurrentHitPoints : IComponentData
    {
        public int Value;
    }

    public struct DamageThisFrame : IBufferElementData
    {
        public int Value;
    }
}