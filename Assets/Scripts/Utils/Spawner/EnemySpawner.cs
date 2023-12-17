using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[Description("This component requires a Collider2D object to set spawn area")]
[RequireComponent(typeof(Collider2D))]
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject _enemyPrefab;
    Collider2D _spawnArea;
    public int numberOfObjects = 5;

    private void Awake()
    {
        _spawnArea = GetComponent<Collider2D>();
        for (int i = 0; i < numberOfObjects; i++)
        {
            Vector2 randomSpawnPoint = GenerateSpawnPosition();
            var createdObject = Instantiate(_enemyPrefab, randomSpawnPoint, Quaternion.identity);
            createdObject.transform.parent = transform;
        }
    }

    Vector2 GenerateSpawnPosition()
    {
        float randomX = Random.Range(_spawnArea.bounds.min.x, _spawnArea.bounds.max.x);
        float randomY = Random.Range(_spawnArea.bounds.min.y, _spawnArea.bounds.max.y);

        return new Vector2(randomX, randomY);
    }
}
