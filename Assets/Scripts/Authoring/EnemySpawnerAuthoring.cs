
using UnityEngine;
using Unity.Entities;

using Random = Unity.Mathematics.Random;

namespace TMG.Survivors
{

    
    public class EnemySpawnerAuthoring : MonoBehaviour
    {
        public GameObject EnemyPrefab;
        public GameObject ReaperPrefab;
        public float ReaperSpawnTime;
        public float SpawnInterval;
        public float SpawnDistance;
        public uint RandomSeed;
        
        private class Baker : Baker<EnemySpawnerAuthoring>
        {
            public override void Bake(EnemySpawnerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new EnemySpawnData
                {
                    EnemyPrefab = GetEntity(authoring.EnemyPrefab, TransformUsageFlags.Dynamic),
                    ReaperPrefab = GetEntity(authoring.ReaperPrefab, TransformUsageFlags.Dynamic),
                    SpawnInterval = authoring.SpawnInterval,
                    SpawnDistance = authoring.SpawnDistance
                });
                AddComponent(entity, new EnemySpawnState
                {
                    SpawnTimer = 0f,
                    ReaperSpawnTimer = authoring.ReaperSpawnTime,
                    Random = Random.CreateFromIndex(authoring.RandomSeed)
                });
            }
        }
    }

    
}
