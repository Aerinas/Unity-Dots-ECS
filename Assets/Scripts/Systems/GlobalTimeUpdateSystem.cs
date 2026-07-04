using Unity.Entities;
using UnityEngine;

namespace TMG.Survivors
{
    public partial struct GlobalTimeUpdateSystem : ISystem
    {
        private static int _globalTimeShaderPropertyID;

        public void OnCreate(ref SystemState state)
        {
            _globalTimeShaderPropertyID = Shader.PropertyToID("_GlobalTime");
        }

        public void OnUpdate(ref SystemState state)
        {
            Shader.SetGlobalFloat(_globalTimeShaderPropertyID, (float)SystemAPI.Time.ElapsedTime);
        }
    }
}