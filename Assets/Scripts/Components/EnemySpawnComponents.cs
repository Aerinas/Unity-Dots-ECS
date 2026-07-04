using Unity.Entities;
using Random = Unity.Mathematics.Random;
namespace TMG.Survivors
{
    public struct EnemySpawnData : IComponentData
    {
        public Entity EnemyPrefab;
        public Entity ReaperPrefab;
        public float SpawnInterval;
        public float SpawnDistance;
    }

    public struct EnemySpawnState : IComponentData
    {
        public float SpawnTimer;
        public float ReaperSpawnTimer;
        public Random Random;
    }
}