using UnityEngine;

namespace SpaceShooter
{
    public class EntitySpawner : MonoBehaviour
    {
        public enum SpawnMode
        {
            Start,
            Loop
        }

        [SerializeField] protected Entity[] entityPrefabs;

        [SerializeField] protected CircleArea area;

        [SerializeField] private SpawnMode spawnMode;

        [SerializeField] protected int spawnsCount;

        [SerializeField] private float respawnTime;

        private float timer;

        private void Start()
        {
            if (spawnMode == SpawnMode.Start)
            {
                SpawnEntities();
            }

            timer = respawnTime;
        }

        private void Update()
        {
            if (timer > 0)
                timer -= Time.deltaTime;

            if (spawnMode == SpawnMode.Loop && timer < 0)
            {
                SpawnEntities();

                timer = respawnTime;
            }
        }

        protected virtual void SpawnEntities()
        {
            for (int i = 0; i < spawnsCount; i++)
            {
                int index = Random.Range(0, entityPrefabs.Length);

                GameObject entity = Instantiate(entityPrefabs[index].gameObject);

                entity.transform.position = area.GetRandomInsideZone();
            }
        }
    }
}