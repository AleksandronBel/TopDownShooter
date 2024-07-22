using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] _weaponBonusPrefabs;
    [SerializeField] GameObject[] _powerUpBonusPrefabs;
    [SerializeField] Transform _playerTransform;
    [SerializeField] PlayerShooting _playerShooting;

    WaitForSeconds _weaponSpawnInterval = new WaitForSeconds(10f);
    WaitForSeconds _powerUpSpawnInterval = new WaitForSeconds(27f);

    float _delayToRemoveBonus = 5f;

    void Start()
    {
        StartCoroutine(SpawnWeaponBonusCouroutine());
        StartCoroutine(SpawnPowerUpBonusesCouroutine());
    }

    IEnumerator SpawnWeaponBonusCouroutine()
    {
        while (true)
        {
            yield return _weaponSpawnInterval;
            SpawnWeaponBonus();
        }
    }

    IEnumerator SpawnPowerUpBonusesCouroutine()
    {
        while (true)
        {
            yield return _powerUpSpawnInterval;
            SpawnPowerUpBonus();
        }
    }

    void SpawnWeaponBonus()
    {
        string currentWeapon = _playerShooting.GetCurrentWeapon();

        List<GameObject> availableBonuses = new List<GameObject>();
        foreach (GameObject bonusPrefab in _weaponBonusPrefabs)
        {
            if (bonusPrefab.GetComponent<Bonus>().BonusName != currentWeapon)
                availableBonuses.Add(bonusPrefab);
        }

        if (availableBonuses.Count > 0)
        {
            int weaponIndex = Random.Range(0, availableBonuses.Count);
            GameObject bonusPrefab = availableBonuses[weaponIndex];

            Vector3 spawnPosition = GetRandomPositionInCameraView();
            GameObject bonus = Instantiate(bonusPrefab, spawnPosition, Quaternion.identity);
            StartCoroutine(RemoveBonus(bonus, _delayToRemoveBonus));
        }
    }

    void SpawnPowerUpBonus()
    {
        GameObject bonusPrefab = _powerUpBonusPrefabs[Random.Range(0, _powerUpBonusPrefabs.Length)];
        Vector3 spawnPosition = GetRandomPositionInCameraView();

        GameObject bonus = Instantiate(bonusPrefab, spawnPosition, Quaternion.identity);
        StartCoroutine(RemoveBonus(bonus, _delayToRemoveBonus));
    }

    Vector3 GetRandomPositionInCameraView()
    {
        Camera mainCamera = Camera.main;
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        float x = Random.Range(-cameraWidth / 2, cameraWidth / 2);
        float z = Random.Range(-cameraHeight / 2, cameraHeight / 2);
        float y = 0;

        Vector3 cameraPosition = new Vector3(mainCamera.transform.position.x, 0, mainCamera.transform.position.z);
        return cameraPosition + new Vector3(x, y, z);
    }

    IEnumerator RemoveBonus(GameObject bonus, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (bonus != null)
        {
            bonus.SetActive(false);
            Destroy(bonus, Random.Range(0f, 5f));
        }
    }
}
