using Unity.Entities;

namespace TMG.Survivors
{
    public partial struct UpdateGemUISystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (var (gemCount, shouldUpdateUI) in SystemAPI.Query<GemsCollectedCount, EnabledRefRW<UpdateGemUIFlag>>())
            {
                GameUIController.Instance.UpdateGemsCollectedText(gemCount.Value);
                shouldUpdateUI.ValueRW = false;
            }
        }
    }
}