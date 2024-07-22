using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject _commonEnemyPrefab;
    [SerializeField] GameObject _fastEnemyPrefab;
    [SerializeField] GameObject _armoredEnemyPrefab;
    [SerializeField] Transform _playerTransform;

    float _spawnInterval = 2f;
    float _minSpawnInterval = 0.5f;
    float _intervalDecreaseRate = 0.1f;
    float _timeSinceStart = 0f;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(_spawnInterval);

            _timeSinceStart += _spawnInterval;
            if (_timeSinceStart >= 10f)
            {
                _timeSinceStart = 0f;
                _spawnInterval = Mathf.Max(_spawnInterval - _intervalDecreaseRate, _minSpawnInterval);
            }
        }
    }

    void SpawnEnemy()
    {
        float spawnChance = Random.Range(0, 101);
        GameObject enemyPrefab;

        if (spawnChance < 61)
            enemyPrefab = _commonEnemyPrefab;
        else if (spawnChance < 91)
            enemyPrefab = _fastEnemyPrefab;
        else
            enemyPrefab = _armoredEnemyPrefab;

        Vector3 spawnPosition = GetSpawnPosition();
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    Vector3 GetSpawnPosition()
    {
        Camera mainCamera = Camera.main;
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        float x, y, z;

        switch (Random.Range(0, 4))
        {
            case 0: 
                x = Random.Range(-cameraWidth / 2, cameraWidth / 2);
                z = cameraHeight / 2 + 4;
                break;
            case 1: 
                x = Random.Range(-cameraWidth / 2, cameraWidth / 2);
                z = -cameraHeight / 2 - 4;
                break;
            case 2: 
                x = -cameraWidth / 2 - 4;
                z = Random.Range(-cameraHeight / 2, cameraHeight / 2);
                break;
            case 3:
                x = cameraWidth / 2 + 4;
                z = Random.Range(-cameraHeight / 2, cameraHeight / 2);
                break;
            default:
                x = 0;
                z = 0;
                break;
        }

        y = 0.5f;

        return new Vector3(x, y, z);
    }
}
