using System.Collections;
using UnityEngine;

namespace Managers
{
    public class SpawnManager : Manager
    {
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private GameObject _enemyContainer;
        
        [SerializeField] private GameObject[] _powerups;

        private bool _stopSpawn = false;


        public void StartSpawning()
        {
            StartCoroutine(SpawnEnemyRoutine());
            StartCoroutine(SpawnPowerupRoutine());
        }

        IEnumerator SpawnEnemyRoutine()
        {
            yield return new WaitForSeconds(3f);
            while (_stopSpawn == false)
            {
                var spawnPosition = new Vector3(Random.Range(-9f, 9f), 7f, 0f);
                var newEnemy = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
            
                newEnemy.transform.parent = _enemyContainer.transform;
            
                yield return new WaitForSeconds(1.0f);
            }
        }

        IEnumerator SpawnPowerupRoutine()
        {
            yield return new WaitForSeconds(3f);
            while (_stopSpawn == false)
            {
                var spawnPosition = new Vector3(Random.Range(-9f, 9f), 7f, 0f);
                var powerupID = Random.Range(0, 3);
                Instantiate(_powerups[powerupID], spawnPosition, Quaternion.identity);
            
                yield return new WaitForSeconds(Random.Range(8f, 16f));
            }
        }

        public void OnPlayerDeath()
        {
            _stopSpawn = true;
        }
    }
}