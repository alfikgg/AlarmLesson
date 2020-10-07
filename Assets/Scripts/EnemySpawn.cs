using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] private Transform _enemySpawns;
    [SerializeField] private GameObject _enemyPrefab;

    private List<Transform> _enemySpawned = new List<Transform>();
    private List<Transform> _spawnPositions = new List<Transform>();

    private void Start()
    {

        for (int i = 0; i < _enemySpawns.childCount; i++)
        {
            _spawnPositions.Add(_enemySpawns.GetChild(i));
        }

        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        for (int i = 0; i < _enemySpawns.childCount; i++)
        {
            int randomSpawn = Random.Range(0, _spawnPositions.Count);

            _enemySpawned.Add(_spawnPositions[randomSpawn]);            

            Instantiate(_enemyPrefab, _spawnPositions[randomSpawn].position, Quaternion.identity);

            _spawnPositions.RemoveAt(randomSpawn);

            yield return new WaitForSeconds(2);        
        }
    }

    
}
